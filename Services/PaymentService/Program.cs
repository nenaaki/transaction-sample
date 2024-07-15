using PaymentService.Commands;
using PaymentService.CommandHandlers;
using PaymentService.Events;
using PaymentService.EventHandlers;
using PaymentService.EventStore;
using PaymentService.Messaging;
using PaymentService.QueryHandlers;
using PaymentService.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IEventStore, EventStore>();
builder.Services.AddSingleton<IMessageBroker, InMemoryMessageBroker>();
builder.Services.AddTransient<ICommandHandler<CreatePaymentCommand>, CreatePaymentCommandHandler>();
builder.Services.AddSingleton<IPaymentQueryHandler, PaymentQueryHandler>();
builder.Services.AddTransient<IEventHandler<PaymentCreatedEvent>, PaymentCreatedEventHandler>();

var app = builder.Build();

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

var serviceProvider = app.Services;

var eventStore = serviceProvider.GetService<IEventStore>();
var messageBroker = serviceProvider.GetService<IMessageBroker>();
var eventHandler = serviceProvider.GetService<IEventHandler<PaymentCreatedEvent>>();

// サブスクリプションを設定
await messageBroker.SubscribeAsync<PaymentCreatedEvent>(async (e) => await eventHandler.HandleAsync(e));

app.Run();
