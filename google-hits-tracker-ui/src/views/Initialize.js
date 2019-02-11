import PropTypes from 'prop-types';

const Initialize = ({ children }) => children;

Initialize.propTypes = {
  children: PropTypes.node.isRequired,
};

export default Initialize;
