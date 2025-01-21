using Microsoft.Net.Http.Headers;
using StudentWebAppNewClient.Data.Interfaces;
using StudentWebAppNewClient.Data.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy.WithOrigins("http://localhost:7170", "http://localhost:7246", "https://studentwebappnewclient.azurewebsites.net") 
              .AllowAnyHeader() 
              .WithHeaders(HeaderNames.ContentType, HeaderNames.Authorization, "x-custom-header"); 
    });
});

// Add services to the container.
builder.Services.AddScoped<IStudentService, StudentService>();

// Hårdkodad BaseUrl
builder.Services.AddHttpClient<IStudentService, StudentService>(client =>
{  
    client.BaseAddress = new Uri("https://studentapi-app-new.calmtree-16028aa1.northeurope.azurecontainerapps.io"); 
    ;
});

builder.Services.AddRazorPages();

var app = builder.Build();

app.UseCors("AllowLocalHost");

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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
