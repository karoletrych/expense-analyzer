import React from 'react';
import logo from './logo.svg';
import './App.css';

import Dropzone from 'react-dropzone'





function App() {
  return (
    <div className="App">
      <header className="App-header">
        <Dropzone onDrop={acceptedFiles => console.log(acceptedFiles)}>
        {({getRootProps, getInputProps}) => (
            <section>
            <div {...getRootProps()}>
                <input {...getInputProps()} />
                <p>Drag 'n' drop some files here, or click to select files</p>
            </div>
            </section>
        )}
        </Dropzone>
      </header>
      </div>
      
  );
}

export default App;
