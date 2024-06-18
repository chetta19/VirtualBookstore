import React, { useState } from 'react';

function BookForm() {
  const [book, setBook] = useState({
    Id: '',
    ISBN: '',
    Title: '',
    Author: '',
    Genre: '',
    Price: 0,
    CoverImage: '',
    Description: '',
  });

  const handleChange = (e) => {
    setBook({ ...book, [e.target.name]: e.target.value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    fetch('http://localhost:5000/api/books', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(book),
    })
      .then((response) => response.json())
      .then((data) => console.log(data))
      .catch((error) => console.error('Error:', error));
  };

  return (
    <form onSubmit={handleSubmit}>
      <label>
        ID:
        <input type="text" name="Id" onChange={handleChange} />
      </label>
      <label>
        ISBN:
        <input type="text" name="ISBN" onChange={handleChange} />
      </label>
      <label>
        Title:
        <input type="text" name="Title" onChange={handleChange} />
      </label>
      <label>
        Author:
        <input type="text" name="Author" onChange={handleChange} />
      </label>
      <label>
        Genre:
        <input type="text" name="Genre" onChange={handleChange} />
      </label>
      <label>
        Price:
        <input type="number" name="Price" onChange={handleChange} />
      </label>
      <label>
        Cover Image:
        <input type="text" name="CoverImage" onChange={handleChange} />
      </label>
      <label>
        Description:
        <textarea name="Description" onChange={handleChange} />
      </label>
      <input type="submit" value="Submit" />
    </form>
  );
}

export default BookForm;