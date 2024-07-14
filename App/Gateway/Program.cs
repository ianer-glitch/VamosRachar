using Identity.Data;
using Identity.Repositories.IdentityRepo;
using Identity.Repositories.UserRepo;
using Identity.UseCases.UserCase;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped(typeof(IIdentityRepository<>),typeof(IdentityRepository<>));
builder.Services.AddScoped<IUserRepositroy,UserRepository>();
builder.Services.AddScoped<IUseCaseUser,UseCaseUser>();

builder.Services.AddDbContext<IdentityContext>(options =>{
    string connectionString = builder.Configuration.GetConnectionString("IdenttiyConnection");
    options.UseSqlServer(connectionString);
});



var app = builder.Build();

using (var scope= app.Services.CreateScope()){
    var context = scope.ServiceProvider.GetRequiredService<IdentityContext>();

    if(!context.Database.CanConnect())
        throw new NotImplementedException("Can't connect to database");
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


