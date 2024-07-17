using Identity.Repositories.IdentityRepo;
using Identity.Repositories.UserRepo;
using Identity.UseCases.UserUseCase;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserRepositroy, UserRepository>();
builder.Services.AddScoped(typeof(IIdentityRepository<>), typeof(IdentityRepository<>));

//configure serice port to listen
builder.WebHost.ConfigureKestrel(op =>
{
    op.ListenAnyIP(5001, li =>
    {
        li.Protocols = HttpProtocols.Http2;
    });
});


builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGrpcService<UserUseCase>();
app.Run();


