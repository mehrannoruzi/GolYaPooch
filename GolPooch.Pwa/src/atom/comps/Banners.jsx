import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import Slider from "react-slick";
import { Contaner, Button, makeStyles, Paper, Container } from '@material-ui/core';
import { Skeleton } from '@material-ui/lab';
import bannerService from '../../services/bannerSrv';
import { AiOutlineGift } from 'react-icons/ai';
import { MdKeyboardArrowLeft } from 'react-icons/md';

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
        margin: '7.5px 0',
        borderRadius: 5,
        minHeight: 30,
        paddingLeft: 5,
        paddingRight: 5
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
            return (
                <Paper className={`Ctr comp-text-banner ${visile ? null : 'd-none'} ${classes.textBanner}`}>
                    <a href={item.href} className="txt">{item.text}</a>
                </Paper>);
        case actions.openApp:
        case actions.openPage:
            return (
                <Paper className={`Ctr comp-text-banner ${visile ? null : 'd-none'} ${classes.textBanner}`}>
                    <Link to={item.href} className="txt">{item.text}</Link>
                </Paper>);
        default:
            return (
                <Paper className={`Ctr comp-text-banner ${visile ? null : 'd-none'} ${classes.textBanner}`} style={{ backgroundColor: `${item.backColor}` }}>
                    <Button onClick={() => setVisibility(false)} className="txt w-100" style={{ color: `${item.fontColor}` }} >
                        <AiOutlineGift className="icon" />
                        <span className="text">{item.text}</span>
                        <MdKeyboardArrowLeft className="arrow"/>
                    </Button>
                </Paper>);
    }

}

const Banners = (props) => {
    const classes = useStyles();
    const [inProgress, setInProgress] = useState(true);
    const [items, setItems] = useState([]);
    const [query, setQuery] = useState('');
    const { pageName, location } = props;

    const getDate = async () => {
        setInProgress(true);
        let get = await bannerService.get(pageName, location);
        setInProgress(false);
        if (get.isSuccessful) {
            setItems(get.result);
        }
    }
    useEffect(() => {
        getDate();
    }, [query]);
    const settings = {
        dots: true,
        infinite: false,
        speed: 500,
        slidesToShow: 1,
    };
    if (!inProgress && items.length === 0) return null;
    return (
        <Container className="Banner">
            <Slider {...settings}>
                {
                    inProgress ? <Skeleton vcariant='rect' height={50} className={`w-100 mb-15 ${classes.skeleton}`} /> :
                        items.map((item, i) => {
                            switch (item.type) {
                                default:
                                    return <TextBanner item={item} key={i} />;
                            }
                        })
                }
            </Slider>
        </Container>

    )
}
export default Banners;