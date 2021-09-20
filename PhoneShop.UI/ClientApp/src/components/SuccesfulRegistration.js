import React from 'react'
import Typography from '@material-ui/core/Typography'
import{ makeStyles } from '@material-ui/styles'
import { useParams } from 'react-router'
const useStyles = makeStyles({
    root: {
        textAlign: 'center',
        color : 'rgb(65, 209, 72)'
    }
});


function SuccesfulRegistration() {
    const classes = useStyles();
    const { user } = useParams();
    return (
        <div>
            <Typography className={classes.root} variant="h6" color="textSecondary">You have succesfully registered user: {user}</Typography>
        </div>
    )
}

export default SuccesfulRegistration
