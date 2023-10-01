using Azure.Identity;
using DocumentUploader.Services;

using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddAzureClients(azureBuilder =>
{
    
    var storageAccountConnectionString = builder.Configuration.GetSection("Storage").GetValue<string>("ConnectionString");
    
    azureBuilder.AddBlobServiceClient(connectionString: storageAccountConnectionString);
});

builder.Services.AddSingleton<CosmosService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();