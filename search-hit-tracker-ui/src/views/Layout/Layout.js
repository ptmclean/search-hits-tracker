import React from 'react';
import PropTypes from 'prop-types';
import Icon from 'components/Icon/Icon.js';
import styles from './Layout.css';

const Layout = ({ children }) => (
  <React.Fragment>
    <div className={styles.header}>
      <div className={styles.headerContent}>
        <Icon icon="logoWhite" />
        <h3>SEO Test</h3>
      </div>
    </div>
    <div className={styles.body}>
      <div className={styles.pageContent}>
        {children}
      </div>
    </div>
  </React.Fragment>
);

Layout.propTypes = {
  children: PropTypes.node.isRequired,
};

export default Layout;
