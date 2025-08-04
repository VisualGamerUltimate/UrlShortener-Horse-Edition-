// src/pages/Login.js
import { useState } from 'react';
import api from '../api';

export default function Login() {
    const [loginData, setLoginData] = useState({ username: '', password: '' });
    const [message, setMessage] = useState('');

    const login = async () => {
        try {
            const res = await api.post('/auth/login', loginData);
            localStorage.setItem('token', res.data.token);
            setMessage('Login successful!');
        } catch (err) {
            setMessage(err.response?.data || 'Login failed');
        }
    };

    return (
        <div className="app-wrapper">
            <div className="container"> 
            <h2>Login</h2>
            <input placeholder="Username" onChange={e => setLoginData({ ...loginData, username: e.target.value })} />
            <button onClick={login}>Login</button>
            <p>{message}</p>
        </div>
        </div>
    );
}
