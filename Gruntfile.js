/// <binding />
module.exports = function (grunt) {

    require('./grunt_phonemanager.js')(grunt);
    //require('./grunt_phonemanager_personalisation.js')(grunt);

    grunt.loadNpmTasks('grunt-umbraco-package');
    grunt.loadNpmTasks('grunt-contrib-clean');
    grunt.loadNpmTasks('grunt-contrib-copy');
    grunt.loadNpmTasks('grunt-nuget');
    grunt.loadNpmTasks('grunt-zip');

    //grunt.registerTask('release_all', ['release_phonemanager', 'release_personalisation_plugin']);
    //grunt.registerTask('default_all', ['default_phonemanager', 'default_personalisation_plugin']);
};