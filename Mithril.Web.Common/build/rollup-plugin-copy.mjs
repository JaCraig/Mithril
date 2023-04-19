"use strict";

import fs from "fs-extra";
import path from "path";

/**
 * Takes an array of files/directories to copy to the final build directory.
 * @param {Object} options The options object.
 * @param {string[]} options.assets A list of { source: '', destination: ''} objects.
 * @return {Object} The rollup code object.
 */
export default function copy(options = { assets: [] }) {
    const { assets } = options;
    let basedir = "";
    return {
        name: "copy-assets",
        options({ input }) {
            // Cache the base directory so we can figure out where to put assets.
            if (Array.isArray(input)) {
                basedir = path.dirname(input[0]);
            } else if (typeof input === "object") {
                basedir = path.dirname(input[Object.keys(input)[0]]);
            } else {
                basedir = path.dirname(input);
            }
        },
        async generateBundle({ file, dir }) {
            const outputDirectory = dir || path.dirname(file);
            return Promise.all(
                assets.map(async asset => {
                    try {
                        const assetSourceChildPath = asset.source
                            .replace(
                                path.basename(basedir),
                                ""
                            );
                        const assetDestinationPath = asset.destination
                            .replace(
                                path.basename(basedir),
                                ""
                            );
                        const destination = path.join(outputDirectory, assetDestinationPath);
                        const normalizedAssetPath = path.normalize(basedir);
                        const source = path.join(normalizedAssetPath, assetSourceChildPath);
                        const destinationIsDir = path.extname(destination) === "";
                        const destinationExists = await fs.pathExists(destination);
                        if (destinationIsDir) {
                            if (destinationExists) await fs.emptyDir(destination);
                        } else {
                            if (destinationExists) {
                                await fs.mkdirs(path.dirname(destination));
                                await fs.remove(destination);
                            }
                        }
                        this.warn(`Copying assets from ${source} to ${destination}`);
                        return await fs.copy(source, destination, {
                            overwrite: true,
                            errorOnExist: false,
                        });
                    } catch (e) {
                        this.warn(`Could not copy ${asset} because of an error: ${e}`);
                    }
                })
            );
        },
    };
}