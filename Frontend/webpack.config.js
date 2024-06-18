const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');

module.exports = {
  mode: 'development',
  entry: './src/index.js',
  output: {
    filename: 'main.js',
    path: path.resolve(__dirname, 'dist'),
  },
  plugins: [
    new HtmlWebpackPlugin({
      template: './src/index.html',
    }),
  ],
  module: {
    rules: [
      {
        test: /\.(js|jsxÀts|tsx)$/,
        exclude: /node_modules/,
        use: {
          loader: 'babel-loader',
        },
      },
      // ...
      {
        test: /\.css$/,
        use: [
          'style-loader', // Injects styles into DOM
          'css-loader', // Turns css into commonjs
          'postcss-loader', // Uses postcss (Tailwind CSS is a PostCSS plugin)
        ],
      },
    ],
  },
  resolve: {
    extensions: ['.js', '.jsx', '.tsx', '.ts'],
  },
  devServer: {
    static: {
      directory: path.join(__dirname, 'dist'),
    },
  },
};