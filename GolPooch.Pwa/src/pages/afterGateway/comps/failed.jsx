import React from 'react';
import { Link } from 'react-router-dom';
import strings from '../../../core/strings';
import { Grid, makeStyles } from '@material-ui/core';
import { CgCloseO } from 'react-icons/cg';
import { commaThousondSeperator } from '../../../core/utils';
const useStyles = makeStyles({
    failedComp: {
        paddingTop: 15,
        paddingBottom: 15,

        '& .heading': {
            textAlign: 'center',
            '& svg': {
                fontSize: 50
            },
            '& .hx': {
                padding: '30px 0px 80px 0px',
                textAlign: 'center',
            }
        },
        '& .product': {
            padding: '0 30px',
            '& .img': {
                height: '150px',
                width: '100%'
            },
            '& .info': {
                display: 'flex',
                flexDirection: 'column',
                justifyContent: 'center',
                paddingLeft: 15
            },
            '& .price': {
                paddingTop: 50
            }
        }

    },
    linkToStore: {
        backgroundColor: '#ffffff',
        borderRadius: '3px',
        position: 'fixed',
        left: '50%',
        zIndex: 999,
        bottom: '30px',
        transform: 'translateX(-50%)',
        boxShadow: '-1px 2px 2px 2px rgba(0,0,0,0.5)',
        padding: '10px 20px'

    },
    traceId: {
        paddingBottom: '50px'
    }
});

const Failed = (props) => {
    //HOOKS
    const classes = useStyles();

    const {info} = props;

    return (
        <Grid container className={classes.failedComp}>
            <Grid xs={12} item className='heading'>
                <CgCloseO />
                <h3 className='hx'>{strings.failedPayment}</h3>
            </Grid>
            <Grid xs={12} item className='product mb-15'>
                <Grid container>
                    <Grid item xs={12}>
                        <h4>جزییات خرید</h4>
                    </Grid>
                    <Grid xs={4} item className='pic'>
                        <img className='img' src={info.ProductImageUrl} />
                    </Grid>
                    <Grid xs={8} item className='info'>
                            <label className='mb-15'>{info.ProductText}</label>
                            <label>تعداد شانس: {info.NewChance}</label>
                    </Grid>
                    <Grid item xs={12} className='price'>
                        <label>مبلغ قابل پرداخت: {commaThousondSeperator(info.Price)} {strings.moneyCurrency}</label>
                    </Grid>
                </Grid>
            </Grid>
            <Link to='/nl/store' className={classes.linkToStore}>پرداخت مجدد</Link>
        </Grid>
    );
}
export default Failed;