import React, { Component } from 'react';

import { connect } from 'react-redux';
import { Route, Switch } from 'react-router';
import { Routes } from '../Utils/Routes';
import Home from '../Pages/Home.jsx';



class UiView extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <Switch>
                {Routes.map(r => (
                    <Route key={r.name} exact={r.exact} path={r.url} component={r.component} />
                ))}
                <Route component={Home} />
            </Switch>
        );
    }
}

export default UiView;