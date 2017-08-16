const config = require("./webpack.config.base");
const env = require('../environment/prod.env');
const helpers = require('./webpack.helper');
const UglifyJsPlugin = require('uglifyjs-webpack-plugin');
const CompressionPlugin = require("compression-webpack-plugin");
const DefinePlugin = require('webpack').DefinePlugin;
const ExtractTextPlugin = require('extract-text-webpack-plugin');

config.entry.app.push(helpers.fromRootPath("/src/assets/sass/main.scss"));
config.plugins = [
    ...config.plugins,
    new ExtractTextPlugin({
        filename: 'bundle.css',
        allChunks: true,
    }),
    new DefinePlugin({
        'process.env': env
    })
];
module.exports = config;
