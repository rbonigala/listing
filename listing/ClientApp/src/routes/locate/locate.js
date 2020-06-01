import React, { Component } from 'react';
import GoogleMapReact from 'google-map-react';

const AnyReactComponent = ({ text }) => (<div style={{
    color: 'white',
    background: 'grey',
    padding: '15px 10px',
    display: 'inline-flex',
    textAlign: 'center',
    alignItems: 'center',
    justifyContent: 'center',
    borderRadius: '100%',
    transform: 'translate(-50%, -50%)'
}}>{text}</div>);

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
            google_api_key: 'AIzaSyBJwKoxNC5jEYGgC2B0FRfjowg5umG4kCo'            
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
        const url = 'https://0.0.0.0:8443/chargepoints';
        const options = {
            method: 'GET'
            //headers: {
            //    'Accept': 'application/json',
            //    'Content-Type': 'application/json;charset=UTF-8'
            //}
        };
        fetch(url, options)
            .then(response => {
                console.log(response.status);
            })
            .catch(error => console.error(error));
    }

    render() {
        return (
            <div style={{ height: '100vh', width: '100%' }}>
                <h1>Testing2</h1>
                <button onClick={this.getLocation}>Get coordinates</button>
                {   
                    this.state.latitude && this.state.longitude ?
                        
                            <GoogleMapReact
                            bootstrapURLKeys={{ key: this.state.google_api_key }}
                            defaultZoom={this.props.zoom}
                            defaultCenter={this.props.center}
                                
                            >
                                <AnyReactComponent
                                    lat={this.state.latitude}
                                    lng={this.state.longitude}                                    
                                    text="My Marker"
                            />
                            <AnyReactComponent
                                lat={-38.0856594}
                                lng={145.351561}
                                text="My Marker"
                            />
                            </GoogleMapReact>
                        
                        : 
                        <h1>No Coordinates</h1>
                }
            </div>
            
        );
    }


    
}

