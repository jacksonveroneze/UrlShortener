import http from "k6/http";

export function factoryHeaders(token) {
    return {
        'Accept': "application/json",
        'X-Correlation-ID': crypto.randomUUID(),
        'Authorization': `Bearer ${token}`
    };
}

export function getToken() {
    const url = `${__ENV.URL_TOKEN}`;
    const payload = JSON.stringify({
        client_id: `${__ENV.CLIENT_ID_TOKEN}`,
        client_secret: `${__ENV.CLIENT_SECRET_TOKEN}`,
        audience: `${__ENV.AUDIENCE_TOKEN}`,
        grant_type: `${__ENV.GRANT_TYPE_TOKEN}`,
        scope: 'user:read',
    });

    const params = {
        headers: {
            'Content-Type': 'application/json',
        },
    };

    const res = http.post(url, payload, params);
    console.log(res.json().access_token)
    
    return res.json().access_token;
}