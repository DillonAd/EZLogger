final String stashName = "EZLogger-" + env.BUILD_NUMBER;

pipeline {
    //agent {
    //    docker { image 'microsoft/dotnet:latest' }
    //}
    agent any

    stages {
        stage('Build') {
            steps {
                checkout scm
                sh 'dotnet restore'
                sh 'dotnet build'
                sh 'dotnet pack --no-build --no-restore'
                //sh 'dotnet push --no-restore --version-suffix ' + env.BUILD_NUMBER
                stash stashName
            }
        }
        stage('Test') {
            steps {
                unstash stashName
                sh 'dotnet test'
                stash stashName
            }
        }
        stage('Publish') {
            input {
                message "Deploy?"
                ok "Make it so."
                submitter "dillon"
            }
            steps {
                unstash stashName
                sh 'dotnet pack --no-build --no-restore'
                //sh 'dotnet publish'
            }
        }
    }
}