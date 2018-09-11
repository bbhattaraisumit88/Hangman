import React, { Component } from 'react';
import { Route } from 'react-router';
import { Login } from './components/Login';
import { Register } from './components/Register';
import { Admindashboard } from './components/Admindashboard';
import { LeaveStatus } from './components/LeaveStatus';
import { ManageUsers } from './components/ManageUsers';
import { Userdashboard } from './components/Userdashboard';
import { ApplyLeave } from './components/ApplyLeave';
import { UserProfile } from './components/UserProfile';

export default class App extends Component {
    displayName = App.name

    render() {
        return (
            <div>
                <Route exact path='/' component={Login} />
                <Route exact path='/login' component={Login} />
                <Route path='/register' component={Register} />
                <Route path='/admindashboard' component={Admindashboard} />
                <Route path='/approveleave' component={LeaveStatus} />
                <Route path='/manageuser' component={ManageUsers} />
                <Route path='/userdashboard' component={Userdashboard} />
                <Route path='/applyleave' component={ApplyLeave} />
                <Route path='/userprofile' component={UserProfile} />
            </div>
        );
    }
}
