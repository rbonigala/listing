import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import  locate  from './routes/locate/Index';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout> 
            <Route path='/map' component={locate}/>
      </Layout>
    );
  }
}
