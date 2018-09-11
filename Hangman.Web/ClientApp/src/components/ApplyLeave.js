import React, { Component } from 'react';
import { UserLayout } from './UserLayout';
import { Button, Container, Input, Table, TableBody, TableHead, Fa, Modal, ModalBody, ModalHeader, ModalFooter } from 'mdbreact';
import swal from 'sweetalert';
import './ApplyLeave.css';

export class ApplyLeave extends Component {

    constructor(props) {
        super(props);
        let $this = this;
        $this.state =
            {
                userLeave: [],
                userid: '',
                username: '',
                fromdate: '',
                todate: '',
                reason: '',
                modal: false
            };
        if (localStorage.getItem('auth_token') !== null) {
            var tokens = JSON.parse(localStorage.auth_token);
            $this.state.username = tokens;
            $this.state.userid = tokens.id;
        }
        $this.handleChange = $this.handleChange.bind($this);
        $this.applyLeave = $this.applyLeave.bind($this);
        $this.getLeaveDetails($this.state.userid);
    }
    toggle = () => {
        this.setState({
            modal: !this.state.modal
        });
    }
    handleChange(e) {
        this.setState({
            [e.target.name]: e.target.value
        });
    }
    getLeaveDetails(userid) {
        let $this = this;
        fetch(`https://localhost:44321/api/user/GetLeaveById/${userid}`, {
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
    applyLeave(e) {
        e.preventDefault();
        let $this = this;
        let userid = $this.state.userid;
        let fromdate = $this.state.fromdate;
        let todate = $this.state.todate;
        let reason = $this.state.reason;
        fetch('https://localhost:44321/api/user/applyleave', {
            method: 'POST',
            body: JSON.stringify({ identityid: userid, from: fromdate, to: todate, reason: reason, status: 'unapproved' }),
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + $this.state.username.auth_token
            }
        }).then(function (response) {
            if (response.status === 200) {
                response.json().then(function (json) {
                    swal("Leave Applied Successfully", "", "error");
                    $this.getLeaveDetails(userid);
                }).catch(function (ex) {
                    console.log(ex.message);
                });
            } else if (response.status === 403) {
                swal("Invalid username or password", "", "error");
            }
        });
    }
    cancelLeave(item) {
        let $this = this;
        let userid = item.id;
        let identityid = $this.state.userid;
        let fromdate = item.from;
        let todate = item.to;
        let reason = item.reason;
        let status = 'deleted';
        fetch('https://localhost:44321/api/user/cancelleave', {
            method: 'POST',
            body: JSON.stringify({ id: userid, identityid: identityid, from: fromdate, to: todate, reason: reason, status: status }),
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + $this.state.username.auth_token
            }
        }).then(function (response) {
            if (response.status === 200) {
                response.json().then(function (json) {
                    swal("Leave Canceled Successfully", "", "error");
                    $this.getLeaveDetails(identityid);
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
            <UserLayout>
                <div className="apply-leave-comp">
                    <Container>
                        <Button onClick={this.toggle}>Apply</Button>
                        <Modal isOpen={this.state.modal} toggle={this.toggle} className="flex-center modals" size="lg">
                            <ModalHeader toggle={this.toggle}>Apply Leave</ModalHeader>
                            <ModalBody>
                                <form className="apply-leave-form">
                                    <div className="grey-text">
                                        <Input name="username" value={this.state.username.username} onChange={this.handleChange} label="Your Name" icon="user" group type="text" validate error="wrong" success="right" disabled />
                                        <Input name="fromdate" value={this.state.fromdate} onChange={this.handleChange} label="From" icon="date" group type="date" validate error="wrong" success="right" />
                                        <Input name="todate" value={this.state.todate} onChange={this.handleChange} label="To" icon="date" group type="date" validate error="wrong" success="right" />
                                        <Input name="reason" value={this.state.reason} onChange={this.handleChange} label="Reason" group type="email" validate error="wrong" success="right" />
                                    </div>
                                </form>   
          </ModalBody>
                            <ModalFooter>
                                <Button color="secondary" onClick={this.toggle}>Close</Button>{' '}
                                <Button color="cyan" type="button" onClick={this.applyLeave}>Apply</Button>
                            </ModalFooter>
                        </Modal>      
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
                                            <td><Button color="primary" title="Cancel User" onClick={() => this.cancelLeave(item)}><Fa icon="trash-o" /></Button></td>
                                        </tr>
                                    );
                                })}
                            </TableBody>
                        </Table>
                    </Container>
                </div>
            </UserLayout>
        );
    }
}