
using System.Text;
 
using Microsoft.OpenApi.Models;
 
using Microsoft.AspNetCore.Http.Features;
 
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

var builder = WebApplication.CreateBuilder(args);

 


builder.Services.AddControllers();

 


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder => builder
         
          .WithOrigins("http://localhost:3000", "http://localhost:80/api")
          .AllowAnyMethod()
          .AllowAnyHeader()
          .AllowCredentials()
    .Build());
});

 
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = " docker test  Web API" });
      
});

 


var app = builder.Build();

 
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{ 
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", " docker Web API");

    });

}


app.UseDeveloperExceptionPage();

app.UseCors("AllowOrigin");



app.UseHttpsRedirection();

app.MapControllers();


app.UseAuthentication();
app.UseAuthorization();




app.Run();



