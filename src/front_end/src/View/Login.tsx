import React, { useEffect, useState } from 'react';
import '../Css/Login.css'
import { AuthenticationHelper } from '../Helper/AuthenticationHelper';
import swal from 'sweetalert';


const Login = () => {
    const [mount, setMount] = useState(false);
    const [username, setUsername] = useState("joao");
    const [password, setPassword] = useState("joao");
    async function clickEnter(e: any) {
        if (e.charCode == 13)
            fazerLogin();
    }
    async function fazerLogin() {
        try {
            var credential: any = await AuthenticationHelper.getAuthentication(username, password);
            if (credential)
                window.location.replace('/');
        }
        catch
        {
            swal("Wrong", "Wrong UserName or Password", "error");
        }
    }

    return (
        <div>
            <div className="container">
                <div className="card card-container">
                    <img id="profile-img" className="profile-img-card" src="https://i.pinimg.com/736x/3e/aa/24/3eaa245d923949b6f662b8ba07b7a3b2.jpg" />
                    <p id="profile-name" className="profile-name-card"></p>
                    <div className="form-signin">
                        <span id="reauth-email" className="reauth-email"></span>
                        <input type="text" id="nome" defaultValue="joao" className="form-control" onChange={(e) => setUsername(e.target.value)} placeholder="Nome" />
                        <input type="password" defaultValue="joao" id="senha" className="form-control" onKeyPress={(e) => clickEnter(e)} onChange={(e) => setPassword(e.target.value)} placeholder="Senha" />

                        <button className="btn btn-lg btn-primary btn-block btn-signin" onClick={fazerLogin}>Login</button>
                        <button onClick={() => window.location.replace("/Signin")} className="btn btn-lg btn-primary btn-block btn-signin">Signin</button>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default Login;