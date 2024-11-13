using Microsoft.EntityFrameworkCore;
using TechTrackers.Data;
using TechTrackers.Service;
<<<<<<< HEAD
using TechTrackers.Service.Administrator;
using TechTrackers.Service.General;
=======
>>>>>>> 0aafebb7408ef4ae9ffcd75151f844618c68d878
using TechTrackers.Service.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Services.AddDbContext<TechTrackersDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TechSupportDb"))
.EnableSensitiveDataLogging()
.EnableDetailedErrors());

builder.Services.AddControllers();
builder.Services.AddCors(option => option.AddPolicy("corspolicy", builder =>
{
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
}));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
<<<<<<< HEAD


=======
builder.Services.AddScoped<AssignTechnicianService>();
>>>>>>> e7206164baadaf945e6e04d19d8bf54d13533bfe
builder.Services.AddScoped<LogService, LogService>();
builder.Services.AddScoped<UserLogService, UserLogService>();
builder.Services.AddScoped<UserLogService, UserLogService>();
builder.Services.AddScoped<LogService, LogService>();
builder.Services.AddScoped<AdminLogsService, AdminLogsService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IOTPService, OTPService>();
builder.Services.AddScoped<IUserService, UserOtpService>();

<<<<<<< HEAD
//Lunga
builder.Services.AddScoped<IGeneralService, GeneralService>();
builder.Services.AddScoped<INotifyService, NotifyService>();
builder.Services.AddScoped<IAdministratorService, AdministratorService>();




=======
>>>>>>> 0aafebb7408ef4ae9ffcd75151f844618c68d878

//Nicole
builder.Services.AddScoped<IAddUserService, AddUserService>();

var app = builder.Build();
app.UseCors("corspolicy");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseDeveloperExceptionPage();

}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
