using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ShiftLogger.API.Data;
using ShiftLogger.API.Interfaces;
using ShiftLogger.API.Services;

namespace ShiftLogger.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddSwaggerGen(); // swagger
        builder.Services.AddControllers();
        builder.Services.AddControllers().AddNewtonsoftJson(options => 
        { 
            options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); 
            options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore; 
        });
        builder.Services.AddDbContext<ShiftLoggerDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddScoped<IShiftService, ShiftService>();
        builder.Services.AddScoped<IWorkerService, WorkerService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.MapControllers();
      
        app.Run();
    }
}