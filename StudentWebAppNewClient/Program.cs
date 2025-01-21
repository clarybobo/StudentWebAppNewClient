using Microsoft.Net.Http.Headers;
using StudentWebAppNewClient.Data.Interfaces;
using StudentWebAppNewClient.Data.Services;

var builder = WebApplication.CreateBuilder(args);

// Lägg till CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy.WithOrigins("http://localhost:7170", "http://localhost:7246") // Lägg till de URL:er som ska tillåtas
              .AllowAnyMethod() // Tillåt alla metoder (GET, POST, etc.)
              .AllowAnyHeader() // Tillåt alla headers
              .WithHeaders(HeaderNames.ContentType, HeaderNames.Authorization, "x-custom-header"); // Specifika headers
    });
});

// Add services to the container.
builder.Services.AddScoped<IStudentService, StudentService>();
// Hårdkodad BaseUrl
builder.Services.AddHttpClient<IStudentService, StudentService>(client =>
{
    // Ange direkt BaseUrl här för API:t 
    // UPPDATERAD med aktuell API-länk från Azure
    client.BaseAddress = new Uri("https://studentapi-app-new.calmtree-16028aa1.northeurope.azurecontainerapps.io"); // Exempel för lokal utveckling
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

//TODO: Lägg till rätt länk från Azure! 
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
