import React, { Component } from 'react';
import './Login.css';
import { Container, Row, Col, Input, Button, Fa, Card, CardBody, ModalFooter } from 'mdbreact';
import { Link } from 'react-router-dom';

export class Login extends Component {
    render() {
        return (
            <Container>
                <Row className="flex-center">
                    <Col md="6">
                        <Card>
                            <CardBody>
                                <form className="login-form">
                                    <p className="h4 text-center py-4">Log In</p>
                                    <div className="grey-text">
                                        <Input label="Username" icon="user" group type="text" validate error="wrong" success="right" />
                                        <Input label="Password" icon="lock" group type="password" validate />
                                    </div>
                                    <div className="text-center py-4 mt-3">
                                        <Button color="cyan" type="button">Login</Button>
                                    </div>
                                </form>
                            </CardBody>
                            <ModalFooter className="mx-5 pt-3 mb-1">
                                <p className="font-small grey-text d-flex justify-content-end">Not a member? <Link to="/register" className="blue-text ml-1">Sign Up</Link></p>
                            </ModalFooter>
                        </Card>
                    </Col>
                </Row>
            </Container>
        );
    }
}
