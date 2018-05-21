import React, { Component } from 'react';

import { connect } from 'react-redux';

import Loadable from '../Components/Loadable.jsx';
import {
    Header,
    Divider,
    Accordion,
    Icon,
    Label,
    Table,
} from 'semantic-ui-react';
import { keys } from 'lodash';
import Api from '../Utils/Api';

import {
    CartesianGrid,
    Legend,
    ResponsiveContainer,
    Scatter,
    ScatterChart,
    Tooltip,
    XAxis,
    YAxis,
} from 'recharts';

class ViewSite extends Component {
    constructor(props) {
        super(props);

        this.state = {
            site: {
                
            },
            activeIndex: -1,
            loading: true
        }
    }

    componentDidMount() {
        Api.Data.GetSite(this.props.match.params.siteid)
            .then(data => this.setState({ site: data }))
            .finally(() => this.setState({ loading: false }));
    }

    stringToLocaleDate(str) {
        return (new Date(str)).toLocaleString();
    }

    handleClickAccordion(index) {
        const { activeIndex } = this.state;
        const newIndex = activeIndex === index ? -1 : index

        this.setState({ activeIndex: newIndex })
    }

    render() {
        if (this.state.loading)
            return (<Header size='small'><Icon loading name='spinner' /> Loading</Header>);

        return (
            <React.Fragment>
                <Header as='h1'>{this.state.site.Name} <Header.Subheader>{this.state.site.EndpointsCount} endpoints</Header.Subheader></Header>
                <Divider />
                <Accordion fluid styled>
                    {this.state.site.Endpoints.map((endpoint, i) => (
                        <React.Fragment key={endpoint.Id}>
                            <Accordion.Title active={this.state.activeIndex === i} index={i} onClick={() => this.handleClickAccordion(i)}>
                                {endpoint.Uptime && (
                                    <Label color={endpoint.Uptime > 99.5 ? 'green' : (endpoint.Uptime > 98 ? 'yellow' : 'orange')}>
                                        {endpoint.Uptime}% uptime
                                    </Label>
                                ) || ''}
                                {!endpoint.Uptime && (
                                    <Label color='red'>
                                        No uptime
                                    </Label>
                                )}
                                <Header className='no-top-margin' as='h2'>
                                    {endpoint.Name}
                                    <Header.Subheader>
                                        {endpoint.Url}
                                    </Header.Subheader>
                                </Header>
                            </Accordion.Title>
                            <Accordion.Content active={this.state.activeIndex === i}>
                                <ResponsiveContainer width='95%' height={500} >
                                    <ScatterChart>
                                        <XAxis
                                            dataKey='Time'
                                            domain={['auto', 'auto']}
                                            name='Time'
                                            tickFormatter={(unixTime) => new Date(unixTime).toLocaleString()}
                                            type='number'
                                        />
                                        <YAxis dataKey='Response' name='Response time' unit='ms' />

                                        <Scatter
                                            data={endpoint.Chart.map(x => ({ ...x, Time: (new Date(x.Time)).getTime() }))}
                                            line={{ stroke: 'green' }}
                                            shape={() => ""}
                                            lineJointType='monotoneX'
                                            lineType='joint'
                                            name='Response time'
                                        />

                                    </ScatterChart>
                                </ResponsiveContainer>
                                <Table celled attached='top'>
                                    <Table.Header>
                                        <Table.Row>
                                            <Table.HeaderCell>Status codes since {this.stringToLocaleDate(endpoint.Stat.Since)}</Table.HeaderCell>
                                            <Table.HeaderCell></Table.HeaderCell>
                                        </Table.Row>
                                    </Table.Header>

                                    <Table.Body>
                                        {keys(endpoint.Stat.StatusCodes).map(x => (
                                            <Table.Row key={x}>
                                                <Table.Cell>{x}</Table.Cell>
                                                <Table.Cell>{endpoint.Stat.StatusCodes[x] / endpoint.Stat.TotalRequests}%</Table.Cell>
                                            </Table.Row>
                                        ))}
                                    </Table.Body>
                                </Table>
                                <Table celled attached='bottom'>
                                    <Table.Header>
                                        <Table.Row>
                                            <Table.HeaderCell>Errors since {this.stringToLocaleDate(endpoint.Stat.Since)}</Table.HeaderCell>
                                            <Table.HeaderCell></Table.HeaderCell>
                                        </Table.Row>
                                    </Table.Header>
                                    <Table.Body>
                                        {keys(endpoint.Stat.Errors).map(x => (
                                            <Table.Row key={x}>
                                                <Table.Cell>{x}</Table.Cell>
                                                <Table.Cell>{endpoint.Stat.Errors[x] / endpoint.Stat.TotalRequests}%</Table.Cell>
                                            </Table.Row>
                                        ))}
                                    </Table.Body>
                                </Table>
                            </Accordion.Content>
                        </React.Fragment>
                    ))}
                </Accordion>
            </React.Fragment>
        );
    }
}



export default ViewSite;