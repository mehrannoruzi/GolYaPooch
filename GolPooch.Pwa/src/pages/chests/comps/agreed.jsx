import React from 'react';
import { makeStyles, Container, Grid, Divider, LinearProgress } from '@material-ui/core';
import { createMuiTheme } from '@material-ui/core/styles';
import strings from '../../../core/strings';

const useStyles = makeStyles({
    agreedComp: {
        '& .heading': {
            textAlign: 'center',
            marginBottom: 30
        },
        '& .chance': {
            textAlign: 'center',
            fontWeight: 800,
            fontSize: '1.5rem',
            margin: '0 0 45px 0'
        },
        '& .participants': {
            textAlign: 'right',

            '& label': {
                textAlign: 'right',
                fontWeight: 800
            }
        },
        '& .desc': {
            textAlign: 'justify'
        }
    }
});

export default function (props) {
    //Hooks
    const classes = useStyles();
    const { info } = props;

    return (<div className={classes.agreedComp}>
        <Container>
            <h4 className='heading'>
                {strings.yourChance}
            </h4>
            <h5 className='chance'>
                <span>{info.chanceOnRound}</span>
            </h5>
            <Divider component="div" className='mb-15' />
            <Grid container className='mb-15'>
                <Grid item xs={6}><label>{strings.allParticipantCount}</label></Grid>
                <Grid item xs={6} className='participants'><label>{info.currentParticipantCount}</label></Grid>
            </Grid>

            <Grid container className='mb-15'>
                <Grid item xs={6}><label>{strings.participantRemainCount}</label></Grid>
                <Grid item xs={6} className='participants'><label>{(info.allParticipantCount - info.currentParticipantCount)}</label></Grid>
            </Grid>
            <Divider component="div" className='mb-15' />
            <p className='desc'>
                متن توضیحات
            </p>
        </Container>
    </div>)
}