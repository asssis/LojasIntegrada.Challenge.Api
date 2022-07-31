import handleResponse from './HandleResponse';
import { postOptions } from './HeaderHelper'
import settings from "../settings.json";

const apiBaseUrl: string = settings.apiBaseUrl;


export const LoginService = {
    getLogin,
    saveUser
};
export class User {
    username: string = "";
    password: string = "";
}

async function getLogin(username: string, password: string): Promise<any> {
    const userHeader = postOptions({ username: username, password: password });

    const apiUrl = `${apiBaseUrl}/v1/Login`;
    return await fetch(apiUrl, userHeader)
        .then((response: Response) =>
            handleResponse<any>(response)
        );
}
async function saveUser(username: string, password: string): Promise<any> {
    const apiUrl = `${apiBaseUrl}/v1/create`;
    const userHeader = postOptions({ userName: username, password: password });
    return await fetch(apiUrl, userHeader)
        .then((response: Response) =>
            handleResponse<any>(response)
        );
}