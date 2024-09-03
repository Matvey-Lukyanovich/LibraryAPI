// src/pages/BookList.js
import React, { useEffect, useState } from 'react';
import { getBooks } from '../services/bookService';
import { Table, Button, Form, Pagination } from 'react-bootstrap';
import { Link } from 'react-router-dom';

const BookList = () => {
    const [books, setBooks] = useState([]);
    const [filteredBooks, setFilteredBooks] = useState([]);
    const [error, setError] = useState('');
    const [search, setSearch] = useState('');
    const [genreFilter, setGenreFilter] = useState('');
    const [authorFilter, setAuthorFilter] = useState('');
    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(1);
    const itemsPerPage = 10;

    useEffect(() => {
        const fetchBooks = async () => {
            try {
                const data = await getBooks();
                setBooks(data);
                setTotalPages(Math.ceil(data.length / itemsPerPage));
            } catch (err) {
                setError('Failed to fetch books.');
            }
        };
        fetchBooks();
    }, []);

    useEffect(() => {
        const applyFilters = () => {
            let result = books;

            if (search) {
                result = result.filter(book => book.title.toLowerCase().includes(search.toLowerCase()));
            }
            if (genreFilter) {
                result = result.filter(book => book.genre.toLowerCase() === genreFilter.toLowerCase());
            }
            if (authorFilter) {
                result = result.filter(book => book.author.toLowerCase() === authorFilter.toLowerCase());
            }

            setFilteredBooks(result);
            setTotalPages(Math.ceil(result.length / itemsPerPage));
        };

        applyFilters();
    }, [search, genreFilter, authorFilter, books]);

    const handlePageChange = (pageNumber) => {
        setCurrentPage(pageNumber);
    };

    const displayedBooks = filteredBooks.slice((currentPage - 1) * itemsPerPage, currentPage * itemsPerPage);

    return (
        <div className="container mt-4">
            <h2>Book List</h2>
            {error && <div className="alert alert-danger">{error}</div>}
            <Form inline className="mb-4">
                <Form.Control 
                    type="text" 
                    placeholder="Search by title" 
                    className="mr-sm-2" 
                    value={search} 
                    onChange={(e) => setSearch(e.target.value)} 
                />
                <Form.Control 
                    type="text" 
                    placeholder="Filter by genre" 
                    className="mr-sm-2" 
                    value={genreFilter} 
                    onChange={(e) => setGenreFilter(e.target.value)} 
                />
                <Form.Control 
                    type="text" 
                    placeholder="Filter by author" 
                    value={authorFilter} 
                  
                    onChange={(e) => setAuthorFilter(e.target.value)} 
                />
            </Form>
            <Table striped bordered hover>
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Title</th>
                        <th>Author</th>
                        <th>Genre</th>
                        <th>Status</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {displayedBooks.length > 0 ? (
                        displayedBooks.map((book) => (
                            <tr key={book.id}>
                                <td>{book.id}</td>
                                <td>{book.title}</td>
                                <td>{book.author}</td>
                                <td>{book.genre}</td>
                                <td>{book.isBorrowed ? 'Not Available' : 'Available'}</td>
                                <td>
                                    <Link to={`/books/${book.id}`}>
                                        <Button variant="info" size="sm">View</Button>
                                    </Link>
                                </td>
                            </tr>
                        ))
                    ) : (
                        <tr>
                            <td colSpan="6">No books found</td>
                        </tr>
                    )}
                </tbody>
            </Table>
            <Pagination>
                {[...Array(totalPages).keys()].map((pageNumber) => (
                    <Pagination.Item 
                        key={pageNumber + 1} 
                        active={pageNumber + 1 === currentPage}
                        onClick={() => handlePageChange(pageNumber + 1)}
                    >
                        {pageNumber + 1}
                    </Pagination.Item>
                ))}
            </Pagination>
        </div>
    );
};

export default BookList;
