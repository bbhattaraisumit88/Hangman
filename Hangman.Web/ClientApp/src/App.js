import React, { Component } from 'react';
import { Container, Row, Col, Input, Button, Fa, Card, CardBody } from 'mdbreact';

export default class App extends Component {
    render() {
        return (
            <Container>
                <Row>
                    <Col md="6">
                        <Card>
                            <CardBody>
                                <form>
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
                        </Card>
                    </Col>
                </Row>
            </Container>
        );
    }
}
