module.exports = function (grunt) {

    var path = require('path');

    var friendlyName = "Phone Manager";
    var friendlyFilename = "umbracophonemanager";

    var buildFolderName = "phonemanager";
    var tempFolderPath = "releases/temp/";
    var tempProjectFolderPath = tempFolderPath + buildFolderName + '/';
    var tempUmbracoFolderPath = tempFolderPath + buildFolderName + '_umbraco/';

    // Load the package JSON file
    var pkg = grunt.file.readJSON('build/' + buildFolderName + '/package.json');

    // get the root path of the project
    var projectRoot = pkg.name + '/';

    grunt.initConfig({
        pkg: pkg,
        clean: {
            files: [
                tempFolderPath, tempProjectFolderPath, tempUmbracoFolderPath
            ]
        },
        copy: {
            bacon: {
                files: [
                    {
                        expand: true,
                        cwd: projectRoot + 'obj/Release/',
                        src: [
                            pkg.name + '.dll',
                            pkg.name + '.xml'
                        ],
                        dest: tempProjectFolderPath + 'bin/'
                    },
                    {   // Files for Umbraco package - requires a flat folder structure
                        expand: true,
                        cwd: projectRoot + 'obj/Release/',
                        src: [
                            pkg.name + '.dll',
                            pkg.name + '.xml'
                        ],
                        dest: tempUmbracoFolderPath
                    }
                ]
            }
        },
        zip: {
            release: {
                cwd: tempProjectFolderPath,
                src: [
                    tempProjectFolderPath + '**/*.*'
                ],
                dest: 'releases/github/' + friendlyFilename + '.' + pkg.version + '.zip'
            }
        },
        umbracoPackage: {
            dist: {
                src: tempUmbracoFolderPath,
                dest: 'releases/umbraco',
                options: {
                    name: friendlyName,
                    version: pkg.version,
                    url: pkg.url,
                    license: pkg.license.name,
                    licenseUrl: pkg.license.url,
                    author: pkg.author.name,
                    authorUrl: pkg.author.url,
                    manifest: 'build/' + buildFolderName + '/package.xml',
                    outputName: friendlyFilename + '.' + pkg.version + '.zip'
                }
            }
        },
        nugetpack: {
            dist: {
                src: pkg.name + '/' + pkg.name + '.csproj',
                dest: 'releases/nuget/'
            }
        }
    });

    grunt.registerTask('release_phonemanager', ['clean', 'copy', 'zip', 'umbracoPackage', 'nugetpack', 'clean']);
    grunt.registerTask('default_phonemanager', ['release_phonemanager']);

};
