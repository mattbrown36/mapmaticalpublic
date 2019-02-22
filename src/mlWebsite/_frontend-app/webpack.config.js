var path = require('path');
var chalk = require('chalk');
var webpack = require('webpack');

//Figure out our build mode:
var isDev = false;
process.argv.forEach(function (val, index, array) {
    if (val == "--env.build=dev") {
        isDev = true;
    }
});

//Try to prevent mistaken builds:
if (isDev) {
    console.log(chalk.blue("Build Mode == DEV"));
}
else {
    console.log(chalk.red("Build Mode == PROD"));
}


//Set mode specific items:
var devtool = isDev ? 'source-map' : 'source-map';

//Main config:
module.exports =
    {
        context: path.join(__dirname, 'app'),
        entry: './main.js',
        output: {
            path: path.join(__dirname, '../wwwroot/app'),
            filename: 'mapmatical.bundle.js'
        },
        devtool: devtool,
        module: {
            loaders: [
                { test: /\.sass$/, loader: "style-loader!css-loader!sass-loader" },
                { test: /\.less$/, loader: "style-loader!css-loader!less-loader" },
                { test: /\.(woff|woff2)(\?v=\d+\.\d+\.\d+)?$/, loader: 'url-loader?limit=10000&mimetype=application/font-woff' },
                { test: /\.ttf(\?v=\d+\.\d+\.\d+)?$/, loader: 'url-loader?limit=10000&mimetype=application/octet-stream' },
                { test: /\.eot(\?v=\d+\.\d+\.\d+)?$/, loader: 'file-loader' },
                { test: /\.svg(\?v=\d+\.\d+\.\d+)?$/, loader: 'url-loader?limit=10000&mimetype=image/svg+xml' },
                { test: /\.css$/, loader: "style-loader!css-loader" },
                { test: /\.png$/, loader: "url-loader?limit=100000" },
                { test: /\.jpg$/, loader: "file-loader" },
                {
                    test: /\.js$/, loader: 'babel-loader',
                    exclude: /node_modules/,
                    query: { presets: ['es2015'] }
                },
                { test: /\.vue$/, loader: "vue-loader" }
            ]
        },
        plugins: [
            new webpack.ProvidePlugin({
                $: "jquery",
                jQuery: "jquery"
            })
        ]
    };