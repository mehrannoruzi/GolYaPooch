import React from 'react';
import { makeStyles, Container } from '@material-ui/core';
import { useHistory, Link } from 'react-router-dom';
import { FiChevronLeft } from 'react-icons/fi';

const useStyles = makeStyles({
    root: {
        '& .row': {
            paddingTop: 10,
            paddingBottom: 10,
            display: 'flex',
            justifyContent: 'space-between',
            flexDirection: 'row',
            alignItems: 'center',
            '& svg': {
                fontSize: 20
            }
        }
    }
});


export default function (props) {
    //Hooks
    const classes = useStyles();
    //Recoil
    const { item } = props;

    return (<Link className={classes.root} to={`/bl/ticket?add=false&text=${item.text}&answer=${item.answer||'تست پاسخ'}`}>
        <Container className='row'>
            <span className='text'>{item.text}</span>
            <FiChevronLeft className='arrow-left' />
        </Container>
    </Link>);
}