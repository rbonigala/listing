import React, { Component } from 'react';
import GoogleMapReact from 'google-map-react';

const AnyReactComponent = ({ text }) => (<div style={{
    color: 'white',
    background: 'green',
    padding: '5px 5px',
    display: 'inline-flex',
    textAlign: 'center',
    alignItems: 'center',
    justifyContent: 'center',
    borderRadius: '100%',
    transform: 'translate(-50%, -50%)'
}}>{text}</div>);

const axios = require('axios').default;

export default class locate extends Component {

    static defaultProps = {
        center: { lat: -37.896192, lng: 145.0377216 },
        zoom: 11
    }

    constructor(props){
        super(props);
        this.state = {
            latitude: null,
            longitude: null,
            google_api_key: 'AIzaSyAxP2ud0asDGy_dinK5D5KHao-f5bFlerg',
            charge_locations: []
        };
        this.getLocation = this.getLocation.bind(this);
        this.getCoordinates = this.getCoordinates.bind(this);
        this.handleLocationError = this.handleLocationError.bind(this);
        this.getChargePoints = this.getChargePoints.bind(this);
        
    }

    

    getLocation() {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(this.getCoordinates, this.handleLocationError);
        } else {
            alert('Geolocation is not supported by this browser');
        }
    }

    getCoordinates(position) {
        this.setState({
            latitude: position.coords.latitude,
            longitude: position.coords.longitude
        });
        console.log(this.state.latitude);
        console.log(this.state.longitude);
        this.getChargePoints();
        }
    

    handleLocationError(error) {
    switch (error.code) {
        case error.PERMISSION_DENIED:
            alert("User denied the request for Geolocation.");
            break;
        case error.POSITION_UNAVAILABLE:
            alert("Location information is unavailable.");
            break;
        case error.TIMEOUT:
            alert("The request to get user location timed out.");
            break;
        case error.UNKNOWN_ERROR:
            alert("An unknown error occurred.");
            break;
        default:
            alert("An unknown error occurred.");           
    }
}

    getChargePoints() {
        this.setState({
            charge_locations: [{ "latitude": "-38.027640", "longitude": "145.212350" },
                { "latitude": "-38.025860", "longitude": "145.203860" },
                { "latitude": "-38.0098923", "longitude": "145.1060457" },
                { "latitude": "-37.8632748", "longitude": "145.3528256" }
            ]
        })
    }

    render() {
        return (
            <div style={{ height: '100vh', width: '100%' }}>
                <h1>Testing2</h1>
                <button onClick={this.getLocation}>Get coordinates</button>
                {   
                    this.state.latitude && this.state.longitude && this.state.charge_locations ?
                        
                            <GoogleMapReact
                            bootstrapURLKeys={{ key: this.state.google_api_key }}
                            defaultZoom={this.props.zoom}
                            defaultCenter={this.props.center}
                                
                        >
                            {this.state.charge_locations.map(location => 
                                <AnyReactComponent
                                    lat={location.latitude}
                                    lng={location.longitude}
                                    text="My Marker"
                                />
                            )}

                            
                                
                            </GoogleMapReact>
                        
                        : 
                        <h1>No Coordinates</h1>
                }
            </div>
            
        );
    }


    
}

