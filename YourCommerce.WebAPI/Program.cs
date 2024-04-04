using YourCommerce.Application;
using YourCommerce.Infrastructure;
using YourCommerce.WebAPI;
using YourCommerce.WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWebAPI()
    .AddInfrastructure(builder.Configuration)
    .AddApplication();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.UseExceptionHandler("/error");

app.UseHttpsRedirection();

app.MapControllers();

app.Run();