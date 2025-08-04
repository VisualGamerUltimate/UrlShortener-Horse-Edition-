// src/pages/Register.js
import { useState } from 'react';
import api from '../api';

export default function Register() {
    const [user, setUser] = useState({ username: '', email: '', password: '' });
    const [message, setMessage] = useState('');

    const register = async () => {
        try {
            const res = await api.post('/auth/register', user);
            setMessage(res.data.message || 'Registered successfully');
        } catch (err) {
            setMessage(err.response?.data?.message || 'Registration failed');
        }
    };

    return (
        <div className="app-wrapper">
            <div className="container">
            <h2>Register</h2>
            <input placeholder="Username" onChange={e => setUser({ ...user, username: e.target.value })} />
            <input placeholder="Email" onChange={e => setUser({ ...user, email: e.target.value })} />
            <input type="password" placeholder="Password" onChange={e => setUser({ ...user, password: e.target.value })} />
            <button onClick={register}>Register</button>
            <p>{message}</p>
        </div>
        </div>
    );
}
