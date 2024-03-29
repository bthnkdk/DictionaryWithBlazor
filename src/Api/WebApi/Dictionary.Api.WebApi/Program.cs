using Dictionary.Api.Application.Extension;
using Dictionary.Api.WebApi.Infrastructure.ActionFilters.Extensions;
using Dictionary.Infrastructure.Persistence.Extensions;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
                 .AddJsonOptions(opt =>
                 {
                     opt.JsonSerializerOptions.PropertyNamingPolicy = null;
                 })
                .AddFluentValidation();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureAuth(builder.Configuration);

builder.Services.AddApplicationRegistration();
builder.Services.AddInfrastructureRegistration(builder.Configuration);

builder.Services.AddCors(s => s.AddPolicy("policy", builder =>
{
    builder.WithOrigins("*").AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.//
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.ConfigureExceptionHandling(app.Environment.IsDevelopment());

app.UseCors("policy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
