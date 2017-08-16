const webpack = require('webpack');
const config = require('./webpack.config.base');
const DefinePlugin = require('webpack/lib/DefinePlugin');
const env = require('../environment/dev.env');

config.module.rules = [{
        test: /\.ts$/,
        exclude: /node_modules/,
        use: 'ts-loader',
        query: {
            compilerOptions: {
                inlineSourceMap: true,
                sourceMap: false
            }
        }
    },
    {
        test: /\.html$/,
        use: 'raw-loader',
        exclude: ['./src/index.html']
    }
];

config.plugins = [...config.plugins,
    new webpack.SourceMapDevToolPlugin({
        filename: null, // if no value is provided the sourcemap is inlined
        test: /\.(ts|js)($|\?)/i
    }),
    new DefinePlugin({
        'process.env': env
    })
];

config.devtool = 'inline-source-map';

module.exports = config;
