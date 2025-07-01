using AshwellForge.Mechanism;
using AshwellForge.Mechanism.Admin;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
    options.AddPolicy("origins",
    policy => policy
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()));

builder.Services.AddLiveStreamServer(1935);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseCors("origins");
}

app.UseFlv();
app.MapServerApiEndpoints();
app.UseAdminPanelUI(new AdminOptions { BasePath = "/ui", HasHttpFlvPreview = true });

app.Run();
