import React from 'react';
import { Skeleton } from '@material-ui/lab';
import { Link } from 'react-router-dom';
import strings from '../../../core/strings';
import { commaThousondSeperator } from '../../../core/utils';
import { makeStyles, Paper } from '@material-ui/core';
import Button from '../../../atom/comps/Button';
import { BiCheck } from 'react-icons/bi';

const useStyles = makeStyles({
    product: {
        boxShadow: "0px 3px 10px 2px #bbbbbb",
        borderRadius: 15,
        marginTop: 5,
        marginBottom: 7.5,
        padding: 10,
        '& ul.props': {
            listStyle: 'none',
            '& li': {
                padding: '7.5px 0'
            },
            '& .name': {
                textShadow: '1px 1px #630000',
                fontWeight: 900
            },
            '& .price': {
                textAlign: 'center',
                fontSize: '1.5rem'
            },
            '& .btn': {
                textAlign: 'center',
            },
            '& .chk-icon': {
                fontSize: '20px',
                color: 'green',
                verticalAlign: 'middle'
            },
            '& .details': {
                paddingBottom: 0,
                fontSize: '0.8rem'
            }

        }
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
        <>
            {props.inProgress ? [0, 1, 2].map((x, idx) => <div key={idx} className={classes.product} >
                <Skeleton variant='rect' className='w-100 mb-15' height={110} /></div>) :
                props.items.map((item, idx) => <Paper key={idx} className={classes.product}>
                    <Link to={`/bl/product/${item.productOfferId}`}>
                        <ul className='props'>
                            <li className='name'>{item.product.text}</li>
                            <li className='price'>
                                {commaThousondSeperator(item.totalPrice)}{strings.moneyCurrency}
                            </li>
                            <li className='btn'><Button>خرید بسته</Button></li>
                            <li className='chance details'><BiCheck className='chk-icon' /> {item.chance} شانس برنده شدن </li>
                            {item.discount > 0 ? <>
                                <li className='discount details'><BiCheck className='chk-icon' /> {item.discount}% تخفیف </li>
                                <li className='profit details'><BiCheck className='chk-icon' />سود شما {commaThousondSeperator(item.profit)} {strings.moneyCurrency}</li>
                                <li className='raw-price details'><BiCheck className='chk-icon' />قیمت قبل از تخفیف {commaThousondSeperator(item.price)} {strings.moneyCurrency}</li>
                            </> : null}

                        </ul>
                    </Link>

                </Paper>)
            }
        </>
    );
}
export default Items;