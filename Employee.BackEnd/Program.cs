using Employee.IoC.Configuration;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.MapCore(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(options =>

  options.SwaggerDoc("v1",
  new OpenApiInfo
  {

      Title = "Employee",
      Version = "v1",
      Description = "This is a Employee project to see how documentation can easily be generated for ASP.NET Core Web APIs using swagger and ReDoc",
      Contact = new OpenApiContact
      {
          Name = "Md Abul Bashar",
          Email = "bashar.iiuc77@gmail.com"
      }


  })
  );


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    options.SwaggerEndpoint("/swagger/v1/swagger.json",
    "Demo Documentation v1"));

    app.UseReDoc(options =>
    {
        options.DocumentTitle = "Demo documentation";
        options.SpecUrl = "/swagger/v1/swagger.json";


    });



}




app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
