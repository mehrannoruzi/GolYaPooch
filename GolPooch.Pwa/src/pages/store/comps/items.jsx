import React from 'react';
import { Skeleton } from '@material-ui/lab';
import { Link } from 'react-router-dom';
import strings from '../../../core/strings';
import { commaThousondSeperator } from '../../../core/utils';
import { makeStyles} from '@material-ui/core';

const useStyles = makeStyles({
    product: {
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
            { props.inProgress ? [0, 1, 2].map((x, idx) => <div key={idx} className={classes.product}><Skeleton variant='rect' className='w-100 mb-15' height={300} /></div>) :
                props.items.map((item, idx) => <div key={idx} className={classes.product}>
                    <Link to={`/bl/product/${item.productOfferId}`}>
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

                </div>)
            }
        </>
    );
}
export default Items;