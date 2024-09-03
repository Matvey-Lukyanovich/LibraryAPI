// src/components/BookList.js
import React, { useEffect, useState } from 'react';
import { getBooks, deleteBook } from '../services/bookService';
import { Table, Button, Modal } from 'react-bootstrap';
import { Link } from 'react-router-dom';

const BookList = () => {
    const [books, setBooks] = useState([]);
    const [showModal, setShowModal] = useState(false);
    const [selectedBookId, setSelectedBookId] = useState(null);

    useEffect(() => {
        const fetchBooks = async () => {
            try {
                const data = await getBooks();
                setBooks(data);
            } catch (error) {
                console.error('Error fetching books:', error);
            }
        };
        fetchBooks();
    }, []);

    const handleDelete = async () => {
        if (selectedBookId) {
            try {
                await deleteBook(selectedBookId);
                setBooks(books.filter(book => book.id !== selectedBookId));
                setShowModal(false);
                setSelectedBookId(null);
            } catch (error) {
                console.error('Error deleting book:', error);
            }
        }
    };

    const showDeleteModal = (id) => {
        setSelectedBookId(id);
        setShowModal(true);
    };

    return (
        <div>
            <h2>Book List</h2>
            <Table striped bordered hover>
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Title</th>
                        <th>Author</th>
                        <th>ISBN</th>
                        <th>Publication Date</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {books.map((book) => (
                        <tr key={book.id}>
                            <td>{book.id}</td>
                            <td>{book.title}</td>
                            <td>{book.author ? `${book.author.firstName} ${book.author.lastName}` : 'Unknown'}</td>
                            <td>{book.isbn}</td>
                            <td>{new Date(book.publicationDate).toLocaleDateString()}</td>
                            <td>
                                <Link to={`/books/edit/${book.id}`}>
                                    <Button variant="warning" size="sm">Edit</Button>
                                </Link>
                                <Button 
                                    variant="danger" 
                                    size="sm" 
                                    className="ml-2" 
                                    onClick={() => showDeleteModal(book.id)}
                                >
                                    Delete
                                </Button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </Table>

            <Modal show={showModal} onHide={() => setShowModal(false)}>
                <Modal.Header closeButton>
                    <Modal.Title>Confirm Deletion</Modal.Title>
                </Modal.Header>
                <Modal.Body>Are you sure you want to delete this book?</Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={() => setShowModal(false)}>
                        Cancel
                    </Button>
                    <Button variant="danger" onClick={handleDelete}>
                        Delete
                    </Button>
                </Modal.Footer>
            </Modal>
        </div>
    );
};

export default BookList;
