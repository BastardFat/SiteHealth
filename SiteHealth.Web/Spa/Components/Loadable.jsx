import React from 'react';

import { Dimmer, Header, Message, Icon } from 'semantic-ui-react';

function Loadable({ children, loads = [] }) {

    let isLoading = loads.some(x => window.isNotLoaded(x));
    let isError = loads.some(x => window.isErrored(x));
    let firstError = loads.find(x => window.isErrored(x));

    let loader = <Header size='small'><Icon loading name='spinner' /> Loading</Header>;
    let error = (
        <Message negative>
            <Message.Header>There was some error</Message.Header>
            <p>{firstError && firstError.error}</p>
        </Message>
    );

    if (isLoading) return loader;
    return isError ? error : children ;
}


export default Loadable;