import React, { Component } from 'react';
import './Login.css';
import { Container, Row, Col, Input, Button, Fa, Card, CardBody, ModalFooter } from 'mdbreact';
import { Link } from 'react-router-dom';

export class Login extends Component {
    constructor(props) {
        super(props);
        this.state = {
            username: '',
            password: ''
        };
        this.handleChange = this.handleChange.bind(this);
        this.loginUser = this.loginUser.bind(this);
    }

    loginUser(e) {
        e.preventDefault();
        let $this = this;
        let username = $this.state.username;
        let password = $this.state.password;
        fetch('https://localhost:44321/api/accounts/login', {
            method: 'POST',
            body: JSON.stringify({ username: username, password: password }),
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(function (response) {
            return response.json();
            }).then(function (json) {
                $this.loginSuccess(json);
        }).catch(function (ex) {
            console.log(ex.message);
        });
    }

    loginSuccess(token) {
        console.log('parsed json', token);
    }

    handleChange(e) {
        this.setState({
            [e.target.name]: e.target.value
        });
    }
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
                                        <Input value={this.state.username} onChange={this.handleChange} label="Username" icon="user" group type="text" validate error="wrong" success="right" name="username" />
                                        <Input value={this.state.password} onChange={this.handleChange} label="Password" icon="lock" group type="password" validate name="password" />
                                    </div>
                                    <div className="text-center py-4 mt-3">
                                        <Button color="cyan" type="button" onClick={this.loginUser}>Login</Button>
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
