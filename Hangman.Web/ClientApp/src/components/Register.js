import { Button, Card, CardBody, Col, Container, Input, Row } from 'mdbreact';
import React, { Component } from 'react';
import './Login.css';
import './Register.css';
import { Link } from 'react-router-dom';
import swal from 'sweetalert';

export class Register extends Component {
    constructor(props) {
        super(props);
        let $this = this;
        $this.state =
            {
                firstname: '',
                lastname: '',
                contact: '',
                address: '',
                email: '',
                username: '',
                password: '',
                confirmpassword: ''
            };
        $this.handleChange = $this.handleChange.bind($this);
        $this.registerUser = $this.registerUser.bind($this);
    }
    registerUser(event) {
        event.preventDefault();
        let $this = this;
        let firstname = $this.state.firstname;
        let lastname = $this.state.lastname;
        let contact = $this.state.contact;
        let address = $this.state.address;
        let email = $this.state.email;
        let username = $this.state.username;
        let password = $this.state.password;
        let confirmpassword = $this.state.confirmpassword;
        if (firstname === '' && lastname === '' && contact === '' && address === '' && email === '' && username === '' && password === '' && confirmpassword === '') {
            swal("All fields are required required", "", "error");
        } else {
            if (firstname === '') {
                swal("First name is required", "", "error");
            } else if (lastname === '') {
                swal("Last name is required", "", "error");
            } else if (contact === '') {
                swal("Contact is required", "", "error");
            } else if (address === '') {
                swal("Address is required", "", "error");
            } else if (email === '') {
                swal("Email is required", "", "error");
            } else if (username === '') {
                swal("Username is required", "", "error");
            } else if (password === '') {
                swal("Password is required", "", "error");
            } else if (confirmpassword === '') {
                swal("Confirm password is required", "", "error");
            } else if (firstname !== '' && lastname !== '' && contact !== '' && address !== '' && email !== '' && username !== '' && password !== '' && confirmpassword !== '') {
                if (password !== confirmpassword) {
                    swal("Password and confirm password do not match", "", "error");
                }
                if (!this.validateEmail(email)) {
                    swal("Not a valid email", "", "error");
                }
                if (password.length < 6 || password.length > 12) {
                    swal("The field Password must be a string with a minimum length of 6 and a maximum length of 12", "", "error");
                } else {
                    fetch('https://localhost:44321/api/accounts/register', {
                        method: 'POST',
                        body: JSON.stringify({ firstname: firstname, lastname: lastname, contact: contact, address: address, email: email, username: username, password: password, confirmpassword: confirmpassword }),
                        headers: {
                            'Content-Type': 'application/json'
                        }
                    }).then(function (response) {
                        if (response.status === 200) {
                            $this.loginUser($this.state.username, $this.state.password)
                        }
                    });
                }
            }
        }
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
    }

    validateEmail(email) {
        var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(String(email).toLowerCase());
    }

    handleChange(e) {
        this.setState({
            [e.target.name]: e.target.value
        });
    }

    render() {
        return (
            <div className="reg-comp">
                <Container>
                    <section className="form-gradient">
                        <Row className="flex-center">
                            <Col md="6">
                                <Card>
                                    <CardBody>
                                        <div className="form-header deep-blue-gradient rounded">
                                            <h3 className="my-3"><i className="fa fa-lock" />Sign Up:</h3>
                                        </div>
                                        <form className="reg-form">
                                            <div className="image-firstname">
                                                <Input name="firstname" value={this.state.firstname} onChange={this.handleChange} label="Firstname" icon="user" group type="text" validate error="wrong" success="right" />
                                                <div className="reg-img-div">Click or drop image to upload ...</div>
                                            </div>
                                            <div className="grey-text">
                                                <Input name="lastname" value={this.state.lastname} onChange={this.handleChange} label="Lastname" icon="user" group type="text" validate error="wrong" success="right" />
                                                <Input name="contact" value={this.state.contact} onChange={this.handleChange} label="Contact Number" icon="mobile" group type="text" validate error="wrong" success="right" />
                                                <Input name="address" value={this.state.address} onChange={this.handleChange} label="Address" icon="address-card" group type="text" validate error="wrong" success="right" />
                                                <Input name="email" value={this.state.email} onChange={this.handleChange} label="Email" icon="envelope" group type="email" validate error="wrong" success="right" />
                                                <Input name="username" value={this.state.username} onChange={this.handleChange} label="Username" icon="user" group type="text" validate error="wrong" success="right" />
                                                <Input name="password" value={this.state.password} onChange={this.handleChange} label="Password" icon="lock" group type="password" validate />
                                                <Input name="confirmpassword" value={this.state.confirmpassword} onChange={this.handleChange} label="Confirm Password" icon="lock" group type="password" validate />
                                            </div>
                                            <div className="text-center py-4 mt-3">
                                                <Button color="cyan" type="button" onClick={this.registerUser}>Register</Button>
                                            </div>
                                        </form>
                                    </CardBody>
                                </Card>
                            </Col>
                        </Row>
                    </section>
                </Container>
            </div>
        );
    }
}