// VideoPlayerPage.js
import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import VideoPlayer from './VideoPlayer';
import VideoList from './VideoList';

function VideoPlayerPage() {
  const { id } = useParams();
  const [videos, setVideos] = useState([]);
  const [currentVideoIndex, setCurrentVideoIndex] = useState(0);
  const [videoProgress, setVideoProgress] = useState({});

  useEffect(() => {
    // Obter a lista de vídeos do backend
    fetch('/api/videos')
      .then(response => response.json())
      .then(data => {
        setVideos(data);
        const index = data.findIndex(v => v.videoId === parseInt(id));
        setCurrentVideoIndex(index !== -1 ? index : 0);
      });
  }, [id]);

  const handleProgressUpdate = (videoId, currentTime) => {
    setVideoProgress(prevState => ({
      ...prevState,
      [videoId]: currentTime,
    }));

    // Enviar atualização de progresso para o backend
    fetch(`/api/videoprogress/${videoId}`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ currentTime }),
    });
  };

  const handleVideoSelect = index => {
    setCurrentVideoIndex(index);
  };

  const handleNextVideo = () => {
    if (currentVideoIndex < videos.length - 1) {
      setCurrentVideoIndex(currentVideoIndex + 1);
    }
  };

  const handlePreviousVideo = () => {
    if (currentVideoIndex > 0) {
      setCurrentVideoIndex(currentVideoIndex - 1);
    }
  };

  const handleCheckboxChange = videoId => {
    // Alternar o status de conclusão do vídeo
    // ... (mesmo que anteriormente)
  };

  if (videos.length === 0) {
    return <div>Carregando...</div>;
  }

  return (
    <div style={{ display: 'flex' }}>
      <div style={{ flex: 3 }}>
        <VideoPlayer
          video={videos[currentVideoIndex]}
          onProgressUpdate={handleProgressUpdate}
          onNextVideo={handleNextVideo}
          onPreviousVideo={handlePreviousVideo}
        />
      </div>
      <div style={{ flex: 1, overflowY: 'scroll', maxHeight: '100vh' }}>
        <VideoList
          videos={videos}
          currentVideoIndex={currentVideoIndex}
          onVideoSelect={handleVideoSelect}
          videoProgress={videoProgress}
          onCheckboxChange={handleCheckboxChange}
        />
      </div>
    </div>
  );
}

export default VideoPlayerPage;
