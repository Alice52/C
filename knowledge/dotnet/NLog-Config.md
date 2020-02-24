### elements: use the following elements to customize log config for scenarios.

1. `targets` – defines log targets/outputs
2. `rules` – defines log routing rules
3. `extensions` – loads NLog extensions from the *.dll file
4. `include`– includes external configuration file
5. `variable` – sets the value of a configuration variable


### Rules

1. `name` – logger name filter - may include wildcard characters `*, ?`
2. `minlevel` – minimal level to log
3. `maxlevel` – maximum level to log
4. `level, levels`
5. `writeTo` – targets to write to
6. `final` – no rules are processed after a final rule matches
7. `enabled` - set to `false` to disable the rule without deleting it

#### Customize Rules with Wildcard `*, ?` 

1. disable linq in log

```xml
 <!-- rules to map from logger name to target -->
<rules>
    <logger name="*" minlevel="Trace" writeTo="allfile"/>

    <logger name="Microsoft.EntityFrameworkCore.Model.Validation" minlevel="Warn" final="true"/>
    <logger name="Microsoft.EntityFrameworkCore.Database.Command" minlevel="Info" final="true"/>
    <logger name="*" minlevel="Info" writeTo="infofile"/>
</rules>
```

### Target

1. `name` - name of the target
2. `layout` - text to be rendered
3. `encoding` 
4. `archiveAboveSize` -  size in bytes, and above it, log files will be automatically archived.
5. `maxArchiveFiles` - max number of archive files, and above it, log files will be automatically rolled.
6. `archiveFileName ` - name of archive file
7. `archiveNumbering` - way file archives are numbered, recommand use `Rolling`, which is numbering
8. `archiveOldFileOnStartup` - auto achive old log file on startup
9. `fileName` - named file to write to
10. `createDirs` 
11. `concurrentWrites` - concurrent writes to same log file and defalut `true`
12. `keepFileOpen` - keep log file open instead of opening and closing it on each logging event and default: `false`
13. `enableArchiveFileCompression` - compress the archive files into the zip files, and default `false`

#### Customize Roll

1. daily roll:
   - fileName: can use the parameter to daily roll
   ```xml 
   fileName="${logDirectory}/${serviceName}-${shortdate}-all.log"
   ```

2. size roll:
   - archiveAboveSize: Size in bytes
   ```xml 
   archiveAboveSize="20000000"
   ```
   - maxArchiveFiles: max file number for each `fileName`
   ```xml 
   maxArchiveFiles="50"
   ```
     > according by combination of fileName, such as `VeloConnector.Background-2019-12-27-info.log`
     > if this log size more than 20M, it will be backed and renamed with VeloConnector.Background-2019-12-27-info`INTEGER`.log

### Level:

1. Trace
2. Debug
3. Info
4. Warn
5. Error
6. Fatal
7. Off

### Sample

```xml
<?xml version="1.0" encoding="utf-8" ?>
<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
    xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" autoReload="true" internalLogLevel="info" internalLogFile="Log\internal-nlog.log">

    <!-- log file directory -->
    <variable name="logDirectory" value="Log"/>

    <!-- log file name prefix -->
    <variable name="serviceName" value="VeloSimulator.PatronManagement"/>

    <!-- enable asp.net core layout renderers -->
    <extensions>
        <add assembly="NLog.Web.AspNetCore"/>
    </extensions>

    <!-- the targets to write to -->
    <targets async="true">
        <!-- write logs to file  -->
        <target xsi:type="File" name="tracefile" fileName="${logDirectory}/${serviceName}-${shortdate}-trace.log" archiveAboveSize="20000000" archiveNumbering="Rolling" maxArchiveFiles="50" concurrentWrites="false" keepFileOpen="false" encoding="utf-8" createDirs="true" layout="${longdate}|${pad:padding=-5:inner=${level:uppercase=true}}|thread-${threadid}|${logger}|${message} ${exception:format=tostring}" />
        <target xsi:type="File" name="infofile" fileName="${logDirectory}/${serviceName}-${shortdate}-info.log" archiveAboveSize="20000000" archiveNumbering="Rolling" maxArchiveFiles="50" concurrentWrites="false" keepFileOpen="false" encoding="utf-8" createDirs="true" layout="${longdate}|${pad:padding=-5:inner=${level:uppercase=true}}|thread-${threadid}|${logger}|${message} ${exception:format=tostring}" />
        <target xsi:type="File" name="errorfile" fileName="${logDirectory}/${serviceName}-${shortdate}-error.log" archiveAboveSize="20000000" archiveNumbering="Rolling" maxArchiveFiles="50" concurrentWrites="false" keepFileOpen="false" encoding="utf-8" createDirs="true" layout="${longdate}|${pad:padding=-5:inner=${level:uppercase=true}}|thread-${threadid}|${logger}|${message} ${exception:format=tostring}" />
        <target xsi:type="File" name="accessfile" fileName="${logDirectory}/${serviceName}-${shortdate}-access.log" archiveAboveSize="20000000" archiveNumbering="Rolling" maxArchiveFiles="50" concurrentWrites="false" keepFileOpen="false" encoding="utf-8" createDirs="true" layout="${longdate}|${pad:padding=-5:inner=${level:uppercase=true}}|thread-${threadid}|${message}"/>
    </targets>

    <!-- rules to map from logger name to target -->
    <rules>
        <!--All logs, including from Microsoft-->
        <logger name="*" minlevel="Trace" writeTo="tracefile" />
        <logger name="*" minlevel="Error" writeTo="errorfile" />
        <logger name="Microsoft.AspNetCore.Hosting.Internal.WebHost" minlevel="Trace" writeTo="accessfile" />

        <logger name="Microsoft.EntityFrameworkCore.Model.Validation" minlevel="Warn" final="true" />
        <logger name="Microsoft.EntityFrameworkCore.Database.Command" minlevel="Info" final="true"/>
        <logger name="*" minlevel="Info" writeTo="infofile" />
    </rules>
</nlog>
```

## Reference

1. https://nlog-project.org/config/
2. https://github.com/nlog/NLog/wiki/Configuration-file
3. https://github.com/NLog/NLog/wiki/File-target