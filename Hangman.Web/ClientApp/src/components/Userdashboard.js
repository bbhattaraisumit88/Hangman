import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Glyphicon, Nav, Navbar, NavItem, Col, Grid, Row } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';
import './Admindashboard.css';

export class Userdashboard extends Component {
    displayName = Userdashboard.name

    render() {
        return (
            <Navbar inverse fixedTop fluid collapseOnSelect>
                <Navbar.Header>
                    <Navbar.Brand>
                        <Link to={'/applyleave'}>User Dashboard</Link>
                    </Navbar.Brand>
                    <Navbar.Toggle />
                </Navbar.Header>
                <Navbar.Collapse>
                    <Nav>
                        <LinkContainer to={'/applyleave'}>
                            <NavItem>
                                <Glyphicon glyph='th-list' /> Apply Leave
              </NavItem>
                        </LinkContainer>
                        <LinkContainer to={'/login'} exact>
                            <NavItem>
                                <Glyphicon glyph='user' /> Logout
                              
              </NavItem>
                        </LinkContainer>
                    </Nav>
                </Navbar.Collapse>
            </Navbar>
        );
    }
}
