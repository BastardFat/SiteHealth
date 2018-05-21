import React, { Component } from 'react';

import { connect } from 'react-redux';

import SitesFetchers from '../State/Fetchers/Sites';
import Loadable from '../Components/Loadable.jsx';
import {
    Grid,
    Segment,
    Header,
    Icon,
    Form,
    Button,
    Input,
    Loader
} from 'semantic-ui-react';
import { bindModel } from '../Utils/Helpers';
import { cloneDeep } from 'lodash';
import Api from '../Utils/Api';
import { MappedRoutes } from '../Utils/Routes';

class EditSite extends Component {
    constructor(props) {
        super(props);

        this.state = {
            site: {
                Id: 0,
                Name: '',
                FetchIntervalInMinutes: 1,
                Endpoints: [this.defaultEndpoint()]
            },
            saving: false,
            adding: MappedRoutes.AddSite.url == props.match.path,
            editing: MappedRoutes.EditSite.url == props.match.path,
            loading: MappedRoutes.EditSite.url == props.match.path
        }
    }

    componentDidMount() {
        if (this.state.editing) {
            Api.Admin.EditSite(this.props.match.params.siteid)
                .then(data => this.setState({ site: data }))
                .finally(() => this.setState({ loading: false }));
        }
    }

    defaultEndpoint() {
        return { Name: '', Url: '', Id: 0 };
    }

    addEndpoint() {
        let newState = cloneDeep(this.state);
        newState.site.Endpoints.push(this.defaultEndpoint());
        this.setState(newState);
    }

    removeEndpoint(index) {
        let newState = cloneDeep(this.state);
        newState.site.Endpoints.splice(index, 1);
        if (newState.site.Endpoints.length < 1) {
            newState.site.Endpoints = [this.defaultEndpoint()]
        }
        this.setState(newState);
    }

    save() {
        this.setState({ saving: true });
        Api.Admin.SaveSite(this.state.site)
            .then(() => this.props.history.push("/"))
            .finally(() => this.setState({ saving: true }));
    }

    render() {
        if (this.state.loading)
            return (<Header size='small'><Icon loading name='spinner' /> Loading</Header>);



        let data = this.props.Sites && this.props.Sites.Items;

        const bind = bindModel(this);

        return (
            <React.Fragment>
                <Header as='h3'>Add site</Header>
                <Form onSubmit={() => this.save()}>
                    <Grid stackable columns={2}>
                        <Grid.Column>
                            <Grid verticalAlign='middle'>
                                <Grid.Column width={10}>
                                    <Form.Input fluid placeholder='Site name' required { ...bind('site', 'Name') } />
                                </Grid.Column>
                                <Grid.Column width={6}>
                                    <Form.Input fluid placeholder='Fetch interval' min={1} max={720} type='number' required { ...bind('site', 'FetchIntervalInMinutes') } />
                                </Grid.Column>
                            </Grid>
                        </Grid.Column>
                        <Grid.Column>
                            <Grid verticalAlign='middle'>
                                {this.state.site.Endpoints.map((endpoint, index) => (
                                    <Grid.Row key={index}>
                                        <Grid.Column width={5}>
                                            <Form.Input fluid placeholder='Endpoint name' required {  ...bind('site', 'Endpoints', index, 'Name') } />
                                        </Grid.Column>
                                        <Grid.Column width={9}>
                                            <Form.Input fluid placeholder='Endpoint URL' required {  ...bind('site', 'Endpoints', index, 'Url') } />
                                        </Grid.Column>
                                        <Grid.Column width={2}>
                                            <Button circular size='small' type='button' icon='close' onClick={() => this.removeEndpoint(index)} />
                                        </Grid.Column>
                                    </Grid.Row>
                                ))}
                                <Grid.Row>
                                    <Grid.Column width={16}>
                                        <Button color='green' type='button' onClick={() => this.addEndpoint()} disabled={this.state.saving} loading={this.state.saving}>
                                            Add endpoint
                                        </Button>
                                        <Button color='blue' type='submit' disabled={this.state.saving} loading={this.state.saving}>
                                            Save
                    </Button>
                                        <Button type='button' disabled={this.state.saving} loading={this.state.saving} onClick={() => this.props.history.push("/")}>
                                            Cancel
                    </Button>
                                    </Grid.Column>
                                </Grid.Row>
                            </Grid>

                        </Grid.Column>
                    </Grid>

                </Form>
            </React.Fragment>
        );
    }
}



export default EditSite;