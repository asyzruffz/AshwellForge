using AshwellForge.Mechanism;
using AshwellForge.Mechanism.RtmpServer;
using AshwellForge.Server.Chassis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLiveStreamServer(new ServerOptions { Port = 1935 });
builder.Services.AddChassis();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseAntiforgery();
app.MapStaticAssets();

app.UseAdmin(new AdminOptions { HasHttpFlvPreview = true });

app.Run();
