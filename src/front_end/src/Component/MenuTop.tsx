
import React, { useEffect, useState } from 'react';
import { PurchaseOrderService } from '../Services/PurchaseOrderServices'
import { AuthenticationHelper } from '../Helper/AuthenticationHelper';

export const MenuTop = () => {
    const [mount, setMount] = useState(false);
    const [totalItens, setTotalItens] = useState(0);
    async function getCar() {
        var purchaseOrder = await PurchaseOrderService.getPurchaseOrder();
        var totalItens = purchaseOrder.itensPurchaseOrder.length;

        setTotalItens(totalItens);

    }

    function onLoad() {
        if (mount) {
            return;
        }
        setMount(true);
        getCar();
    }
    useEffect(onLoad);
    const shopingCar = () => {

        if (totalItens > 0) {
            return (<a className="nav-link" aria-current="page" href="/PurchaseOrder">
                Shopping Car <span className="badge text-bg-danger ml-2">{totalItens}</span>
            </a>)
        }
        else {
            return (<></>)

        }
    }
    const loginLogout = () => {
        if (AuthenticationHelper.checkAuthentication()) {
            return (
                <li><a onClick={() => AuthenticationHelper.doingLogout()} style={{ cursor: 'pointer' }} className="nav-link">Logout</a></li>
            )
        }
        else {

            return (
                <li><a href="/Login" style={{ cursor: 'pointer' }} className="nav-link">Login</a></li>
            )
        }
    }


    return (
        <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
            <div className="container-fluid">
                <a className="navbar-brand" href="#">CompreJá</a>
                <div className="collapse navbar-collapse" id="navbarTogglerDemo02">
                    <ul className="navbar-nav me-auto mb-2 mb-lg-0">
                        <li className="nav-item">
                            <a className="nav-link" aria-current="page" href="/">Products</a>
                        </li>
                        <li className="nav-item">
                            {shopingCar()}
                        </li>
                    </ul>
                    <ul className="nav navbar-nav navbar-right">
                        {loginLogout()}
                    </ul>
                </div>
            </div>
        </nav >
    )
}