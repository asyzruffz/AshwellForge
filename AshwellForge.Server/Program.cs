using AshwellForge.Mechanism;
using AshwellForge.Peripheral;
using AshwellForge.Server;
using AshwellForge.Server.Admin;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ServerOptions>(
    builder.Configuration.GetSection(ServerOptions.SectionName));

builder.Services.AddServer(builder.Configuration);
builder.Services.AddMechanism();
builder.Services.AddPeripheral();
builder.Services.AddAdmin();

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
