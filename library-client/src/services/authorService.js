// src/services/authorService.js

const API_URL = 'http://localhost:5000/authors'; // Замените на ваш API URL

export const getAuthors = async () => {
    const response = await fetch(API_URL);
    if (!response.ok) throw new Error('Failed to fetch authors');
    return response.json();
};

export const getAuthorById = async (id) => {
    const response = await fetch(`${API_URL}/${id}`);
    if (!response.ok) throw new Error('Failed to fetch author');
    return response.json();
};

export const registerUser = async (userData) => {
    const response = await fetch('/api/register', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(userData),
    });
    if (!response.ok) {
        throw new Error('Failed to register');
    }
    return await response.json();
};

export const loginUser = async (credentials) => {
    const response = await fetch('/api/login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(credentials),
    });
    if (!response.ok) {
        throw new Error('Failed to login');
    }
    return await response.json();
};

export const addAuthor = async (author) => {
    const response = await fetch(API_URL, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(author),
    });
    if (!response.ok) throw new Error('Failed to add author');
    return response.json();
};

// src/services/authorService.js
export const deleteAuthor = async (id) => {
    const response = await fetch(`http://localhost:5000/api/authors/${id}`, {
        method: 'DELETE',
    });

    if (!response.ok) {
        throw new Error('Failed to delete author');
    }
};


export const updateAuthor = async (id, author) => {
    const response = await fetch(`${API_URL}/${id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(author),
    });
    if (!response.ok) throw new Error('Failed to update author');
    return response.json();
};

/*// src/services/authorService.js
import axios from 'axios';

const API_URL = 'http://localhost:5000/api/authors';

export const getAuthors = () => {
    return axios.get(API_URL);
};

export const getAuthor = (id) => {
    return axios.get(`${API_URL}/${id}`);
};

export const addAuthor = async (author) => {
    try {
        // Проверка перед отправкой
        console.log('Отправка данных на сервер:', author);
        
        const response = await axios.post('http://localhost:5000/api/authors', author, {
            headers: {
                'Content-Type': 'application/json'
            }
        });
        return response.data;
    } catch (error) {
        // Логирование ошибки
        console.error('Ошибка в addAuthor:', error.response ? error.response.data : error.message);
        throw error;
    }
};*/