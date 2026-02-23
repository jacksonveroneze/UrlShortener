import http from "k6/http";
import { check, sleep } from "k6";
import { randomItem, randomString } from "https://jslib.k6.io/k6-utils/1.4.0/index.js";

import { factoryHeaders, getToken } from "./scenarios/util.js";

/**
 * Fluxo:
 * 1) setup(): cria IDS_COUNT URLs via api-write e devolve lista de ids
 * 2) warmup_read: constant-arrival-rate
 * 3) step_load_read: ramping-arrival-rate em degraus de 60s
 *
 * SLO (tua regra):
 * - erro <= 1%
 * - checks >= 99%
 * - p95 <= 300ms
 * - sem dropped_iterations (opcional via REQUIRE_ZERO_DROPS)
 *
 * Observação:
 * - CPU <= 80% fica como “guardrail” para sizing (avaliado via Grafana/Prometheus)
 */

// Base
const BASE_URL = __ENV.BASE_URL || "http://127.0.0.1:8080";

// Endpoints (write + read)
const WRITE_PATH = __ENV.WRITE_PATH || "/url-shortener-write/v1/urls";
const READ_PATH = __ENV.READ_PATH || "/v1/urls"; // vamos passar ?id=...

// Carga
const WARMUP_RPS = Number(__ENV.WARMUP_RPS || 100);
const START_RPS = Number(__ENV.START_RPS || 200);
const STEP_RPS = Number(__ENV.STEP_RPS || 100);
const STEPS = Number(__ENV.STEPS || 4);

// k6 VUs
const PRE_VUS = Number(__ENV.PRE_VUS || 300);
const MAX_VUS = Number(__ENV.MAX_VUS || 3000);

// Dados
const IDS_COUNT = Number(__ENV.IDS_COUNT || 1000);
const COOLDOWN_SECONDS = Number(__ENV.COOLDOWN_SECONDS || 30);

// Regras
const REQUIRE_ZERO_DROPS = (__ENV.REQUIRE_ZERO_DROPS ?? "true").toLowerCase().trim() === "true";

// Criação controlada dos IDs no setup
const WRITE_CREATE_RPS = Number(__ENV.WRITE_CREATE_RPS || 30); // req/s (ritmo “seguro”)
const WRITE_TIMEOUT = __ENV.WRITE_TIMEOUT || "5s";
const READ_TIMEOUT = __ENV.READ_TIMEOUT || "1s";

const filecontent = open("./data.json");

function buildStages() {
    const stages = [];
    for (let i = 0; i < STEPS; i++) {
        stages.push({ target: START_RPS + i * STEP_RPS, duration: "1m" });
    }
    return stages;
}

export const options = {
    summaryTrendStats: ["avg", "p(90)", "p(95)", "p(99)", "max", "count"],

    scenarios: {
        warmup_read_step_load_app: {
            executor: "constant-arrival-rate",
            rate: 1,
            timeUnit: "1s",
            duration: "20s",
            preAllocatedVUs: 20,
            maxVUs: 20,
            tags: { phase: "warmup_app", endpoint: "read_url_random_id" },
            gracefulStop: "0s",
        },
        
        warmup_read_step: {
            executor: "constant-arrival-rate",
            rate: WARMUP_RPS,
            timeUnit: "1s",
            duration: "10s",
            preAllocatedVUs: PRE_VUS,
            maxVUs: MAX_VUS,
            startTime: "20s",
            tags: { phase: "warmup", endpoint: "read_url_random_id" },
            gracefulStop: "0s",
        },

        step_load_read: {
            executor: "ramping-arrival-rate",
            startRate: START_RPS,
            timeUnit: "1s",
            preAllocatedVUs: PRE_VUS,
            maxVUs: MAX_VUS,
            startTime: "30s",
            stages: buildStages(),
            tags: { phase: "step", endpoint: "read_url_random_id" },
            gracefulStop: "0s",
        }
    },

    setupTimeout: '4m',

    thresholds: (() => {
        const t = {
            // Transporte/timeouts/etc.
            http_req_failed: ["rate<=0.01"],

            // SLO de latência
            http_req_duration: ["p(95)<=300"],

            // “status 200” precisa ser >= 99% (teu erro <= 1%)
            checks: ["rate>=0.99"],
        };

        if (REQUIRE_ZERO_DROPS) {
            t["dropped_iterations"] = ["count==0"];
        }

        return t;
    })(),
};

function createOneUrl(headers) {
    const alias = `k6-${randomString(10)}`;
    const payload = JSON.stringify({
        originalUrl: "https://github.com/",
        customAlias: alias,
        expirationDate: "2026-05-12T12:20:00+00:00",
    });

    const res = http.post(`${BASE_URL}${WRITE_PATH}`, payload, {
        timeout: WRITE_TIMEOUT,
        headers: {
            ...headers,
            "Content-Type": "application/json",
        },
        tags: { name: "POST /url-shortener-write/v1/urls" },
    });

    const ok = check(res, {
        "write status is 200/201": (r) => r.status === 200 || r.status === 201,
    });

    if (!ok) return null;

    const body = res.json();

    return body?.data.code;
}

export function setup() {
    const token = getToken();
    const headers = factoryHeaders(token);

    const ids = JSON.parse(filecontent);
    
    return { headers, ids };
}

export default function (data) {
    // console.log(data.ids);
    
    const id = randomItem(data.ids);
    const url = `${BASE_URL}${READ_PATH}?id=${encodeURIComponent(id)}`;

    const res = http.get(url, {
        timeout: READ_TIMEOUT,
        headers: data.headers,
        tags: { name: "GET /url-shortener-read/v1/urls" },
    });

    const ok = check(res, {
        "status is 200": (r) => r.status === 200,
    });

    // micropausa só se falhar, evita “loop quente”
    if (!ok) sleep(0.01);
}