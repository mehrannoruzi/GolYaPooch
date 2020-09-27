import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import AppBar from '@material-ui/core/AppBar';
import Tabs from '@material-ui/core/Tabs';
import Tab from '@material-ui/core/Tab';
import { TabPanel } from '@material-ui/lab';


const useStyles = makeStyles(() => ({
    root: {
        backgroundColor: '#FFF',
        width: 500,
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
            <AppBar position="static" color="default">
                <Tabs
                    indicatorColor="primary"
                    textColor="primary"
                    variant="fullWidth"
                    aria-label="full width tabs example"
                >
                    <Tab label="Item One" />
                    <Tab label="Item Two" />
                    <Tab label="Item Three" />
                </Tabs>
            </AppBar>
            <TabPanel index={0} >
                Item One
                 </TabPanel>
            <TabPanel index={1} >
                Item Two
                </TabPanel>
            <TabPanel index={2} >
                Item Three
                </TabPanel>
        </div>
    );
};

export default Activities;