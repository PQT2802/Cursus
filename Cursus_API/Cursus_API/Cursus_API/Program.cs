using Cursus_API.Helper;
using Cursus_Business.Common;
using Cursus_Business.Service.Implements;
using Cursus_Business.Service.Interfaces;
using Cursus_Data.Context;
using Cursus_Data.Models.DTOs;
using Cursus_Data.Repositories.Implements;
using Cursus_Data.Repositories.Interfaces;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using FluentValidation;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Configuration;
using System.Text;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<LMS_CursusDbContext>(opt =>
{
   // opt.UseSqlServer(builder.Configuration.GetConnectionString("Azure"));
    opt.UseSqlServer(builder.Configuration.GetConnectionString("MyDb"));
}, ServiceLifetime.Transient);

builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
    // Add custom document filter
   option.DocumentFilter<CustomDocumentFilter>();
    // option.OperationFilter<FileUploadOperationFilter>();
    option.SchemaFilter<SimpleEnumSchemaFilter>();
   // option.OperationFilter<SwaggerEnumOperationFilter>();
});

// Add CORS
builder.Services.AddCors(opt => opt.AddDefaultPolicy(policy =>
    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

Global.DomainName = builder.Configuration["DomainName"];
Global.SignInPage = builder.Configuration["SignInPage"];

builder.Services.AddHangfire(config =>
        config.UseSqlServerStorage(builder.Configuration.GetConnectionString("MyDb")));
builder.Services.AddHangfireServer();

// Add Authentication
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);
// JWT 
builder.Services.Configure<AppSetting>(builder.Configuration.GetSection("AppSettings"));
var secretKey = builder.Configuration["AppSettings:SecretKey"];
var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        //tu cap Token
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),
        ClockSkew = TimeSpan.Zero
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("3"));
    options.AddPolicy("RequireInstructorRole", policy => policy.RequireRole("2"));
    options.AddPolicy("RequireStudentRole", policy => policy.RequireRole("1"));
    options.AddPolicy("RequireAdminOrStudentRole", policy =>  policy.RequireAssertion(context => context.User.IsInRole("3") || context.User.IsInRole("1")));

});


builder.Services.AddControllers()
//.AddNewtonsoftJson(options =>
//{
//    //options.SerializerSettings.Converters.Add(new RemoveIdConverter());
//    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
//    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
//    options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects; // Helps with circular references
//    options.SerializerSettings.TypeNameHandling = TypeNameHandling.None; // Optional, depends on your needs


//    //options.SerializerSettings.Converters.Add(new RemoveIdConverter());
//    //options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
//    //options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
//});
.AddJsonOptions(options =>
{
   // options.JsonSerializerOptions.Converters.Add(new RemoveIdAndValuesConverter());
   //options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
   options.JsonSerializerOptions.MaxDepth = 64; // Optionally increase the depth
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});



builder.Services.AddHttpContextAccessor();
builder.Services.AddServicesConfiguration(builder.Configuration);
builder.Services.AddProblemDetails();
builder.Services.AddMemoryCache();
builder.Services.AddCors();
// Add AWS Lambda support.
//builder.Services.AddAWSLambdaHosting(LambdaEventSource.RestApi);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.UseHangfireDashboard();
//app.UseHangfireServer();
//app.MigrationDBHelper();
app.MigrateDatabases();
app.MapControllers();

app.Run();
