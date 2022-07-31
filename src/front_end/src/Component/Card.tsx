import React from 'react';
import { PurchaseOrderService } from '../Services/PurchaseOrderServices'
import swal from 'sweetalert';

interface Props {
    productId: string;
    quantidade: string;
    valor: string;
    descricao: string;
    imagem: string;
}

const Login = (props: Props) => {
    async function putProduct(productId: string, quantidade: string) {
        if (quantidade <= "0") {
            swal("Produto Indisponivel", "", "error");
            return;
        }

        await PurchaseOrderService.addItem({ Id: productId, Quantidade: quantidade });

        swal("Good job!",
            "Produto adicionado no carrinho!",
            "success",
            {
                buttons: ["Continuar Comprando", "Ir Para o Carrionho"]
            }
        ).then((value) => {
            if (value) {
                window.location.replace('/PurchaseOrder');
            }
            else {
                window.location.reload();
            }
        });
    }
    return (
        <div className="col-md-3 mt-2">
            <div className="card card-body">
                <img src={props.imagem} className="card-img-top" alt="Computador" />
                <hr></hr>
                <p className="card-text mt-0 mb-0"><strong>Produto:</strong> {props.descricao}</p>
                <p className="card-text mt-0 mb-0"><strong>Valor:</strong> {props.valor} R$</p>
                <p className="card-text mt-0 mb-0"><strong>Estoque:</strong> {props.quantidade}</p>
                <hr></hr>
                <div className="d-grid gap-2 d-md-flex justify-content-md-end">
                    <button onClick={() => putProduct(props.productId, props.quantidade)} className="btn btn-outline-primary btn-sm">Add</button>
                </div>
            </div>
        </div>
    );
}

export default Login;

