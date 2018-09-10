import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Glyphicon, Nav, Navbar, NavItem } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';
import './Admindashboard.css';

export class Admindashboard extends Component {
    displayName = Admindashboard.name

    render() {
        return (
            <Navbar inverse fixedTop fluid collapseOnSelect>
                <Navbar.Header>
                    <Navbar.Brand>
                        <Link to={'/manageuser'}>Admin Dashboard</Link>
                    </Navbar.Brand>
                    <Navbar.Toggle />
                </Navbar.Header>
                <Navbar.Collapse>
                    <Nav>
                        <LinkContainer to={'/manageuser'} exact>
                            <NavItem>
                                <Glyphicon glyph='user' /> Manage Users
              </NavItem>
                        </LinkContainer>
                        <LinkContainer to={'/approveleave'}>
                            <NavItem>
                                <Glyphicon glyph='th-list' /> Check Leave Status
              </NavItem>
                        </LinkContainer>
                    </Nav>
                </Navbar.Collapse>
            </Navbar>
        );
    }
}
