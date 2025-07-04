using AshwellForge.Mechanism;
using AshwellForge.Mechanism.Admin;
using AshwellForge.Mechanism.RtmpServer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
    options.AddPolicy("Development",
    policy => policy
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()));

builder.Services.AddLiveStreamServer(new ServerOptions { Port = 1935 });

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseCors("Development");
}

app.UseAdminPanelUI(new AdminOptions { BasePath = "/ui", HasHttpFlvPreview = true });

app.Run();
