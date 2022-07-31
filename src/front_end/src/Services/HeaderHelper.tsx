import { AuthenticationHelper } from '../Helper/AuthenticationHelper';

export function authHeader() {
    var credential: any = AuthenticationHelper.checkAuthentication();
    var token = "";
    if (credential) {
        token = "Bearer " + credential.token;
    }
    const header: any = {
        "Content-Type": "application/json",
        "Authorization": token
    };
    return header;
}

export const HeaderHelper = {
    getOptions,
    postOptions
};

export function getOptions() {
    return {
        method: 'GET',
        headers: authHeader()
    };
}

export function postOptions(body: any) {
    return {
        method: 'POST',
        headers: authHeader(),
        body: (body && JSON.stringify(body)) || null
    };
}
