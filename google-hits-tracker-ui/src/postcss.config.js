/* eslint-disable */
const webpackPostcssTools = require('webpack-postcss-tools');
const cssNext = require('postcss-cssnext');
/* eslint-enable */

const map = webpackPostcssTools.makeVarMap('./src/variables.css');

module.exports = {
  plugins: [
    cssNext({
      features: {
        customProperties: {
          variables: map.vars,
        },
        customMedia: {
          extensions: map.media,
        },
        customSelectors: {
          extensions: map.selector,
        },
      },
    }),
  ],
};
