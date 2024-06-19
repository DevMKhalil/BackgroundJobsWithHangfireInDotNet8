using BackgroundJobsWithHangfireInDotNet8.Services;
using Hangfire;
using HangfireBasicAuthenticationFilter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHangfire(x =>
{
    x.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"));
    x.UseRecommendedSerializerSettings();
    x.UseSimpleAssemblyNameTypeSerializer();
    x.UseColouredConsoleLogProvider();
});

builder.Services.AddTransient<IServiceManagement, ServiceManagement>();

builder.Services.AddHangfireServer();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseHangfireDashboard("/dashboard",new DashboardOptions()
{
    DashboardTitle = "Dashboard Home",
    Authorization = new[]
    {
        new HangfireCustomBasicAuthenticationFilter()
        {
            Pass = "123",
            User = "user"
        }
    }
});

app.MapControllers();

RecurringJob.AddOrUpdate<IServiceManagement>("SyncData-recurring-job", x => x.SyncData(), "0 * * ? * *");

app.Run();
