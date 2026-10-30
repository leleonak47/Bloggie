using Bloggie.Web.Data;
using Bloggie.Web.Repositories;
using Bloggie.Web.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Configuration.AddJsonFile(
    "appconfig.Local.json",
    optional: true,
    reloadOnChange: true);

builder.Services.AddDbContext<BloggieDbContext>(opptions =>
    opptions.UseSqlServer(
        builder.Configuration.GetConnectionString("BloggieDbConectionString")
    )
);

builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();
