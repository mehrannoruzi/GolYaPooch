import React, { useState, useEffect } from 'react';
import { makeStyles, Container, Grid, Divider, Paper } from '@material-ui/core';
const useStyles = makeStyles({
    agreedComp: {

    }
});
export default function (props) {
    //Hooks
    const classes = useStyles();
    const [inProgress, setInProgress] = useState(true);
    const info = { props };
    return (<div className={classes.agreedComp}>
        <Container>
            <h4 className='heading'>
                میزان شانس شما در این قرعه کشی
                </h4>
        </Container>
        <h5 className='chance'>
            <span>{info.totalChance}</span>
        </h5>
    </div>)
}