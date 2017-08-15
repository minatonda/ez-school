var webpack = require('webpack');
const fs = require("fs");
const path = require('path');

module.exports = {
    entry: {
        app: path.resolve(__dirname, "../src/app.ts"),
    },
    target: 'node',
    devtool: 'source-map',
    output: {
        filename: 'app.js',
        path: path.resolve(__dirname, '../public/'),
        devtoolModuleFilenameTemplate: function (info) {
            return info.resourcePath.replace("./", "../");
        }
    },
    devServer: {
        contentBase: path.resolve(__dirname, "public/")
    },
    resolve: {
        extensions: [".webpack.js", ".web.js", ".ts", ".tsx", ".js"]
    },
    module: {
        loaders: [
            // all files with a '.ts' or '.tsx' extension will be handled by 'ts-loader'
            { test: /\.tsx?$/, loader: "ts-loader", exclude: /node_modules/ },
        ]
    },
    externals: fs.readdirSync("node_modules")
        .reduce(function (acc, mod) {
            if (mod === ".bin") {
                return acc
            }

            acc[mod] = "commonjs " + mod
            return acc
        }, {})
}