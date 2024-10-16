using Microsoft.EntityFrameworkCore;
using TechTrackers.Data;
using TechTrackers.Service;
using TechTrackers.Service.Authorization;
using TechTrackers.Service.General;
using TechTrackers.Service.IssueLog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<TeckTrackersDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("tech_support_db")));
builder.Services.AddControllers();
builder.Services.AddCors(option => option.AddPolicy("corspolicy", builder =>
{
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
}));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Mine
//Configure service and add contextDB
builder.Services.AddScoped<TeckTrackersDbContext, TeckTrackersDbContext>();
builder.Services.AddScoped<ITechTrackerService, TechTrackerService>();

//Lunga
builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IGeneralService, GeneralService>();



var app = builder.Build();
app.UseCors("corspolicy");

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
