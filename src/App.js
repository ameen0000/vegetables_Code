import React from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Login from './components/Login';
import Dashboard from './components/Dashboard';
import Map from './components/Map'; // Import the Map component

function App() {
  return (
    <div>
      <BrowserRouter>
        <Routes>
          <Route index element={<Login/>} />
          <Route path='/dashboard' element={<Dashboard/>} />
          <Route path='/map' element={<Map />} /> 
        </Routes>
      </BrowserRouter>
    </div>
  );
}

export default App;
