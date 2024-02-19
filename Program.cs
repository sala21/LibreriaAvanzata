using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using WebAppLibreria.Services;


var builder = WebApplication.CreateBuilder(args);

string connStr = builder.Configuration.GetConnectionString("DefaultConnection") ?? "stringa_di_default";


builder.Services.AddDbContext<LibraryDB>(options =>
    options.UseSqlServer(connStr));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = " Library ",
        Description = " La libreria più bella al mondo! ",
        Version = "v1"
    });
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", " TEST ");
    });
}


app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
