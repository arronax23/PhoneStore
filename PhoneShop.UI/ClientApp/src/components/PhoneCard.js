import React from 'react'
import Card from '@material-ui/core/Card'
import CardContent from '@material-ui/core/CardContent'
import CardActions from '@material-ui/core/CardActions'
import Typography from '@material-ui/core/Typography'
import CardMedia from '@material-ui/core/CardMedia'
import Grid from '@material-ui/core/Grid'
import Button from '@material-ui/core/Button'
import {makeStyles} from '@material-ui/styles'

const useStyles = makeStyles({
    root: {
        width: 200,
        height: 150,
        textAlign: 'center'
    }
});
function PhoneCard({ phone }) {
    const classes = useStyles();
    return (
        <Grid item xs={3} className={classes.root}>
            <Card >
                <CardContent>
                    <Typography color="textSecondary">
                        {phone.brand} {phone.model}
                    </Typography>
                    <img className="phone-card-img" src={phone.imageUrl} />
                </CardContent>
                <Button>Show details</Button>
            </Card>
        </Grid>
    )
}

export default PhoneCard
