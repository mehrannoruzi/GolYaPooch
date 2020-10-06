import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import { Tabs, Tab, Paper, AppBar, Container } from '@material-ui/core';
import { TabPanel } from '@material-ui/lab';
import Banners from '../../atom/comps/Banners';


const useStyles = makeStyles(() => ({
    root: {
        backgroundColor: '#fff',
        paddingTop: 7.5,
        paddingBottom: 7.5,
        '& .MuiPaper-root':{
            boxShadow: "0px 1px 8px 0px #d2d2d2",
            borderRadius: 5,
        }
    },
}));


const Activities = () => {
    const classes = useStyles();
    const [value, setValue] = React.useState(0);

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
                        <Tab label="بسته های فعال" />
                        <Tab label="کلیه بسته ها" />
                    </Tabs>
                </Paper>

                <Paper>
                    <div
                        role="tabpanel"
                        index={0}
                        hidden={value !== 0}
                        id='active-packages'>
                        Item One

                    </div>
                    <div
                        role="tabpanel"
                        index={1}
                        hidden={value !== 1}
                        id='active-packages'>
                        Item Two

                </div>
                </Paper>
            </Container>

            <Banners pageName="Activities" location="bottom" />
        </div>
    );
};

export default Activities;