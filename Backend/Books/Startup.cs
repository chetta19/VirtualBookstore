using Books.Data;
using Books.Interfaces;
using Books.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;


namespace ProductMicroservice
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      var connectionString = Configuration.GetConnectionString("BooksMongoDB");
      if (connectionString == null)
      {
        throw new ArgumentNullException("BooksMongoDB connection string is missing");
      }

      services.AddControllers();
      services.AddDbContext<BookContext>(o => o.UseMongoDB(connectionString, "Books"));
      services.AddTransient<IBookRepository, BookRepository>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseHsts();
      }
      app.UseHttpsRedirection();
      app.UseRouting();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
