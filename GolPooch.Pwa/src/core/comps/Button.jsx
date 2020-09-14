import React from 'react';
import { CircularProgress } from '@material-ui/core';

export default class extends React.Component {
    render() {
        return (<button disabled={this.props.disabled || this.props.loading} className={this.props.className} onClick={this.props.onClick}>{this.props.children}&nbsp;{this.props.loading ? <CircularProgress size={20} className='va-middle' /> : ''}</button>);
    }
}