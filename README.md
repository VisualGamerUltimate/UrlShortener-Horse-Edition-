# UrlShortener Web Client

This is the frontend client for the UrlShortener project. It is built using modern JavaScript tooling and is designed to interact with the backend services to provide URL shortening, user registration, login, and related features.

## Features

- Shorten long URLs to easy-to-share short links
- User registration and authentication
- Responsive user interface
- Integration with backend services via REST APIs
- Built with [Vite](https://vitejs.dev/) for fast development and builds
- Unit testing with [Vitest](https://vitest.dev/)

## Prerequisites

- [Node.js](https://nodejs.org/) (v18 or higher recommended)
- [npm](https://www.npmjs.com/) (comes with Node.js)

## Getting Started

1. **Install dependencies:**
npm install
2. **Run the development server:**
npm run dev
The app will be available at [http://localhost:5173](http://localhost:5173) by default.

3. **Build for production:**
npm run build
The production build will be output to the `dist` folder.

4. **Run tests:**
npm run test

## Project Structure

- `src/` - Main source code for the frontend
  - `pages/` - React components for each page (Register, Login, ShortenUrl, etc.)
  - `api.jsx` - API integration logic
  - `App.jsx` - Main application component
  - `main.jsx` - Entry point
- `public/` - Static assets
- `dist/` - Production build output (generated after build)
- `package.json` - Project metadata and scripts

## Configuration

- The frontend expects backend services to be running and accessible. Update API endpoints in `src/api.jsx` as needed to match your backend configuration.

## Testing

- Unit tests are written using [Vitest](https://vitest.dev/).
- To run tests:
npm run test

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request.

## License

This project is licensed under the MIT License.
