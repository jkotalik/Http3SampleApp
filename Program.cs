using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Http3TestApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    var cert = CertificateLoader.LoadFromStoreCert("localhost", StoreName.My.ToString(), StoreLocation.CurrentUser, false);

                    webBuilder.UseKestrel()
                    .UseQuic(options =>
                    {
                        options.Certificate = cert; // Shouldn't need this either here.
                        options.Alpn = "h3-29";
                    })
                    .ConfigureKestrel((context, options) =>
                    {
                        var basePort = 5557;
                        options.EnableAltSvc = true;

                        options.Listen(IPAddress.Any, basePort, listenOptions =>
                        {
                            listenOptions.UseHttps(httpsOptions =>
                            {
                                httpsOptions.ServerCertificate = cert;
                            });
                            listenOptions.Protocols = HttpProtocols.Http1AndHttp2AndHttp3;
                        });
                    })
                    .UseStartup<Startup>();
                });
    }
}
