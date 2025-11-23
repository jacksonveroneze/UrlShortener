import http from 'k6/http';
import {check} from 'k6';

import {randomString} from 'https://jslib.k6.io/k6-utils/1.2.0/index.js';
import {checker} from "../util.js";

export function create(baseUrl, headers) {
    const body = JSON.stringify({
        fullName: `${randomString(8)} ${randomString(8)}`,
        document: "06399214939",
        email: `${randomString(10)}@mail.com`
    });

    const response = http.post(`${baseUrl}/drivers`, body, headers);
    check(response, {'[Driver] - Created - status is 201': (r) => r.status === 201});

    const content = JSON.parse(response.body);

    return content.data.id;
}

export function getPaged(baseUrl, headers, statusCodeDefault = null) {
    const response = http.get(`${baseUrl}/drivers`, headers);

    if (statusCodeDefault) {
        checker(response, 'Driver', 'GetPaged', statusCodeDefault)
    }

    return response;
}

export function getById(baseUrl, headers, id, statusCodeDefault = null) {
    const response = http.get(`${baseUrl}/drivers/${id}`, headers);

    if (statusCodeDefault) {
        checker(response, 'Driver', 'GetById', statusCodeDefault)
    }

    return response;
}

export function activate(baseUrl, headers, id) {
    const response = http.put(`${baseUrl}/drivers/${id}/activate`, {}, headers);
    check(response, {'[Driver] - Activated - status is 204': (r) => r.status === 204});

    return response;
}

export function inactivate(baseUrl, headers, id) {
    const response = http.put(`${baseUrl}/drivers/${id}/inactivate`, {}, headers);
    check(response, {'[Driver] - Inctivated - status is 204': (r) => r.status === 204});

    return response;
}

export function remove(baseUrl, headers, id) {
    const response = http.del(`${baseUrl}/drivers/${id}`, {}, headers);
    check(response, {'[Driver] - Deleted - status is 204': (r) => r.status === 204});

    return response;
}