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
                stash stashName
            }
        }
        stage('Test') {
            steps {
                unstash stashName
                sh 'dotnet test ./EZLogger.Test/EZLogger.Test.csproj'
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
                sh 'dotnet publish --no-restore'
            }
        }
    }
}