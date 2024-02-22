using PaymentApp.Application;
using PaymentApp.PaymentApi.Classes.Extensions;
using PaymentApp.PaymentApi.Classes.Filters;
using PaymentApp.Persistence;
using System.Text.Encodings.Web;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => {
    options.Filters.Add<ApiKeyFilterAttribute>();
    options.Filters.Add<LogActionFilterAttribute>();
})
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
        options.JsonSerializerOptions.WriteIndented = true;
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseErrorHandler();

app.Run();
