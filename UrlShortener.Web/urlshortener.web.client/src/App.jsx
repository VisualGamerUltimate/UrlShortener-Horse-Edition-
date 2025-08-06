import { BrowserRouter, Routes, Route, NavLink } from 'react-router-dom';
import Register from './pages/Register';
import Login from './pages/Login';
import ShortenUrl from './pages/ShortenUrl';
import './App.css';
import { useAuth } from './AuthContext';

function App() {
    const { isLoggedIn, username } = useAuth();

    return (
        <BrowserRouter>
            {/* Container that holds both header and main app */}
            <div style={{ position: 'relative', minHeight: '100vh', paddingTop: '60px' }}>
                {/* Top-left user info */}
                {isLoggedIn() && (
                    <div style={{
                        position: 'absolute',
                        top: '10px',
                        left: '20px',
                        fontWeight: 'bold',
                        fontSize: '0.95rem',
                        color: '#2185d0'
                    }}>
                        Logged in as: <span>{username}</span>
                    </div>
                )}

                {/* Centered nav buttons below header */}
                <nav style={{ marginBottom: '20px', display: 'flex', justifyContent: 'center', gap: '10px' }}>
                    <NavLink to="/login"><button>Login</button></NavLink>
                    <NavLink to="/register"><button>Register</button></NavLink>
                    <NavLink to="/shorten"><button>Shorten URL</button></NavLink>
                </nav>

                {/* Page routes */}
                <Routes>
                    <Route path="/register" element={<Register />} />
                    <Route path="/login" element={<Login />} />
                    <Route path="/shorten" element={<ShortenUrl />} />
                </Routes>
            </div>
        </BrowserRouter>
    );
}

export default App;
