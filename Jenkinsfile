stage("Build") {
    node {
        docker.image('microsoft/dotnet:sdk').inside {
        
            checkout scm

            sh 'dotnet restore'
            sh 'dotnet build'

            stash "${JOB_NAME}-${BRANCH_NAME}-${BUILD_ID}-Build"
        }
    }
}
stage('Test') {
    node {
        docker.image('microsoft/dotnet:sdk').inside {
            unstash "${JOB_NAME}-${BRANCH_NAME}-${BUILD_ID}-Build"
            sh 'dotnet test ./EZLogger.Test/EZLogger.Test.csproj'
            stash "${JOB_NAME}-${BRANCH_NAME}-${BUILD_ID}-Test"
        }
    }
}
stage('Deploy') {
    timeout()
    input {
        message "Deploy?"
        ok "Make it so."
    }

    node {
        docker.image('microsoft/dotnet:sdk').inside {
            if("${BRANCH_NAME}" == 'master') {
                tagName = ""
            } else {
                tagName = "${BUILD_NUMBER}"
            }

            unstash "${JOB_NAME}-${BRANCH_NAME}-${BUILD_ID}-Test"

            sh "dotnet pack --no-build --no-restore --configuration release --output ./out --version-suffix ${tagName} ./EZLogger/EZLogger.csproj"
            sh "dotnet pack --no-build --no-restore --configuration release --output ./out --version-suffix ${tagName} ./EZLogger.Console/EZLogger.Console.csproj"
            sh "dotnet pack --no-build --no-restore --configuration release --output ./out --version-suffix ${tagName} ./EZLogger.File/EZLogger.File.csproj"

            withCredentials([string(credentialsId: 'NUGET', variable: 'NUGET_API_KEY')]) {    
                sh "dotnet nuget push ./out/EZLogger.nupkg -k ${NUGET_API_KEY}"
                sh "dotnet nuget push ./out/EZLogger.Console.nupkg -k ${NUGET_API_KEY}"
                sh "dotnet nuget push ./out/EZLogger.File.nupkg -k ${NUGET_API_KEY}"
            }
        }
    }
}