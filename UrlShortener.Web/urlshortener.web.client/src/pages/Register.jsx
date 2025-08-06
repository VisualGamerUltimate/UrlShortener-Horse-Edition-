import { useState } from 'react';
import api from '../api';

export default function Register() {
    const [user, setUser] = useState({
        username: '',
        email: '',
        password: ''
    });

    const [message, setMessage] = useState('');

    const handleChange = (e) => {
        const { name, value } = e.target;
        setUser(prev => ({ ...prev, [name]: value }));
    };

    const register = async () => {
        try {
            const res = await api.post('/auth/register', user);
            setMessage(res.data.message || ' Registered successfully');
        } catch (err) {
            setMessage(err.response?.data || ' Registration failed');
        }
    };

    return (
        <div className="app-wrapper">
            <div className="container">
                <h2>Register</h2>

                <input
                    name="username"
                    placeholder="Username"
                    value={user.username}
                    onChange={handleChange}
                />

                <input
                    name="email"
                    placeholder="Email"
                    value={user.email}
                    onChange={handleChange}
                />

                <input
                    name="password"
                    type="password"
                    placeholder="Password"
                    value={user.password}
                    onChange={handleChange}
                />

                <button onClick={register}>Register</button>
                <p>{message}</p>
            </div>
        </div>
    );
}
