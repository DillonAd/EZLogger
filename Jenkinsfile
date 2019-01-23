node {
    docker.image('microsoft/dotnet:sdk').inside {
        
        checkout scm

        stage("Build") {
            sh 'dotnet restore'
            sh 'dotnet build'
        }
        stage('Test') {
            sh 'dotnet test ./EZLogger.Test/EZLogger.Test.csproj'
        }
        stage('Deploy') {
            input {
                message "Deploy?"
                ok "Make it so."
            }

            if("${BRANCH_NAME}" == 'master') {
                tagName = ""
            } else {
                tagName = "${BUILD_NUMBER}"
            }

            sh "dotnet pack --no-build --no-restore --configuration release --output ./out --version-suffix ${tagName} ./EZLogger/EZLogger.csproj"
            sh "dotnet pack --no-build --no-restore --configuration release --output ./out --version-suffix ${tagName} ./EZLogger.Console/EZLogger.Console.csproj"
            sh "dotnet pack --no-build --no-restore --configuration release --output ./out --version-suffix ${tagName} ./EZLogger.File/EZLogger.File.csproj"

            sh "dotnet nuget push ./out/EZLogger.nupkg -k ${NUGET_API_KEY}"
            sh "dotnet nuget push ./out/EZLogger.Console.nupkg -k ${NUGET_API_KEY}"
            sh "dotnet nuget push ./out/EZLogger.File.nupkg -k ${NUGET_API_KEY}"
        }
    }
}