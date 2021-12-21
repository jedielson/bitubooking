using BituBooking.Domain;
using BituBooking.Infra.Storage.Postgres;
using BituBooking.Reading;
using BituBooking.Infra.Kafka;
using KafkaFlow;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(opt => opt.SuppressAsyncSuffixInActionNames = false);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDomainServices();
builder.Services.AddPostgresServices(builder.Configuration);
builder.Services.AddKafkaServices(builder.Configuration);
//builder.Services.AddKafkaConsumerServices(builder.Configuration);
builder.Services.AddReadingServices();

var app = builder.Build();

// // Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var kafkaBus = app.Services.CreateKafkaBus();

app.Lifetime.ApplicationStarted.Register(() => kafkaBus.StartAsync(app.Lifetime.ApplicationStopped));

app.Run();

#pragma warning disable CA1050
public partial class Program { }

#pragma warning restore CA1050
