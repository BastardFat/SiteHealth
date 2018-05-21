import React, { Component } from 'react';

import { connect } from 'react-redux';
import { Container, Divider, Header } from 'semantic-ui-react';

import SitesFetchers from '../State/Fetchers/Sites';
import UiView from '../Components/UiView.jsx';

class App extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <React.Fragment>
                <Header>Web Health Monitor</Header>
                <Divider />
                <Container>
                    <UiView />
                </Container>
            </React.Fragment>
        );
    }
}


export default App;