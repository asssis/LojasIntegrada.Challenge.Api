import ReactDOM from "react-dom/client";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Login from "./View/Login"
import Product from "./View/Product";
import PurchaseOrder from "./View/PurchaseOrder";
import Signin from "./View/Signin";
import { MenuTop } from "./Component/MenuTop";

function App() {
  return (
    <div>
      <MenuTop />
      <div className="container">
        <BrowserRouter>
          <Routes>
            <Route path="/" element={<Product />} />
            <Route path="/Login" element={<Login />} />
            <Route path="/Signin" element={<Signin />} />
            <Route path="/PurchaseOrder" element={<PurchaseOrder />} />
          </Routes>
        </BrowserRouter>
      </div>
    </div>
  );
}

export default App;
