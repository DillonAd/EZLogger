# EZLogger

EZLogger is a simple, easy logging framework. The whole idea is to cut down on setup time and get to actual programming.

Right now, this only supports logging to files, but I plan on enhancing the code to support other operations.

## Usage

Setup is made to be intuitive and easy. A `FileWriter` object will need to be created to specify what file will be used.
Once that is created, it can be passed to the `FileLogger` and then setup is done! You are ready to log!

``` cpp

IWriter fileWriter = new FileWriter("Log.log");
IFormatter formatter = new Formatter();
ILogger fileLogger = new Logger(formatter, fileWriter);

`

Happy Logging!
