var gulp = require("gulp");
var concat = require("gulp-concat");
var sourcemaps = require("gulp-sourcemaps");

gulp.task("bundle-styles", function () {
    return gulp.src("wwwroot/styles/**/*.css")
        .pipe(sourcemaps.init({ loadMaps: true }))
        .pipe(concat("bundle.css"))
        .pipe(sourcemaps.write("./"))
        .pipe(gulp.dest("wwwroot/styles/dist"));
});