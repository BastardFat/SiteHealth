import React, { Component } from 'react';

import { Container, Divider, Header } from 'semantic-ui-react';
import { withRouter } from 'react-router-dom';
import UiView from '../Components/UiView.jsx';
import { connect } from 'react-redux';

function App(props) {
    return (
        <React.Fragment>
            <Header onClick={() => props.history.push("/admin")}>Web Health Monitor {props.IsAdmin && <Header.Subheader>(Admin)</Header.Subheader>}</Header>
            <Divider />
            <Container>
                <UiView />
            </Container>
        </React.Fragment>
    );
}


export default withRouter(connect(
    state => ({
        IsAdmin: state.IsAdmin
    }),
    dispatch => ({ })
)(App));