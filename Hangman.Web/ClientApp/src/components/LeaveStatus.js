import React, { Component } from 'react';
import { Layout } from './Layout';
import { Card, CardBody, Col, Container, Row, Button, Table, TableBody, TableHead, Fa, Input } from 'mdbreact';
import swal from 'sweetalert';

export class LeaveStatus extends Component {
    constructor(props) {
        super(props);
        let $this = this;
        $this.state =
            {
                userLeave: []
            };
        if (localStorage.getItem('auth_token') !== null) {
            var tokens = JSON.parse(localStorage.auth_token);
            $this.state.username = tokens;
            $this.state.userid = tokens.id;
        }
        $this.getallleave();
    }
    getallleave() {
        let $this = this;
        fetch(`https://localhost:44321/api/user/getallleave`, {
            method: 'Get',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + $this.state.username.auth_token
            }
        }).then(function (response) {
            response.json().then(function (json) {
                $this.setState({ userLeave: json });
            }).catch(function (ex) {
                console.log(ex.message);
            });
        });
    }
    rejectLeave(item) {
        let $this = this;
        let userid = item.id;
        let identityid = item.identityId;
        let fromdate = item.from;
        let todate = item.to;
        let reason = item.reason;
        let status = 'rejected';
        fetch('https://localhost:44321/api/admin/rejectleave', {
            method: 'POST',
            body: JSON.stringify({ id: userid, identityid: identityid, from: fromdate, to: todate, reason: reason, status: status }),
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + $this.state.username.auth_token
            }
        }).then(function (response) {
            if (response.status === 200) {
                response.json().then(function (json) {
                    swal("Leave rejected Successfully", "", "error");
                    $this.getallleave(identityid);
                }).catch(function (ex) {
                    console.log(ex.message);
                });
            } else if (response.status === 403) {
                swal("Invalid username or password", "", "error");
            }
        });
    }
    approveLeave(item) {
        let $this = this;
        let userid = item.id;
        let identityid = item.identityId;
        let fromdate = item.from;
        let todate = item.to;
        let reason = item.reason;
        let status = 'approved';
        fetch('https://localhost:44321/api/admin/approveleave', {
            method: 'POST',
            body: JSON.stringify({ id: userid, identityid: identityid, from: fromdate, to: todate, reason: reason, status: status }),
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + $this.state.username.auth_token
            }
        }).then(function (response) {
            if (response.status === 200) {
                response.json().then(function (json) {
                    swal("Leave Approved Successfully", "", "error");
                    $this.getallleave();
                }).catch(function (ex) {
                    console.log(ex.message);
                });
            } else if (response.status === 403) {
                swal("Invalid username or password", "", "error");
            }
        });
    }
    render() {
        return (
            <Layout>
                <Container>
                    <section>
                        <Row>
                            <Col md="10">
                                <Card>
                                    <CardBody>
                                        <Table striped hover responsive style={{ height: 20 + 'vw' }}>
                                            <TableHead>
                                                <tr>
                                                    <th>#</th>
                                                    <th>Username</th>
                                                    <th>From</th>
                                                    <th>To</th>
                                                    <th>Reason</th>
                                                    <th>Status</th>
                                                    <th>Action</th>
                                                </tr>
                                            </TableHead>
                                            <TableBody>
                                                {this.state.userLeave.map((item, key) => {
                                                    return (
                                                        <tr key={key}>
                                                            <td>{key + 1}</td>
                                                            <td>{item.userName}</td>
                                                            <td>{item.from}</td>
                                                            <td>{item.to}</td>
                                                            <td>{item.reason}</td>
                                                            <td>{item.status}</td>
                                                            <td><Button color="primary" title="Approve Leave" onClick={() => this.approveLeave(item)}><Fa icon="thumbs-up" /></Button><Button color="primary" title="Cancel Leave" onClick={() => this.rejectLeave(item)}><Fa icon="trash-o" /></Button></td>
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