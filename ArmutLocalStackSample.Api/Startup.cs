using System;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.SQS;
using ArmutLocalStackSample.Core;
using ArmutLocalStackSample.Core.Dtos;
using ArmutLocalStackSample.Core.Repositories.Contracts;
using ArmutLocalStackSample.Core.Services.Contracts;
using ArmutLocalStackSample.Core.Services.Implementations;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ArmutLocalStackSample.Api
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
            services.AddControllers();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IValidatorService, ValidatorService>();
            services.AddAWSService<IAmazonDynamoDB>(ServiceLifetime.Scoped);
            services.AddScoped<IDynamoDBContext>(c => new
                DynamoDBContext(c.GetService<IAmazonDynamoDB>()));
            services.AddScoped<IAmazonSQS, AmazonSQSClient>();
            services.Configure<SqsQueueConfig>(opt => Configuration.GetSection("SqsQueueConfig").Bind(opt));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(x =>
            {
                x.AllowAnyHeader();
                x.AllowAnyMethod();
                x.AllowAnyOrigin();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            TypeAdapterConfig<AddMovieRequestViewModel, AddMovieRequestModel>
                .NewConfig()
                .Map(dest => dest.MovieId, src => Guid.NewGuid())
                .Map(dest => dest.CreateDate, src => DateTime.UtcNow.ToComparableDateString());

            TypeAdapterConfig<CommentMovieRequestModel, CommentModel>
                .NewConfig()
                .Map(dest => dest.CommentId, src => Guid.NewGuid())
                .Map(dest => dest.CreateDate, src => DateTime.UtcNow.ToComparableDateString());
        }
    }
}
