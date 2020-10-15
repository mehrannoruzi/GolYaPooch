import React from 'react';
import { makeStyles, Link, Paper, Button, Grid } from '@material-ui/core';
import fullBottomUpModalState from '../../../atom/state/fullBottomUpModalState';
import { useRecoilState } from 'recoil';
import Chest from './chest';
import strings from '../../../core/strings';

const useStyles = makeStyles({
    chest: {
        '& .wrapper': {
            marginBottom: 15,
            borderRadius: 15,
            '& figure': {
                margin: 0,
                padding: 0,
                display: 'flex',
                flexDirection: 'column',
                position: 'relative',
                //borderRadius: 10,
                '& .img-main': {
                    width: '100%',
                    borderTopRightRadius: 15,
                    borderTopLeftRadius: 15,
                },
                '& figcaption': {
                    display: 'flex',
                    justifyContent: 'space-between',
                    alignItems: 'center',
                    padding: '7.5px 10px',
                    '& button': {
                        padding: '5px 10px',
                        borderRadius: 20,
                        border: 'solid 1px #ccc',
                        backgroundColor: '#fff',
                        color: '#666',
                        backgroundColor: '#8BC34A',
                        color: '#fff'
                    }
                },

            }
        }
    }
});

const Items = (props) => {
    //Hooks
    const classes = useStyles();
    const [modal, setModalState] = useRecoilState(fullBottomUpModalState);
    const { item } = props;
    return (<Grid item xs={12} sm={6} md={4} className={classes.chest}>
        <Link
            onClick={() => setModalState({
                ...modal,
                open: true,
                title: item.title,
                children: Chest,
                props: { id: item.chestId }
            })}>
            <Paper className='wrapper'>
                <figure>
                    <img className='img-main' src={item.imageUrl} />
                    <figcaption>
                        <span>{item.title}</span>
                        <Button>{strings.joinInLottery}</Button>
                    </figcaption>
                </figure>
            </Paper>
        </Link>
    </Grid>);
}
export default Items;