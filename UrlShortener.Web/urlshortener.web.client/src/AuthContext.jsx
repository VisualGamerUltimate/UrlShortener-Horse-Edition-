import { createContext, useContext, useEffect, useState } from 'react';

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const [token, setToken] = useState(localStorage.getItem('token') || null);
    const [username, setUsername] = useState(localStorage.getItem('username') || null);

    const login = (newToken, user) => {
        setToken(newToken);
        setUsername(user);
        localStorage.setItem('token', newToken);
        localStorage.setItem('username', user);
    };

    const logout = () => {
        setToken(null);
        setUsername(null);
        localStorage.removeItem('token');
        localStorage.removeItem('username');
    };

    const isLoggedIn = () => !!token;

    return (
        <AuthContext.Provider value={{ token, username, login, logout, isLoggedIn }}>
            {children}
        </AuthContext.Provider>
    );
};

export const useAuth = () => useContext(AuthContext);
