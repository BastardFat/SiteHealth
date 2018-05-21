import * as Toastr from 'toastr';
import { interpolateWith } from './Helpers';

function status(response) {
    if (response.status >= 200 && response.status < 300) {
        return response;
    } else {
        if (response.status === 400 || response.status === 401 || response.status === 403) {
            return response.text()
                .then(text => { throw new Error(text); });
        } else {
            throw new Error(response.statusText);
        }
    }
}

function json(response) {
    return response.json();
}

function toastrError(error) {
    let message = error;
    if (error.message)
        message = error.message;
    Toastr.error(message);
    throw error;
}

function getHeaders() {
    let requestHeaders = new Headers();
    requestHeaders.append('Accept', 'application/json');
    requestHeaders.append('Content-Type', 'application/json');
    return requestHeaders;
}

function get(url) {
    return fetch(url, { method: 'GET', headers: getHeaders() })
        .then(status)
        .then(json)
        .catch(toastrError);
}

function post(url, data) {
    return fetch(url, { method: 'POST', body: JSON.stringify(data), headers: getHeaders() })
        .then(status)
        .then(json)
        .catch(toastrError);
}

function del(url) {
    return fetch(url, { method: 'DELETE', headers: getHeaders() })
        .then(status)
        .then(json)
        .catch(toastrError);
}

const encode = interpolateWith(encodeURIComponent);

const Api = {
    Admin: {
        SaveSite(model) {
            return post('api/admin/site/save', model);
        },
        RemoveSite(id) {
            return del(encode`api/admin/site/remove?id=${id}`);
        },
        EditSite(id) {
            return get(encode`api/admin/site/edit?id=${id}`);
        },
        SetOption(key, type, model) {
            return post(encode`api/admin/options/set?key=${key}&type=${type}`, model);
        },
        GetOption(key) {
            return get(encode`api/admin/options/get?key=${key}`);
        },
        GetOptions() {
            return get('api/admin/options/get');
        }
    },
    Data: {
        GetSites(page, search) {
            return get(encode`api/data/sites/get?page=${page}&search=${search}`);
        },
        GetSite(id) {
            return get(encode`api/data/sites/get?id=${id}`);
        }
    }
};

export default Api;