import React, { Component } from 'react';

import { connect } from 'react-redux';

import SitesFetchers from '../State/Fetchers/Sites';
import Loadable from '../Components/Loadable.jsx';
import {
    List,
    Image,
    Input,
    Pagination,
    Icon,
    Divider,
    Header,
    Button
} from 'semantic-ui-react';
import { debounce } from '../Utils/Helpers';

class Home extends Component {
    constructor(props) {
        super(props);
        this.state = {
            page: 1,
            search: ''
        }
    }

    componentDidMount() {
        this.loadSites(this.state.page, this.state.search);
    }

    loadSites(page, search) {
        this.props.FetchSites(page, search);
    }

    render() {
        let data = this.props.Sites && this.props.Sites.Items;

        let paginator = (
            <Pagination
                activePage={(this.props.Sites && this.props.Sites.CurrentPage) || 1}
                ellipsisItem={{ content: <Icon name='ellipsis horizontal' />, icon: true }}
                firstItem={{ content: <Icon name='angle double left' />, icon: true, disabled: (this.props.Sites && !this.props.Sites.HasPrevious) || false }}
                lastItem={{ content: <Icon name='angle double right' />, icon: true, disabled: (this.props.Sites && !this.props.Sites.HasNext) || false }}
                prevItem={{ content: <Icon name='angle left' />, icon: true, disabled: (this.props.Sites && !this.props.Sites.HasPrevious) || false }}
                nextItem={{ content: <Icon name='angle right' />, icon: true, disabled: (this.props.Sites && !this.props.Sites.HasNext) || false }}
                totalPages={(this.props.Sites && this.props.Sites.TotalPages) || 0}
                secondary pointing size='mini'
                onPageChange={(e, d) => { this.setState({ page: d.activePage }); this.loadSites(d.activePage, this.state.search); }}
            />
        );

        return (
            <div>
                <Input fluid icon='search' placeholder='Search...' onChange={debounce((e, d) => { this.setState({ search: d.value }); this.loadSites(this.state.page, d.value); }, 1000)} />
                <Divider />
                <Loadable loads={[this.props.Sites]}>
                    <div>
                        {data && data.length == 0 && (<Header size='small'>No sites added yet</Header>)}
                        {data && data.length > 0 && this.props.Sites.TotalPages > 1 && paginator}
                        {this.props.IsAdmin && (
                            <Button icon basic color='black' onClick={() => this.props.history.push("/site/add")}>
                                <Icon.Group>
                                    <Icon name='world' />
                                    <Icon corner name='add' />
                                </Icon.Group>
                            </Button>
                        )}
                        <List divided verticalAlign='middle'>
                            {data && data.length > 0 && data.map(x => (
                                <List.Item key={x.Id}>
                                    {this.props.IsAdmin && (
                                        <List.Content floated='right'>
                                            <Icon link name='pencil' color='black' onClick={() => this.props.history.push("/site/edit/" + x.Id)} />
                                        </List.Content>
                                    )}
                                    <List.Icon name='world' size='large' verticalAlign='middle' />
                                    <List.Content>
                                        <List.Header as='a' onClick={() => this.props.history.push("/site/view/" + x.Id)}>{x.Name}</List.Header>
                                        <List.Description>{x.EndpointsCount} endpoint(s)</List.Description>
                                    </List.Content>
                                </List.Item>
                            ))}
                        </List>
                        {data && data.length > 0 && this.props.Sites.TotalPages > 1 && paginator}
                    </div>
                </Loadable>
            </div>
        );
    }
}

export default connect(
    state => ({
        Sites: state.Sites,
        IsAdmin: state.IsAdmin
    }),
    dispatch => ({
        FetchSites: SitesFetchers.RefreshSites(dispatch)
    })
)(Home);