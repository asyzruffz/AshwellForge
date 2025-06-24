using AshwellForge.Mechanism;
using AshwellForge.Mechanism.Admin;
using LiveStreamingServerNet;
using LiveStreamingServerNet.Flv.Installer;
using LiveStreamingServerNet.Standalone;
using LiveStreamingServerNet.Standalone.Installer;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLiveStreamingServer(
    new IPEndPoint(IPAddress.Any, 1935),
    options => options.AddStandaloneServices().AddFlv()
);

var app = builder.Build();

app.UseHttpFlv();
app.MapStandaloneServerApiEndPoints();
app.UseAdminPanelUI(new AdminOptions { BasePath = "/ui", HasHttpFlvPreview = true });

app.Run();
