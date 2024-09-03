/*// src/index.js
import 'bootstrap/dist/css/bootstrap.min.css'; // Импорт стилей Bootstrap
import React from 'react';
import ReactDOM from 'react-dom';
import App from './App';

ReactDOM.render(
    <React.StrictMode>
        <App />
    </React.StrictMode>,
    document.getElementById('root')
);
*/
// src/index.js
import React from 'react';
import ReactDOM from 'react-dom/client'; // Изменено
import App from './App';

const root = ReactDOM.createRoot(document.getElementById('root')); // Изменено
root.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>
);