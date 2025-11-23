import {check} from 'k6';

export function checker(response, tag, desc, statusCode) {
    const des = `[${tag}] - ${desc} - status is ${statusCode}`;

    check(response, {[des]: (r) => r.status === statusCode});
}

export function factoryHeaders() {
    return {
        headers: {
            'Content-Type': 'application/json',
            'X-TenantId': crypto.randomUUID(),
            'X-Correlation-ID': crypto.randomUUID(),
            'Authorization': 'Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IjRVQzhiOVJFZl83cVlkUDRWUV9JeSJ9.eyJpc3MiOiJodHRwczovL2Rldi1lMWRyN3RlZTVxOGNxZ3prLnVzLmF1dGgwLmNvbS8iLCJzdWIiOiJkTHlOTkVwTnlsZzBRcWtiYkl5azJOM1dNWW5TQTN3cEBjbGllbnRzIiwiYXVkIjoiaHR0cHM6Ly90YXhpLXNlcnZpY2UtYXBpIiwiaWF0IjoxNzU2MDU2MzAxLCJleHAiOjE3NTYxNDI3MDEsInNjb3BlIjoidXNlcjpyZWFkIiwiZ3R5IjoiY2xpZW50LWNyZWRlbnRpYWxzIiwiYXpwIjoiZEx5Tk5FcE55bGcwUXFrYmJJeWsyTjNXTVluU0Ezd3AifQ.hq96Ka_n0cpMRhVa2K0dIB1Rx7BPrt6hz_Mjy4X0UVO5eXFkmnePr2lNhhA9fZTXJAZjup8rn5VVf3HC2C1zcFJ4pRd_2RApTdNNtPfHyWF2QCq_XDBG5IbeKOylbaUVnlB2g8MDwn1i27g9ItzpF02xbGNaUNWzq8Vhx0JqTTTJsJjWBn1ENymAOpKAJOsEE-IIgEx4jD85WD2tdzIMiR0dgYMppO4DSfJUg8UXoFDfIpmCmIrlClbjkzhAyxpfQ1srqZKUBPqcVa9KUmLKrPSypanxM2umg8GZZnfHO3ihYHPI79IdjH5B-oPJDgJrsnotmPTROh1L2aR0ZLXCjg'
        }
    };
}