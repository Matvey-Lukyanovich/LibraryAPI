// src/components/Navbar.js
import React from 'react';
import { Link } from 'react-router-dom';
import { Navbar, Nav } from 'react-bootstrap';
import './Navbar.css'; // Подключение дополнительных стилей, если нужно

const NavigationBar = () => {
    return (
        <Navbar bg="dark" variant="dark" expand="lg">
            <Navbar.Brand as={Link} to="/">Library</Navbar.Brand>
            <Navbar.Toggle aria-controls="basic-navbar-nav" />
            <Navbar.Collapse id="basic-navbar-nav">
                <Nav className="mr-auto">
                    <Nav.Link as={Link} to="/books">Books</Nav.Link>
                    <Nav.Link as={Link} to="/books/new">Add Book</Nav.Link>
                    <Nav.Link as={Link} to="/authors">Authors</Nav.Link>
                    <Nav.Link as={Link} to="/authors/new">Add Author</Nav.Link>
                </Nav>
            </Navbar.Collapse>
        </Navbar>
    );
};

export default NavigationBar;
