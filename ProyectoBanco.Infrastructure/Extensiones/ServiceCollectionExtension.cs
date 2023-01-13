using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProyectoBanco.Core.Interfaces;
using ProyectoBanco.Core.OpcionesEntidades;
using ProyectoBanco.Core.Servicios;
using ProyectoBanco.Infrastructure.Data;
using ProyectoBanco.Infrastructure.Filters;
using ProyectoBanco.Infrastructure.Interfaces;
using ProyectoBanco.Infrastructure.Opciones;
using ProyectoBanco.Infrastructure.Repositorios;
using ProyectoBanco.Infrastructure.Servicios;
using System.Text;

namespace ProyectoBanco.Infrastructure.Extensiones
{
    public static class ServiceCollectionExtension
    {
        public static WebApplicationBuilder AddDbContexts(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<BancoInterandinoDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ProyectoBanca")));
            return builder;
        }
        public static WebApplicationBuilder AddOptions(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<OpcionesPaginacion>(builder.Configuration.GetSection("Pagination"));
            builder.Services.Configure<ContrasenaOpciones>(builder.Configuration.GetSection("PasswordOptions"));
            return builder;

        }
        public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IMovimientoServicio, MovimientoServicio>();
            builder.Services.AddTransient<IClienteServicio, ClienteServicio>();
            builder.Services.AddTransient<ICuentaServicio, CuentaServicio>();
            builder.Services.AddTransient<IUnidadDeTrabajo, UnidadDeTrabajo>();
            builder.Services.AddTransient<ITokenService, TokenService>() ;
            builder.Services.AddTransient<ISeguridadServicio, SeguridadServicio>();
            //builder.Services.AddTransient<IOpcionesPaginacion, OpcionesPaginacion>();
            builder.Services.AddSingleton<IContrasenaServicio, ContrasenaServicio>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped(typeof(IRepositorioBase<>), typeof(RepositorioBase<>));

            builder.Services.AddSingleton<IUriServicio>(provider =>
            {
                var accesor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accesor.HttpContext.Request;
                var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriServicio(absoluteUri);
            });
            return builder;

        }
        public static WebApplicationBuilder AddJwtAuthentication(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Authentication:Isser"],
                    ValidAudience = builder.Configuration["Authentication:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Authentication:SecretKey"]))
                };
            });
            return builder;

        }

        public static WebApplicationBuilder AddSwaggerGen(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "PolizaSOAT.Api JWTToken_Auth_API",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
                });
            });
            return builder;
        }

        public static WebApplicationBuilder AddMVCFilters(this WebApplicationBuilder builder)
        {

            builder.Services.AddControllers(options => options.Filters.Add<FiltroDeExcepcionesGlobal>());

            builder.Services.AddMvc(options =>
            {
                options.Filters.Add<FiltroDeValidacion>();
            });
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            return builder;
        }
        public static WebApplicationBuilder AutoMapper(this WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return builder;
        }

    }
}
