import {group} from 'k6';
import * as driver from "./driver.js";
import {factoryHeaders} from "../util.js";

export default (baseUrl) => {
    group('Complete Flow', () => {
        for (let i = 0; i < 10; i++) {
            driver.create(baseUrl, factoryHeaders(), 201);
        }

        const idDriver = driver.create(baseUrl, factoryHeaders(), 201);
        
        driver.getPaged(baseUrl, factoryHeaders(), 200);
        
        driver.getById(baseUrl, factoryHeaders(), idDriver, 200);
        // driver.activate(baseUrl, factoryHeaders(), idDriver);
        // driver.inactivate(baseUrl, factoryHeaders(), idDriver);
    });
}