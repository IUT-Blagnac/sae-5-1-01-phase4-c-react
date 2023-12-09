using backend.Data;
using backend.Services.Class;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");

// Add services to the container.
builder.Services.AddDbContext<EntityContext>(opt =>
    opt.UseNpgsql(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IUserTeamService, UserTeamService>();
builder.Services.AddScoped<ISkillService, SkillService>();
builder.Services.AddScoped<IChallengeservice, ChallengeService>();
builder.Services.AddScoped<ISaeService, SaeService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleUserService, RoleUserService>();
builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<ICSVService, CSVService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ITeamWishService, TeamWishService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();

//JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<EntityContext>();

    if (context.Database.GetPendingMigrations().Any())
        context.Database.Migrate();

    context.InitializeDefaultData();
}

app.Run();