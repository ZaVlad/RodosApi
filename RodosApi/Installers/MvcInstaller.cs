using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RodosApi.Data;
using RodosApi.Filters;
using RodosApi.Filters.Validators;
using RodosApi.Options;
using RodosApi.Services;

namespace RodosApi.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {

            var jwtSettings = new JwtSettings();
            configuration.Bind(nameof(JwtSettings), jwtSettings);

            services.AddSingleton(jwtSettings);



            services.AddControllers();
            services.AddMvc(options =>
                {
                    options.Filters.Add<ValidationFilters>();
                })
                .AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<Startup>())
                .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddAutoMapper(typeof(Startup));

            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        RequireExpirationTime = false,
                        ValidateLifetime = true
                    };
                });
            services.AddSingleton<IUriService>(provider =>
            {
                var accessor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(absoluteUri);
            });

            services.AddCors();

            services.AddScoped<ITypeOfDoorService, TypeOfDoorService>();
            services.AddScoped<ICoatingService, CoatingService>();
            services.AddScoped<ICollectionService, CollectionService>();
            services.AddScoped<IDoorModelService, DoorModelService>();
            services.AddScoped<IColorService, ColorService>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IMakerService, MakerService>();
            services.AddScoped<IFurnitureTypeService, FurnitureTypeService>();
            services.AddScoped<IMaterialService, MaterialService>();
            services.AddScoped<ITypeOfHingesService, TypeOfHingesService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IHingesService, HingesService>();
            services.AddScoped<IDoorHandleService, DoorHandleService>();
            services.AddScoped<IDoorService, DoorService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IOrderService, OrderService>();
            services.Configure<IdentityOptions>(option =>
            {
                option.Password.RequireDigit = false;
                option.Password.RequireLowercase = false;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireUppercase = false;
                option.Password.RequiredLength = 6;
            });
            services.AddAuthorization();
        }
    }
}
