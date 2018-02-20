const helpers = require('./helpers');
const CopyWebpackPlugin = require('copy-webpack-plugin');
const ExtractTextPlugin = require('extract-text-webpack-plugin');

let config = {
    entry: {
        'main': helpers.root('/src/main.ts')
    },
    output: {
        path: helpers.root('/dist'),
        filename: 'js/[name].[hash].js'
    },
    devtool: 'source-map',
    resolve: {
        extensions: ['.ts', '.js', '.html'],
        alias: {
            'vue$': 'vue/dist/vue.esm.js',
        }
    },
    module: {
        rules: [
            { test: /\.vue$/, loader: 'vue-loader' },
            { test: /\.ts$/, exclude: /node_modules/, enforce: 'pre', loader: 'tslint-loader' },
            { test: /\.ts$/, exclude: /node_modules/, loader: 'awesome-typescript-loader' },
            { test: /\.html$/, loader: 'raw-loader', exclude: ['./src/index.html'] }
        ],
    },
    plugins: [
        new CopyWebpackPlugin([{
            from: 'src/assets',
            to: './assets'
        }, ]),
    ]
};

module.exports = config;