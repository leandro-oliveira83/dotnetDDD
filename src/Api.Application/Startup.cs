using System;
using System.Text;
using System.Collections.Generic;
using Api.CrossCutting.DependencyInjection;
using Api.CrossCutting.Mappings;
using Api.Domain.Security;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Api.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Application
{
  public class Startup
  {
    public Startup(IConfiguration configuration, IHostingEnvironment environment)
    {
      Configuration = configuration;
      _environment = environment;
    }

    public IConfiguration Configuration { get; }

    public IHostingEnvironment _environment { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      if (_environment.IsEnvironment("Testing"))
      {
        Environment.SetEnvironmentVariable("DB_CONNECTION", "Persist Security Info=True; Server=127.0.0.1;Port=3306;Database=dbSampleDDD_Integration;Uid=root;Pwd=102030");
        Environment.SetEnvironmentVariable("DATABASE", "MYSQL");
        Environment.SetEnvironmentVariable("MIGRATION", "APLICAR");
        Environment.SetEnvironmentVariable("Issuer", "SampleDDD");
        Environment.SetEnvironmentVariable("Audience", "http://localhost:5000, http://localhost:5001");
        Environment.SetEnvironmentVariable("Seconds", "120");
        Environment.SetEnvironmentVariable("Issuer", "");
      }

      ConfigureService.ConfigureDependenciesService(services);
      ConfigureRepository.ConfigureDependencieRepository(services);

      var config = new AutoMapper.MapperConfiguration(cfg =>
      {
        cfg.AddProfile(new DtoToModelProfile());
        cfg.AddProfile(new EntityToDtoProfile());
        cfg.AddProfile(new ModelToEntityProfile());
      });

      IMapper mapper = config.CreateMapper();
      services.AddSingleton(mapper);

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
          Title = "SampleDDD - Api Rest",
          Version = "v1",
          Description = "DDD architecture-based api example.",
          Contact = new OpenApiContact
          {
            Name = "Leandro Silva de Oliveira",
            Url = new Uri("https://github.com")
          }
        });

        //Colocar JWT no Swagger
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
          In = ParameterLocation.Header,
          Description = "Entre com o Token JWT",
          Name = "Authorization",
          Type = SecuritySchemeType.ApiKey
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement {
          {
            new OpenApiSecurityScheme {
              Reference = new OpenApiReference {
                Id = "Bearer",
                Type = ReferenceType.SecurityScheme
              }
            }, new List<string>()
          }
        });
      });

      var signingConfigurations = new SigningConfigurations();
      services.AddSingleton(signingConfigurations);

      services.AddAuthentication(authOptions =>
                  {
                    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                  })
                  .AddJwtBearer(bearerOptions =>
                  {
                    var paramsValidation = bearerOptions.TokenValidationParameters;
                    paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                    paramsValidation.ValidAudience = Environment.GetEnvironmentVariable("Audience");
                    paramsValidation.ValidIssuer = Environment.GetEnvironmentVariable("Issuer");

                    // Valida a assinatura de um token recebido
                    paramsValidation.ValidateIssuerSigningKey = true;

                    // Verifica se um token recebido ainda é válido
                    paramsValidation.ValidateLifetime = true;

                    // Tempo de tolerância para a expiração de um token (utilizado
                    // caso haja problemas de sincronismo de horário entre diferentes
                    // computadores envolvidos no processo de comunicação)
                    paramsValidation.ClockSkew = TimeSpan.Zero;
                  });

      // Ativa o uso do token como forma de autorizar o acesso
      // a recursos deste projeto
      services.AddAuthorization(auth =>
      {
        auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                  .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                  .RequireAuthenticatedUser().Build());

      });

      services.AddControllers()
              .AddNewtonsoftJson();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      // if (env.IsDevelopment())
      // {
      //   app.UseDeveloperExceptionPage();
      // }

      // Enabling middleware for Swagger use
      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
        c.RoutePrefix = string.Empty;
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My exemple project DDD AspNet Core 3.0");
      });

      //Redirect link to swagger when accessing main route
      var option = new RewriteOptions();
      option.AddRedirect("^$", "swagger");
      app.UseRewriter(option);

      app.UseRouting();
      app.UseAuthorization();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });

      //Example execute migration on startup application
      if (Environment.GetEnvironmentVariable("MIGRATION").ToLower() == "APLICAR".ToLower())
        using (var service = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
          using (var context = service.ServiceProvider.GetService<MyContext>())
          {
            context.Database.Migrate();
          }
        }
    }
  }
}
