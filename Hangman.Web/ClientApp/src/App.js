import React, { Component } from 'react';
import './App.css';
import { Container, Row, Col, Input, Button, Card, CardBody } from 'mdbreact';

export default class App extends Component {
    render() {
        return (
            <Container>
                <Row>
                    <Col md="6">
                        <Card>
                            <CardBody>
                                <div className="form-header deep-blue-gradient rounded">
                                    <h3 className="my-3"><i className="fa fa-lock" /> Sign In:</h3>
                                </div>
                                <form>
                                    <div className="grey-text">
                                        <Input label="Your username" icon="user" group type="text" validate error="wrong" success="right" />
                                        <Input label="Your password" icon="lock" group type="password" validate />
                                    </div>
                                    <div className="text-center py-4 mt-3">
                                        <Button color="cyan" type="button" className="login">Login</Button>
                                    </div>
                                </form>
                            </CardBody>
                        </Card>
                    </Col>
                </Row>
            </Container>
        );
    }
}
