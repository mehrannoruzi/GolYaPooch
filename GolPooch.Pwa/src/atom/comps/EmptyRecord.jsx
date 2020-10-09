import React from 'react';
import { makeStyles, Container } from '@material-ui/core';
import { AiOutlineFileSearch } from 'react-icons/ai';

const useStyles = makeStyles({
    emptyRecordCompa: {
        color: 'rgb(102,60,0)',
        paddingTop: 30,
        display: 'flex',
        flexDirection: 'column',
        justifyContent: 'center',
        alignItems: 'center',

        '& svg': {
            fontSize: '60px',
            marginBottom: '15px'
        }
    }

});


const Heading = (props) => {
    const classes = useStyles();
    return (<Container className={classes.emptyRecordCompa}>
        {props.icon ? <props.icon /> : <AiOutlineFileSearch />}
        <span>{props.text}</span>
    </Container>);
}
export default Heading;