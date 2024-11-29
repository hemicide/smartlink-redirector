using Redirector.Application.Interfaces;
using Redirector.Application.Services;
using Redirector.Extensions;
using Redirector.Middleware;
using Redirector.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddRepository(builder.Configuration);

#region Evaluators

builder.Services.AddEvaluatorRegistration();

//builder.Services.AddSingleton<IRedirectRulesEvaluator, DeviceEvaluator>();
//builder.Services.AddSingleton<IRedirectRulesEvaluator, DateRangeEvaluator>();
//builder.Services.AddSingleton<IRedirectRulesEvaluator, BrowserEvaluator>();
//builder.Services.AddSingleton<IRedirectRulesEvaluator, LanguageEvaluator>();
#endregion

builder.Services.AddTransient<RedirectMiddleware>();

builder.Services.AddScoped<ISmartLinkRedirectService, SmartLinkRedirectService>();
builder.Services.AddScoped<ISmartLinkRedirectRulesRepository, SmartLinkRedirectRulesRepository>();
builder.Services.AddScoped<ISmartLink, SmartLink>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<RedirectMiddleware>();

app.MapControllers();

app.Run();
