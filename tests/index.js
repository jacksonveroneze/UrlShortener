import {group} from 'k6';
import * as driver from "./scenarios/driver/driver.js";
import {factoryHeaders} from "./scenarios/util.js";
import {health} from "./scenarios/driver/driver.js";

export let optionsRamp = {
    stages: [
        {duration: '5s', target: 1},
        {duration: '120s', target: 25},
        {duration: '300s', target: 25},
        {duration: '300s', target: 25},
        {duration: '300s', target: 50},
        {duration: '100s', target: 5},
        {duration: '30s', target: 5},
        {duration: '300s', target: 5}
    ]
};

export const optionsOneIteration = {
    iterations: 1,
    vus: 1,
    thresholds: {
        'http_req_duration{kind:write}': ['p(95)<300'],
        'http_req_duration{kind:read}':  ['p(95)<300'],
    }
};

export const optionsIterations = {
    iterations: 20000,
    vus: 100
};

export const optionsDuration = {
    stages: [
        {duration: '2s', target: 1},
        {duration: '240s', target: 100}
    ],
    thresholds: {
        'http_req_duration{kind:write}': ['p(95)<300'],
        'http_req_duration{kind:read}':  ['p(95)<300'],
    }
};

export const options = optionsDuration;

// const baseUrl = 'http://172.19.0.8:8080';
const baseUrl = 'http://127.0.0.1:8080';

export default () => {
   // driver.health(baseUrl, factoryHeaders());
   //  driver.getById(baseUrl, factoryHeaders(), "xZRYP7x", 200);
    
    
    group('Endpoint Driver', () => {
        const result = driver.create(baseUrl, factoryHeaders(), 201);

        if (result.data.code) {
            for (let i = 0; i < 100; i++) {
                driver.getById(baseUrl, factoryHeaders(), result.data.code, 200);
            }
        }
    });
}
