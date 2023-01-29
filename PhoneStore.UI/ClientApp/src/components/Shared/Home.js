import React from 'react'
import Typography from '@material-ui/core/Typography'
import{ makeStyles } from '@material-ui/styles'

const useStyles = makeStyles({
    root: {
        textAlign: 'center'
    }
});

function Home() {
    const classes = useStyles()
    return (
        <div>
            <Typography className={classes.root} variant="h6" color="textSecondary">
                Welcome! This is the PhoneStore application.
            </Typography>
        </div>
    )
}

export default Home
