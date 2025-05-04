using CRUD_Task.BL;
using CRUD_Task.DAL;
using CRUD_Task.DAL.Context;
using CRUD_Task.DAL.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDataAccessServices(builder.Configuration);
builder.Services.AddBusinessServices(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#region Identity
builder.Services.AddIdentityCore<Employee>(options =>
{
	options.Password.RequiredUniqueChars = 1;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireDigit = false;
	options.Password.RequireLowercase = false;
	options.Password.RequireUppercase = false;
	options.Password.RequiredLength = 6;
	options.User.RequireUniqueEmail = true;
})
	.AddEntityFrameworkStores<CRUD_TaskContext>();
#endregion
// Add CORS
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAngularApp",
		builder =>
		{
			builder.WithOrigins("http://localhost:4200")
				   .AllowAnyMethod()
				   .AllowAnyHeader()
				   .AllowCredentials();
		});
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAngularApp");
app.UseAuthorization();

app.MapControllers();

app.Run();
