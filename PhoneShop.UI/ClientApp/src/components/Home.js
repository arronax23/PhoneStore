import React, { useState, useEffect } from 'react'
import useFetchGet from '../customHooks/useFetchGet';
import PhoneCard from './PhoneCard'
import Grid from '@material-ui/core/Grid'

function Home() {
    // const [phones, setPhones] = useState();
    const  {data : phones, isPending, error}  = useFetchGet('api/GetAllPhones')
    //const list = (<ol>{phones.map(phone => (<li>{phones.name}</li>))}</ol>);

      return (
        <div>
        <h2>Hello</h2>
        <h3>{console.log(phones)}</h3>
        {isPending && <div>Loading...</div>}
        {phones && <Grid container>{phones.map(phone => <PhoneCard key={phone.phoneId} phone={phone} />)}</Grid>}
        </div>
      );
}

export default Home
