using Books.Data;
using Books.Interfaces;
using Books.Repositories;
using Microsoft.AspNetCore.Mvc;


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
      //services.AddDbContext<BookContext>(o => o.UseSqlServer(Configuration.GetConnectionString("ProductDB")));
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
    }
  }
}
