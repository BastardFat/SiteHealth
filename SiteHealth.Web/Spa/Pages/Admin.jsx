import React, { Component } from 'react';

import { connect } from 'react-redux';

import { ActionTypes } from '../State/Actions';
import { bindModel, debounce } from '../Utils/Helpers';
import Api from '../Utils/Api';
import Loadable from '../Components/Loadable.jsx';

import {
    Input,
    Icon,
    Header,
    Button,
    Form,
    Grid,
    Message
} from 'semantic-ui-react';

class Admin extends Component {
    constructor(props) {
        super(props);
        this.state = {
            inputs: {
                password: '',
                oldPassword: '',
                newPassword: ''
            },
            loading: false
        }
    }

    authorize() {
        this.setState({ loading: true });
        Api.Admin.GetToken(this.state.inputs.password)
            .then(token => this.props.SetToken(token))
            .finally(() => this.setState({ loading: false }));
    }

    changePassword() {
        this.setState({ loading: true });
        Api.Admin.ChangePassword(this.state.inputs.oldPassword, this.state.inputs.newPassword)
            .then(token => this.props.SetToken(token))
            .finally(() => this.setState({ loading: false }));
    }

    render() {
        if (this.state.loading)
            return (<Header size='small'><Icon loading name='spinner' /> Loading</Header>);

        const bind = bindModel(this);

        return (
            <React.Fragment>
                <Header as='h3'>Add site</Header>
                <Grid stackable columns={2}>
                    <Grid.Column>

                        {this.props.IsAdmin && (
                            <React.Fragment>
                                <Message info>
                                    <Message.Header>You are already logged in as admin</Message.Header>
                                    <p>You will automatically be logged out when you refresh page</p>
                                </Message>
                                <Button basic type='button' disabled={this.state.loading} loading={this.state.loading} onClick={() => this.props.history.push("/")}>
                                    Back
                                </Button>
                            </React.Fragment>
                        )}

                        {!this.props.IsAdmin && (
                            <Form onSubmit={() => this.authorize()}>
                                <Form.Input fluid placeholder='Admin password' required { ...bind('inputs', 'password') } />
                                <Button basic color='blue' type='submit' disabled={this.state.loading} loading={this.state.loading}>
                                    Log In
                                    </Button>
                                <Button basic type='button' disabled={this.state.loading} loading={this.state.loading} onClick={() => this.props.history.push("/")}>
                                    Back
                                </Button>
                            </Form>
                        )}

                    </Grid.Column>
                    <Grid.Column>
                        <Form onSubmit={() => this.changePassword()}>
                            <Form.Input fluid placeholder='Old password' required { ...bind('inputs', 'oldPassword') } />
                            <Form.Input fluid placeholder='New password' required { ...bind('inputs', 'newPassword') } />
                            <Button basic color='blue' type='submit' disabled={this.state.loading} loading={this.state.loading}>
                                Change password
                            </Button>
                        </Form>
                    </Grid.Column>
                </Grid>
            </React.Fragment>
        );
    }
}

export default connect(
    state => ({
        IsAdmin: state.IsAdmin
    }),
    dispatch => ({
        SetToken: (token) => dispatch(ActionTypes.Admin.SetToken.create(token))
    })
)(Admin);