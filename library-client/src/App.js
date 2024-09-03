// src/App.js
import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import BookForm from './components/BookForm';
import AuthorForm from './components/AuthorForm';
import BookList from './components/BookList';
import AuthorList from './components/AuthorList';
import NavigationBar from './components/Navbar'; // Импорт компонента Navbar
// src/index.js
import 'bootstrap/dist/css/bootstrap.min.css';

const App = () => {
    return (
        <Router>
            <NavigationBar /> {/* Добавляем панель навигации */}
            <div className="container mt-4"> {/* Bootstrap container for spacing */}
                <Routes>
                    <Route path="/books/new" element={<BookForm />} />
                    <Route path="/books/edit/:id" element={<BookForm />} />
                    <Route path="/books" element={<BookList />} />

                    <Route path="/authors/new" element={<AuthorForm />} />
                    <Route path="/authors/edit/:id" element={<AuthorForm />} />
                    <Route path="/authors" element={<AuthorList />} />
                </Routes>
            </div>
        </Router>
    );
};

export default App;
