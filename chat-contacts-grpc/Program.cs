using chat_contacts_grpc.Services;
using Domain.Interfaces;
using Infra.Mongodb;
using Infra.Mongodb.Services;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection("MongoDbConfig"));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSingleton<IFriendsService, FriendsService>();
builder.Services.AddSingleton<IBlockedsService, BlockedsService>();
builder.Services.AddSingleton<IRequestsReceivedService, RequestsReceivedService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<BlockedService>();
app.MapGrpcService<GreeterService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
