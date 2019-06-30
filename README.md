# EZLogger

[![Build Status](https://dev.azure.com/dillon-adams/GitHub/_apis/build/status/EZLogger?branchName=master)](https://dev.azure.com/dillon-adams/GitHub/_build/latest?definitionId=11&branchName=master) [![SonarCloud Coverage](https://sonarcloud.io/api/project_badges/measure?project=EZLogger&metric=coverage)](https://sonarcloud.io/dashboard?id=EZLogger)

EZLogger is a simple, easy logging framework. The whole idea is to cut down on setup time and get to actual programming.

Right now, this only supports logging to files, but I plan on enhancing the code to support other operations.

## Usage

Setup is made to be intuitive and easy. A `FileWriter` object will need to be created to specify what file will be used and what format will be output.
Once that is created, it can be passed to the `FileLogger` and then setup is done! You are ready to log!

``` cpp

IWriter fileWriter = new FileWriter("Log.log");
ILogger fileLogger = new Logger(fileWriter);

```

The other option is to use the extension methods provided with each implementation:

``` cpp

var host = new HostBuilder()
    .ConfigureServices(serviceCollection =>
    {
            serviceCollection.AddFileLogger("log.txt");
    })
    .Build();

```

## IWriter Implementations

The supported `IWriter` implementations are:
 - Console
 - File

Happy Logging!
