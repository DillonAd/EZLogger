final stash_name = "${JOB_NAME}-${BUILD_ID}".replace('/','_')

stage("Build") {
    node {
        deleteDir()
        checkout scm

        sh 'dotnet restore'
        sh 'dotnet build --configuration Release'
        
        stash "${stash_name}-Build"
    }
}
stage('Test') {
    node {
        unstash "${stash_name}-Build"
        sh 'dotnet test ./EZLogger.Test/EZLogger.Test.csproj'
        stash "${stash_name}-Test"
    }
}
stage('Approval') {
    timeout(time: 7, unit: "DAYS") {
        input(message: "Deploy?", ok: "Make it so.")
    }
}
stage('Deploy') {
    node {
        if ("${BRANCH_NAME}" == 'master') {
            tagName = ""
        } else if ("${BRANCH_NAME}".contains("-PR-")) {
            tagName = "--version-suffix snapshot-${BUILD_TAG}"
        } else {
            return
        }

        unstash "${stash_name}-Test"

        sh "dotnet pack --configuration Release --output ../out ${tagName}"

        withCredentials([string(credentialsId: 'NUGET', variable: 'NUGET_API_KEY')]) {    
            sh "dotnet nuget push ./out/*.nupkg -k ${NUGET_API_KEY} --source https://api.nuget.org/v3/index.json"
        }
    }
}