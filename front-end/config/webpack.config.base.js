const CopyWebpackPlugin = require('copy-webpack-plugin');
const ExtractTextPlugin = require('extract-text-webpack-plugin');
const helpers = require('./webpack.helper');

let config = {
    entry: {
        app: [helpers.fromRootPath("/src/app.ts")],
    },
    output: {
        path: helpers.fromRootPath("/dist/"),
        filename: "[name].js"
    },
    devtool: "source-map",
    resolve: {
        extensions: [".ts", ".js", ".html", ".js"],
        alias: {
            'vue$': 'vue/dist/vue.common.js'
        }
    },
    node: {
        fs: 'empty'
    },
    module: {
        rules: [
            { test: /\.ts$/, use: 'tslint-loader', exclude: /node_modules/, enforce: 'pre' },
            { test: /\.ts$/, use: "ts-loader", exclude: /node_modules/ },
            { test: /\.html$/, use: 'raw-loader', exclude: ['./src/index.html'] },
            {
                test: /\.css$/,
                use: ExtractTextPlugin.extract({ use: 'css-loader?importLoaders=1' }),
            },
            {
                test: /\.(sass|scss)$/,
                use: ExtractTextPlugin.extract(['css-loader', 'sass-loader'])
            },
            { test: /\.svg$/, use: 'url-loader?limit=65000&mimetype=image/svg+xml&name=public/fonts/[name].[ext]' },
            { test: /\.woff$/, use: 'url-loader?limit=65000&mimetype=application/font-woff&name=public/fonts/[name].[ext]' },
            { test: /\.woff2$/, use: 'url-loader?limit=65000&mimetype=application/font-woff2&name=public/fonts/[name].[ext]' },
            { test: /\.[ot]tf$/, use: 'url-loader?limit=65000&mimetype=application/octet-stream&name=public/fonts/[name].[ext]' },
            { test: /\.eot$/, use: 'url-loader?limit=65000&mimetype=application/vnd.ms-fontobject&name=public/fonts/[name].[ext]' }
        ],
    },
    plugins: [
        new CopyWebpackPlugin([
            { from: './src/index.html', to: './index.html' }
        ])
    ]
}

module.exports = config;