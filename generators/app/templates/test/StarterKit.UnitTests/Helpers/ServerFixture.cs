using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace StarterKit.UnitTests.Helpers
{
  public class ServerFixture : IDisposable
  {
    public TestServer Server { get; private set; }
    public HttpClient Client { get; private set; }

    public ServerFixture()
    {
      var basePath = PlatformServices.Default.Application.ApplicationBasePath;
      var projectPath = Path.GetFullPath(Path.Combine(basePath, "../../../../../src/StarterKit"));

      Server = new TestServer(new WebHostBuilder()
          .UseContentRoot(projectPath)
          .UseStartup<StarterKit.Startup>());
      Client = Server.CreateClient();
      Client.DefaultRequestHeaders.Accept.Clear();
      Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }
    public void Dispose()
    {
      Server.Dispose();
      Client.Dispose();
    }
  }
}
