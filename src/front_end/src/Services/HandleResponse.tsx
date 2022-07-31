export default function handleResponse<T>(response: Response) {
    return response.text().then(text => {
        const data = text && JSON.parse(text);
        if (!response.ok) {
            if (response.status === 401) {
                //AuthService.logout();
                //window.location.reload();
            }
            const error = (data && data.error) ||
                (data && data.errors && data.errors[0]) ||
                (data && data.message) ||
                (response.statusText)
            return Promise.reject(error);
        }
        return data as T;
    });
}
