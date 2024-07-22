using Identity.Data;
using Identity.Models;
using Identity.Repositories.AuthRepo;
using Identity.Repositories.IdentityRepo;
using Identity.Repositories.UserRepo;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using ProtoServer.ProtoFiles;
using UserUseCase = Identity.UseCases.UserUseCase.UserUseCase;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserRepositroy, UserRepository>();
builder.Services.AddScoped(typeof(IIdentityRepository<>), typeof(IdentityRepository<>));
builder.Services.AddDbContext<IdentityContext>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

builder.Services.AddIdentity<User,IdentityRole>()
    .AddEntityFrameworkStores<IdentityContext>()   
    .AddDefaultTokenProviders();


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
app.MapGrpcService<Identity.UseCases.AuthUseCase.AuthUseCase>();


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<IdentityContext>();
    db.Database.Migrate();
}


app.Run();


