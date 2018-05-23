var path = require("path");
var glob = require("glob");

module.exports = {
    entry:
        glob.sync("./wwwroot/scripts/src/**/*.js"),
    output: {
        path: path.resolve(__dirname, "wwwroot/scripts/dist"),
        filename: "bundle.js"
    },
    devtool: "source-maps",
    module: {
        rules: [
            {
                test: /\.js$/,
                exclude: /node_modules/,
                use: {
                    loader: "babel-loader"
                }
            }
        ]
    }
};