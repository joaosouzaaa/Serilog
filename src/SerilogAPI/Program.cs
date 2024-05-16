using Microsoft.AspNetCore.HttpLogging;
using SerilogAPI.Constants;
using SerilogAPI.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDependencyInjection(configuration);
builder.Services.AddHttpLogging(options => 
{
    options.LoggingFields = 
        HttpLoggingFields.RequestBody |
        HttpLoggingFields.RequestPath |
        HttpLoggingFields.ResponseBody |
        HttpLoggingFields.ResponseStatusCode;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(CorsNamesConstants.CorsPolicy);
app.MigrateDatabase();
app.UseAuthorization();
app.MapControllers();
app.UseHttpLogging();

app.Run();
