const ModuleFederationPlugin = require("webpack/lib/container/ModuleFederationPlugin")

module.exports = {
    output: {
        publicPath: "auto",
        uniqueName: "cms_prortal",
        scriptType: "text/javascript"
    },
    optimization: {
        // Only needed to bypass a temporary bug
        runtimeChunk: false
    },
    devServer: {
        headers: {
          "Access-Control-Allow-Origin": "*",
          "Access-Control-Allow-Credentials": "true",
          "Access-Control-Allow-Methods": "GET,HEAD,OPTIONS,POST,PUT",
          "Access-Control-Allow-Headers": "Origin, X-Requested-With, Content-Type, Accept, Authorization"
        }
    },
    plugins: [
      // configure the plugin
        new ModuleFederationPlugin({
            // For remotes (please adjust)
            name: "cms_prortal",
            library: {
                type: "var",
                name: "cms_prortal"
            },
            filename: "remoteEntry.js",
            exposes: {
                './web-components': './src/bootstrap.ts',
            },
            shared: {
                "@angular/core": {
                    singleton: true
                },
                "@angular/common": {
                    singleton: true,
                },
                "@angular/router": {
                    singleton: true
                }
            }
        })
    ],
};
