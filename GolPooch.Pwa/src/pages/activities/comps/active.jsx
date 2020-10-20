import React, { useState, useEffect } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import { useRecoilState } from 'recoil';
import { Skeleton } from '@material-ui/lab';
import { Grid } from '@material-ui/core';
import purchaseSrv from '../../../services/purchaseSrv';
import toastState from '../../../atom/state/toastState';
import EmptyRecord from '../../../atom/comps/EmptyRecord';
import strings from '../../../core/strings';
import Item from './item';

const useStyles = makeStyles(() => ({
    root: {
        backgroundColor: '#fff',
        paddingTop: 7.5,
        paddingBottom: 7.5,
        height: 'calc(100vh - 250px)',
        overflowY: 'auto',
        '& .MuiPaper-root': {
            boxShadow: "0px 1px 8px 0px #d2d2d2",
            borderRadius: 5,
        }
    },
    loaderChance: {
        padding: 10,
        with: 170
    }
}));


const Active = () => {
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
        if (isBottom && (items.length === 0 || items.length > 12)) {
            const getDate = async () => {

                setInProgress(true);
                let get = await purchaseSrv.getActive(12, pageNumber);
                console.log(get);
                if (get.isSuccessful) {
                    setItems([...items, ...get.result.items]);
                    if (get.result.items.length > 0)
                        setPageNumber(pageNumber + 1);
                }
                else setToastState({ ...toast, open: true, severity: 'error', message: get.message });
                setInProgress(false);
                setIsBottom(false);
            }
            getDate();
        }

    }, [isBottom]);

    return (
        <Grid container className={classes.root} onScroll={_handleScroll}>
            {!inProgress && items.length === 0 ? <EmptyRecord text={strings.thereIsNoActivePackage} /> : null}
            {items.map((item, idx) => <Item key={idx} item={item} />)}
            {(inProgress && pageNumber === 1) ? [0, 1, 2, 3].map((x, idx) => <Grid key={idx} item xs={6} className={classes.loaderChance}>
                <Skeleton variant='rect' className='w-100' height={164} />
            </Grid>) : null}
        </Grid>
    );
};

export default Active;