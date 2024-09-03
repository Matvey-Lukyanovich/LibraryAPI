// src/services/bookService.js
import axios from 'axios';

const API_URL = 'http://localhost:5000/api';

export const getAuthors = async () => {
    try {
        const response = await axios.get(`${API_URL}/authors`);
        return response.data;
    } catch (error) {
        console.error('Error fetching authors:', error);
        throw error;
    }
};

export const addAuthor = async (author) => {
    try {
        const response = await axios.post(`${API_URL}/authors`, author);
        return response.data;
    } catch (error) {
        console.error('Error adding author:', error.response ? error.response.data : error.message);
        throw error;
    }
};

export const updateAuthor = async (id, author) => {
    try {
        await axios.put(`${API_URL}/authors/${id}`, author);
    } catch (error) {
        console.error('Error updating author:', error);
        throw error;
    }
};

export const deleteAuthor = async (id) => {
    try {
        await axios.delete(`${API_URL}/authors/${id}`);
    } catch (error) {
        console.error('Error deleting author:', error);
        throw error;
    }
};

// Book-related functions

export const getBooks = async () => {
    try {
        const response = await axios.get(`${API_URL}/books`);
        return response.data;
    } catch (error) {
        console.error('Error fetching books:', error);
        throw error;
    }
};

export const getBookById = async (id) => {
    try {
        const response = await fetch(`${API_URL}/books/${id}`);
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        return await response.json();
    } catch (error) {
        console.error('Error fetching book details:', error);
        throw error;
    }
};



export const addBook = async (book) => {
    try {
        const response = await axios.post(`${API_URL}/books`, book);
        return response.data;
    } catch (error) {
        console.error('Error adding book:', error.response ? error.response.data : error.message);
        throw error;
    }
};

export const updateBook = async (id, book) => {
    try {
        await axios.put(`${API_URL}/books/${id}`, book);
    } catch (error) {
        console.error('Error updating book:', error);
        throw error;
    }
};

export const deleteBook = async (id) => {
    try {
        await axios.delete(`${API_URL}/books/${id}`);
    } catch (error) {
        console.error('Error deleting book:', error);
        throw error;
    }
};

export const getUserBooks = async () => {
    const response = await fetch('/api/user-books');
    if (!response.ok) {
        throw new Error('Failed to fetch user books');
    }
    return await response.json();
};
export const borrowBook = async (id) => {
    const response = await fetch(`/api/books/borrow/${id}`, {
        method: 'PUT',
    });
    if (!response.ok) {
        throw new Error('Failed to borrow book');
    }
    return await response.json();
};