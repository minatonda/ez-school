const config = require("./webpack.config.base");
const env = require('../environment/dev.env');
const helpers = require('./webpack.helper');
const DefinePlugin = require('webpack/lib/DefinePlugin');
const BrowserSyncPlugin = require('browser-sync-webpack-plugin');
const ExtractTextPlugin = require('extract-text-webpack-plugin');
const HotModuleReplacementPlugin = require('webpack').HotModuleReplacementPlugin;

config.entry.app.push(helpers.fromRootPath("/src/assets/sass/main.scss"));
config.entry.app.unshift("webpack-dev-server/client?http://localhost:8080/");

config.plugins = [
    ...config.plugins,
    new ExtractTextPlugin({
        filename: 'bundle.css',
        allChunks: true,
    }),
    new DefinePlugin({
        'process.env': env
    }),
    new HotModuleReplacementPlugin({
        // Options...
    })
];


config.devServer = {
    //https: true,
    port: 8080,
    host: "localhost",
    historyApiFallback: true,
    watchOptions: { aggregateTimeout: 300, poll: 1000 },
    contentBase: helpers.fromRootPath('/dist'),
    hot: true,
    inline: true,
    open: false,
    openPage: ''
}

module.exports = config;