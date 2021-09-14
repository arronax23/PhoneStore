import React, { Component, useEffect } from 'react';

import './custom.css'

 const App = () => {
  fetch('api/GetAllPhones')
  .then(resp => {
    console.log(resp);
    return resp.json();
  })
  .then(data => console.log(data))
  .catch(err => console.log(err));
    return (
      <h2>Hello</h2>
    );

}
export default App