import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import Carousel from 'react-material-ui-carousel';
import { Contaner, Button, makeStyles, Paper, Container } from '@material-ui/core';
import { Skeleton } from '@material-ui/lab';
import bannerService from '../../services/bannerSrv';

const actions = {
    close: 1,
    callApi: 2,
    openLink: 3,
    openApp: 3,
    openPage: 4
}

const useStyles = makeStyles({
    textBanner: {
        textAlign: 'center',
        margin: '15px 0'
    },
    skeleton: {
    }
});

const TextBanner = (props) => {
    const classes = useStyles();
    const [visile, setVisibility] = useState(true);
    const { item } = props;
    switch (item.actionType) {
        case actions.openLink:
            return (<Paper className={`comp-text-banner ${visile ? null : 'd-none'} ${classes.textBanner}`}>
                <a href={item.href} className="txt">{item.text}</a>
            </Paper>);
        case actions.openApp:
        case actions.openPage:
            return (<Paper className={`comp-text-banner ${visile ? null : 'd-none'} ${classes.textBanner}`}>
                <Link to={item.href} className="txt">{item.text}</Link>
            </Paper>);
        default:
            return (<Paper className={`comp-text-banner ${visile ? null : 'd-none'} ${classes.textBanner}`}>
                <Button onClick={() => setVisibility(false)} className="txt">{item.text}</Button>
            </Paper>);
    }

}

const Banners = (props) => {
    const classes = useStyles();
    const [inProgress, setInProgress] = useState(true);
    const [items, setItems] = useState([]);
    const [query, setQuery] = useState('');
    const { pageName, location } = props;


    useEffect(() => {
        const getDate = async () => {
            setInProgress(true);
            let get = await bannerService.get(pageName, location);
            setInProgress(false);
            console.log(get);
            if (get.isSuccessful) {
                setItems(get.result);
            }
        }
        getDate();
    }, [query]);
    if (!inProgress && items.length === 0) return null;
    return (
        <Container>
            <Carousel navButtonsAlwaysInvisible={items.length < 2} indicators={false}>
                {
                    inProgress ? <Skeleton vcariant='rect' height={50} className={`w-100 mb-15 ${classes.skeleton}`} /> :
                        items.map((item, i) => {
                            switch (item.type) {
                                default:
                                    return <TextBanner item={item} key={i} />;
                            }
                        })
                }
            </Carousel>
        </Container>

    )
}
export default Banners;