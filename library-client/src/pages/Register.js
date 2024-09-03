// src/pages/Register.js
import React from 'react';
import { useForm } from 'react-hook-form';
import { Button, Form } from 'react-bootstrap';
import { registerUser } from '../services/authorService';

const Register = () => {
    const { register, handleSubmit, formState: { errors } } = useForm();
    const onSubmit = async (data) => {
        try {
            await registerUser(data);
            // Redirect to login or home page
        } catch (error) {
            console.error('Registration failed', error);
        }
    };

    return (
        <div className="container mt-4">
            <h2>Register</h2>
            <Form onSubmit={handleSubmit(onSubmit)}>
                <Form.Group controlId="formBasicEmail">
                    <Form.Label>Email address</Form.Label>
                    <Form.Control type="email" {...register('email', { required: 'Email is required' })} />
                    {errors.email && <Form.Text className="text-danger">{errors.email.message}</Form.Text>}
                </Form.Group>
                <Form.Group controlId="formBasicPassword">
                    <Form.Label>Password</Form.Label>
                    <Form.Control type="password" {...register('password', { required: 'Password is required' })} />
                    {errors.password && <Form.Text className="text-danger">{errors.password.message}</Form.Text>}
                </Form.Group>
                <Button variant="primary" type="submit">Register</Button>
            </Form>
        </div>
    );
};

export default Register;
