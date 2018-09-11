import React, { Component } from 'react';
import { Card, CardBody, Col, Container, Row, Button, Table, TableBody, TableHead, Fa, Input } from 'mdbreact';
import './ManageUsers.css';
import swal from 'sweetalert';
import { Layout } from './Layout';

export class ManageUsers extends Component {
    constructor(props) {
        super(props);
        let $this = this;
        $this.state =
            {
                userList: [],
                roleList: [],
                selectedOption: null
            };
        $this.getAllUsers();
        $this.searchUsers = $this.searchUsers.bind($this);
    }

    getAllUsers() {
        let $this = this;
        fetch('https://localhost:44321/api/accounts/getallusers', {
            method: 'Get',
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(function (response) {
            response.json().then(function (json) {
                $this.setState({ userList: json });
            }).catch(function (ex) {
                console.log(ex.message);
            });
        });
    }
    searchUsers(e) {
        let $this = this;
        let userName = e.target.value;
        if (userName === '') {
            $this.getAllUsers();
        } else {
            fetch(`https://localhost:44321/api/accounts/searchuser/${e.target.value}`, {
                method: 'Get',
                headers: {
                    'Content-Type': 'application/json'
                }
            }).then(function (response) {
                if (response.status === 200) {
                    response.json().then(function (json) {
                        $this.srchUsrInpt.value = '';
                        $this.setState({ userList: json });
                    }).catch(function (ex) {
                        console.log(ex.message);
                        });
                } else if (response.status === 404) {
                    $this.srchUsrInpt.value = '';
                    swal("No user found by that name", "", "success");
                    $this.getAllUsers();
                }
            });
        }
    }
    deleteUser(userId) {
        let $this = this;
        fetch(`https://localhost:44321/api/accounts/deleteuser/${userId}`, {
            method: 'Post',
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(function (response) {
            response.json().then(function (json) {
                $this.srchUsrInpt.value = '';
                swal(json, "", "success");
                $this.getAllUsers();
            }).catch(function (ex) {
                console.log(ex.message);
            });
        });
    }
    render() {
        return (
<Layout>
            <Container>
                <section>
                    <Row>
                        <Input ref={el => this.srchUsrInpt = el} label="Search User:" hint="Search" size="sm" style={{ width: 20 + '%' }} onChange={this.searchUsers} />
                        <Col md="10">
                            <Card>
                                <CardBody>
                                    <Table striped hover responsive style={{ height: 20 + 'vw' }}>
                                        <TableHead>
                                            <tr>
                                                <th>#</th>
                                                <th>Username</th>
                                                <th>FirstName</th>
                                                <th>LastName</th>
                                                <th>Address</th>
                                                <th>Contact</th>
                                                <th>Role</th>
                                                <th>Image</th>
                                                <th>Action</th>
                                            </tr>
                                        </TableHead>
                                        <TableBody>
                                            {this.state.userList.map((item, key) => {
                                                return (
                                                    <tr key={key}>
                                                        <td>{key + 1}</td>
                                                        <td>{item.userName}</td>
                                                        <td>{item.firstName}</td>
                                                        <td>{item.lastName}</td>
                                                        <td>{item.address}</td>
                                                        <td>{item.phoneNumber}</td>
                                                        <td>
                                                            {item.name}
                                                        </td>
                                                        <td><div className="user-img-td"><img className="user-imgs" src={`data:image/png;base64,${item.imageUrl}`} /></div></td>
                                                        <td><Button color="primary" title="Delete User" onClick={() => this.deleteUser(item.id)}><Fa icon="trash-o" /></Button></td>
                                                    </tr>
                                                );
                                            })}
                                        </TableBody>
                                    </Table>
                                </CardBody>
                            </Card>
                        </Col>
                    </Row>
                </section>
                </Container>
            </Layout>
        );
    }
}