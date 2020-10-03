import React, { useState, useEffect } from 'react';
import { Skeleton } from '@material-ui/lab';
//import { Link } from 'react-router-dom';
//import strings from '../../../core/strings';
//import { commaThousondSeperator } from '../../../core/utils';
import { makeStyles, Link } from '@material-ui/core';
import fullBottomUpModalState from '../../../atom/state/fullBottomUpModalState';
import Chest from '../../chest';
import { useRecoilState } from 'recoil';

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
            { props.inProgress ? [0, 1, 2].map((x, idx) => <div key={idx} className={classes.product}><Skeleton variant='rect' className='w-100 mb-15' height={300} /></div>) :
                props.items.map((item, idx) => <div key={idx} className={classes.product}>
                    <Link onClick={() => setModalState({
                        ...modal,
                        open: true,
                        title: item.title,
                        children: Chest,
                        props: { id: item.chestId }
                    })}>
                        <figure>
                            <img className='w-100' src={item.imageUrl} />
                            <figcaption>
                                <label className='name'>{item.title}</label>
                                <label className='winners'>{item.winnerCount} برنده</label>
                                <label className='participants'>{item.participantCount} شرکت کننده تا الان</label>
                            </figcaption>
                        </figure>
                    </Link>

                </div>)
            }
        </>
    );
}
export default Items;