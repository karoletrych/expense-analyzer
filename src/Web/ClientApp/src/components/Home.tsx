import * as React from "react";
import { connect } from "react-redux";
import Dropzone from "react-dropzone";

async function postData(url : string, data : Object) {
    const response = await fetch(url, {
      method: 'POST', 
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(data) 
    });
    return response.json(); 
}

async function postFile(url: string, data: File) {
    let formData = new FormData();
    formData.append("file", data);
    const response = await fetch(url, {
      method: 'POST', 
      body: formData
    });
    return response.json(); 
  }

const Home = () => (
    <div>
        <Dropzone onDrop={ (acceptedFiles) => {
            let response = postFile("https://localhost:5001/api/import", acceptedFiles[0]);
        }
        }>
            {({ getRootProps, getInputProps }) => (
                <section>
                    <div {...getRootProps()}>
                        <input {...getInputProps()} />
                        <p>
                            Drag 'n' drop some files here, or click to select
                            files
                        </p>
                    </div>
                </section>
            )}
        </Dropzone>
    </div>
);

export default connect()(Home);
