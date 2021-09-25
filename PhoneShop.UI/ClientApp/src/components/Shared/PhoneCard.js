import React from 'react'
import Card from '@material-ui/core/Card'
import CardContent from '@material-ui/core/CardContent'
import CardActions from '@material-ui/core/CardActions'
import Typography from '@material-ui/core/Typography'
import CardMedia from '@material-ui/core/CardMedia'
import Grid from '@material-ui/core/Grid'
import Button from '@material-ui/core/Button'
import {makeStyles} from '@material-ui/styles'
import { useHistory } from 'react-router'

const useStyles = makeStyles({
    root: {
        // width: 100,
        // height: 150,
        textAlign: 'center'
    }
});
function PhoneCard({ phone }) {
    const history = useHistory();
    const classes = useStyles();
    const handleClick = () => {
        history.push('/phonedetails/'+ phone.phoneId)
    }
    return (
        <Grid item xs={2} className={classes.root}>
            <Card >
                <CardContent>
                    <Typography color="textSecondary">
                        {phone.brand} {phone.model}
                    </Typography>
                    <img className="phone-card-img" src={phone.imageUrl} />
                </CardContent>
                <Button onClick={handleClick} color="primary">Show details</Button>
            </Card>
        </Grid>
    )
}

export default PhoneCard
