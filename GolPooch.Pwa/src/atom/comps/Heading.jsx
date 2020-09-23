import React from 'react';
import { makeStyles, Typography } from '@material-ui/core';

const useStyles = makeStyles({
    heading: {
        fontWeight: 800,
        margin:'15px 0'
    },
});


const Heading = (props) => {
    const classes = useStyles();
    return (<Typography component="h4" className={classes.heading}>
        <span>-</span>{props.children}
    </Typography>);
}
export default Heading;