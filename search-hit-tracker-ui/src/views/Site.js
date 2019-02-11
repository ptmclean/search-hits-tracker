import React from 'react';
import { Switch, Route } from 'react-router';
import Home from './Home/Home';
import Layout from './Layout/Layout';
import Initialize from './Initialize';

const Site = () => (
  <Initialize>
    <Layout>
      <Switch>
        <Route path="/" exact component={Home} />
      </Switch>
    </Layout>
  </Initialize>
);

export default Site;
