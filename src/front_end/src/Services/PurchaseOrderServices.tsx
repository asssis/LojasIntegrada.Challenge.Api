import handleResponse from './HandleResponse';
import { getOptions, HeaderHelper } from './HeaderHelper'
import settings from "../settings.json";

const apiBaseUrl: string = settings.apiBaseUrl;


export const PurchaseOrderService = {
    getPurchaseOrder,
    addItem,
    removeItem
};

async function getPurchaseOrder(): Promise<any> {
    const apiUrl = `${apiBaseUrl}/v1/PurchaseOrder/GetPurchaseOrderList`;
    return await fetch(apiUrl, HeaderHelper.getOptions())
        .then((response: Response) =>
            handleResponse<any>(response)
        );
}
async function addItem(product: any): Promise<any> {
    const apiUrl = `${apiBaseUrl}/v1/PurchaseOrder/AddItemPurchaseOrder`;
    var header: any = HeaderHelper.postOptions(product);

    return await fetch(apiUrl, header)
        .then((response: Response) =>
            handleResponse<any>(response)
        );
}

async function removeItem(product: any): Promise<any> {
    const apiUrl = `${apiBaseUrl}/v1/PurchaseOrder/RemoveItemPurchaseOrder`;
    var header: any = HeaderHelper.postOptions(product);
    return await fetch(apiUrl, header)
        .then((response: Response) =>
            handleResponse<any>(response)
        );
}