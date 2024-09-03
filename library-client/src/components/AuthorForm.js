import React, { useEffect } from 'react';
import { useForm } from 'react-hook-form';
import { useNavigate, useParams } from 'react-router-dom';
import { addAuthor, updateAuthor } from '../services/bookService'; // Импортируйте из правильного пути
import { Container, Form, Button } from 'react-bootstrap';

const AuthorForm = ({ existingAuthor }) => {
    const { register, handleSubmit, setValue, formState: { errors } } = useForm();
    const navigate = useNavigate();
    const { id } = useParams(); // Получаем ID из URL для редактирования автора

    useEffect(() => {
        if (existingAuthor) {
            // Если есть существующий автор, заполняем форму данными
            setValue('FirstName', existingAuthor.FirstName);
            setValue('LastName', existingAuthor.LastName);
            setValue('BirthDate', existingAuthor.BirthDate?.split('T')[0]); // Преобразуем дату
            setValue('Country', existingAuthor.Country);
        }
    }, [existingAuthor, setValue]);

    const onSubmit = async (data) => {
        try {
            if (id) {
                // Если ID присутствует, это редактирование
                await updateAuthor(id, data);
            } else {
                // Если ID нет, это добавление
                await addAuthor(data);
            }
            navigate('/authors'); // Перенаправляем на список авторов
        } catch (error) {
            console.error('Error submitting form:', error);
        }
    };

    return (
        <Container className="mt-4">
            <h2>{id ? 'Edit Author' : 'Add Author'}</h2>
            <Form onSubmit={handleSubmit(onSubmit)} className="mt-4">
                <Form.Group controlId="formFirstName">
                    <Form.Label>First Name:</Form.Label>
                    <Form.Control
                        type="text"
                        {...register('FirstName', { required: 'First Name is required' })}
                        isInvalid={!!errors.FirstName}
                    />
                    <Form.Control.Feedback type="invalid">
                        {errors.FirstName?.message}
                    </Form.Control.Feedback>
                </Form.Group>

                <Form.Group controlId="formLastName">
                    <Form.Label>Last Name:</Form.Label>
                    <Form.Control
                        type="text"
                        {...register('LastName', { required: 'Last Name is required' })}
                        isInvalid={!!errors.LastName}
                    />
                    <Form.Control.Feedback type="invalid">
                        {errors.LastName?.message}
                    </Form.Control.Feedback>
                </Form.Group>

                <Form.Group controlId="formBirthDate">
                    <Form.Label>Birth Date:</Form.Label>
                    <Form.Control
                        type="date"
                        {...register('BirthDate', { required: 'Birth Date is required' })}
                        isInvalid={!!errors.BirthDate}
                    />
                    <Form.Control.Feedback type="invalid">
                        {errors.BirthDate?.message}
                    </Form.Control.Feedback>
                </Form.Group>

                <Form.Group controlId="formCountry">
                    <Form.Label>Country:</Form.Label>
                    <Form.Control
                        type="text"
                        {...register('Country', { required: 'Country is required' })}
                        isInvalid={!!errors.Country}
                    />
                    <Form.Control.Feedback type="invalid">
                        {errors.Country?.message}
                    </Form.Control.Feedback>
                </Form.Group>

                <Button variant="primary" type="submit" className="mt-3">
                    Submit
                </Button>
            </Form>
        </Container>
    );
};

export default AuthorForm;
