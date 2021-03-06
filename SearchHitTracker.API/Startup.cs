﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SearchHitTracker.API.Features.SearchHits;
using SearchHitTracker.API.Features.SearchHits.SearchEngines;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace SearchHitTracker.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private string CorsPolicyName => "CORS Policy";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddMemoryCache();
            services.AddCors(a =>
                a.AddPolicy(CorsPolicyName,
                cors => cors.WithOrigins(
                    "https://localhost:8080",
                    "https://0.0.0.0:8080")
                .AllowAnyMethod()
                .AllowAnyHeader())
);

            services.AddScoped<ISearchHitsControllerService, SearchHitsControllerService>();
            services.AddScoped<ISearchEngine, GoogleSearchEngine>();
            services.AddScoped<ISearchEngine, BingSearchEngine>();
            services.Decorate<ISearchEngine, SearchEngineCachingDecorator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCors(CorsPolicyName);
            app.UseMvc();
        }
    }
}
