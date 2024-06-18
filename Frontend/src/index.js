import React from 'react';
import ReactDOM from 'react-dom';
import CreateBookForm from './Books/CreateBookForm';

const root = document.getElementById('root');
ReactDOM.createRoot(root).render(
  <React.StrictMode>
    <CreateBookForm />
  </React.StrictMode>
);