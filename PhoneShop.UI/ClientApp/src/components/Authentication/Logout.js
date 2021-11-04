import React from 'react'
import { Typography } from '@material-ui/core'
import{ makeStyles } from '@material-ui/styles'
import { isClassExpression } from 'typescript';
import Button from '@material-ui/core/Button'
import { useHistory } from 'react-router';
import { useDispatch } from 'react-redux';
import { logout, forgetUsername, resetToken } from './../../actions/index'

const useStyles = makeStyles({
    logoutYes: {
        backgroundColor: '#52b202'
    },
    logoutNo: {
        backgroundColor: '#b2102f'
    }
});
function Logout() {
    const classes = useStyles();
    const history = useHistory();
    const dispatch = useDispatch()

    const noLogout = () => {
        history.push('/');
    }

    const yesLogout = () => {
        dispatch(logout());
        dispatch(forgetUsername());
        dispatch(resetToken());
        history.push('/');
    }

    return (
        <div>
            <div className="logout">
                <Typography  component="span" variant="h6" color="textSecondary">Are you sure that you want to&nbsp;
                <Typography component="span" variant="h6" color="secondary">log out?</Typography></Typography>
            </div>
            <div className="logout">
                <Button className={classes.logoutYes} variant="contained" onClick={yesLogout}>Yes</Button>
                <Button className={classes.logoutNo} variant="contained" onClick={noLogout}>No</Button>
            </div>
        </div>
    )
}

export default Logout
