import React, { useState, useEffect } from 'react';
import { addBook, getAuthors } from '../services/bookService';

const AddBook = () => {
    const [title, setTitle] = useState('');
    const [isbn, setIsbn] = useState('');
    const [genre, setGenre] = useState('');
    const [description, setDescription] = useState('');
    const [authorId, setAuthorId] = useState('');
    const [borrowedAt, setBorrowedAt] = useState('');
    const [returnBy, setReturnBy] = useState('');
    const [imageUrl, setImageUrl] = useState('');
    const [authors, setAuthors] = useState([]);
    const [error, setError] = useState('');

    useEffect(() => {
        const fetchAuthors = async () => {
            try {
                const authorsData = await getAuthors();
                setAuthors(authorsData);
            } catch (error) {
                console.error('Error fetching authors:', error);
                setError('Failed to fetch authors.');
            }
        };

        fetchAuthors();
    }, []);

    const handleSubmit = async (e) => {
        e.preventDefault();

        // Client-side validation
        if (!title || !isbn || !genre || !description || !authorId) {
            setError('Please fill in all required fields.');
            return;
        }

        // Prepare the book data
        const newBook = {
            title,
            isbn,
            genre,
            description,
            authorId: parseInt(authorId, 10), // Ensure authorId is a number
            borrowedAt: borrowedAt || null, // Handle empty dates
            returnBy: returnBy || null, // Handle empty dates
            imageUrl: imageUrl || ''
        };

        console.log('Sending request with data:', newBook); // Add logging

        try {
            await addBook(newBook);
            alert('Book added successfully!');
            // Clear the form
            setTitle('');
            setIsbn('');
            setGenre('');
            setDescription('');
            setAuthorId('');
            setBorrowedAt('');
            setReturnBy('');
            setImageUrl('');
            setError(''); // Clear any previous errors
        } catch (error) {
            console.error('Error adding book:', error);
            setError('Failed to add book. Please check the console for details.');
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <h2>Add Book</h2>
            {error && <div style={{ color: 'red' }}>{error}</div>}
            <div>
                <label>Title:</label>
                <input
                    type="text"
                    value={title}
                    onChange={(e) => setTitle(e.target.value)}
                    required
                />
            </div>
            <div>
                <label>ISBN:</label>
                <input
                    type="text"
                    value={isbn}
                    onChange={(e) => setIsbn(e.target.value)}
                    required
                />
            </div>
            <div>
                <label>Genre:</label>
                <input
                    type="text"
                    value={genre}
                    onChange={(e) => setGenre(e.target.value)}
                    required
                />
            </div>
            <div>
                <label>Description:</label>
                <textarea
                    value={description}
                    onChange={(e) => setDescription(e.target.value)}
                    required
                />
            </div>
            <div>
                <label>Author:</label>
                <select
                    value={authorId}
                    onChange={(e) => setAuthorId(e.target.value)}
                    required
                >
                    <option value="">Select an author</option>
                    {authors.map((author) => (
                        <option key={author.id} value={author.id}>
                            {author.firstName} {author.lastName}
                        </option>
                    ))}
                </select>
            </div>
            <div>
                <label>Borrowed At:</label>
                <input
                    type="date"
                    value={borrowedAt}
                    onChange={(e) => setBorrowedAt(e.target.value)}
                />
            </div>
            <div>
                <label>Return By:</label>
                <input
                    type="date"
                    value={returnBy}
                    onChange={(e) => setReturnBy(e.target.value)}
                />
            </div>
            <div>
                <label>Image URL:</label>
                <input
                    type="text"
                    value={imageUrl}
                    onChange={(e) => setImageUrl(e.target.value)}
                />
            </div>
            <button type="submit">Add Book</button>
        </form>
    );
};

export default AddBook;
