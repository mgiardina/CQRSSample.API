using MediatR;
using CQRSSample.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<AppDbContext>();
builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "CQRSSample.API", Version = "v1" });
});

builder.Services.AddMediatR(typeof(Program).Assembly);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CQRSSample.API v1");
});

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
