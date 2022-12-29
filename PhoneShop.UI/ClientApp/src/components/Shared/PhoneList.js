import React, { useState } from 'react'
import useFetchGet from '../../customHooks/useFetchGet'
import Grid from '@material-ui/core/Grid'
import Pagination from '@material-ui/lab/Pagination';
import TextField from '@material-ui/core/TextField';
import PhoneCard from './PhoneCard'
import { makeStyles } from '@material-ui/styles'
import {useSelector} from 'react-redux'

const useStyles = makeStyles({
    root: {
        maxWidth: '80%',
        marginTop: 5,
        margin: 'auto'
    },
    pagination: {
        display: 'flex',
        justifyContent: 'center'
    },
    searchBar : {
        textAlign: 'center',
        marginTop: 5          
    }
});

function PhoneList() {
    const [pageNumber, setPageNumber] = useState(1);
    const {data : phones, isPending, error, httpResposne}  = useFetchGet('api/GetPhonesForOnePage?pageNumber='+pageNumber);
    const {data : pageCount, isPending: isPendingPagination}  = useFetchGet('api/GetNumberOfPagesInPhoneList');
    const [searchPhones, setSearchPhones] = useState();
    const token = useSelector(state => state.token);

    const classes = useStyles(); 

    const pageChange = (event,value) => {
        // console.log(value);
        // const pageClicked = parseInt(e.target.textContent)
        const pageClicked = value;
        console.log(pageClicked);
        setPageNumber(pageClicked);
    };

    const searchForPhones = (e) => {
        const searchText = e.target.value;
        console.log(searchText);
        if (searchText === ""){
            setSearchPhones(null);
        }else {
            fetch('api/SearchPhones?searchText='+searchText, {
                headers: {
                    "Authorization": "bearer "+token
                  }
            })
            .then(response => response.json())
            .then(phones =>  {
                console.log(phones);
                setSearchPhones(phones)
            });
        }
    };

    return (
        <div className="phone-list-main">
            <div className={classes.searchBar}>
            {phones && <TextField label="Search phones" variant="outlined" onChange={searchForPhones} />}
            </div>
            <Grid 
                container
                direction="row"
                justifyContent="center"
                wrap="wrap"
                className={classes.root}
            >
                {isPending && <div>Loading...</div>}
                {error && httpResposne && (<div>Error: {error} Http Status: {httpResposne}</div>)}

                {phones && !searchPhones &&
                phones.map(phone => <PhoneCard key={phone.phoneId} phone={phone} />)}
                {searchPhones &&
                searchPhones.map(phone => <PhoneCard key={phone.phoneId} phone={phone} />)}
            </Grid>
            <div className="pagination-container">
                {isPendingPagination && <div>Loading...</div>}
                {pageCount && <Pagination onChange={pageChange} className={classes.pagination} count={pageCount} color="primary" />}
            </div>
        </div>     
    )
}

export default PhoneList
