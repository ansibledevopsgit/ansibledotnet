
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
         
          .WithOrigins("http://localhost:3000", "http://192.168.1.100")
          .AllowAnyMethod()
          .AllowAnyHeader()
          .AllowCredentials()
    .Build());
});


builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "ClinicBeauty Web API" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization ",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
         {
             {
               new OpenApiSecurityScheme {
                   Reference = new OpenApiReference {Type=ReferenceType.SecurityScheme, Id="Bearer"},
               }
               ,new String[]{}
            }
      });
});

 


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    // app.UseSwagger();
    // app.UseSwaggerUI();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "AsetCo Web API");

    });

}


app.UseDeveloperExceptionPage();

app.UseCors("AllowOrigin");



app.UseHttpsRedirection();

app.MapControllers();


app.UseAuthentication();
app.UseAuthorization();




app.Run();



