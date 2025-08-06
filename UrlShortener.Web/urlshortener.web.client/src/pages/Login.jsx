import { useState } from 'react';
import api from '../api';
import { useAuth } from '../AuthContext';

export default function Login() {
    const { token, username, login, logout, isLoggedIn } = useAuth();
    const [loginData, setLoginData] = useState({ username: '', password: '' });
    const [message, setMessage] = useState('');

    const handleLogin = async () => {
        try {
            const res = await api.post('/auth/login', loginData);
            login(res.data.token, res.data.user.username);
            setMessage('Login successful!');
        } catch (err) {
            setMessage(err.response?.data || 'Login failed');
        }
    };

    const handleLogout = () => {
        logout();
        setMessage('Logged out successfully.');
    };

    return (
        <div className="app-wrapper">
            <div className="container">
                {!isLoggedIn() ? (
                    <>
                        <h2>Login</h2>
                        <input
                            placeholder="Username"
                            value={loginData.username}
                            onChange={e => setLoginData({ ...loginData, username: e.target.value })}
                        />
                        <input
                            type="password"
                            placeholder="Password"
                            value={loginData.password}
                            onChange={e => setLoginData({ ...loginData, password: e.target.value })}
                        />
                        <button onClick={handleLogin}>Login</button>
                    </>
                ) : (
                    <>
                        <h3>Welcome, {username}!</h3>
                        <button onClick={handleLogout}>Logout</button>
                    </>
                )}
                <p>{message}</p>
            </div>
        </div>
    );
}
