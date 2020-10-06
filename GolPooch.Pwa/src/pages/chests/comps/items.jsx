import React, { useState, useEffect } from 'react';
import { Skeleton } from '@material-ui/lab';
//import { Link } from 'react-router-dom';
//import strings from '../../../core/strings';
//import { commaThousondSeperator } from '../../../core/utils';
import { makeStyles, Link } from '@material-ui/core';
import fullBottomUpModalState from '../../../atom/state/fullBottomUpModalState';
import { useRecoilState } from 'recoil';
import Chest from './chest';

const useStyles = makeStyles({
    product: {
        marginTop: 7.5,
        marginBottom: 7.5,

        '& figure': {
            margin: 0,
            padding: 0,
            display: 'flex',
            flexDirection: 'column',
            position: 'relative',

            '& .chestItem': {
                borderRadius: 10,
                boxShadow: '0px 0px 5px 2px #d0d0d0',
                marginBottom: 15
            },
            '& figcaption': {
                position: 'absolute',
                display: 'flex',
                flexDirection: 'column',
                justifyContent: 'center',
                top: 0,
                bottom: 0,
                right: 10,
                left: 10,
                color: '#000',
                textShadow: '1px 1px #0005ad',
                '& label': {
                    padding: '5px 0'
                },
                '& .title': {
                    fontWeight: 800,
                    fontSize: '1.2rem',
                    textAlign: 'center',
                    padding: '15px 0',
                    textShadow: '1px 1px #3f51b5',
                    textShadow: '2px 2px red',
                },
                '& .price': {
                    fontWeight: 800,
                    fontSize: '1.3rem'
                }
            },

        }
    }
});

const Items = (props) => {
    //Hooks
    const classes = useStyles();
    const [modal, setModalState] = useRecoilState(fullBottomUpModalState);
    return (
        <>
            {props.inProgress ? [0, 1, 2].map((x, idx) => <div key={idx} className={classes.product}><Skeleton variant='rect' className='w-100 mb-15' height={150} /></div>) :
                props.items.map((item, idx) => <div key={idx} className={classes.product}>
                    <Link onClick={() => setModalState({
                        ...modal,
                        open: true,
                        title: item.title,
                        children: Chest,
                        props: { id: item.chestId }
                    })}>
                        <figure>
                            <img className='w-100 chestItem' src={item.imageUrl} />
                        </figure>
                    </Link>

                </div>)
            }
        </>
    );
}
export default Items;