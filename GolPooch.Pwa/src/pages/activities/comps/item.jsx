import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import { Grid, Paper } from '@material-ui/core';
import { commaThousondSeperator } from '../../../core/utils';
import strings from '../../../core/strings';

const useStyles = makeStyles(() => ({
    root: {
        padding: 10,
        boxSizing: 'border-box',
        '& .wrapper': {
            boxShadow: "0px 1px 8px 0px #d2d2d2",
            position: 'relative',
            display: 'flex',
            flexDirection: 'column',
            '& .hx': {
                padding: '7.5px 5px',
                margin: '0 0 5px 0',
                fontSize: '11px',
                textAlign: 'center',
                borderBottom: '1px solid #ccc',
                borderRadius: '5px',
                borderBottomRightRradius: 0,
                borderBottomLeftRadius: 0,

            },
            '& label': {
                padding: 5,
                '& .chk-icon': {
                    fontSize: '20px',
                    color: 'green',
                    verticalAlign: 'middle'
                }
            }
        }
    }
}));


const Item = (props) => {
    //Hooks
    const classes = useStyles();
    const { item } = props;
    return (
        <Grid item xs={6} sm={6} lg={4} className={classes.root}>
            <Paper className='wrapper'>
                <h4 className='hx'>{item.productOffer.product.text}</h4>
                <label className='exp-date'>تاریخ: {item.expireDateSh}</label>
                <label className='price'>قیمت: {commaThousondSeperator(item.productOffer.totalPrice)}{strings.moneyCurrency}</label>
                <label className='remained'>شانس باقیمانده: {(item.chance - item.usedChance)}</label>
                <label className='total'>تعداد کل شانس: {item.chance}</label>
            </Paper>
        </Grid>
    );
};

export default Item;