## Hangfire

### Job Type

1. **Recurrent tasks**
    - Recurring jobs are fired many times on the specified *CRON* schedule.
2. Continuations
    - Continuations are executed when parent job has finished.
3. Delayed
    - Delayed jobs are executed only once too, but not immediately – only after the specified time interval.
4. Fire-and-forget
    - These jobs are executed only once and almost immediately after they are fired.
5. Batches
    - Continuations are executed when parent job has finished.


### Retry Strategy

1. If your background job encounters a problem during its execution, 
    it will be retried automatically after some delay.
2. Follow will auto retry
    - Exceptions
    - Application shutdowns
    - Unexpected process terminations
3. Default retry 10 times, and can customize the retry times

    ```c#
    // define retry times[RetryTime] in config file appsettings
    // then do config in startup.cs
    GlobalJobFilters.Filters.Add(
        new AutomaticRetryAttribute 
        { 
            Attempts = Convert.ToInt32(
                HangfireConfigurationSection.GetSection("RetryTime").Value) 
        });
    ```


### [Use SQLServer as storage engine](https://docs.hangfire.io/en/latest/configuration/using-sql-server.html)

1. Customize hangfire schema
    - can customize hangfire schema in Startup.cs, and default value is HangFire
    
    ```c#
    services.AddHangfire(x => x.UseSqlServerStorage(CONNECTION_STRING, 
        new Hangfire.SqlServer.SqlServerStorageOptions
        {
            SchemaName = "SchemaName"
        }));
    ```

2. About database script
    - default create blank datasource, and start server, meanwhile the server will create relatived table and schema
    - but if run the script manual, means donot use server to create tables and schema, we should do follow change in Sartup.cs code; 
        otherwise, server will throw exception.
    
    ```c#
    services.AddHangfire(x => x.UseSqlServerStorage(CONNECTION_STRING, new Hangfire.SqlServer.SqlServerStorageOptions
    {
        repareSchemaIfNecessary = false
    }));
    ```

### Login feature and access to Dashboard

1. There is no login-feature built into Hangfire. 
    You need to implement your own IDashboardAuthorizationFilter, 
    where you use your internal login-feature to autheticate the user.

2. Defaultly, Hangfire just localhost has access to Dashboard, which is for security

3. Customize to allow IPs have access to Dashboard

    ```c#
    // extands IDashboardAuthorizationFilter to overwrite Authorize
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        private bool _allowAllIP { get; set; }

        public HangfireAuthorizationFilter() {}

        public HangfireAuthorizationFilter(bool allowAllIP)
        {
            _allowAllIP = allowAllIP;
        }

        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();
            // _allowAllIp: true, so this method will always return true
            return _allowAllIP;
        }
    }

    // then define AllowAllIP config file: appsetting.json 
    // and enable Dashboard in startup.cs
    var options = new DashboardOptions()
    {
        Authorization = new[] {
            new HangfireAuthorizationFilter(
                Convert.ToBoolean(HangfireConfigurationSection.GetSection("AllowAllIP").Value))
        }
    };
    app.UseHangfireDashboard(HangfireConfigurationSection.GetSection("HangfireUrl").Value, options);
    ```

## Reference

1. [Documentation](https://docs.hangfire.io/en/latest/)
2. [Customize hangfire schema](https://github.com/HangfireIO/Hangfire/issues/432)
3. [Multiple Instance](https://docs.hangfire.io/en/latest/background-processing/running-multiple-server-instances.html)