import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import { Grid } from '@material-ui/core';

const useStyles = makeStyles(() => ({
    root: {
        padding: 10,
        boxSizing: 'border-box',

        '& figure': {
            margin: 0,
            width: '100%',
            boxSizing: 'border-box',
            border: 'solid 1px #ccc',
            padding: '12px',
            borderRadius: '3px',
            position: 'relative',
            '& svg': {
                top: '-8px',
                right: '-5px',
                color: 'green',
                fontSize: '20px',
                position: 'absolute',
                top: '-10px',
                left: '-10px',
                backgroundColor: '#fff'
            },
            '& .img-main': {
                width: '100%',
                maxHeight: '180px',
                marginBottom: 5
            },
            '& figcaption': {
                display: 'flex',
                flexDirection: 'column',
                '& label': {
                    padding: '5px 0',
                    fontSize: '1.1rem'
                }
            }
        }

    }
}));


const Item = (props) => {
    //Hooks
    const classes = useStyles();
    const {item} = props;
    return (
        <Grid item xs={6} sm={6} lg={4} className={classes.root}>
            <figure>
                <img className='img-main' src={item.productOffer.imageUrl} alt={item.productOffer.product.text} />
                <figcaption>
                    <label className='total'>تعداد کل شانس: {item.chance}</label>
                    <label className='remained'>شانس باقیمانده: {(item.chance - item.usedChance)}</label>
                </figcaption>
            </figure>
        </Grid>
    );
};

export default Item;