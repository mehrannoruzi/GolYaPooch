import React, { useState, useEffect } from 'react';
import { Skeleton } from '@material-ui/lab';
import { makeStyles, List, ListItem, ListItemAvatar, ListItemText } from '@material-ui/core';
import { useRecoilState } from 'recoil';
import notificationSrv from '../../services/notificationSrv';
import toastState from '../../atom/state/toastState';
import Item from './comps/item';

const useStyles = makeStyles({
    notificationComp: {
        paddingTop: 7.5,
        paddingBottom: 7.5,
        '& .MuiDivider-inset': {
            marginRight: 16
        }
    },
    inline: {
        display: 'inline',
    }
});

const Notifications = () => {
    //Hooks
    const classes = useStyles();
    const [inProgress, setInProgress] = useState(true);
    const [items, setItems] = useState([]);
    const [pageNumber, setPageNumber] = useState(1);
    const [isBottom, setIsBottom] = useState(true);
    //recoil
    const [toast, setToastState] = useRecoilState(toastState);


    function handleScroll() {
        const scrollTop = (document.documentElement
            && document.documentElement.scrollTop)
            || document.body.scrollTop;
        const scrollHeight = (document.documentElement
            && document.documentElement.scrollHeight)
            || document.body.scrollHeight;
        if (scrollTop + window.innerHeight + 50 >= scrollHeight) {
            setIsBottom(true);
        }
    }
    //infinit scroll
    useEffect(() => {
        window.addEventListener('scroll', handleScroll);
        return () => window.removeEventListener('scroll', handleScroll);
    }, []);

    useEffect(() => {
        if (isBottom) {
            const getDate = async () => {
                setInProgress(true);
                let get = await notificationSrv.get(10, pageNumber);
                console.log(get);
                if (get.isSuccessful) {
                    setItems(get.result.items);
                    if (get.result.items.hasNext)
                        setPageNumber(++pageNumber);
                }
                else setToastState({ ...toast, open: true, severity: 'error', message: get.message });
                setInProgress(false);
                setIsBottom(false);
            }
            getDate();
        }

    }, [isBottom]);

    return (
        <List id='comp-notifications' className={classes.notificationComp}>
            {inProgress ? [0, 1, 2].map((x, idx) => <ListItem key={idx}>
                <ListItemAvatar>
                    <Skeleton variant='circle' height={35} width={35} />
                </ListItemAvatar>
                <ListItemText
                    primary={<Skeleton />}
                    secondary={<Skeleton />} />
            </ListItem>) : items.map((item, idx) => <Item key={idx} item={item} />)}
        </List>

    );
};

export default Notifications;