@ECHO OFF

dotnet watch --project ./EZLogger.Test/EZLogger.Test.csproj test /p:CollectCoverage=true /p:CoverletOutputFormat=lcov /p:CoverletOutput=./lcov.info 
