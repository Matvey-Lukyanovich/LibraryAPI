// src/pages/UserBooks.js
import React, { useEffect, useState } from 'react';
import { getUserBooks } from '../services/bookService'; // Убедитесь, что импортируете правильный путь
import { Table } from 'react-bootstrap';

const UserBooks = () => {
    const [books, setBooks] = useState([]);
    const [error, setError] = useState('');

    useEffect(() => {
        const fetchUserBooks = async () => {
            try {
                const data = await getUserBooks();
                setBooks(data);
            } catch (err) {
                setError('Failed to fetch user books.');
            }
        };
        fetchUserBooks();
    }, []);

    return (
        <div className="container mt-4">
            <h2>My Borrowed Books</h2>
            {error && <div className="alert alert-danger">{error}</div>}
            <Table striped bordered hover>
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Title</th>
                        <th>Author</th>
                        <th>Genre</th>
                        <th>Borrowed At</th>
                        <th>Return By</th>
                    </tr>
                </thead>
                <tbody>
                    {books.length > 0 ? (
                        books.map((book) => (
                            <tr key={book.id}>
                                <td>{book.id}</td>
                                <td>{book.title}</td>
                                <td>{book.author}</td>
                                <td>{book.genre}</td>
                                <td>{book.borrowedAt}</td>
                                <td>{book.returnBy}</td>
                            </tr>
                        ))
                    ) : (
                        <tr>
                            <td colSpan="6">No books borrowed</td>
                        </tr>
                    )}
                </tbody>
            </Table>
        </div>
    );
};

export default UserBooks;
