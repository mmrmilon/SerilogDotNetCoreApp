# Structured Logging with Serilog & Seq
Serilog is a feature-rich logging library in .NET that has gained popularity for its ability to log to multiple destinations, including files, the console, and other custom sinks. Serilog utilizes a variety of concepts such as sinks, enrichers, properties, and destructuring policies, among others, and offers the flexibility to configure its behavior either programmatically or through configuration files.

Howeve, what sets serilog apart is its built-in support for structured logging, which is a powerful technique for capturing and analyzing log data in a format that is easy to search, filter, and analyze. For more details visit serilog [page](https://serilog.net/)

# Installing Serilog
To install Serilog in an ASP.NET Core project, you can easily add the required package by using NuGet package manager.
```csharp
  Install-Package Serilog.AspNetCore
  ```
The most versatile approach for configuring Serilog is by utilizing application settings, which can be easily accomplished by invoking the <code>ReadFrom.Configuration()</code> method. This allows for centralized management of Serilog's configuration and simplifies the process of adjusting logging behavior.

To enable logging with Serilog, we can modify the **program.cs** file and add the necessary code to configure and initialize Serilog. This will allow us to start capturing and processing log events using Serilog's various sinks and enrichers.
```csharp
  var logger = new LoggerConfiguration()
      .ReadFrom.Configuration(builder.Configuration)
      .CreateLogger();
  builder.Logging.AddSerilog(logger);
```
# Configuring Appsettings.json
In order to incorporate Serilog into your application, it is necessary to add a dedicated section for Serilog in your appsettings.json configuration file. Here, we are configuring Serilog to use both the **console** and **file** sinks by adding them to the <code>Serilog.WriteTo</code> configuration section. 

Additionally, we are able to specify further settings for the **file** sink such as the output path, naming conventions for the log files, and the preferred log formatter. This level of customization and control over the logging output makes Serilog a powerful and flexible logging framework for .NET applications.
```csharp
  "Serilog": {
    "Using": [],
    "MinumumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning",
        "System": "Warning"
      }
    },
    "Enrich": [ "FromLogContext", "WithMachineName", "WithProcessId", "WithThreadId" ],
    "WriteTo": [
      {
        "Name": "Console",
        "Args": {
          "outputTemplate": "===> {Timestamp:G} [{Level}] {Message}{NewLine:1}{Exception:1}"
        }
      },
      {
        "Name": "File",
        "Args": {
          "path": "Logs\\log-.txt",
          "outputTemplate": "{Timestamp:G} {Message}{NewLine:1}{Exception:1}",
          "rollingInterval": "Day"
        }
      },
      {
        "Name": "File",
        "Args": {
          "path": "Logs\\log-.json",
          "formatter": "Serilog.Formatting.Json.JsonFormatter, Serilog",
          "rollingInterval": "Day"
        }
      }
    ]
  }
```
# Seq
Seq has excellent support for all of Serilog's features including complex event data, enrichment and structured events.
# Configuring Seq
To install Seq in an ASP.NET Core project, you can easily add the required package by using NuGet package manager.
```csharp
  Install-Package Serilog.Sinks.Seq
  Install-Package Serilog.Sinks.Console
  ```
# Configuring Appsettings.json
If you installed Seq to the default port you can use "http://your-seq-server:5341" as the Seq address.
```csharp
  "WriteTo": [      
      {
        "Name": "Seq",
        "Application": "SerilogDotNetCoreApp",
        "Args": {
          "serverUrl": "http://localhost:5341"
        }
      },
      .........
    ]
  ```

















