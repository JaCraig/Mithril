import resolve from '@rollup/plugin-node-resolve';
import commonjs from '@rollup/plugin-commonjs';
import typescript from 'rollup-plugin-typescript';
import pkg from './package.json' assert { type: "json" };
import terser from '@rollup/plugin-terser';
import postcss from 'rollup-plugin-postcss';
import autoprefixer from 'autoprefixer';
import postcssImport from 'postcss-import';
import VuePlugin from 'rollup-plugin-vue';
import multi from '@rollup/plugin-multi-entry';
import alias from '@rollup/plugin-alias';
import concat from './Mithril.Web.Common/build/rollup-plugin-concat.mjs';
import replace from '@rollup/plugin-replace';
import copy from "./Mithril.Web.Common/build/rollup-plugin-copy.mjs";

const external = Object.keys(pkg.dependencies);
const isProduction = !process.env.ROLLUP_WATCH;
const globals = { vue: 'Vue', moment: 'moment' };

var LessExports = [
    { directory: 'Mithril.Theme.Default', file: 'default' }
];

var TypeScriptExports = [
	{ directory: 'Mithril.Theme.Default', input: 'default.ts', name:'defaultTheme', output:'site.umd.min.js'  }
];

var fileExports = [
	{ source:'../../node_modules/@fortawesome/fontawesome-free/webfonts', destination:"../../Mithril.Theme.Default/wwwroot/webfonts" }
];

var JavaScriptExports = [

];

var DefaultExport = [];

for (let x = 0; x < LessExports.length; ++x)
{
	let val = LessExports[x];
	DefaultExport.push({
		input: val.directory + '/build/less/' + val.file + '.less',
		output: [{ file: val.directory + '/wwwroot/css/' + val.file + '.min.css' }],
		plugins: [postcss({ extract: true, plugins: [autoprefixer(), postcssImport()], minimize: true, sourceMap: true })]
	});
}

for (let x = 0; x < TypeScriptExports.length; ++x)
{
	let val = TypeScriptExports[x];
	let output = val.outputLocation || (val.directory + '/wwwroot/js/' + val.output);
	DefaultExport.push({
		input: val.directory + '/build/ts/' + val.input,
		external: external,
		output: [{ name: val.name, file: output, format: "umd", globals: globals, sourcemap: true , plugins: [terser()], strict: false}],
		plugins: [VuePlugin(),resolve(),typescript(),commonjs(),multi(),alias({ entries: [{ find: 'vue', replacement: 'vue/dist/vue.js' }]}),replace({'process.env.NODE_ENV': JSON.stringify(process.env.NODE_ENV || 'production'), preventAssignment: false})]
	});
}

for (let x = 0; x < JavaScriptExports.length; ++x)
{
	let val = JavaScriptExports[x];
	DefaultExport.push({
		input: [val.directory + '/build/js/' + val.input],
		output: [{ name: val.name, file: val.directory+'/build/Export.dummy.min.js', format: 'umd'}],
		plugins: [concat({ output: val.directory+'/wwwroot/js/'+val.output })]
	});
}

for (let x = 0; x < fileExports.length; ++x) {
	let val = fileExports[x];
	DefaultExport.push({
			input: "Mithril.Web.Common/build/dummy.js",
			output: {
				file: "Mithril.Web.Common/temp/dummy.js",
				format: "cjs"
			},
			plugins: [
				copy({ assets: [{ source: val.source, destination: val.destination }] })
			]
	});
}

export default DefaultExport;