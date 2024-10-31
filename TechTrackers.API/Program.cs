using Microsoft.EntityFrameworkCore;
using TechTrackers.Data;
using TechTrackers.Service;
<<<<<<< HEAD
using TechTrackers.Service.Services;
=======
using TechTrackers.Service.Authorization;
using TechTrackers.Service.General;
using TechTrackers.Service.IssueLog;
>>>>>>> fba63d8d4b34c85b9368550f59bb012d92a94355

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Services.AddDbContext<TechTrackersDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("tech_support_db"))
.EnableSensitiveDataLogging()
.EnableDetailedErrors());

builder.Services.AddControllers();
builder.Services.AddCors(option => option.AddPolicy("corspolicy", builder =>
{
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
}));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Mine
//Configure service and add contextDB
builder.Services.AddScoped<ITechTrackerService, TechTrackerService>();
builder.Services.AddScoped<UserLogService, UserLogService>();
builder.Services.AddScoped<LogService, LogService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IOTPService, OTPService>();
builder.Services.AddScoped<IUserService, UserOtpService>();

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

    app.UseDeveloperExceptionPage();


}

app.UseMiddleware<TechTrackers.API.Controllers.GlobalExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
