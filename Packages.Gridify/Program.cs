using Gridify;
using Gridify.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Packages.Gridify;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt =>
opt.UseSqlServer("Server=.;Database=DotNetTrainingBatch4;User ID=sa;Password=sa@123;TrustServerCertificate=true"));
var app = builder.Build();

app.MapGet("/", () => "Hello World.");

//app.MapGet("/blog", async ([AsParameters] GridifyQuery query, AppDbContext db) =>
//{
//    return Results.Ok(await db.Blogs.GridifyAsync(query));
//});

app.MapGet("/blog/{pageNo}/{pageSize}", async (int pageNo, int pageSize, AppDbContext db) =>
{
    var query = new GridifyQuery()
    {
        Page = pageNo,
        PageSize = pageSize,
    };
    return Results.Ok(await db.Blogs.GridifyAsync(query));
});

app.MapGet("/blog-generate", async (AppDbContext db) =>
{
    //for (int i = 0; i < 100; i++)
    //{
    //    await db.Blogs.AddAsync(new BlogDataModel
    //    {
    //        BlogTitle = (i + 1) + "title",
    //        BlogAuthor = (i + 1) + "Author",
    //        BlogContent = (i + 1) + "content",
    //    });
    //}
    await db.Blogs.AddRangeAsync(Enumerable.Range(1, 10).Select(x =>
        new BlogDataModel()
        {
            BlogTitle = x + "title",
            BlogAuthor = x + "Author",
            BlogContent = x + "content",
        }
    ));
    await db.SaveChangesAsync();
    return Results.Ok();
});

//Filter

app.MapGet("/blog/{id}", async (int id, AppDbContext db) =>
{
    var query = new GridifyQuery()
    {
        Filter = $"BlogId = {id}"
    };
    return Results.Ok(await db.Blogs.GridifyAsync(query));
});

app.Run();