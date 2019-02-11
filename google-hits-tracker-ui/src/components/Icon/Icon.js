import React, { Component } from 'react';
import PropTypes from 'prop-types';
import _ from 'lodash';
import cx from 'classnames';
import * as icons from './icons';
import styles from './Icon.css';

export default class Icon extends Component {
    static propTypes = {
      icon: PropTypes.string.isRequired,
      fallbackIcon: PropTypes.string,
      className: PropTypes.string,
    };

    static defaultProps = {
      className: null,
      fallbackIcon: 'defaultIcon',
    };

    static hasIcon = (icon) => {
      const IconComponent = icons[_.camelCase(icon)];

      return Boolean(IconComponent);
    };

    render() {
      const { icon, className, fallbackIcon, ...props } = this.props;

      const cxClassName = cx(styles.icon, className);

      const iconName = Icon.hasIcon(icon) ? icon : fallbackIcon;

      let IconComponent = icons[_.camelCase(iconName)];

      if (IconComponent == null) {
        console.error(`<Icon />: ${iconName} not found`); // eslint-disable-line
        IconComponent = icons.defaultIcon;
      }

      return <IconComponent {...props} className={cxClassName} />;
    }
}
