import React from 'react';
import { Skeleton } from '@material-ui/lab';
import Product from './product';

const Items = (props) => {
    return (
        <>
            {props.inProgress ? [0, 1, 2].map((x, idx) => <Skeleton key={idx} variant='rect' className='w-100 mb-15' height={275} />) :
                props.items.map((item, idx) => <Product key={idx} item={item} />)
            }
        </>
    );
}
export default Items;