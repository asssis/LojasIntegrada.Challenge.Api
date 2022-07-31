import React, { useEffect, useState } from 'react';
import { PurchaseOrderService } from '../Services/PurchaseOrderServices'
import swal from 'sweetalert';


const PurchaseOrder = () => {
    const [mount, setMount] = useState(false);
    const [listPurchaseOrder, setListPurchaseOrder] = useState([{ descricao: "", imagem: "", quantidade: "", valor: "", productId: "", productDto: { quantidade: "" } }]);
    const [totalCompra, setTotalCompra] = useState("");

    async function getCar() {
        var purchaseOrder = await PurchaseOrderService.getPurchaseOrder();
        setListPurchaseOrder(purchaseOrder.itensPurchaseOrder);
        setTotalCompra(purchaseOrder.valorTotal);
    }

    async function addItem(productId: string, quantidade: string) {
        if (quantidade <= "0") {
            swal("Produto Indisponivel", "", "error");
            return;
        }

        await PurchaseOrderService.addItem({ Id: productId });
        await getCar();
    }
    async function removeItem(productId: string) {

        await PurchaseOrderService.removeItem({ Id: productId });
        await getCar();

    }

    function onLoad() {
        if (mount) {
            return;
        }
        setMount(true);
        getCar();
    }
    useEffect(onLoad);

    return (
        <div>
            <h1>My Shoppig Car</h1>
            <br />
            <br />
            <br />

            <table className="table">
                <thead>
                    <tr>
                        <th scope="col">Product</th>
                        <th scope="col">Description</th>
                        <th scope="col">Amount</th>
                        <th scope="col">Value</th>
                    </tr>
                </thead>
                <tbody>
                    {
                        listPurchaseOrder.map(function (itensPurchase, index) {
                            return (
                                <tr>
                                    <th className='col-md-3' scope="row"><img width={50} src={itensPurchase.imagem}></img></th>
                                    <td className='col-md-3'>{itensPurchase.descricao}</td>
                                    <td className='col-md-3'>
                                        <div className='row col-md-6'>
                                            <div className="input-group">
                                                <button onClick={() => removeItem(itensPurchase.productId)} className="btn btn-outline-secondary">-</button>
                                                <input type="text" height="37px" className="form-control" value={itensPurchase.quantidade} />
                                                <button onClick={() => addItem(itensPurchase.productId, itensPurchase.productDto.quantidade)} className="btn btn-outline-secondary">+</button>
                                            </div>
                                        </div>
                                    </td>
                                    <td className='col-md-3'>{itensPurchase.valor}</td>
                                </tr>
                            )
                        })
                    }
                    <tr>
                        <th scope="row">Total</th>
                        <th scope="col"></th>
                        <th scope="col"></th>
                        <th scope="col">{totalCompra}</th>
                    </tr>
                </tbody>
            </table>
        </div>
    );
}

export default PurchaseOrder;