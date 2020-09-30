import React from 'react';
import { Avatar,ListItem, ListItemAvatar, ListItemText, Divider, Typography } from '@material-ui/core';

export default function (props) {
    const { item } = props;
    return (<React.Fragment>
        <ListItem alignItems="flex-start" className={item.isRead ? 'read' : null}>
            <ListItemAvatar>
                <Avatar alt="Remy Sharp" src={item.imageUrl} />
            </ListItemAvatar>
            <ListItemText
                primary={item.subject}
                secondary={item.text} />
        </ListItem>
        <Divider variant="inset" component="li" />
    </React.Fragment>);
}