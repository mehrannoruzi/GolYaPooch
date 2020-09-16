import React, { useState, useEffect } from 'react';
import Carousel from 'react-material-ui-carousel';
import { Button, makeStyles, Paper } from '@material-ui/core';
import { Skeleton } from '@material-ui/lab';
import bannerService from '../../services/bannerSrv';

const useStyles = makeStyles({
    btnWithIcon: {
        dispaly: 'flex',
    }
});

const TextBanner = (props) => {
    const [visile, setVisibility] = useState(true);
    const { item } = props;
    switch (item.actionType) {
        default:
            return (<div className={`comp-text-banner ${visile ? null : 'd-none'}`}>
                <Button onClick={setVisibility(false)} className="txt">{item.text}</Button>
            </div>);
    }

}

const Banners = (props) => {
    const classes = useStyles();
    const [items, setItems] = useState([]);
    const { pageName, location } = props;
    useEffect(() => {
        let result = [];
        let get = bannerService.get(pageName, location);
        if(get.isSuccessful){
            for (let i = 0; i < get.result.length; i++) {
                switch (get.result[i].type) {
                    default:
                        result.push(<TextBanner item={get.result[i]} />);
                }
            }
            setItems(result);
        }

    },[items]);
    return (
        <Carousel>
            {
                items.length === 0 ? <Skeleton vcariant='rect' height={100} className='w-100 mb-15' /> :
                    items.map((Item, i) => <Item />)
            }
        </Carousel>
    )
}
export default Banners;