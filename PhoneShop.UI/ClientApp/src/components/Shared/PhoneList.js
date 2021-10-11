import React, { useState } from 'react'
import useFetchGet from '../../customHooks/useFetchGet'
import Grid from '@material-ui/core/Grid'
import Pagination from '@material-ui/lab/Pagination';
import PhoneCard from './PhoneCard'
import { makeStyles } from '@material-ui/styles'

const useStyles = makeStyles({
    root: {
        width: '80%',
        marginTop: '5vh',
        margin: 'auto'
    },
    pagination: {
        display: 'flex',
        justifyContent: 'center'
    },
    paginationContainer : {
        position: 'absolute',
        top: '93%',
        width: '100%',
        textAlign: 'center'
    }
});

function PhoneList() {
    const [pageNumber, setPageNumber] = useState(1);
    const {data : phones, isPending, error, httpResposne}  = useFetchGet('api/GetPhonesForOnePage?pageNumber='+pageNumber);
    const {data : pageCount, isPending: isPendingPagination}  = useFetchGet('api/GetNumberOfPagesInPhoneList');
    const classes = useStyles(); 

    const pageChange = (e) => {
        const pageClicked = parseInt(e.target.textContent)
        console.log(pageClicked);
        setPageNumber(pageClicked);
    };

    return (
        <div>
            <Grid 
                container
                direction="row"
                justifyContent="center"
                wrap="wrap"
                className={classes.root}
            >
                {isPending && <div>Loading...</div>}
                {error && httpResposne && (<div>Error: {error} Http Status: {httpResposne}</div>)}
                {phones && 
                phones.map(phone => <PhoneCard key={phone.phoneId} phone={phone} />)}
            </Grid>
            <div className={classes.paginationContainer}>
                {isPendingPagination && <div>Loading...</div>}
                {pageCount && <Pagination onChange={pageChange} className={classes.pagination} count={pageCount} color="primary" />}
            </div>
        </div>     
    )
}

export default PhoneList
