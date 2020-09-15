import React, { Component } from 'react';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';
import EmptyLayout from './layouts/emptyLayout';
import NavigationLayout from './layouts/navigationLayout';
import Toast from './atom/comps/Toast';
import BottomUpModal from './atom/comps/BottomUpModal';
import Start from './pages/start';

export default class App extends Component {
    render() {
        return (
            <Router className="layout">
                <Toast />
                <Switch>
                    <Route exact path="/" component={Start} />
                    <Route path="/elayout" component={EmptyLayout} />
                    <Route path="/nlayout" component={NavigationLayout} />
                </Switch>
               
                <BottomUpModal/>
            </Router>
        );
    }
}