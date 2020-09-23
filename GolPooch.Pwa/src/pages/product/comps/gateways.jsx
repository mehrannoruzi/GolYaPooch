import React, { useState, useEffect } from 'react';
import { Skeleton } from '@material-ui/lab';
import { makeStyles, Box } from '@material-ui/core';
import strings from '../../../core/strings';
import gatewaySrv from '../../../services/gatewaySrv';
import toastState from '../../../atom/state/toastState';
import { useRecoilState } from 'recoil';
import Slider from "react-slick";
import productAtom from '../../../atom/state/productState';
import Heading from '../../../atom/comps/Heading';
import { BsCheckCircle } from 'react-icons/bs';

const useStyles = makeStyles({
    gateways: {
        '& .gateway': {
            padding: 10,
            display: 'flex',
            justifyContent: 'center',
            alignItems: 'center',
            boxSizing: 'border-box',

            '& .box': {
                border: 'solid 1px #ccc',
                padding: '10px 0',
                borderRadius: '3px',
                textAlign: 'center',
                position: 'relative',
                '&.selected': {
                    color: 'green',
                    border: 'solid 1px green',
                },
                '& svg': {
                    color: 'green',
                    fontSize: '20px',
                    position: 'absolute',
                    top: '-10px',
                    left: '-10px',
                    backgroundColor: '#fff'
                }
            }
        }
    }
});

const Gateways = () => {
    const classes = useStyles();
    //HOOKS
    const [gateways, setGateways] = useState([]);
    const [query, setQuery] = useState('');
    const [inProgress, setInProgress] = useState(true);

    //recoil
    const [productState, setProductState] = useRecoilState(productAtom);
    const [toast, setToastState] = useRecoilState(toastState);

    useEffect(() => {
        const getGateways = async () => {
            setInProgress(true);
            let getGateways = await gatewaySrv.get();
            if (getGateways.isSuccessful) {
                let defItem = getGateways.result.find(x => x.isDefault);
                if (defItem) setProductState({ ...productState, gatewatId: defItem.paymentGatewayId })
                setGateways(getGateways.result);
            }
            else setToastState({ ...toast, open: true, severity: 'error', message: getGateways.message });
            setInProgress(false);
        }

        getGateways();


    }, [setQuery]);

    const _handleSelect = (id) => {
        console.log(id);
        setProductState({ ...productState, gatewatId: id });
    }
    const settings = {
        dots: true,
        infinite: false,
        speed: 500,
        slidesToShow: 3,
    };
    return (
        <div id='comp-gateways' className={classes.gateways}>
            <Heading>{strings.gateways}</Heading>
            <Slider
                {...settings}>
                {inProgress ? [0, 1, 2].map((x, idx) => <div key={idx} className='gateway'>
                    <Skeleton key={idx} className='w-100' />
                </div>)
                    : gateways.map((x, idx) => <div key={idx} className='gateway'>
                        <Box onClick={() => _handleSelect(x.paymentGatewayId)} className={`box  ${(x.paymentGatewayId === productState.gatewatId ? 'selected' : '')}`}>
                            {x.paymentGatewayId === productState.gatewatId ? <BsCheckCircle /> : null}
                            {x.name}
                        </Box>
                    </div>)}

            </Slider>
        </div>
    );
};

export default Gateways;