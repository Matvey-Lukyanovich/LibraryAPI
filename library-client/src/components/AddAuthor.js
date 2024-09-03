import React, { useState } from 'react';
import { addAuthor } from '../services/authorService';

const AddAuthor = () => {
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [birthDate, setBirthDate] = useState('');
    const [country, setCountry] = useState('');

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            // Преобразование даты к формату yyyy-mm-dd
            const formattedBirthDate = new Date(birthDate).toISOString().split('T')[0];
            const newAuthor = {
                firstName: firstName.trim(),
                lastName: lastName.trim(),
                birthDate: formattedBirthDate,
                country: country.trim(),
                books: [] // Можно добавить пустой массив, если нужно
            };
            await addAuthor(newAuthor);
            alert('Author added successfully!');
            setFirstName('');
            setLastName('');
            setBirthDate('');
            setCountry('');
        } catch (error) {
            console.error('Error adding author:', error);
            alert('Failed to add author.');
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <h2>Add Author</h2>
            <div>
                <label>First Name:</label>
                <input type="text" value={firstName} onChange={(e) => setFirstName(e.target.value)} required />
            </div>
            <div>
                <label>Last Name:</label>
                <input type="text" value={lastName} onChange={(e) => setLastName(e.target.value)} required />
            </div>
            <div>
                <label>Birth Date:</label>
                <input
                    type="date"
                    value={birthDate}
                    onChange={(e) => setBirthDate(e.target.value)}
                    required
                    pattern="\d{4}-\d{2}-\d{2}" // Убедитесь, что формат соответствует yyyy-mm-dd
                />
            </div>
            <div>
                <label>Country:</label>
                <input type="text" value={country} onChange={(e) => setCountry(e.target.value)} required />
            </div>
            <button type="submit">Add Author</button>
        </form>
    );
};

export default AddAuthor;
