import React, { Component } from 'react';
import { Container, Row, Col, Input, Button, Fa, Card, CardBody } from 'mdbreact';
import './Register.css';

export class Register extends Component {
    render() {
        function registerUser(event) {
            event.preventDefault();
            const regData = new FormData(event.target);
            fetch('https://localhost:44321/api/accounts/register', {
                method: 'POST',
                body: regData,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                }
            });
        }

        return (
            <Container>
                <Row className="flex-center">
                    <Col md="6">
                        <Card>
                            <CardBody>
                                <form onSubmit={registerUser}>
                                    <p className="h4 text-center py-4">Sign up</p>
                                    <div className="image-firstname">
                                        <Input label="Firstname" icon="user" group type="text" validate error="wrong" success="right" />
                                        <div className="reg-img-div"><img className="reg-img mx-auto d-block" alt="..." src="/images/landingPage.jpg" /></div>
                                    </div>
                                    <div className="grey-text">
                                        <Input label="Lastname" icon="user" group type="text" validate error="wrong" success="right" />
                                        <Input label="Contact Number" icon="mobile" group type="text" validate error="wrong" success="right" />
                                        <Input label="Address" icon="address-card" group type="text" validate error="wrong" success="right" />
                                        <Input label="Email" icon="envelope" group type="email" validate error="wrong" success="right" />
                                        <Input label="Username" icon="user" group type="text" validate error="wrong" success="right" />
                                        <Input label="Password" icon="lock" group type="password" validate />
                                        <Input label="Confirm Password" icon="lock" group type="password" validate />
                                    </div>
                                    <div className="text-center py-4 mt-3">
                                        <Button color="cyan" type="submit">Register</Button>
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