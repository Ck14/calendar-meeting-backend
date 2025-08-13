using Core.Correo.Servicios;
using Core.Modelos;
using Core.Models;
using Core.Models.Sso;
using Core.Repositorios;
using Core.Repositorios.Seguridad;
using Core.Repositorios.Sso;
using Core.Servicios;
using Core.Servicios.Seguridad;
using Core.Servicios.Sso;
using Core.Validadores;
using Exceptionless;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repositorios;
using Repositorios.Seguridad;
using Repositorios.Sso;
using System.Text;

var builder = WebApplication.CreateBuilder();

// Busca el folder espec�fico de configuraci�n
var appConfigFolder = Path.Combine(builder.Environment.ContentRootPath, "appconfig");
Console.WriteLine("appConfigFolder" + appConfigFolder);
Console.WriteLine("appConfigFolder :" + Path.Combine(appConfigFolder, $"appsettings.{builder.Environment.EnvironmentName}.json"));


builder.Configuration
    .AddJsonFile(Path.Combine(appConfigFolder, "appsettings.json"), optional: true, reloadOnChange: true)
    .AddJsonFile(Path.Combine(appConfigFolder, $"appsettings.{builder.Environment.EnvironmentName}.json"), optional: true, reloadOnChange: true);

builder.Configuration.AddEnvironmentVariables();
builder.Configuration.AddCommandLine(args);
builder.Logging.ClearProviders();
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddConsole();
builder.Logging.AddDebug();


// Add services to the container.
builder.Services.AddControllers();

// Configuracion de servidores.
var ssoConfig = builder.Configuration.GetSection(ConfiguracionSsoModelo.Sso).Get<ConfiguracionSsoModelo>();
var dataProtection = builder.Configuration.GetSection(ConfigurationDataProtectionModelo.DataProtection).Get<ConfigurationDataProtectionModelo>();
var configSatServicio = builder.Configuration.GetSection("ConsultaSatServicio").Get<ConsultaCLConfig>();
builder.Services.AddSingleton(configSatServicio);
builder.Services.AddSingleton(ssoConfig);
builder.Services.AddSingleton(dataProtection);
builder.Services.AddExceptionless();

// Configuracion de la pantalla Index.html de Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Software Backend Minfin", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Jwt Authorization",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<IConnectionProvider, ConnectionProvider>();


// Registro de servicio SSO o SAU
builder.Services.AddTransient<ISsoServicio, SsoServicio>();
builder.Services.Decorate<ISsoServicio, SSOValidador>();
builder.Services.AddTransient<IValidador, Validador>();
builder.Services.AddTransient<IUserProvider, UserProviderRepositorio>();

// Registro de todos los servicios y repositorios
builder.Services.AddTransient<IUsuarioServicio, UsuarioServicio>();
builder.Services.AddTransient<IUsuarioRepositorio, Repositorios.Seguridad.UsuarioRepositorio>();
builder.Services.Decorate<IUsuarioServicio, UsuarioValidadorServicio>();

builder.Services.AddTransient<IUsuarioActualServicio, UsuarioActualServicio>();
builder.Services.AddTransient<IOpcionMenuServicio, OpcionMenuServicio>();
builder.Services.AddTransient<IOpcionMenuRepositorio, OpcionMenuRepositorio>();
builder.Services.AddTransient<IRolRepositorio, RolRepositorio>();
builder.Services.AddTransient<IRolServicio, RolServicio>();
builder.Services.AddTransient<ICatalogoRepositorio, CatalogoRepositorio>();
builder.Services.AddTransient<ICatalogoServicio, CatalogoServicio>();
builder.Services.AddTransient<IPoliticaPrivacidadServicio, PoliticaPrivacidadServicio>();
builder.Services.AddTransient<IPoliticaPrivacidadRepositorio, PoliticaPrivacidadRepositorio>();

builder.Services.AddTransient<IContribuyenteServicio, ContribuyenteServicio>();
builder.Services.AddTransient<IContribuyenteRepositorio, ContribuyenteRepositorio>();

builder.Services.AddTransient<IUsuarioClimaServicio, UsuarioClimaServicio>();
builder.Services.AddTransient<IUsuarioClimaRepositorio, UsuarioClimaRepositorio>();
builder.Services.AddTransient<ICorreoServicio, CorreoServicio>();
builder.Services.AddTransient<IRolClimaServicio, RolClimaServicio>();
builder.Services.AddTransient<IRolClimaRepositorio, RolClimaRepositorio>();

// Registro de servicios para reuniones (Meet)
builder.Services.AddTransient<IMeetServicio, MeetServicio>();
builder.Services.AddTransient<IMeetRepositorio, MeetRepositorio>();




// Agregar DataProtection para la COOKIE
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(dataProtection.Path))
    .SetApplicationName("Backend");


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/sso/login";
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? "")),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Jwt", policy =>
    {
        policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
        policy.RequireAuthenticatedUser();
    });
    options.AddPolicy("Cookie", policy =>
    {
        policy.AuthenticationSchemes.Add(CookieAuthenticationDefaults.AuthenticationScheme);
        policy.RequireAuthenticatedUser();
    });
});


builder.Services.AddAuthorization();



var app = builder.Build();

app.UseExceptionless();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
