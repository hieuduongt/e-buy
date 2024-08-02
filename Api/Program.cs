using Application.CategoryServices;
using Application.ProductServices;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<IProductService,ProductService>();

builder.Services.AddDbContext<AppDBContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Development")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// C?u h�nh CORS
builder.Services.AddCors(options =>
{
options.AddPolicy("AllowSpecificOrigins",
        policyBuilder =>
            policyBuilder
                .WithOrigins("http://localhost:3000") // Thay th? b?ng URL frontend c?a b?n
                .AllowAnyHeader()
                .AllowAnyMethod());
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseCors("AllowSpecificOrigins");

app.UseHttpsRedirection();
app.UseCors(builder =>
{
    builder
    .SetIsOriginAllowedToAllowWildcardSubdomains()
    .WithOrigins(
        "http://localhost:3000",
        "https://localhost:3000"
        )
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials();
});
app.UseAuthorization();

app.MapControllers();

app.Run();
