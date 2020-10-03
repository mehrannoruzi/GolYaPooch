import React from 'react';
import { makeStyles, Container, Grid, Divider, LinearProgress } from '@material-ui/core';
import { MuiThemeProvider, createMuiTheme, withStyles } from '@material-ui/core/styles';

const ltrTheme = createMuiTheme({
    direction: 'ltr',
});

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

const BorderLinearProgress = withStyles((theme) => ({
    root: {
        height: 10,
    },
    colorPrimary: {
        backgroundColor: theme.palette.grey[theme.palette.type === 'light' ? 200 : 700],
    },
    bar: {
        backgroundColor: '#1a90ff',
    },
}))(LinearProgress);

export default function (props) {
    //Hooks
    const classes = useStyles();
    const { info } = props;
    console.log(info);
    const progress = (info.currentParticipantCount / info.allParticipantCount) * 100;
    console.log(progress);
    return (<div className={classes.agreedComp}>
        <Container>
            <h4 className='heading'>
                میزان شانس شما در این قرعه کشی
            </h4>
            <h5 className='chance'>
                <span>{info.chanceOnRound}</span>
            </h5>
            <Divider component="div" className='mb-15' />
            <Grid container className='mb-15'>
                <Grid item xs={6}><label>تعداد کل شرکت کنندگان:</label></Grid>
                <Grid item xs={6} className='participants'><label>{info.currentParticipantCount}</label></Grid>
            </Grid>
            <MuiThemeProvider theme={ltrTheme}>
                <BorderLinearProgress className='mb-15' variant="determinate" value={progress} />
            </MuiThemeProvider>
            <Grid container className='mb-15'>
                <Grid item xs={6}><label>تعداد باقیمانده تا قرعه کشی:</label></Grid>
                <Grid item xs={6} className='participants'><label>{(info.allParticipantCount - info.currentParticipantCount)}</label></Grid>
            </Grid>
            <Divider component="div" className='mb-15' />
            <p className='desc'>
                لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ، و با استفاده از طراحان گرافیک است، چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است، و برای شرایط فعلی تکنولوژی مورد نیاز، و کاربردهای متنوع با هدف بهبود ابزارهای کاربردی می باشد، کتابهای زیادی در شصت و سه درصد گذشته حال و آینده، شناخت فراوان جامعه و متخصصان را می طلبد، تا با نرم افزارها شناخت بیشتری را برای طراحان رایانه ای علی الخصوص طراحان خلاقی، و فرهنگ پیشرو در زبان فارسی ایجاد کرد، در این صورت می توان امید داشت که تمام و دشواری موجود در ارائه راهکارها، و شرایط سخت تایپ به پایان رسد و زمان مورد نیاز شامل حروفچینی دستاوردهای اصلی، و جوابگوی سوالات پیوسته اهل دنیای موجود طراحی اساسا مورد استفاده قرار گیرد.
            </p>
        </Container>
    </div>)
}