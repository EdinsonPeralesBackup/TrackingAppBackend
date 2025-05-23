using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Tracking.Api;
using Tracking.Api.Utils;

public class Program
{
    public static void Main(string[] args)
    {
        var config = GetConfiguration();
        Log.Logger = CreateSerilogLogger(config);

        try
        {
            Log.Information(Constants.StarAppMessage);
            Log.Information(Constants.ConfigAppMessge);

            var host = CreateHostBuilder(config, args);

            Log.Information(Constants.StartingAppMesage);
            host.Build().Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, Constants.EndingAppMessage);
            Log.Fatal(Constants.EndAppMessage);
            throw;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    public static IHostBuilder CreateHostBuilder(IConfiguration configuration, string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureHostConfiguration(configHost =>
            {
                configHost.AddConfiguration(configuration);
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.CaptureStartupErrors(false);
                webBuilder.UseStartup<Startup>();
                webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
            })
            .UseSerilog();

    private static IConfiguration GetConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(Constants.JsonFilePath, false, true)
            .AddJsonFile(string.Format(Constants.JsonEnviromentFilePath, Environment.GetEnvironmentVariable(Constants.EnviromentVariable)), true, true)
            .AddEnvironmentVariables()
            .Build();

        return builder;
    }

    private static Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
    {
        return new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
    }
}