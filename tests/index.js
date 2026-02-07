import http from "k6/http";
import { check, sleep } from "k6";
import { textSummary } from "https://jslib.k6.io/k6-summary/0.0.4/index.js";

import { factoryHeaders } from "./scenarios/util.js";

/**
 * Teste com duração máxima ~5 minutos:
 * - Warm-up: 1 minuto em WARMUP_RPS
 * - Step load: STEPS degraus de 1 minuto (default 4), subindo RPS
 *
 * ENV:
 * - BASE_URL (default: http://127.0.0.1:8080)
 * - ENDPOINT_PATH (default: /url-shortener-read/v1/urls?id=NjyX6xq)
 *
 * - WARMUP_RPS (default: 100)
 * - START_RPS (default: 200)
 * - STEP_RPS (default: 200)
 * - STEPS (default: 4) // 4 * 1m = 4 minutos
 *
 * - PRE_VUS / MAX_VUS (default: 300 / 3000)
 *
 * - REQUIRE_ZERO_DROPS (default: true)
 *   Se true, adiciona threshold para dropped_iterations == 0
 *   (garante que o k6 conseguiu cumprir o RPS-alvo).
 */

const BASE_URL = __ENV.BASE_URL || "http://127.0.0.1:8080";
const ENDPOINT_PATH =
    __ENV.ENDPOINT_PATH || "/url-shortener-read/v1/urls?id=NjyX6xq";

const WARMUP_RPS = Number(__ENV.WARMUP_RPS || 200);

const START_RPS = Number(__ENV.START_RPS || 400);
const STEP_RPS = Number(__ENV.STEP_RPS || 400);
const STEPS = Number(__ENV.STEPS || 4);

const PRE_VUS = Number(__ENV.PRE_VUS || 600);
const MAX_VUS = Number(__ENV.MAX_VUS || 6000);

const REQUIRE_ZERO_DROPS = (__ENV.REQUIRE_ZERO_DROPS ?? "true")
    .toLowerCase()
    .trim() === "true";

function buildStages() {
    const stages = [];
    for (let i = 0; i < STEPS; i++) {
        stages.push({ target: START_RPS + i * STEP_RPS, duration: "30s" });
    }
    return stages;
}

export const options = {
    summaryTrendStats: ["avg", "p(90)", "p(95)", "p(99)", "max", "count"],

    scenarios: {
        warmup: {
            executor: "constant-arrival-rate",
            rate: WARMUP_RPS,
            timeUnit: "1s",
            duration: "15s",
            preAllocatedVUs: PRE_VUS,
            maxVUs: MAX_VUS,
            tags: { phase: "warmup", endpoint: "read_url_fixed_id" },
            gracefulStop: "0s",
        },

        step_load: {
            executor: "ramping-arrival-rate",
            startRate: START_RPS,
            timeUnit: "1s",
            preAllocatedVUs: PRE_VUS,
            maxVUs: MAX_VUS,
            startTime: "15s",
            stages: buildStages(),
            tags: { phase: "step", endpoint: "read_url_fixed_id" },
            gracefulStop: "0s",
        },
    },

    thresholds: (() => {
        const t = {
            // Falhas de transporte/timeouts/etc.
            http_req_failed: ["rate==0"],

            // Seu SLO
            http_req_duration: ["p(95)<=300"],

            // Garante que tudo retornou 200
            checks: ["rate==1.0"],
        };

        // Se houver dropped_iterations, o k6 não cumpriu a taxa alvo (gargalo no gerador).
        if (REQUIRE_ZERO_DROPS) {
            t["dropped_iterations"] = ["count==0"];
        }

        return t;
    })(),
};

export default function () {
    const url = `${BASE_URL}${ENDPOINT_PATH}`;

    const res = http.get(url, {
        timeout: "5s",
        headers: factoryHeaders(),
        tags: { name: "GET /url-shortener-read/v1/urls" },
    });

    const ok = check(res, {
        "status is 200": (r) => r.status === 200,
    });

    // Micropausa só em caso de erro, para não “loopar quente”.
    if (!ok) sleep(0.01);
}

export function handleSummary(data) {
    return {
        stdout: textSummary(data, { indent: " ", enableColors: true }),
    };
}
