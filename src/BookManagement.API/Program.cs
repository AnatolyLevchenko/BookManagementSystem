using BookManagement.Infrastructure.Seed;
using BookManagement.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<GenresService>();
builder.Services.AddScoped<AuthorsService>();
builder.Services.AddControllers();
builder.Services.AddDbContext<BookManagementDbContext>(options =>
    options.UseInMemoryDatabase("BookManagementDb"));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateAsyncScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<BookManagementDbContext>();
    DbMigrator.Migrate(dbContext);
}
app.UseCors();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});
app.Run();
