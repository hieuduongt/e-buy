using Application.CategoryServices;
using Application.ProductServices;
using Application.ImageService;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<IProductService,ProductService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddDbContext<AppDBContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Development")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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
//
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath,$"{Environment.CurrentDirectory}/../Shared/Images"))
});

app.UseAuthorization();

app.MapControllers();

app.Run();