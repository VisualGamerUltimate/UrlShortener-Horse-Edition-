import { useState } from 'react';
import api from '../api';

export default function ShortenUrl() {
    const [originalUrl, setOriginalUrl] = useState('');
    const [shortUrl, setShortUrl] = useState('');
    const [copied, setCopied] = useState(false);
    const [error, setError] = useState('');

    const shorten = async () => {
        try {
            const res = await api.post('/urls', { originalUrl });
            const generatedUrl = `http://localhost:5000/${res.data.shortCode}`;
            setShortUrl(generatedUrl);
            setError('');
            setCopied(false);
        } catch (err) {
            setError(err.response?.data || 'Failed to shorten URL');
            setShortUrl('');
        }
    };

    const copyToClipboard = async () => {
        try {
            await navigator.clipboard.writeText(shortUrl);
            setCopied(true);
        } catch (err) {
            console.error('Copy failed:', err);
        }
    };

    return (
        <div className="app-wrapper">
            <div className="container">
                
                <h2>Shorten URL</h2>
                <input
                    placeholder="Paste long URL"
                    value={originalUrl}
                    onChange={e => setOriginalUrl(e.target.value)}
                />
                <button onClick={shorten}>Shorten</button>

                {shortUrl && (
                    <div style={{ marginTop: '1rem' }}>
                        <p>
                            Shortened URL:&nbsp;
                            <a href={shortUrl} target="_blank" rel="noopener noreferrer">
                                {shortUrl}
                            </a>
                        </p>
                        <button onClick={copyToClipboard}>Copy to Clipboard</button>
                        {copied && <span style={{ marginLeft: '10px' }}>Copied!</span>}
                    </div>
                )}

                {error && <p style={{ color: 'red' }}>{error}</p>}
            </div>
        </div>
    );
}
