import React, { Component } from 'react';
import { Route } from 'react-router';
import { Login } from './components/Login';
import { Register } from './components/Register';

export default class App extends Component {
    displayName = App.name

    render() {
        return (
            <div>
                <Route exact path='/' component={Login} />
                <Route exact path='/login' component={Login} />
                <Route path='/register' component={Register} />
            </div>
        );
    }
}
