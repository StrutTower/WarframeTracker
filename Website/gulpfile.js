/// <binding ProjectOpened='sass-watch' />
var gulp = require('gulp'),
    concat = require('gulp-concat'),
    sass = require('gulp-sass'),
    cleancss = require('gulp-clean-css'),
    terser = require('gulp-terser'),
    rename = require('gulp-rename'),
    stripComments = require('gulp-strip-comments'),
    merge = require('merge2');

//#region Options
var options = {
    js: {
        libFiles: [
            'jquery/dist/jquery.js',
            'jquery-validation/dist/jquery.validate.js',
            'jquery-validation-unobtrusive/dist/jquery.validate.unobtrusive.js',
            'popper.js/dist/umd/popper.js',
            'bootstrap/dist/js/bootstrap.js',
            'lobibox/dist/js/notifications.js',
            'select2/dist/js/select2.js',
            'inputmask/dist/inputmask/inputmask.js',
            'inputmask/dist/inputmask/jquery.inputmask.js',
            'disable-form-on-submit/src/disable-form-on-submit.js'
        ],
        workingDirectory: 'node_modules',
        libOutput: 'lib.js',
        appFiles: ['wwwroot/js/**/*.js'],
        output: 'bundle.min.js',
        dest: 'wwwroot/lib'
    },
    css: {
        libFiles: [
            //'@mdi/font/css/materialdesignicons.css',
            'select2/dist/css/select2.css',
            'lobibox/dist/css/lobibox.css',
            'inputmask/css/inputmask.css'
        ],
        workingDirectory: 'node_modules',
        sassSource: 'Sass/site.scss',
        sassFiles: 'Sass/**/*.scss',
        output: 'bundle.css',
        dest: 'wwwroot/lib'
    },
    devOnly: {
        files: [
            'node_modules/@mdi/font/css/materialdesignicons.css'
        ],
        output: 'devOnly.css',
        dest: 'wwwroot/lib'
    }
};
//#endregion

//#region Tasks
gulp.task('bundle-JS', function () {
    var appJS = gulp.src(options.js.appFiles);
    var libJS = gulp.src(options.js.libFiles, { cwd: options.js.workingDirectory })
        .pipe(concat(options.js.libOutput))
        .pipe(stripComments())
        .pipe(gulp.dest(options.js.dest));

    return merge(libJS, appJS)
        .pipe(concat(options.js.output))
        .pipe(terser())
        .pipe(gulp.dest(options.js.dest));
});

gulp.task('bundle-CSS', function () {
    var libCSS = gulp.src(options.css.libFiles, { cwd: options.css.workingDirectory });

    var siteCSS = gulp.src(options.css.sassSource)
        .pipe(sass({
            errLogToConsole: true
        }).on('error', sass.logError));

    return merge(libCSS, siteCSS)
        .pipe(concat(options.css.output))
        .pipe(gulp.dest(options.css.dest))
        .pipe(cleancss({ level: { 1: { specialComments: 0 } } }))
        .pipe(rename({ suffix: '.min' }))
        .pipe(gulp.dest(options.css.dest));
});

gulp.task('copy-fonts', function () {
    return gulp.src(options.devOnly.files)
        .pipe(concat(options.devOnly.output))
        .pipe(gulp.dest(options.devOnly.dest));
});

gulp.task('sass-watch', function () {
    gulp.watch(options.css.sassFiles, gulp.parallel('bundle-CSS'));
});

gulp.task('default', gulp.parallel('bundle-JS', 'bundle-CSS', 'copy-fonts'));
//#endregion