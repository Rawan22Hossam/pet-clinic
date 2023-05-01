using Abstractions;
using Contexts;
using StudentAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<IClinicAdminServices, ClinicAdminServices>();
builder.Services.AddScoped<IDatabaseContext, DatabaseContext>();
builder.Services.AddLogging(builder =>
{
    builder.AddConsole();
    builder.AddDebug();
});


//
Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, collection) =>
    {
        collection.AddHostedService<KafkaConsumerHostedService>();
    });

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

app.MapControllers();

app.Run();
