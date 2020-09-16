import React from 'react';
import Banners from '../../atom/comps/Banners';

const Store = () => {
    console.log('fired');
    return (
        <div id='page-store'>
            <Banners pageName='Store' location='top'/>
            <h1>store page</h1>
            <Banners pageName='Store' location='top'/>
        </div>
    );
};

export default Store;