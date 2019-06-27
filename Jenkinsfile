final stash_name = "${JOB_NAME}-${BUILD_ID}".replace('/','_')

node {
    stage ("SCM") {
        deleteDir()
        checkout scm
    }
    docker.image('dillonad/dotnet-sonar:2.1').inside {
        stage("Setup") {
            withCredentials([string(credentialsId: 'SonarCloud_EZLogger', variable: 'SONAR_API_KEY')]) {
                sh 'dotnet-sonarscanner begin /k:"EZLogger" /o:dillonad-github /d:sonar.host.url="https://sonarcloud.io" /d:sonar.login="${SONAR_API_KEY}"'
            }
        }
        stage("Build") {
            sh 'dotnet build --configuration Release -v diag $WORKSPACE/EZLogger.sln'
            
            stash "${stash_name}-Build"
        }
        stage('Test') {
            unstash "${stash_name}-Build"
            sh 'dotnet test ./EZLogger.Test/EZLogger.Test.csproj'
            stash "${stash_name}-Test"
        }
        stage('Analyze') {
            withCredentials([string(credentialsId: 'SonarCloud_EZLogger', variable: 'SONAR_API_KEY')]) {
                sh 'dotnet-sonarscanner end /d:sonar.login="${SONAR_API_KEY}"'
            }
        }
        stage('Deploy') {
            if ("${BRANCH_NAME}" == 'master') {
                tagName = ""
            } else if ("${BRANCH_NAME}".startsWith("PR-")) {
                tagName = "--version-suffix snapshot-${BUILD_TAG}"
            } else {
                println "Build is not for a Pull Request or the main line of development. Skipping publish. Branch Name: ${BUILD_TAG}"
                return
            }

            unstash "${stash_name}-Test"

            sh "dotnet pack --configuration Release --output ../out ${tagName}"
    
            def files = findFiles(glob: '**/*.nupkg')
            def exceptions = "";

            withCredentials([string(credentialsId: 'NUGET', variable: 'NUGET_API_KEY')]) {
                files.each { file ->
                    try {
                        sh "dotnet nuget push ${file} -k ${NUGET_API_KEY} --source https://api.nuget.org/v3/index.json"
                    } catch(Exception ex) {
                        println ex
                        exceptions += "${ex}\n"
                    }
                }
            }

            if (exceptions?.trim()) {
                throw new Exception(exceptions)
            }
        }
    }
}