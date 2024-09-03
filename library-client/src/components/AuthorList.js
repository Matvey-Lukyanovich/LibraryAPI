import React, { useEffect, useState } from 'react';
import { deleteAuthor } from '../services/authorService'; // Убедитесь, что импортируете правильный путь
import { getAuthors } from '../services/bookService'; // Убедитесь, что импортируете правильный путь
import { Table, Button, Alert } from 'react-bootstrap';
import { Link } from 'react-router-dom';

const AuthorList = () => {
    const [authors, setAuthors] = useState([]);
    const [error, setError] = useState('');

    useEffect(() => {
        const fetchAuthors = async () => {
            try {
                const data = await getAuthors();
                setAuthors(data);
            } catch (err) {
                setError('Failed to fetch authors.');
            }
        };
        fetchAuthors();
    }, []);

    const handleDelete = async (id) => {
        if (window.confirm('Are you sure you want to delete this author?')) {
            try {
                await deleteAuthor(id);
                setAuthors(authors.filter(author => author.id !== id));
            } catch (err) {
                setError('Failed to delete author.');
            }
        }
    };

    return (
        <div>
            <h2 className="mb-4">Author List</h2>
            {error && <Alert variant="danger">{error}</Alert>}
            <Table striped bordered hover responsive>
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>First Name</th>
                        <th>Last Name</th>
                        <th>Birth Date</th>
                        <th>Country</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {authors.length > 0 ? (
                        authors.map((author) => (
                            <tr key={author.id}>
                                <td>{author.id}</td>
                                <td>{author.firstName}</td>
                                <td>{author.lastName}</td>
                                <td>{author.birthDate}</td>
                                <td>{author.country}</td>
                                <td>
                                    <Link to={`/authors/edit/${author.id}`}>
                                        <Button variant="warning" size="sm" className="mr-2">Edit</Button>
                                    </Link>
                                    <Button 
                                        variant="danger" 
                                        size="sm" 
                                        onClick={() => handleDelete(author.id)}
                                    >
                                        Delete
                                    </Button>
                                </td>
                            </tr>
                        ))
                    ) : (
                        <tr>
                            <td colSpan="6" className="text-center">No authors found</td>
                        </tr>
                    )}
                </tbody>
            </Table>
        </div>
    );
};

export default AuthorList;
