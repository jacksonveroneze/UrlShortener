import {group} from 'k6';
import * as driver from "./driver.js";
import {factoryHeaders} from "../util.js";

export default (baseUrl) => {
    group('Complete Flow', () => {
        const result = driver.create(baseUrl, factoryHeaders(), 201);
        
        driver.getById(baseUrl, factoryHeaders(), result.data.code, 200);
    });
}