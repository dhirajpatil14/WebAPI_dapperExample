using System.Text.Json.Serialization;
using WebAPI_dapper.Helpers;
using WebAPI_dapper.Repository;
using WebAPI_dapper.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddControllers().AddJsonOptions(j => { 
    j.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    j.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddSingleton<DbContextData>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

{
    using var appscope = app.Services.CreateScope();
    var context = appscope.ServiceProvider.GetRequiredService<DbContextData>();
    await context.Init();
}
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
