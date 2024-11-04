using Microsoft.EntityFrameworkCore;
using TechTrackers.Data;
using TechTrackers.Service;
using TechTrackers.Service.AdminService;
using TechTrackers.Service.General;
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
builder.Services.AddScoped<LogService, LogService>();
builder.Services.AddScoped<UserLogService, UserLogService>();
builder.Services.AddScoped<UserLogService, UserLogService>();
builder.Services.AddScoped<LogService, LogService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IOTPService, OTPService>();
builder.Services.AddScoped<IUserService, UserOtpService>();

//Lunga
builder.Services.AddScoped<IGeneralService, GeneralService>();
builder.Services.AddScoped<IAdminService, AdminService>();




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
