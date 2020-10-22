import React from 'react';
import { Button, CircularProgress, makeStyles } from '@material-ui/core';

const useStyles = makeStyles({
    btnWithIcon: {
        dispaly: 'flex',
        paddingRight: 0,
        '& .icon': {
            fontSize: 20,
            color: '#fff',
            verticalAlign: 'middle',
            display: 'flex',
            width: 30,
            padding: '0 5px',
            justifyContent: 'center',
            '& .MuiCircularProgress-indeterminate': {
                marginLeft: 'auto',
                marginRight: 'auto'
            }
        },
        '& .text': {
            textAlign: 'center',
            display: 'flex',
            flex: 1,
            justifyContent: 'center'
        },
        '& .MuiCircularProgress-colorPrimary': {
            color: '#666!important'
        }
    },
    btnWithoutIcon:{
        '& .MuiCircularProgress-root':{
            position: 'absolute',
            right: 'calc(50% - 10px)',
            color:'#666!important',
            zIndex:990
        }

    }
});


const CustomButton = (props) => {
    const classes = useStyles();
    const Icon = props.icon;
    if (Icon)
        return (<Button variant="contained" color={props.color || 'primary'} disabled={props.disabled || props.loading} className={`${props.className} ${classes.btnWithIcon}`} onClick={props.onClick}>
            <span className='text'>{props.children}</span>
            <span className='icon'>{props.loading ? <CircularProgress size={20} className='va-middle' /> : <Icon/>}</span>
        </Button>);
    else
        return (<Button variant="contained" color={props.color || 'primary'} disabled={props.disabled || props.loading} className={`${classes.btnWithoutIcon} ${props.className}`} onClick={props.onClick}>
           {props.loading ? <CircularProgress size={20} className='va-middle' /> : props.children}
        </Button>);
}
export default CustomButton;