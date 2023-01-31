import React from "react";
import Typography from '@material-ui/core/Typography'
import{ makeStyles } from '@material-ui/styles'

const useStyles = makeStyles({
  root: {
      textAlign: 'center'
  }
});

function WrongRole({ path }) {
  const classes = useStyles()
  return (  
    <Typography className={classes.root} variant="h6" color="textSecondary">
      Your user role doesn't allow you to access page located in path: {path}.
    </Typography>
  )

}

export default WrongRole;
