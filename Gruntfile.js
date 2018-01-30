/// <binding />
module.exports = function (grunt) {

    var path = require('path');

    var friendlyName = ["Phone Manager", "Phone Manager Personalisation Groups plugin"];
    var friendlyFilename = ["umbracophonemanager", "umbracophonemanager.personalisationgroups"];
    var packageName = ["wr.umbracophonemanager", "wr.umbracophonemanager.personalisation"];
    var version = ["1.0.1", "1.0.1"];

    var buildFolderName = ["phonemanager", "phonemanager_personalisation"];
    var tempFolderPath = "releases/temp/";
    var tempProjectFolderPath = [tempFolderPath + buildFolderName[0] + '/', tempFolderPath + buildFolderName[1] + '/'];
    var tempUmbracoFolderPath = [tempFolderPath + buildFolderName[0] + '_umbraco/', tempFolderPath + buildFolderName[1] + '_umbraco/'];

    // Load the package JSON file
    var pkg = grunt.file.readJSON('./package.json');

    // get the root path of the project
    var projectRoot = [packageName[0] + '/', packageName[1] + '/'];

    grunt.initConfig({
        pkg: pkg,
        clean: {
            files: [
                tempFolderPath, tempProjectFolderPath[0], tempUmbracoFolderPath[0], tempProjectFolderPath[1], tempUmbracoFolderPath[1]
            ]
        },
        copy: {
            phonemanager: {
                files: [
                    {
                        expand: true,
                        cwd: projectRoot[0] + 'obj/Release/',
                        src: [
                            packageName[0] + '.dll',
                            packageName[0] + '.xml'
                        ],
                        dest: tempProjectFolderPath[0] + 'bin/'
                    },
                    {   // Files for Umbraco package - requires a flat folder structure
                        expand: true,
                        cwd: projectRoot[0] + 'obj/Release/',
                        src: [
                            packageName[0] + '.dll',
                            packageName[0] + '.xml'
                        ],
                        dest: tempUmbracoFolderPath[0]
                    }
                ]
            },
            plugin: {
                files: [
                    {
                        expand: true,
                        cwd: projectRoot[1] + 'obj/Release/',
                        src: [
                            packageName[1] + '.dll',
                            packageName[1] + '.xml'
                        ],
                        dest: tempProjectFolderPath[1] + 'bin/'
                    },
                    {
                        expand: true,
                        cwd: projectRoot[1] + 'App_Plugins/UmbracoPersonalisationGroups/',
                        src: [
                            'package.manifest'
                        ],
                        dest: tempProjectFolderPath[1] + 'App_Plugins/UmbracoPersonalisationGroups/'
                    }
                ]
            }
        },
        umbracoPackage: {
            phonemanager: {
                src: tempUmbracoFolderPath[0],
                dest: 'releases/umbraco',
                options: {
                    name: friendlyName[0],
                    version: version[0],
                    url: pkg.url,
                    license: pkg.license.name,
                    licenseUrl: pkg.license.url,
                    author: pkg.author.name,
                    authorUrl: pkg.author.url,
                    manifest: 'build/' + buildFolderName[0] + '/package.xml',
                    outputName: friendlyFilename[0] + '.' + version[0] + '.zip'
                }
            },
            plugin: {
                src: tempProjectFolderPath[1],
                dest: 'releases/umbraco',
                options: {
                    name: friendlyName[1],
                    version: version[1],
                    url: pkg.url,
                    iconUrl: 'https://raw.githubusercontent.com/willroscoe/UmbracoPhoneManager/master/assets/umbracophonemanager_icon64.png',
                    license: pkg.license.name,
                    licenseUrl: pkg.license.url,
                    author: pkg.author.name,
                    authorUrl: pkg.author.url,
                    readme: "This package requires both Phone Manager and Personalisation Groups packages to be installed before installing. For setup instructions and to inspect the source code, visit: https://github.com/willroscoe/UmbracoPhoneManager",
                    outputName: friendlyFilename[1] + '.' + version[1] + '.zip',
                    requirements: { type: 'strict', major: 7, minor: 6, patch: 0 }
                }
            }
        },
        nugetpack: {
            phonemanager: {
                src: packageName[0] + '/' + packageName[0] + '.csproj',
                dest: 'releases/nuget/'
            },
            plugin: {
                src: packageName[1] + '/' + packageName[1] + '.csproj',
                dest: 'releases/nuget/'
            }
        }
    });

    grunt.registerTask('release_phonemanager', ['clean', 'copy', 'umbracoPackage', 'nugetpack', 'clean']);
    grunt.registerTask('default', ['release_phonemanager']);

    grunt.loadNpmTasks('grunt-umbraco-package');
    grunt.loadNpmTasks('grunt-contrib-clean');
    grunt.loadNpmTasks('grunt-contrib-copy');
    grunt.loadNpmTasks('grunt-nuget');
};