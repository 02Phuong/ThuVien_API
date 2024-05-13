using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ThuVien_API.Data;
using ThuVien_API.Repositories;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IBookRepository, BookRepository>();
//-------
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IPublisherRepository,PublisherRepository>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// khai báo service Authentication + using thu vien 
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
option.TokenValidationParameters = new TokenValidationParameters
{
	ValidateIssuer = true,
	ValidateAudience = true,
	ValidateLifetime = true,
	ValidateIssuerSigningKey = true,
	ValidIssuer = builder.Configuration["Jwt:Issuer"],
	ValidAudience = builder.Configuration["Jwt:Audience"],
	ClockSkew = TimeSpan.Zero,
	IssuerSigningKey = new SymmetricSecurityKey(
Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
});
// Register database
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("StringConnection")));
//
builder.Services.AddDbContext<BookAuthDbContext>(options =>

options.UseSqlServer(builder.Configuration.GetConnectionString("BookAuthConnection")));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
