using AzureFunctionTimerTriger.Services.Abstracts;
using AzureFunctionTimerTriger.Services.Concretes;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true);

var connStr = builder.Configuration["Values:ConnectionString"];
var queueName = builder.Configuration["Values:QueueName"];

builder.Services.AddSingleton<IQueueService>(sp => new QueueService(connStr, queueName));

var app = builder.Build();
app.Run();
