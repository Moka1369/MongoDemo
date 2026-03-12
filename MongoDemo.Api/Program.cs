using MongoDemo.Application;
using MongoDemo.Infrastrukture;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastrukture(builder.Configuration);


 //CORS in der API aktivieren , Damit Blazor auf die API zugreifen darf, ergänze in:
 builder.Services.AddCors(options =>
 {
     options.AddPolicy("BlazorUi" , policy =>
     {
         policy.WithOrigins("https://localhost:7168", "http://localhost:5168")
         .AllowAnyHeader()
         .AllowAnyMethod();
     });
 });
var app = builder.Build();

// if (app.Environment.IsDevelopment())  //Swagger funktioniert nur lokal, aber nicht im Docker Container, weil Docker standardmäßig Production benutzt.
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.MapGet("/", () => "MongoDemo API ✅");
app.UseCors("BlazorUi");
app.Run();

