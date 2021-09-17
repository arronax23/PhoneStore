import React from 'react'
import useFetchGet from './customHooks/useFetchGet';
import PhoneCard from './components/PhoneCard'
import Grid from '@material-ui/core/Grid'
import Navbar from './components/Navbar';
import Footer from './components/Footer';
import './custom.css'
import { Switch, Route } from 'react-router-dom'
import PhoneList from './components/PhoneList'
import Home from './components/Home'
import PhoneDetails from './components/PhoneDetails';
import AddPhone from './components/AddPhone';

function App() {

      return (      
        <div>
        <Navbar />
        <Switch>
            <Route exact path="/">
                <Home />
            </Route>
            <Route exact path="/phonelist">
                <PhoneList />
            </Route>
            <Route exact path="/phonedetails/:id">
                <PhoneDetails />
            </Route>
            <Route exact path="/addphone">
                <AddPhone />
            </Route>
        </Switch>
        <Footer />
        </div>
      );
}

export default App