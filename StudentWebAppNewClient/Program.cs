using Microsoft.Net.Http.Headers;
using StudentWebAppNewClient.Data.Interfaces;
using StudentWebAppNewClient.Data.Services;

var builder = WebApplication.CreateBuilder(args);

// L�gg till CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy.WithOrigins("http://localhost:7170", "http://localhost:7246") // L�gg till de URL:er som ska till�tas
              .AllowAnyMethod() // Till�t alla metoder (GET, POST, etc.)
              .AllowAnyHeader() // Till�t alla headers
              .WithHeaders(HeaderNames.ContentType, HeaderNames.Authorization, "x-custom-header"); // Specifika headers
    });
});

// Add services to the container.
builder.Services.AddScoped<IStudentService, StudentService>();
// H�rdkodad BaseUrl
builder.Services.AddHttpClient<IStudentService, StudentService>(client =>
{
    // Ange direkt BaseUrl h�r f�r API:t 
    // UPPDATERAD med aktuell API-l�nk fr�n Azure
    client.BaseAddress = new Uri("https://studentapi-app-new.calmtree-16028aa1.northeurope.azurecontainerapps.io"); // Exempel f�r lokal utveckling
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

//Suzies CORS 
//app.UseCors(policy =>
//policy.WithOrigins("https://localhost:7246/", "https://localhost:7277/", "https://studentwebappclient20250120153311.azurewebsites.net")
//.AllowAnyMethod()
//.WithHeaders(HeaderNames.ContentType, HeaderNames.Authorization, "x-custom-header"));

//TODO: L�gg till r�tt l�nk fr�n Azure! 
// AKTUELL CORS! 
//WebApp https://localhost:7170 
//Api https://localhost:7246
//Azure 
//app.UseCors(policy =>
//policy.WithOrigins("https://localhost:7246", "https://localhost:7170", "http://localhost:5289")
//.AllowAnyMethod()
//.WithHeaders(HeaderNames.ContentType, HeaderNames.Authorization, "x-custom-header"));

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
