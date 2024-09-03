// src/pages/BookDetail.js
import React, { useEffect, useState } from 'react';
import { useParams, Link } from 'react-router-dom';
import { getBookById, deleteBook, borrowBook } from '../services/bookService';
import { Button, Modal, Alert } from 'react-bootstrap';

const BookDetail = () => {
    const { id } = useParams();
    const [book, setBook] = useState(null);
    const [showModal, setShowModal] = useState(false);
    const [error, setError] = useState('');

    useEffect(() => {
        const fetchBook = async () => {
            try {
                const data = await getBookById(id);
                setBook(data);
            } catch (err) {
                setError('Failed to fetch book details.');
            }
        };
        fetchBook();
    }, [id]);

    const handleDelete = async () => {
        try {
            await deleteBook(id);
            setShowModal(false);
            // Redirect to book list or show a success message
        } catch (err) {
            setError('Failed to delete book.');
        }
    };

    const handleBorrow = async () => {
        try {
            await borrowBook(id);
            setBook(prevBook => ({ ...prevBook, isBorrowed: true }));
        } catch (err) {
            setError('Failed to borrow the book.');
        }
    };

    return (
        <div className="container mt-4">
            {error && <Alert variant="danger">{error}</Alert>}
            {book ? (
                <div>
                    <h2>{book.title}</h2>
                    <p><strong>Author:</strong> {book.author}</p>
                    <p><strong>Genre:</strong> {book.genre}</p>
                    <p><strong>Description:</strong> {book.description}</p>
                    <p><strong>Status:</strong> {book.isBorrowed ? 'Not Available' : 'Available'}</p>
                    {!book.isBorrowed && <Button onClick={handleBorrow}>Borrow</Button>}
                    <Link to={`/books/edit/${book.id}`}>
                        <Button variant="warning" className="ml-2">Edit</Button>
                    </Link>
                    <Button 
                        variant="danger" 
                        className="ml-2" 
                        onClick={() => setShowModal(true)}
                    >
                        Delete
                    </Button>

                    <Modal show={showModal} onHide={() => setShowModal(false)}>
                        <Modal.Header closeButton>
                            <Modal.Title>Confirm Delete</Modal.Title>
                        </Modal.Header>
                        <Modal.Body>Are you sure you want to delete this book?</Modal.Body>
                        <Modal.Footer>
                            <Button variant="secondary" onClick={() => setShowModal(false)}>Cancel</Button>
                            <Button variant="danger" onClick={handleDelete}>Delete</Button>
                        </Modal.Footer>
                    </Modal>
                </div>
            ) : (
                <p>Loading book details...</p>
            )}
        </div>
    );
};

export default BookDetail;
