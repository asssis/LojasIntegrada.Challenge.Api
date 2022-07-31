using AutoMapper;
using LojasIntegrada.Challenge.Application;
using LojasIntegrada.Challenge.Application.Helper;
using LojasIntegrada.Challenge.Application.Services;
using LojasIntegrada.Challenge.DataBase;
using LojasIntegrada.Challenge.DataBase.Repositories;
using LojasIntegrada.Challenge.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace LojasIntegrada.Challenge.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // cors
            services.AddCors();
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            });

            services.AddMemoryCache();

            services.AddDbContext<AppContexto>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddSwaggerGen(s =>
            s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Insert JWT Token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            }));
            services.AddSwaggerGen(w =>
                w.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
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
                            new string[]{}
                        }
                    }
                ));

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDto>();
                cfg.CreateMap<ProductDto, Product>();

                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<UserDto, User>();

                cfg.CreateMap<PurchaseOrder, PurchaseOrderDto>();
                cfg.CreateMap<PurchaseOrderDto, PurchaseOrder>();
                 
                cfg.CreateMap<ItensPurchaseOrder, ItensPurchaseOrderDto>();
                cfg.CreateMap<ItensPurchaseOrderDto, ItensPurchaseOrder>();                
            });
            IMapper mapper = config.CreateMapper();

            services.AddSingleton(mapper);

            services.AddDbContext<AppContexto>();

            services.AddScoped<ProductService>();
            services.AddScoped<ProductRepository>();

            services.AddScoped<UserRepository>();
            services.AddScoped<UserService>();

            services.AddScoped<PurchaseOrderService>();
            services.AddScoped<PurchaseOrderRepository>();
            //services.AddScoped<UserService>();
            
            services.AddScoped<ValidatorHelper>();

            services.AddControllers();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger(); 
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                    options.RoutePrefix = string.Empty;
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            //global cors policy
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()
                .WithExposedHeaders("RefreshToken")); // allow credentials

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
