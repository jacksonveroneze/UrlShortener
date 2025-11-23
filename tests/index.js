import {group, sleep} from 'k6';
import driver from "./scenarios/driver/driver.scenario.js";

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
    vus: 1
};

export const optionsIterations = {
    iterations: 20000,
    vus: 250
};

export const optionsDuration = {
    stages: [
        {duration: '10s', target: 10},
        {duration: '60s', target: 200}
    ]
};

export const options = optionsRamp;

// const baseUrl = 'http://localhost:7000/api/v1';
const baseUrl = 'http://0.0.0.0:8080/url-shortener/api/v1';

export default () => {
    group('Endpoint Driver', () => {
        driver(baseUrl);
    });
}
