import React, { Component } from 'react';
import './Login.css';
import { Container, Row, Col, Input, Button, Fa, Card, CardBody, ModalFooter } from 'mdbreact';
import { Link } from 'react-router-dom';
import swal from 'sweetalert';
import 'sweetalert/dist/sweetalert.css';
import { Admindashboard } from './Admindashboard';
import { Userdashboard } from './Userdashboard';

export class Login extends Component {
    constructor(props) {
        super(props);
        let $this = this;
        $this.state = {
            username: '',
            password: ''
        };
        $this.handleChange = $this.handleChange.bind($this);
        $this.getLoginDetails = $this.getLoginDetails.bind($this);
        $this.loginUser = $this.loginUser.bind($this);
    }

    getLoginDetails(e) {
        e.preventDefault();
        let $this = this;  
        let username = $this.state.username;
        let password = $this.state.password;
        $this.loginUser(username, password);
    }

    loginUser(username, password) {
        let $this = this;     
        if (username === '' && password === '') {
            swal("Username and password is required", "", "error");
        } else {
            if (username === '') {
                swal("Username is required", "", "error");
            } else if (password === '') {
                swal("Password is required", "", "error");
            } else if (username !== '' && password !== '') {
                if (password.length < 6 || password.length > 12) {
                    swal("The field Password must be a string with a minimum length of 6 and a maximum length of 12", "", "error");
                } else {
                    fetch('https://localhost:44321/api/accounts/login', {
                        method: 'POST',
                        body: JSON.stringify({ username: username, password: password }),
                        headers: {
                            'Content-Type': 'application/json'
                        }
                    }).then(function (response) {
                        if (response.status === 200) {
                            response.json().then(function (json) {
                                $this.loginSuccess(JSON.parse(json));
                            }).catch(function (ex) {
                                console.log(ex.message);
                            });
                        } else if (response.status === 401) {
                            swal("Invalid username or password", "", "error");
                        }
                    });
                }
            }
        }
    }

    loginSuccess(token) {
        localStorage.setItem('auth_token', JSON.stringify(token));
        var a = document.createElement('a');     
        if (token.role === 'superuser') {
            a.href = '/manageuser';
        } else {
            a.href = '/applyleave';
        }
        document.body.appendChild(a);
        a.click();
        a.remove();
    }

    handleChange(e) {
        this.setState({
            [e.target.name]: e.target.value
        });
    }
    render() {
        return (
            <div className="login-comp">
                <Container>
                    <section className="form-gradient">
                        <Row className="flex-center">
                            <Col md="6">
                                <Card>
                                    <CardBody>
                                        <div className="form-header deep-blue-gradient rounded">
                                            <h3 className="my-3"><i className="fa fa-lock" />Login:</h3>
                                        </div>
                                        <form className="login-form">
                                            <div className="grey-text">
                                                <Input value={this.state.username} onChange={this.handleChange} label="Username" icon="user" group type="text" validate error="wrong" success="right" name="username" />
                                                <Input value={this.state.password} onChange={this.handleChange} label="Password" icon="lock" group type="password" validate name="password" />
                                            </div>
                                            <div className="text-center py-4 mt-3">
                                                <Button color="cyan" type="button" onClick={this.getLoginDetails}>Login</Button>
                                            </div>
                                        </form>
                                    </CardBody>
                                    <ModalFooter className="mx-5 pt-3 mb-1">
                                        <p className="grey-text d-flex justify-content-end">Not a member? <Link to="/register" className="blue-text ml-1">Sign Up</Link></p>
                                    </ModalFooter>
                                </Card>
                            </Col>
                        </Row>
                    </section>
                </Container>
            </div>
        );
    }
}