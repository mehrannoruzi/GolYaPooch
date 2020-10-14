import React, { useState, useEffect } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import { Tabs, Tab, Paper, AppBar, Container } from '@material-ui/core';
import Banners from '../../atom/comps/Banners';
import ActivePurchase from './comps/active';
import AllPurchase from './comps/all';
import strings from '../../core/strings';
import { BsListCheck, BsList } from 'react-icons/bs';

const useStyles = makeStyles(() => ({
    root: {
        backgroundColor: '#fff',
        paddingTop: 15,
        paddingBottom: 15,
        '& .MuiPaper-root': {
            boxShadow: "0px 1px 8px 0px #d2d2d2",
            borderRadius: 5,
        },
        '& .MuiTabs-root':{
            minHeight: 40,
            '& .MuiTab-labelIcon': {
                minHeight: 'auto!important',
                '& .MuiTab-wrapper': {
                    flexDirection: 'row',
                    justifyContent: 'center',
                    alignItems: 'center',
                    '& svg': {
                        fontSize: 20,
                        marginBottom: '0!important',
                        marginRight: '5px'
                    }
                }
            }
        }

    },
}));


const Activities = () => {
    //Hooks
    const classes = useStyles();
    const [value, setValue] = useState(0);

    const handleChange = (event, newValue) => {
        setValue(newValue);
    };

    return (
        <div className={classes.root}>
            <Banners pageName="Activities" location="top" />
            <Container>
                <Paper className='mb-15'>
                    <Tabs
                        value={value}
                        onChange={handleChange}
                        indicatorColor="primary"
                        textColor="primary">
                        <Tab label={strings.active} icon={<BsListCheck />} />
                        <Tab label={strings.all} icon={<BsList />} />
                    </Tabs>
                </Paper>

                <Paper>
                    <div
                        role="tabpanel"
                        index={0}
                        hidden={value !== 0}
                        id='active-packages'>
                        <ActivePurchase />

                    </div>
                    <div
                        role="tabpanel"
                        index={1}
                        hidden={value !== 1}
                        id='all-packages'>
                        <AllPurchase />ّ

                </div>
                </Paper>
            </Container>

            <Banners pageName="Activities" location="bottom" />
        </div>
    );
};

export default Activities;