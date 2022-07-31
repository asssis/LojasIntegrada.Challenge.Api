import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import '../Css/Login.css'
import { LoginService } from '../Services/LoginServices';
import swal from 'sweetalert';


const Signin = () => {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [repeatPassword, setRepeatPassword] = useState("");
    const [mensageAlert, setMensageAlert] = useState([]);

    var alertErro = () => {
        if (mensageAlert) {
            return (
                <div className="alert alert-danger" role="alert">
                    <ul>
                        {
                            mensageAlert.map(function (itemMensagem, index) {
                                return (<li style={{ fontSize: 14 }}>{itemMensagem['errorMessage']}</li>)
                            })
                        }
                    </ul>
                </div>
            );
        }
    }

    async function saveUser() {
        try {
            if (password == repeatPassword) {
                await LoginService.saveUser(username, password);
            }
            else {
                swal("Wrong", "Senha não confere", "error");
            }
        }
        catch (err) {
            console.log("================");
            console.log(err);
            var tes = err;
            setMensageAlert(err as any);
        }
    }

    return (
        <div>
            <div className="container">
                <div className="card card-container">
                    <img id="profile-img" className="profile-img-card" src="https://i.pinimg.com/736x/3e/aa/24/3eaa245d923949b6f662b8ba07b7a3b2.jpg" />
                    <p id="profile-name" className="profile-name-card"></p>

                    {alertErro()}

                    <div className="form-signin">
                        <span id="reauth-email" className="reauth-email"></span>
                        <input type="text" onChange={(e) => setUsername(e.target.value)} className="form-control" placeholder="User Name" required />
                        <input type="password" onChange={(e) => setPassword(e.target.value)} className="form-control" placeholder="Senha" required />
                        <input type="password" onChange={(e) => setRepeatPassword(e.target.value)} className="form-control" placeholder="Repetir Senha" required />

                        <button className="btn btn-lg btn-primary btn-block btn-signin" onClick={() => saveUser()}>Finish</button>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default Signin;