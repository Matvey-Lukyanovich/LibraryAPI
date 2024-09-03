// src/pages/BookForm.js
import React, { useEffect } from 'react';
import { useForm } from 'react-hook-form';
import { useNavigate, useParams } from 'react-router-dom';
import { addBook, updateBook, getBookById } from '../services/bookService';
import { Button, Form } from 'react-bootstrap';

const BookForm = () => {
    const { register, handleSubmit, setValue } = useForm();
    const navigate = useNavigate();
    const { id } = useParams();

    useEffect(() => {
        if (id) {
            const fetchBook = async () => {
                const data = await getBookById(id);
                setValue('title', data.title);
                setValue('author', data.author);
                setValue('genre', data.genre);
                setValue('description', data.description);
                setValue('isBorrowed', data.isBorrowed);
            };
            fetchBook();
        }
    }, [id, setValue]);

    const onSubmit = async (data) => {
        try {
            if (id) {
                await updateBook(id, data);
            } else {
                await addBook(data);
            }
            navigate('/books');
        } catch (error) {
            console.error('Error submitting form:', error);
        }
    };

    return (
        <div className="container mt-4">
            <h2>{id ? 'Edit Book' : 'Add Book'}</h2>
            <Form onSubmit={handleSubmit(onSubmit)}>
                <Form.Group controlId="formTitle">
                    <Form.Label>Title</Form.Label>
                    <Form.Control type="text" {...register('title', { required: 'Title is required' })} />
                </Form.Group>
             
                <Form.Group controlId="formAuthor">
                    <Form.Label>Author</Form.Label>
                    <Form.Control type="text" {...register('author', { required: 'Author is required' })} />
                </Form.Group>
                <Form.Group controlId="formGenre">
                    <Form.Label>Genre</Form.Label>
                    <Form.Control type="text" {...register('genre')} />
                </Form.Group>
                <Form.Group controlId="formDescription">
                    <Form.Label>Description</Form.Label>
                    <Form.Control as="textarea" {...register('description')} />
                </Form.Group>
                <Form.Group controlId="formIsBorrowed">
                    <Form.Check type="checkbox" label="Is Borrowed" {...register('isBorrowed')} />
                </Form.Group>
                <Button variant="primary" type="submit">Submit</Button>
            </Form>
        </div>
    );
};

export default BookForm;
