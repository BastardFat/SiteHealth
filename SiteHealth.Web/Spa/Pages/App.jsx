import React, { Component } from 'react';

import { connect } from 'react-redux';
import { Container } from 'semantic-ui-react';

import SitesFetchers from '../State/Fetchers/Sites';
import UiView from '../Components/UiView.jsx';

class App extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <Container>
                <UiView />
            </Container>
        );
    }
}


export default App;