import React, { useEffect } from 'react';
import { useForm } from 'react-hook-form';
import { useNavigate, useParams } from 'react-router-dom';
import { addBook, updateBook } from '../services/bookService'; // Импортируйте из правильного пути
import { Container, Form, Button } from 'react-bootstrap';

const BookForm = ({ existingBook }) => {
    const { register, handleSubmit, setValue, formState: { errors } } = useForm();
    const navigate = useNavigate();
    const { id } = useParams(); // Получаем ID из URL для редактирования книги

    useEffect(() => {
        if (existingBook) {
            // Если есть существующая книга, заполняем форму данными
            setValue('ISBN', existingBook.ISBN);
            setValue('Title', existingBook.Title);
            setValue('Genre', existingBook.Genre);
            setValue('Description', existingBook.Description);
            setValue('AuthorId', existingBook.AuthorId);
            setValue('BorrowedAt', existingBook.BorrowedAt);
            setValue('ReturnBy', existingBook.ReturnBy);
            setValue('ImageUrl', existingBook.ImageUrl);
            setValue('IsBorrowed', existingBook.IsBorrowed);
        }
    }, [existingBook, setValue]);

    const onSubmit = async (data) => {
        try {
            if (id) {
                // Если ID присутствует, это редактирование
                await updateBook(id, data);
            } else {
                // Если ID нет, это добавление
                await addBook(data);
            }
            navigate('/books'); // Перенаправляем на список книг
        } catch (error) {
            console.error('Error submitting form:', error);
        }
    };

    return (
        <Container className="mt-4">
            <h2>{id ? 'Edit Book' : 'Add Book'}</h2>
            <Form onSubmit={handleSubmit(onSubmit)} className="mt-4">
                <Form.Group controlId="formISBN">
                    <Form.Label>ISBN:</Form.Label>
                    <Form.Control
                        type="text"
                        {...register('ISBN', { required: 'ISBN is required' })}
                        isInvalid={!!errors.ISBN}
                    />
                    <Form.Control.Feedback type="invalid">
                        {errors.ISBN?.message}
                    </Form.Control.Feedback>
                </Form.Group>

                <Form.Group controlId="formTitle">
                    <Form.Label>Title:</Form.Label>
                    <Form.Control
                        type="text"
                        {...register('Title', { required: 'Title is required' })}
                        isInvalid={!!errors.Title}
                    />
                    <Form.Control.Feedback type="invalid">
                        {errors.Title?.message}
                    </Form.Control.Feedback>
                </Form.Group>

                <Form.Group controlId="formGenre">
                    <Form.Label>Genre:</Form.Label>
                    <Form.Control
                        type="text"
                        {...register('Genre')}
                    />
                </Form.Group>

                <Form.Group controlId="formDescription">
                    <Form.Label>Description:</Form.Label>
                    <Form.Control
                        as="textarea"
                        {...register('Description')}
                    />
                </Form.Group>

                <Form.Group controlId="formAuthorId">
                    <Form.Label>Author ID:</Form.Label>
                    <Form.Control
                        type="number"
                        {...register('AuthorId', { required: 'Author ID is required' })}
                        isInvalid={!!errors.AuthorId}
                    />
                    <Form.Control.Feedback type="invalid">
                        {errors.AuthorId?.message}
                    </Form.Control.Feedback>
                </Form.Group>

                <Form.Group controlId="formBorrowedAt">
                    <Form.Label>Borrowed At:</Form.Label>
                    <Form.Control
                        type="date"
                        {...register('BorrowedAt')}
                    />
                </Form.Group>

                <Form.Group controlId="formReturnBy">
                    <Form.Label>Return By:</Form.Label>
                    <Form.Control
                        type="date"
                        {...register('ReturnBy')}
                    />
                </Form.Group>

                <Form.Group controlId="formImageUrl">
                    <Form.Label>Image URL:</Form.Label>
                    <Form.Control
                        type="text"
                        {...register('ImageUrl')}
                    />
                </Form.Group>

                <Form.Group controlId="formIsBorrowed">
                    <Form.Check
                        type="checkbox"
                        label="Is Borrowed"
                        {...register('IsBorrowed')}
                    />
                </Form.Group>

                <Button variant="primary" type="submit">
                    Submit
                </Button>
            </Form>
        </Container>
    );
};

export default BookForm;
