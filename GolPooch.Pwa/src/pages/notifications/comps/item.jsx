import React from 'react';
import { makeStyles, Typography, Accordion, AccordionSummary, AccordionDetails, Button } from '@material-ui/core';
import { FcExpand } from 'react-icons/fc';
import notificationSrv from './../../../services/notificationSrv';

const useStyles = makeStyles({
    notificationComp: {
        paddingTop: 7.5,
        paddingBottom: 7.5,
        '& .heading': {
            display: 'flex',
            alignItems: 'center',
            '& .img-icon': {
                width: 50
            },
            '& .subject': {
                margin: '0 10px'
            }
        },
        '& .MuiAccordionDetails-root': {
            flexDirection: 'column',
            justifyContent: 'center',
            alignItems: 'center',
            '& .txt': {
                textAlign: 'justify',
                width: '100%',
                marginBottom: '15px'
            },
            '& .img-full': {
                width: '100%',
                marginBottom: '15px'
            }
        }
    },
});


export default function (props) {
    const { item } = props;
    const classes = useStyles();

    const _handleClick = (id, isRead) => {
        console.log(id);
        if (!isRead)
            notificationSrv.read(id);
    }
    // return (<React.Fragment>
    //     <ListItem alignItems="flex-start" className={item.isRead ? 'read' : null}>
    //         <ListItemAvatar>
    //             <Avatar alt="Remy Sharp" src={item.imageUrl} />
    //         </ListItemAvatar>
    //         <ListItemText
    //             primary={item.subject}
    //             secondary={item.text} />
    //     </ListItem>
    //     <Divider variant="inset" component="li" />
    // </React.Fragment>);
    return (<Accordion className={`${classes.notificationComp} ${(item.isRead ? 'read' : 'not-read')}`}>
        <AccordionSummary
            onClick={() => _handleClick(item.notificationId, item.isRead)}
            expandIcon={<FcExpand />}
            id={`summary_${item.notificationId}`}>
            <Typography className='heading' component='h4'>
                <img className='img-icon' alt="Remy Sharp" src={item.iconUrl || item.imageUrl} />
                <span className='subject'>{item.subject}</span>
                {/* <Typography component='p'>{item.text}</Typography> */}
            </Typography>
        </AccordionSummary>
        <AccordionDetails>
            <p className='txt'>{item.text}</p>
            {item.imageUrl ? <a className='w-100' href={item.actionUrl}><img className='img-full' src={item.imageUrl} alt={item.subject} /></a> : null}
            {item.actionText ? <Button variant="contained" color="primary" href={item.actionUrl}>{item.actionText}</Button> : null}
        </AccordionDetails>
    </Accordion>);
}