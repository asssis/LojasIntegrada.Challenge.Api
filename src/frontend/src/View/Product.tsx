import React, { useEffect, useState } from 'react';
import Card from '../Component/Card'
import { ProductsService } from '../Services/ProductServices'

const Product = () => {

    const [listProducts, setListProducts] = useState([{ id: "", valor: "", imagem: "", descricao: "", quantidade: "" }]);
    const [mount, setMount] = useState(false);
    const buscarProducts = async (): Promise<void> => {
        var listProducts = await ProductsService.getAll();
        setListProducts(listProducts);
    }
    function onLoad() {
        if (mount) {
            return;
        }
        setMount(true);
        buscarProducts();
    }
    useEffect(onLoad);

    return (
        <div className="row ">
            <h1>Product</h1>
            <br />
            <br />
            {
                listProducts.map(function (product, index) {
                    return <Card
                        valor={product.valor}
                        imagem={product.imagem}
                        descricao={product.descricao}
                        productId={product.id}
                        quantidade={product.quantidade} />;
                })
            }


        </div>
    );
}

export default Product;