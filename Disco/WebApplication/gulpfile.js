var gulp = require("gulp");
var concat = require("gulp-concat");
var sourcemaps = require("gulp-sourcemaps");
var del = require("del");

gulp.task("bundle-styles", function () {
    del("wwwroot/styles/dist/bundle.css");
    del("wwwroot/styles/dist/bundle.css.map");

    return gulp.src("wwwroot/styles/**/*.css")
        .pipe(sourcemaps.init({ loadMaps: true }))
        .pipe(concat("bundle.css"))
        .pipe(sourcemaps.write("./"))
        .pipe(gulp.dest("wwwroot/styles/dist"));
});