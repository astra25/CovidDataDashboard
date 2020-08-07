import React, { Component } from 'react';
import { UncontrolledDropdown, Dropdown, DropdownToggle, DropdownMenu, DropdownItem } from 'reactstrap';
import {
    ResponsiveContainer, ComposedChart, Line, Bar, Area, Scatter, XAxis,
    YAxis, ReferenceLine, ReferenceDot, Tooltip, Legend, CartesianGrid, Brush,
    LineChart
} from 'recharts';

export class CountryData extends Component {
    static displayName = CountryData.name;

    constructor(props) {
        super(props);

        this.toggle = this.toggle.bind(this);
        this.state = {
            dropDownOpen: false,
            code: 'OWID_WRL',
            continents: [],
            country: {},
            loading: true
        };
    }

    componentDidMount() {
        this.populateContinentData();
        this.populateCountryData(this.state.code);
    }

    toggle(e) {
        this.setState({ dropDownOpen: !this.state.dropDownOpen });
    }

    static renderComposedChart(country) {
        return (
            <div className="line-charts">
                <p>A simple ComposedChart of Line, Bar</p>
                <div className="composed-chart-wrapper">
                    <ComposedChart width={800} height={400} data={country.data}
                        margin={{ top: 20, right: 20, bottom: 20, left: 20 }}>
                        <XAxis dataKey="date" />
                        <YAxis />
                        <Legend />
                        <CartesianGrid stroke="#f5f5f5" />
                        <Tooltip />
                        <Bar dataKey="newCases" barSize={20} fill="#413ea0" />
                        <Line dataKey="newCases" type="monotone" stroke="#ff7300" />
                    </ComposedChart>
                </div>
            </div>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : CountryData.renderComposedChart(this.state.country);

        return (
            <div>
                <h1 id="tabelLabel">Covid Data By Country</h1>
                
                <div class="row">
                {
                    this.state.continents.map(cnt => (
                        <div class="col">
                            <UncontrolledDropdown>
                                <DropdownToggle caret>
                                    {cnt.name}
                                </DropdownToggle>
                                <DropdownMenu>
                                    {
                                        cnt.countries.map(ctr => (
                                            <DropdownItem onClick={() => this.populateCountryData(ctr.countryKey)}>{ctr.countryValue}</DropdownItem>
                                        ))
                                    }
                                </DropdownMenu>
                            </UncontrolledDropdown>
                        </div>
                    ))
                }
                </div>

                <h2>{this.state.country.location}</h2>
                <h3>{this.state.country.continent}</h3>
                <h3>{this.state.country.population}</h3>

                {/*
                <Dropdown isOpen={this.state.dropDownOpen} toggle={this.toggle}>
                    <DropdownToggle caret>
                        {this.state.country.location}
                    </DropdownToggle>
                    <DropdownMenu>
                        {codes.map(name => (
                            <DropdownItem onClick={() => this.populateCountryData(name)}>{name}</DropdownItem>
                        ))}
                    </DropdownMenu>
                </Dropdown>
                */}

                <p>This component demonstrates fetching data from the server.</p>
                {contents}
            </div>
        );
    }

    async populateContinentData() {
        try {
            const response = await fetch('continent?name=Africa&name=Asia&name=Oceania&name=Europe&name=North America&name=South America');
            const data = await response.json();
            this.setState({ continents: data });
        } catch (error) {
            console.log(error);
        }
    }

    async populateCountryData(code) {
        try {
            const response = await fetch('country?code=' + code);
            const data = await response.json();
            this.setState({ code: code, country: data, loading: false });
        } catch (error) {
            console.log(error);
        }
    }
}
