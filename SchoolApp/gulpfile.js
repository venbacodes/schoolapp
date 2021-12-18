/// <binding BeforeBuild='minify' Clean='clean' ProjectOpened='watch' />

const gulp = require("gulp"),
    concat = require("gulp-concat"),
    cleanCSS = require("gulp-clean-css"),
    uglify = require("gulp-uglify"),
    sass = require("gulp-dart-sass"),
    sourcemaps = require("gulp-sourcemaps"),
    del = require("del"),
    bundleconfig = require("./bundleconfig.json");

const paths = {
    webroot: "./wwwroot/",
    nodemodules: "./node_modules/",
    packageJson: "./package.json"
};

const regex = {
    css: /\.css$/,
    html: /\.(html|htm)$/,
    js: /\.js$/
};

paths.css = paths.webroot + "dist/css/";
paths.js = paths.webroot + "dist/js/";
paths.runtimedeps = paths.webroot + "node_modules/";
paths.sass = paths.webroot + "styles/**/*.scss";
paths.scripts = paths.webroot + "scripts/";

const taskNames = {
    compileSass: "compile:sass",
    sassWatch: "watch:sass",
    jsWatch: "watch:js",
    watch: "watch",
    cleanCss: "clean:css",
    cleanJs: "clean:js",
    cleanDeps: "clean:deps",
    clean: "clean",
    bundleJs: "bundle:js",
    bundleCss: "bundle:css",
    bundleAndMinifyJs: "minify:js",
    bundleAndMinifyCss: "minify:css",
    bundleAndMinify: "minify"
};

const getBundles = regexPattern => bundleconfig.filter(
    bundle => regexPattern.test(bundle.outputFileName),
);

gulp.task(taskNames.compileSass,
    () => gulp.src(paths.sass)
        .pipe(sourcemaps.init())
        .pipe(sass().on("error", sass.logError))
        .pipe(sourcemaps.write("."))
        .pipe(gulp.dest(paths.css)),
);

const bundleJsTasksName = [];
const bundleConfigsJs = getBundles(regex.js);

for (const bundleConfigKey in bundleConfigsJs) {
    if (!bundleConfigsJs.hasOwnProperty(bundleConfigKey)) {
        continue;
    }

    const taskName = `${taskNames.bundleJs}:${bundleConfigKey}`;

    gulp.task(taskName,
        () => {
            const bundle = bundleConfigsJs[bundleConfigKey];

            const inputs = [];
            for (const input of bundle.inputFiles) {
                inputs.push(`${paths.scripts}${input}`);
            }

            let gulpStream = gulp.src(inputs, { base: "." })
                .pipe(sourcemaps.init())
                .pipe(concat(`${paths.js}${bundle.outputFileName}`));

            if (bundle.minify && bundle.minify.enabled) {
                gulpStream = gulpStream.pipe(uglify());
            }

            return gulpStream.pipe(sourcemaps.write("."))
                .pipe(gulp.dest("."));
        }
    );

    bundleJsTasksName[bundleConfigKey] = taskName;
}

gulp.task(taskNames.bundleJs,
    bundleJsTasksName.length > 0
        ? gulp.parallel(...bundleJsTasksName)
        : done => {
            done();
        },
);

gulp.task(taskNames.bundleAndMinifyJs,
    gulp.series(
        taskNames.bundleJs
    ),
);

const bundleCssTasksName = [];
const bundleConfigsCss = getBundles(regex.css);

for (const bundleConfigKey in bundleConfigsCss) {

    if (!bundleConfigsCss.hasOwnProperty(bundleConfigKey)) {
        continue;
    }

    const taskName = `${taskNames.bundleCss}:${bundleConfigKey}`;

    gulp.task(taskName,
        () => {
            const bundle = bundleConfigsCss[bundleConfigKey];

            const inputs = [];
            for (const input of bundle.inputFiles) {
                inputs.push(`${paths.css}${input}`);
            }

            let gulpStream = gulp.src(inputs, { base: "." })
                .pipe(concat(`${paths.css}${bundle.outputFileName}`));

            if (bundle.minify && bundle.minify.enabled) {
                gulpStream = gulpStream
                    .pipe(sourcemaps.init())
                    .pipe(cleanCSS())
                    .pipe(sourcemaps.write());
            }

            return gulpStream.pipe(gulp.dest("."));
        },
    );

    bundleCssTasksName[bundleConfigKey] = taskName;
}

gulp.task(taskNames.bundleCss,
    bundleCssTasksName.length > 0
        ? gulp.parallel(...bundleCssTasksName)
        : done => {
            done();
        },
);

gulp.task(taskNames.bundleAndMinifyCss,
    gulp.series(
        gulp.parallel(
            taskNames.compileSass
        ),
        taskNames.bundleCss
    ),
);

gulp.task(taskNames.bundleAndMinify,
    gulp.parallel(
        taskNames.bundleAndMinifyJs,
        taskNames.bundleAndMinifyCss
    )
);

gulp.task(taskNames.cleanJs, () => del([paths.js]),);

gulp.task(taskNames.cleanCss, () => del([paths.css]),);

gulp.task(taskNames.cleanDeps, () => del([paths.runtimedeps]),);

gulp.task(taskNames.clean, gulp.parallel(taskNames.cleanJs, taskNames.cleanCss, taskNames.cleanDeps),);

gulp.task(taskNames.sassWatch, () => gulp.watch(paths.sass, gulp.parallel(taskNames.bundleAndMinifyCss)),);

gulp.task(taskNames.jsWatch, () => gulp.watch(paths.scripts, gulp.parallel(taskNames.bundleAndMinifyJs)),);

gulp.task(taskNames.watch, gulp.parallel(taskNames.sassWatch, taskNames.jsWatch),);