// App.js
import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import VideoListPage from './VideoListPage';
import VideoPlayerPage from './VideoPlayerPage';

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<VideoListPage />} />
        <Route path="/videos/:id" element={<VideoPlayerPage />} />
      </Routes>
    </Router>
  );
}

export default App;
