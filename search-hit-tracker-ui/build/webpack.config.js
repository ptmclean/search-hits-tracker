const path = require('path');
const CleanPlugin = require('clean-webpack-plugin');
const HtmlPlugin = require('html-webpack-plugin');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const OptimizeCssAssetsPlugin = require('optimize-css-assets-webpack-plugin');

const isProduction = process.env.NODE_ENV === 'production';

module.exports = {
  mode: process.env.NODE_ENV,

  entry: {
    app: ['@babel/polyfill', 'core-js/fn/array/flat-map', 'whatwg-fetch', './src/App.js'],
  },

  output: {
    publicPath: '/public/',
    filename: isProduction ? '[name].[chunkhash].js' : '[name].js',
  },

  module: {
    rules: [
      {
        test: /\.js$/,
        loader: isProduction ? 'babel-loader' : 'babel-loader?cacheDirectory',
        exclude: /node_modules/,
      },
      {
        test: /\.css$/,
        use: [
          {
            loader: isProduction ? MiniCssExtractPlugin.loader : 'style-loader',
          },
          {
            loader: 'css-loader',
            options: {
              modules: true,
              localIdentName: isProduction ? '[name]__[local]--[hash:base64:5]' : '[path][name]__[local]--[hash:base64:5]',
            },
          },
          {
            loader: 'postcss-loader',
          }
        ],
        exclude: /node_modules/,
      },
      {
        test: /\.svg$/,
        loader: 'svg-react-loader',
        exclude: /node_modules/,
      },
      {
        test: /\.(png|jpg|gif|woff2)$/,
        loader: 'file-loader?name=[name].[hash:base64:5].[ext]',
        exclude: /node_modules/,
      },
    ],
  },

  plugins: [
    new CleanPlugin('./dist', { root: `${process.cwd()}`, verbose: isProduction }),
    new HtmlPlugin({
      template: `${process.cwd()}/src/index.html`,
      favicon: './src/public/images/favicon.ico',
    }),
    ...(isProduction ? [new MiniCssExtractPlugin({ filename: '[name].[contenthash].css' }), new OptimizeCssAssetsPlugin()] : []),
  ],

  optimization: {
    splitChunks: {
      cacheGroups: {
        vendor: {
          test: /node_modules/,
          name: 'vendor',
          chunks: 'all',
        },
      },
    },
  },

  devServer: {
    https: true,
    port: 8080,
    hot: true,
    open: true,
    historyApiFallback: true,
    stats: 'errors-only',
    host: 'localhost',
    contentBase: path.join(__dirname, '../src/public'), proxy: [
      {
        context: '**',
        target: 'https://localhost:8080/public/index.html',
        ignorePath: true,
        secure: false,
      },
    ],
  },
};
