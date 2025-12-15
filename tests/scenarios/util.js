import {check} from 'k6';

export function checker(response, tag, desc, statusCode) {
    const des = `[${tag}] - ${desc} - status is ${statusCode}`;

    check(response, {[des]: (r) => r.status === statusCode});
}

export function factoryHeaders() {
    return {
        headers: {
            'Content-Type': 'application/json',
            'X-Correlation-ID': crypto.randomUUID(),
            'Authorization': 'Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IjRVQzhiOVJFZl83cVlkUDRWUV9JeSJ9.eyJpc3MiOiJodHRwczovL2Rldi1lMWRyN3RlZTVxOGNxZ3prLnVzLmF1dGgwLmNvbS8iLCJzdWIiOiJkTHlOTkVwTnlsZzBRcWtiYkl5azJOM1dNWW5TQTN3cEBjbGllbnRzIiwiYXVkIjoiaHR0cHM6Ly90YXhpLXNlcnZpY2UtYXBpIiwiaWF0IjoxNzY1ODA4ODE0LCJleHAiOjE3NjU4OTUyMTQsInNjb3BlIjoidXNlcjpyZWFkIiwiZ3R5IjoiY2xpZW50LWNyZWRlbnRpYWxzIiwiYXpwIjoiZEx5Tk5FcE55bGcwUXFrYmJJeWsyTjNXTVluU0Ezd3AifQ.XdjXxcW28mxbEbzc3couVM3dph9HCs6U4YcPvNM6HLamirIGq-dEmZCef0RgSDo-U8oFqi-wKth5oYJpNzaQneraqXEyiF5bLLBksQjLSx42kQW40P3-SzrebGtw4n-GatfnWZKobnRKTnwqksGZibKvLhvDhshTfYgED0fiZeZZAyQKNCkL1B2NVpqBi4oCcB6xgFjmV1h8OGiiX4tygFXqvq09qY34EoPceb4IKpaPQqxJeJJfXtLSzIsd7uTQZ0g9PtnUH0MFRg0CnrYS8tx8VjQgxF5FRtYzl8tTolRPTZWf7Gre3NPoKe0h1gbktmq_vlEwP6kIoVme3oTncA'
        }
    };
}