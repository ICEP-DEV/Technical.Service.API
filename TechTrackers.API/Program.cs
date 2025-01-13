using Microsoft.EntityFrameworkCore;
using TechTrackers.Service;
using TechTrackers.Data;
using TechTrackers.Service.Services;
using TechTrackers.Service.ManageLogs;
using TechTrackers.Service.TechnicianServices;
using TechTrackers.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Services.AddDbContext<TechTrackersDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TechnicalSupportDb"))
.EnableSensitiveDataLogging()
.EnableDetailedErrors());


builder.Services.AddControllers();
builder.Services.AddCors(option => option.AddPolicy("corspolicy", builder =>
{
    builder.WithOrigins("http://localhost:3000", "http://localhost:3001")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials();
}));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<AssignTechnicianService>();
builder.Services.AddScoped<LogService, LogService>();
builder.Services.AddScoped< UserLogService, UserLogService>();
builder.Services.AddScoped<LogService, LogService>();
builder.Services.AddScoped<AdminLogsService, AdminLogsService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IOTPService, OTPService>();
builder.Services.AddScoped<IUserService, UserOtpService>();
builder.Services.AddHostedService<SLAMonitoringService>();
//
builder.Services.AddScoped<IManageLogs, ManageLogsService>();
builder.Services.AddScoped<ITechnicianService, TechnicianService>();
builder.Services.AddScoped<ITechReviews, TechReviews>();


//Amo
builder.Services.AddScoped<IDepartmentService, DepartmentService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
