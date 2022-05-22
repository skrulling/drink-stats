using MediatR;
using FluentValidation.AspNetCore;
using drink_stats.Drinks;
using drink_stats.Drinks.GetDrink;
using Microsoft.EntityFrameworkCore;

namespace drink_stats
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddFluentValidation(options =>
                {
                    options.RegisterValidatorsFromAssemblyContaining<Startup>();
                });
            services.AddAutoMapper(config =>
            {
                config.CreateMap<Drink, GetDrinkResponse>();
                config.CreateMap<CreateDrinkRequest, Drink>();
            });
            services.AddMediatR(typeof(Startup).Assembly);
            services.AddSwaggerGen();
            services.AddDbContext<DrinkStatDbContext>(
                options => options.UseInMemoryDatabase("testdb"));
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseAuthentication();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(builder =>
            {
                builder.MapControllers();
            });
        }
    }
}
