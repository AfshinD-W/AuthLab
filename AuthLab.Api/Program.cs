using AuthLab.Api.Middlewares;
using AuthLab.Application.Interfaces;
using AuthLab.Infrastructure.Database;
using AuthLab.Infrastructure.Identity.Entities;
using AuthLab.Infrastructure.Identity.Services;
using AuthLab.Infrastructure.Identity.Validators;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//DataBase
builder.Services.AddDbContext<AppDbContext>(c => c.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<User, Role>(c =>
    {
        c.User.RequireUniqueEmail = true;

        c.Password.RequiredLength = 8;
        c.Password.RequireNonAlphanumeric = true;
        c.Password.RequireUppercase = true;
        c.Password.RequireLowercase = true;
        c.Password.RequireDigit = true;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddPasswordValidator<PasswordValidator<User>>();

//Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();

builder.Services.AddControllers();

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
    {
        Title = "AuthLab",
        Version = "v1",
        Description = "API documentation for AuthLab project",
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthLab v1");
        c.RoutePrefix = string.Empty;
    });
}

app.MapControllers();

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();
