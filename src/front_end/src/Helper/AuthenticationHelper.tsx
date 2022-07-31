
import { LoginService } from '../Services/LoginServices';
import settings from "../settings.json";

const apiBaseUrl: string = settings.apiBaseUrl;


export const AuthenticationHelper = {
    getAuthentication,
    checkAuthentication,
    doingLogout
};
async function getAuthentication(username: string, password: string) {
    const apiUrl = `${apiBaseUrl}/v1/Login`;

    var credential = await LoginService.getLogin(username, password);
    if (!credential) {
        return null;
    }
    var jsonCredential = JSON.stringify(credential);
    localStorage.setItem('ComprasJa:AccessToken', jsonCredential);
    return checkAuthentication();
}
function checkAuthentication() {
    var credential: any = localStorage.getItem('ComprasJa:AccessToken');
    if (!credential) {
        return null;
    }
    return JSON.parse(credential);
}
function doingLogout() {
    localStorage.removeItem('ComprasJa:AccessToken');
    window.location.replace('/Login');
}