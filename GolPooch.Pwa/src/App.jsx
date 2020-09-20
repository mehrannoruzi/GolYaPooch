import React, { Component } from 'react';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';
import EmptyLayout from './layouts/emptyLayout';
import NavigationLayout from './layouts/navigationLayout';
import BackLayout from './layouts/backLayout';
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
                    <Route path="/el" component={EmptyLayout} />
                    <Route path="/nl" component={NavigationLayout} />
                    <Route path="/bl" component={BackLayout} />
                </Switch>
               
                <BottomUpModal/>
            </Router>
        );
    }
}