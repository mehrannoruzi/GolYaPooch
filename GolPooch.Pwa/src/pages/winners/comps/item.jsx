import React from 'react';
import { makeStyles, Grid } from '@material-ui/core';

const useStyles = makeStyles({
    root: {
        '& > div': {
            padding: '5px 0'
        }
    },
    ltr: {
        direction: 'rtl'
    }
});


export default function (props) {
    //Hooks
    const classes = useStyles();
    //Recoil
    const { item } = props;

    return (<Grid container className={classes.root}>
        <Grid item xs={1}>{item.number}</Grid>
        <Grid item xs={4} className={classes.ltr}>{item.mobileNumber}</Grid>
        <Grid item xs={4} className={classes.ltr}>{item.chanceCount}</Grid>
        <Grid item xs={3} className={classes.ltr}>{item.winCount}</Grid>
    </Grid>);
}