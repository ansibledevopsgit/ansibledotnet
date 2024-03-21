 
 
using Microsoft.OpenApi.Models;
 
  
var builder = WebApplication.CreateBuilder(args);
 

builder.Services.AddControllers();

  
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder => builder
         
          .WithOrigins("http://localhost")
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



