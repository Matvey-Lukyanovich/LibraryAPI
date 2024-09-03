// src/pages/Login.js
import React from 'react';
import { useForm } from 'react-hook-form';
import { Button, Form } from 'react-bootstrap';
import { loginUser } from '../services/authorService';

const Login = () => {
    const { register, handleSubmit, formState: { errors } } = useForm();
    const onSubmit = async (data) => {
        try {
            await loginUser(data);
            // Redirect to home page or user dashboard
        } catch (error) {
            console.error('Login failed', error);
        }
    };

    return (
        <div className="container mt-4">
            <h2>Login</h2>
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
                <Button variant="primary" type="submit">Login</Button>
            </Form>
        </div>
    );
};

export default Login;
