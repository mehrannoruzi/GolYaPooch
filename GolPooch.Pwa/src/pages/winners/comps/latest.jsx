import React, { useState, useEffect } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import { useRecoilState } from 'recoil';
import { Skeleton } from '@material-ui/lab';
import { Grid } from '@material-ui/core';
import toastState from '../../../atom/state/toastState';
import EmptyRecord from '../../../atom/comps/EmptyRecord';
import strings from '../../../core/strings';
import Item from './item';
import winnerSrv from './../../../services/winnerSrv';
import TableHeader from '../comps/header';
const useStyles = makeStyles(() => ({
    root: {
        '& .t-body': {
            overflowY: 'auto',
            maxHeight: 'calc(100vh - 234px)'
        }
    },

}));


const Latest = () => {
    //Hooks
    const classes = useStyles();
    const [isBottom, setIsBottom] = useState(true);
    const [items, setItems] = useState([]);
    const [inProgress, setInProgress] = useState(true);
    const [pageNumber, setPageNumber] = useState(1);

    //recoil
    const [toast, setToastState] = useRecoilState(toastState);

    function _handleScroll(e) {
        let element = e.target
        if (!inProgress && element.scrollHeight - element.scrollTop === element.clientHeight) {
            setIsBottom(true);
        }
    }

    useEffect(() => {
        if (isBottom && (items.length === 0 || items.length > 10)) {
            const getDate = async () => {
                setInProgress(true);
                let get = await winnerSrv.getLatest(12, pageNumber);

                if (get.isSuccessful) {
                    setItems([...items, ...get.result]);
                    if (get.result.length > 0)
                        setPageNumber(pageNumber + 1);
                }
                else setToastState({ ...toast, open: true, severity: 'error', message: get.message });
                //setInProgress(false);
                setIsBottom(false);
            }
            getDate();
        }

    }, [isBottom]);

    return (<Grid container className={classes.root}>
        <TableHeader />
        <Grid xs={12} item className='t-body' onScroll={_handleScroll}>
            {!inProgress && items.length === 0 ? <EmptyRecord text={strings.thereIsNoWinner} /> : null}
            {items.map((item, idx) => <Item key={idx} item={item} />)}
            {(inProgress && pageNumber === 1) ? [0, 1, 2].map((x, idx) => <Skeleton key={idx} variant='rect' className='w-100 mb-15' height={45} />) : null}
        </Grid>
    </Grid>);
}
export default Latest;