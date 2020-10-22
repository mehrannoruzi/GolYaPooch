import React from 'react';
import { makeStyles } from '@material-ui/core';
import { BsArrowUpRight, BsArrowDownRight } from 'react-icons/bs';
import strings from '../../../core/strings';
import { commaThousondSeperator } from '../../../core/utils';

const useStyles = makeStyles({
    root: {
        margin: 10,
        paddingBottom: 10,
        borderBottom: 'solid 1px #ccc',
        alignItems: 'center',
        display: 'flex',
        flexDirection: 'row',
        // '&.success': {
        //     '& > svg': {
        //         color: 'green'
        //     }
        // },
        // '&.failed': {
        //     '& > svg': {
        //         color: 'red'
        //     }
        // },
        '& > svg': {
            fontSize: 20,

        },
        '& .info': {
            listStyle: 'none',
            paddingLeft: 10,
            '& li': {
                padding: '5px'
            }
        }
    },
    up: {
        color: 'green'
    },
    down: {
        color: 'red'
    }
});

export default function (props) {
    const { item } = props;
    //Hooks
    const classes = useStyles();
    let priceText = '';
    switch (item.type) {
        case 1:
            priceText = strings.purchaseFromStore;
            break;
        case 2:
            priceText = strings.returnedToUserAccount;
            break;
        case 3:
            priceText = strings.depositeToAccount;
            break;
    }

    return (<li className={`${classes.root} ${(item.isSuccess ? 'success' : 'failed')}`}>
        {item.type !== 1 ? <BsArrowUpRight className={classes.up} /> : <BsArrowDownRight className={classes.down} />}
        <ul className="info">
            <li>{item.gatewayName}(<strong>{item.isSuccess ? strings.successful : strings.failed}</strong>)</li>
            <li><span className='time'>{item.time}</span> | <span className='date'>{item.insertDateSh}</span></li>
            <li>{item.productText}</li>
            <li><strong>{commaThousondSeperator(item.price)} {strings.moneyCurrency} </strong>{priceText}</li>
            {/* <li></li> */}
        </ul>
    </li>);
}