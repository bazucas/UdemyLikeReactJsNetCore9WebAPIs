// VideoListPage.js
import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';

function VideoListPage() {
  const [videos, setVideos] = useState([]);

  useEffect(() => {
    // Obter a lista de vídeos do backend
    fetch('/api/videos')
      .then(response => response.json())
      .then(data => setVideos(data));
  }, []);

  return (
    <div style={{ padding: '20px' }}>
      <h1>Cursos Disponíveis</h1>
      <div style={{ display: 'flex', flexWrap: 'wrap' }}>
        {videos.map(video => (
          <div key={video.videoId} style={{ margin: '10px' }}>
            <Link to={`/videos/${video.videoId}`}>
              <img
                src={video.thumbnailUrl}
                alt={video.title}
                style={{ width: '200px', height: 'auto', cursor: 'pointer' }}
              />
            </Link>
            <h3>{video.title}</h3>
          </div>
        ))}
      </div>
    </div>
  );
}

export default VideoListPage;
