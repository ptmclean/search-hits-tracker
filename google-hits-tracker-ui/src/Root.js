import React from 'react';
import { hot } from 'react-hot-loader';
import { BrowserRouter } from 'react-router-dom';
import Site from './views/Site';

const Root = () => (
  <BrowserRouter>
    <Site />
  </BrowserRouter>
);

export default hot(module)(Root);
