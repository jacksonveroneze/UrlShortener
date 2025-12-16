import http from 'k6/http';
import {check} from 'k6';

export function create(baseUrl, headers) {
    const params = {
        headers: headers,
        tags: {
            kind: 'write',
        },
    };
    
    const body = JSON.stringify({
        originalUrl: 'https://github.com/',
        customAlias: "aa",
        expirationDate: '2026-05-12T12:20:00+00:00'
    });

    const response = http.post(`${baseUrl}/urls`, body, params);
    check(response, {'[Url] - Created - status is 201': (r) => r.status === 201});

    if (response.status !== 201) {
        console.log('----------------------');
        console.log(`status: ${response.status}`);
        console.log(`status: ${JSON.parse(response.body)}`);
        console.log('----------------------');
    }

    return JSON.parse(response.body);
}

export function getById(baseUrl, headers, id, statusCodeDefault = null) {
    const params = {
        headers: headers,
        tags: {
            kind: 'read',
        },
    };
    
    const response = http.get(`${baseUrl}/urls?id=${id}`, params);

    check(response, {'[Url] - getById - status is 200': (r) => r.status === 200});

    if (response.status !== 200) {
        console.log('----------------------');
        console.log(`status: ${response.status}`);
        console.log(`status: ${id}`);
        console.log('----------------------');
    }

    return response;
}