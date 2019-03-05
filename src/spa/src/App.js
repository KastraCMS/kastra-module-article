import React, { Component } from 'react';
import { Routes } from './routes';
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap/dist/js/bootstrap.js'
import './App.css';

class App extends Component {
  
  render() {
    return (
      <div className="module-article">
        <Routes />
      </div>
    );
  }
}

export default App;
