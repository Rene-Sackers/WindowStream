var gulp = require("gulp");
var sass = require("gulp-sass");
var rimraf = require("gulp-rimraf");
var rename = require("gulp-rename");
var autoprefixer = require("gulp-autoprefixer");
var sourcemaps = require("gulp-sourcemaps");

var paths = {
	scss: {
		src: "wwwroot/scss/*.scss",
		dest: "wwwroot/css/"
	},
	css: {
		src: "wwwroot/css/"
	}
};

function cleanStyles() {
	return gulp.src(paths.css.src)
		.pipe(rimraf());
}

function buildStyles() {
	return gulp.src(paths.scss.src)
		.pipe(sourcemaps.init())
		.pipe(sass())
		.pipe(autoprefixer())
		.pipe(rename("site.css"))
		.pipe(sourcemaps.write())
		.pipe(gulp.dest(paths.scss.dest));
}

function watch() {
	gulp.watch(paths.scss.src, buildStyles);
}

exports.watch = watch;
exports.default = buildStyles;
exports.clean = cleanStyles;