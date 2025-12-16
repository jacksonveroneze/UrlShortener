import {group} from 'k6';
import * as driver from "./scenarios/driver/driver.js";
import {factoryHeaders} from "./scenarios/util.js";

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
        {duration: '10s', target: 25},
        {duration: '10s', target: 200},
        {duration: '30s', target: 200}
    ],
    thresholds: {
        'http_req_duration{kind:write}': ['p(95)<300'],
        'http_req_duration{kind:read}':  ['p(95)<300'],
    }
};

export const options = optionsDuration;

// const baseUrl = 'http://127.0.0.1:7000/v1';
const baseUrl = 'http://127.0.0.1:8080/url-shortener/v1';

export default () => {
   
    
    
    group('Endpoint Driver', () => {
        const result = driver.create(baseUrl, factoryHeaders(), 201);

        if (result.data.code) {

            for (let i = 0; i < 10; i++) {
                driver.getById(baseUrl, factoryHeaders(), result.data.code, 200);
            }
        }
    });
}
