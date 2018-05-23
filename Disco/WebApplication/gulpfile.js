var gulp = require("gulp");

var concat = require("gulp-concat");
var sourcemaps = require("gulp-sourcemaps");

var webpack = require("webpack");
var webpackConfig = require("./webpack.config.js");

gulp.task("bundle-styles", function () {
    return gulp.src("wwwroot/styles/src/**/*.css")
        .pipe(sourcemaps.init({ loadMaps: true }))
        .pipe(concat("bundle.css"))
        .pipe(sourcemaps.write("./"))
        .pipe(gulp.dest("wwwroot/styles/dist"));
});

gulp.task("bundle-scripts", function () {
    webpack(webpackConfig, function (err, stats) {
    });
});