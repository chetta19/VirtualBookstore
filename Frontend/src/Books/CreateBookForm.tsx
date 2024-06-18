import React, { useState } from 'react';
import { v4 as uuidv4 } from 'uuid';
import * as yup from 'yup';
import '../tailwind.css';

const schema = yup.object().shape({
  ISBN: yup.string().required('ISBN is required'),
  Title: yup.string().required('Title is required'),
  Author: yup.string().required('Author is required'),
  Price: yup.number().positive('Price must be a positive number').required('Price is required'),
  CoverImage: yup.string().url('Must be a valid URL for an image').required('Cover image is required'),
});

function BookForm() {
  const [book, setBook] = useState({
    Id: uuidv4(),
    ISBN: '',
    Title: '',
    Author: '',
    Genre: '',
    Price: 0,
    CoverImage: '',
    Description: '',
  });

  const [errors, setErrors] = useState({});

  const handleChange = (e) => {
    setBook({ ...book, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    try {
      await schema.validate(book);
      setErrors({});
      e.preventDefault();
      await fetch('http://127.0.0.1:5000/api/books', {
        method: 'POST',
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
          "Access-Control-Allow-Origin": "*",
          "Access-Control-Allow-Methods": "POST,",
        },
        body: JSON.stringify(book),
      })
        .then((response) => response.json())
        .then((data) => console.log(data))
        .catch((error) => console.error('Error:', error));
    } catch (err) {
      setErrors(err.errors);
    }

  };

  return (
    <form onSubmit={handleSubmit}>
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