// VideoList.js
import React from 'react';

function VideoList({ videos, currentVideoIndex, onVideoSelect, videoProgress, onCheckboxChange }) {
  return (
    <ul style={{ listStyleType: 'none', padding: 0 }}>
      {videos.map((video, index) => {
        const isCompleted = videoProgress[video.videoId] >= video.duration;
        return (
          <li
            key={video.videoId}
            style={{
              backgroundColor: index === currentVideoIndex ? '#f0f0f0' : '#fff',
              padding: '10px',
              cursor: 'pointer',
              borderBottom: '1px solid #ccc',
            }}
          >
            <div style={{ display: 'flex', alignItems: 'center' }}>
              <input
                type="checkbox"
                checked={isCompleted}
                onChange={() => onCheckboxChange(video.videoId)}
              />
              <span onClick={() => onVideoSelect(index)} style={{ marginLeft: '10px' }}>
                {video.title}
              </span>
            </div>
          </li>
        );
      })}
    </ul>
  );
}

export default VideoList;
