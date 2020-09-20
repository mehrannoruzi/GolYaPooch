import React, { useState, useEffect } from 'react';
import Banners from '../../atom/comps/Banners';
import { Skeleton } from '@material-ui/lab';
import { Link } from 'react-router-dom';
import { Button, makeStyles, Paper, Container, Grid, Box } from '@material-ui/core';
import strings from '../../core/strings';
import productSrv from '../../services/productSrv';
import { commaThousondSeperator } from '../../core/utils';
const useStyles = makeStyles({
    storePage: {
        paddingTop: 7.5,
        paddingBottom: 7.5
    },
    col2: {
        width: '50%',
        display: 'inline-block',
        verticalAlign: 'top',
        boxSizing: 'border-box'
    },
    products: {
        '& .l-col': {
            paddingLeft: '7.5px'
        },
        '& .r-col': {
            paddingRight: '7.5px'
        },
        '& .product': {
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
                    top: 15,
                    right: 10,
                    left: 10,
                    color: '#fff',
                    textShadow: '1px 1px #3f51b5',
                    '& label': {
                        padding: '5px 0'
                    },
                    '& .name': {
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
        },
    },
    rawPrice: {
        position: 'relative',
        marginRight: 10,
        '&:after': {
            content: "''",
            backgroundColor: 'red',
            position: 'absolute',
            height: '1px',
            left: 0,
            right: 0,
            top: '50%',
            transform: 'translateY(-50%)'
        }
    }




});

const Items = (props) => {
    const classes = useStyles();
    return (
        <React.Fragment>
            {props.items.map((item, idx) => <div key={idx} className='product'>
                <Link to={`/nl/product/${item.productOfferId}`}>
                    <figure>
                        <img className='w-100' src={item.imageUrl} />
                        <figcaption>
                            <label className='name'>{item.product.text}</label>
                            <label className='chance'>{item.chance} شانس برنده شدن</label>
                            {item.discount > 0 ? <label className='discount'>{item.discount}% تخفیف</label> : null}
                            <label className='price'>
                                {item.discount > 0 ? <span className={classes.rawPrice}>{commaThousondSeperator(item.price)}</span> : null}
                                {commaThousondSeperator(item.totalPrice)}{strings.moneyCurrency}</label>
                        </figcaption>
                    </figure>
                </Link>

            </div>)}
        </React.Fragment>
    );
}

const Store = () => {
    const classes = useStyles();
    const [inProgress, setInProgress] = useState(true);
    const [items1, setItems1] = useState([]);
    const [items2, setItems2] = useState([]);
    const [query, setQuery] = useState('');

    useEffect(() => {
        const getDate = async () => {
            setInProgress(true);
            let get = await productSrv.get();
            setInProgress(false);
            if (get.isSuccessful) {
                let tempItems1 = [], tempItems2 = [];
                for (let i = 0; i < get.result.length; i++) {
                    if (i % 2 === 0) tempItems1.push(get.result[i]);
                    else tempItems2.push(get.result[i]);
                }
                setItems1(tempItems1);
                setItems2(tempItems2);
            }
        }
        getDate();
    }, [query]);
    
    return (
        <div id='page-store' className={classes.storePage}>
            <Banners pageName="Store" location="top" />
            <Container className={classes.products}>
                <div className={`r-col ${classes.col2}`}>
                    {inProgress ? [0, 1, 2].map((x, idx) => <div key={idx} className='product'><Skeleton variant=' rect' className='w-100 mb-15' height={100} /></div>) :
                        <Items items={items1} />}
                </div>
                <div className={`l-col ${classes.col2}`}>
                    {inProgress ? [0, 1, 2].map((x, idx) => <div className='product'><Skeleton variant=' rect' className='w - 100 mb - 15' height={100} /></div>) :
                        <Items items={items2} />}
                </div>
            </Container>
            <Banners pageName="Store" location="bottom" />

        </div>
    );
};

export default Store;