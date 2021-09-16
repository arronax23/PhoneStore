import React, { Component, useEffect } from 'react';
import { Route, Switch } from 'react-router-dom';
import Home from './components/Home';

import './custom.css'

 const App = () => {
return(
    <Route exact path="/" component={Home}/>
)

}
export default App