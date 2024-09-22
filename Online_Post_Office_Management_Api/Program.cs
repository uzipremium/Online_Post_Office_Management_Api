using MediatR;
using MongoDB.Driver;
using Online_Post_Office_Management_Api.Repositories;
using Online_Post_Office_Management_Api.Commands.EmployeeCommand;
using Online_Post_Office_Management_Api.Data;
using System.Reflection;
using Online_Post_Office_Management_Api.Repositories.Impl;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Online_Post_Office_Management_Api.Repositories.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Configure JWT key and authentication
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policyBuilder =>
        {
            policyBuilder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

// Register services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register MongoDbService
builder.Services.AddSingleton<MongoDbService>();

// Register IMongoClient
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var mongoDbService = sp.GetRequiredService<MongoDbService>();
    return new MongoClient(mongoDbService.Database.Client.Settings); // Điều chỉnh cách lấy cấu hình
});

// Register IMongoDatabase
builder.Services.AddScoped(sp =>
{
    var mongoDbService = sp.GetRequiredService<MongoDbService>();
    return mongoDbService.Database;
});

// Register repositories
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IDescriptionRepository, DescriptionRepository>();
builder.Services.AddScoped<IDeliveryRepository, DeliveryRepository>();
builder.Services.AddScoped<IPackageRepository, PackageRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IOfficeRepository, OfficeRepository>();
builder.Services.AddScoped<ICustomerPackageRepository, CustomerPackageRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICustomerSendHistoryRepository, CustomerSendHistoryRepository>();
builder.Services.AddScoped<IOfficeSendHistoryRepository, OfficeSendHistoryRepository>();
builder.Services.AddScoped<IReceiveHistoryRepository, ReceiveHistoryRepository>();
builder.Services.AddScoped<IPricingServiceRepository, PricingServiceRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

// Add MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateEmployeeAndAccount).Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable routing
app.UseRouting();

// Apply CORS policy
app.UseCors("AllowAll");

// Enable authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

// Map controllers
app.MapControllers();

app.Run();
