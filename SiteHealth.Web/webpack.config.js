var path = require('path');
var webpack = require('webpack');

module.exports = {
    entry: './Scripts/app.js',
    output: { path: __dirname + '/dist/', publicPath: '/dist/', filename: 'bundle.js' },
    mode: 'development',
    devtool: 'source-map',
    module: {
        rules: [
            {
                test: /.jsx?$/,
                loader: 'babel-loader',
                exclude: /node_modules/,
                query: {
                    presets: ['es2015', 'react'],
                    plugins: ['transform-object-rest-spread']
                }
            },
            {
                test: /\.jpe?g$|\.gif$|\.png$|\.ttf$|\.eot$|\.svg$/,
                use: 'file-loader?name=[name].[ext]?[hash]'
            },
            {
                test: /\.woff(2)?(\?v=[0-9]\.[0-9]\.[0-9])?$/,
                loader: 'url-loader?limit=10000&mimetype=application/fontwoff'
            },
            {
                test: /\.css$/, loader: 'style-loader!css-loader'
            }
        ]
    }
};﻿