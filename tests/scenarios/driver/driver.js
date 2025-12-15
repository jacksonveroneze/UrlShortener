import http from 'k6/http';
import {check} from 'k6';

export function create(baseUrl, headers) {
    const body = JSON.stringify({
        originalUrl: 'https://github.com/',
        customAlias: "aa",
        expirationDate: '2026-05-12T12:20:00+00:00'
    });

    const response = http.post(`${baseUrl}/urls`, body, headers);
    check(response, {'[Url] - Created - status is 201': (r) => r.status === 201});

    const content = JSON.parse(response.body);

    return content.data;
}