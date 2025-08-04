import { BrowserRouter, Routes, Route, Link, NavLink } from 'react-router-dom';
import Register from './pages/Register';
import Login from './pages/Login';
import ShortenUrl from './pages/ShortenUrl';
import './App.css';

function App() {
    return (
        <BrowserRouter>
            <nav style={{ marginBottom: '20px' }}>
                <div className="nav-buttons">
                    <NavLink to="/login" className="nav-button"> <button>Login</button> </NavLink>
                    <NavLink to="/register" className="nav-button"> <button>Register</button> </NavLink>
                    <NavLink to="/shorten" className="nav-button"> <button>Shorten URL</button> </NavLink>
                </div>
            </nav>
            <Routes>
                <Route path="/register" element={<Register />} />
                <Route path="/login" element={<Login />} />
                <Route path="/shorten" element={<ShortenUrl />} />
            </Routes>
        </BrowserRouter>
    );
}

export default App;