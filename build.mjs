import { build } from 'vite';
import { readdirSync, existsSync, statSync } from "fs";
import {resolve} from "path";
import vue from '@vitejs/plugin-vue';
import pkg from './package.json' assert { type: "json"};

// External dependencies
const external = Object.keys(pkg.dependencies);

// Base directory
const baseDir = process.cwd();

// Watch mode
const watchMode = process.argv.includes('--watch') ? {} : null;

// Globals used
const globals = pkg.globals;

var Modules = [];


const scanDirectories = (dir, fileType, modules, plugins) => {
  const files = readdirSync(dir);
  for (const file of files) {
    let filePath = resolve(dir, file);
    if(!statSync(filePath).isDirectory()) {
      continue;
    }
    const moduleEntryDirectory = resolve(filePath, 'build');
    if (!existsSync(moduleEntryDirectory)) {
      continue;
    }
    let moduleFileName = "module." + fileType;
    const moduleFile = resolve(moduleEntryDirectory, fileType, moduleFileName);
    if (!existsSync(moduleFile)) {
      continue;
    }

    const directoryName = file;
    const name = file.replace(/Mithril/gi,"").replace(/\./gi,"").toLowerCase();
    const entry = baseDir + "/" + directoryName + "/build/" + fileType + "/" + moduleFileName;
    const outputDir = resolve(dir, directoryName, 'wwwroot');
    modules.push({ directory: directoryName, entry: entry, input: moduleFileName, name: name, outputDir: outputDir, plugins: plugins });
  }

  return modules;
};

scanDirectories(baseDir, "ts", Modules, [vue()]);

for (let x = 0; x < Modules.length; ++x)
{
	let val = Modules[x];
	let output = val.outputDir || (baseDir  +"/"+val.directory + "/wwwroot/");
  await build({
    configFile: false,
    plugins: val.plugins,
    build: {
      watch: watchMode,
      lib: {
        entry: val.entry,
        name: val.name,
        fileName: val.name
      },
      rollupOptions: {
        external: external,
        output: {
          dir: output,
          entryFileNames:"js/[name].[format].min.js",
          assetFileNames: "css/[name].min.css",
          globals: globals
        }
      }
    }
  });
}