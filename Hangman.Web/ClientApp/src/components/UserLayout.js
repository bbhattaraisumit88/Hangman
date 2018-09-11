import React, { Component } from 'react';
import { Col, Grid, Row } from 'react-bootstrap';
import { Userdashboard } from './Userdashboard';

export class UserLayout extends Component {
    displayName = UserLayout.name

    render() {
        return (
            <Grid fluid>
                <Row>
                    <Col sm={3}>
                        <Userdashboard />
                    </Col>
                    <Col sm={9}>
                        {this.props.children}
                    </Col>
                </Row>
            </Grid>
        );
    }
}
