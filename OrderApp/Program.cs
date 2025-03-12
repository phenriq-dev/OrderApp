using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrderApp.Data;
using OrderApp.Hubs;
using OrderApp.Messaging;
using OrderApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseInMemoryDatabase("OrdersDb"));

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<OrderConsumer>();

builder.Services.AddControllersWithViews();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("orderQueue", e =>
        {
            e.ConfigureConsumer<OrderConsumer>(context);
        });


    });
});

builder.Services.AddSignalR();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<OrderHub>("/orderHub");
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Order}/{action=Index}/{id?}");
});

app.Run();
