module.exports = function (grunt) {

    var path = require('path');

    var friendlyName = "Phone Manager Personalisation Groups plugin";
    var friendlyFilename = "umbracophonemanager.personalisationgroups";

    var buildFolderName = "phonemanager_personalisation";
    var tempFolderPath = "releases/temp/";
    var tempProjectFolderPath = tempFolderPath + buildFolderName + '/';

    // Load the package JSON file
    var pkg = grunt.file.readJSON('build/' + buildFolderName + '/package.json');

    // get the root path of the project
    var projectRoot = pkg.name + '/';

    grunt.initConfig({
        pkg: pkg,
        clean: {
            files: [
                tempFolderPath, tempProjectFolderPath
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
                    {
                        expand: true,
                        cwd: projectRoot + 'App_Plugins/UmbracoPersonalisationGroups/',
                        src: [
                            'package.manifest'
                        ],
                        dest: tempProjectFolderPath + 'App_Plugins/UmbracoPersonalisationGroups/'
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
                src: tempProjectFolderPath,
                dest: 'releases/umbraco',
                options: {
                    name: friendlyName,
                    version: pkg.version,
                    url: pkg.url,
                    iconUrl: 'https://raw.githubusercontent.com/willroscoe/UmbracoPhoneManager/master/assets/umbracophonemanager_icon64.png',
                    license: pkg.license.name,
                    licenseUrl: pkg.license.url,
                    author: pkg.author.name,
                    authorUrl: pkg.author.url,
                    readme: pkg.readme,
                    outputName: friendlyFilename + '.' + pkg.version + '.zip',
                    requirements: {major: 7, minor: 6, patch: 0}
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

    grunt.registerTask('release_personalisation_plugin', ['clean', 'copy', 'zip', 'umbracoPackage', 'nugetpack', 'clean']);
    grunt.registerTask('default_personalisation_plugin', ['release_personalisation_plugin']);

};
