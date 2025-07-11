using AshwellForge.Admin;
using AshwellForge.Admin.Services;
using AshwellForge.Admin.UI;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Logging.AddConfiguration(
    builder.Configuration.GetSection("Logging"));
builder.Services.Configure<AshwellForgeSettings>(
    builder.Configuration.GetSection(AshwellForgeSettings.SectionName));

builder.RootComponents.Add<Routes>("#route");

builder.Services.AddStreamingClient();

builder.Services.AddMudServices();
builder.Services.AddVideoPlayerServices();

await builder.Build().RunAsync();
