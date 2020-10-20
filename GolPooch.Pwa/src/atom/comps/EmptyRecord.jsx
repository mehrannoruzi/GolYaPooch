import React from 'react';
import { makeStyles, Container } from '@material-ui/core';
import { BiMessageSquareX } from 'react-icons/bi';

const useStyles = makeStyles({
    emptyRecordCompa: {
        color: 'rgb(102,60,0)',
        paddingTop: 30,
        display: 'flex',
        flexDirection: 'column',
        justifyContent: 'center',
        alignItems: 'center',
        //height: 'calc(100vh - 70px)',
        //paddingBottom: 30,
        '& svg': {
            fontSize: '60px',
            marginBottom: '15px'
        }
    }

});


const Heading = (props) => {
    const classes = useStyles();
    return (<Container className={classes.emptyRecordCompa}>
        {props.icon ? <props.icon /> : <BiMessageSquareX />}
        <span>{props.text}</span>
    </Container>);
}
export default Heading;