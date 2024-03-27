using eCommerceAPI.DBContext;
using eCommerceAPI.Services.ProductService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Enable CORS
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddDbContext<DbContext, ProductListDBContext>(options =>
{
    options.UseSqlServer(
        "Server=DESKTOP-5D2A9FB;" +
        "Database=eCommerceDB;" +
        "Trusted_Connection=True;" +
        "TrustServerCertificate=True;");
});

var app = builder.Build();
//Enable Cors
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();