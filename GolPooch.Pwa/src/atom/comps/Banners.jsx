import React, { useState, useEffect } from 'react';
import { useHistory } from 'react-router-dom';
import Slider from "react-slick";
import { Button, makeStyles, Paper, Container } from '@material-ui/core';
import { Skeleton } from '@material-ui/lab';
import bannerService from '../../services/bannerSrv';
import { AiOutlineGift } from 'react-icons/ai';
import { MdKeyboardArrowLeft } from 'react-icons/md';
import bannerSrv from '../../services/bannerSrv';

const actions = {
    close: 1,
    callApi: 2,
    openLink: 3,
    openApp: 4,
    openPage: 5
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
    const history = useHistory();
    const [visile, setVisibility] = useState(true);
    const { item } = props;
    const _handleClick = (item) => {
        bannerSrv.handleClick(item);
        switch (item.actionType) {
            case actions.openLink:
                history.push(item.href);
                break;
            case actions.openApp:
            case actions.openPage:
                window.location.href = item.href;
                break;
            default:
                setVisibility(false)
                break;
        }
        history.push(props.item.href);
    }
    switch (item.actionType) {
        case actions.openLink:
            return (
                <Paper className={`Ctr comp-text-banner ${visile ? null : 'd-none'} ${classes.textBanner}`} style={{ backgroundColor: `${item.backColor}` }}>
                    <Button onClick={() => _handleClick(item)} className="txt w-100" style={{ color: `${item.fontColor}` }} >
                        <AiOutlineGift className="icon" />
                        <span className="text">{item.text}</span>
                        <MdKeyboardArrowLeft className="arrow" />
                    </Button>
                </Paper>);
        case actions.openApp:
        case actions.openPage:
            return (
                <Paper className={`Ctr comp-text-banner ${visile ? null : 'd-none'} ${classes.textBanner}`}>
                    <a onClick={() => _handleClick(item)} className="txt">{item.text}</a>
                </Paper>);
        default:
            return (
                <Paper className={`Ctr comp-text-banner ${visile ? null : 'd-none'} ${classes.textBanner}`} style={{ backgroundColor: `${item.backColor}` }}>
                    <Button onClick={() => _handleClick(item)} className="txt w-100" style={{ color: `${item.fontColor}` }} >
                        <AiOutlineGift className="icon" />
                        <span className="text">{item.text}</span>
                        <MdKeyboardArrowLeft className="arrow" />
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