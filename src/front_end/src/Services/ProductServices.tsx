import handleResponse from './HandleResponse';
import { HeaderHelper } from './HeaderHelper';
import settings from "../settings.json";

const apiBaseUrl: string = settings.apiBaseUrl;


export const ProductsService = {
    getAll
};

async function getAll(): Promise<any> {
    const apiUrl = `${apiBaseUrl}/v1/Product/Index`;

    return await fetch(apiUrl, HeaderHelper.getOptions())
        .then((response: Response) =>
            handleResponse<any>(response)
        );
}
