import React from 'react';
import { makeStyles, Avatar, Card, Typography, CardContent } from '@material-ui/core';
import { Link } from 'react-router-dom';
import { FiChevronLeft } from 'react-icons/fi';
import userSrv from '../../../services/userSrv';
import { BiUser } from 'react-icons/bi';
import { red } from '@material-ui/core/colors';
import CardHeader from '@material-ui/core/CardHeader';
import strings from '../../../core/strings';
import { Alert } from '@material-ui/lab';

const useStyles = makeStyles({

    avatar: {
        backgroundColor: red[500],
    },
    root: {
        marginTop: 10,
        '& .icon': {
            fontSize: 20
        }
    },
    cardContent: {
        padding: 16,
        paddingTop: 0
    }
});


export default function (props) {
    //Hooks
    const classes = useStyles();
    const userInfo = userSrv.getInfo();
    const { item } = props;
    const date = new Date(item.insertDateMi);
    return (
        <Link to={`/bl/addticket?add=false&text=${item.text}&answer=${item.answer || ''}`}>
            <Card className={classes.root}>
                <CardHeader
                    avatar={userInfo.avatar ? <Avatar src={userInfo.avatar} /> : <BiUser className='icon' />}
                    title={`${strings.ticketTitle} #${item.ticketId}`}
                    subheader={`${date.getHours()}:${date.getMinutes()} ${item.insertDateSh}`}
                />
                <CardContent className={classes.cardContent}>
                    <Typography variant="body2" color="textSecondary" component="p">
                        {item.text.length > 100 ? `${item.text.substring(0, 100)} [...]` : item.text}
                    </Typography>
                </CardContent>

                {item.answer == null ?
                    <Alert severity="error">{strings.ticketNotAnswered}</Alert>
                    :
                    <Alert severity="success">{strings.ticketAnswered}</Alert>
                }

            </Card>
        </Link>
    );
}