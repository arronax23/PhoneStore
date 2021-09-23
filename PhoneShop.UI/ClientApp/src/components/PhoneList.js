import React from 'react'
import useFetchGet from '../customHooks/useFetchGet'
import Grid from '@material-ui/core/Grid'
import PhoneCard from './PhoneCard'

function PhoneList() {
    const  {data : phones, isPending, error, httpResposne}  = useFetchGet('api/GetAllPhones')
    
    return (
       <Grid 
            container
            direction="row"
            justifyContent="center"
            wrap="wrap"
        >
            {isPending && <div>Loading...</div>}
            {error && httpResposne && (<div>Error: {error} Http Status: {httpResposne}</div>)}
            {phones && 
            phones.map(phone => <PhoneCard key={phone.phoneId} phone={phone} />)}
        </Grid>     
    )
}

export default PhoneList
