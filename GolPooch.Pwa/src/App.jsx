import React, { Component } from 'react';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';
import EmptyLayout from './layouts/emptyLayout';
import NavigationLayout from './layouts/navigationLayout';
import Toast from './atom/comps/Toast';
import BottomUpModal from './atom/comps/BottomUpModal';

export default class App extends Component {
    render() {
        return (
            <Router className="layout">
                <Switch>
                    <Route exact path="/pages" component={NavigationLayout} />
                    <Route exact path="/" component={EmptyLayout} />
                </Switch>
                <Toast/>
                <BottomUpModal/>
            </Router>
        );
    }
}