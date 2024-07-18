
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OrderManagementSystem.APIs.Extensions;
using OrderManagementSystem.Core.Entities.User_Model;
using OrderManagementSystem.Core.Utilities.EmailStampModel;
using OrderManagementSystem.Repository._Data;
using OrderManagementSystem.Repository._Identity;

namespace OrderManagementSystem.APIs
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var WebApplicationBuilder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			WebApplicationBuilder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

			WebApplicationBuilder.Services.AddEndpointsApiExplorer();
			WebApplicationBuilder.Services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "OrderManagementSystem API", Version = "v1" });

				// Customize the schema ID generation to avoid conflicts
				c.CustomSchemaIds(type => type.FullName);

				// Other Swagger configurations
			});
			WebApplicationBuilder.Services.AddApplicationServices();
			WebApplicationBuilder.Services.Configure<EmailStamp>(WebApplicationBuilder.Configuration.GetSection("EmailStamp"));
			WebApplicationBuilder.Services.AddDbContext<OrderManagementDbContext>(options => { options.UseSqlServer(WebApplicationBuilder.Configuration.GetConnectionString("DefaultConnection")); });
			WebApplicationBuilder.Services.AddDbContext<ApplicationIdentityDbContext>(options => { options.UseSqlServer(WebApplicationBuilder.Configuration.GetConnectionString("IdentityConnection")); });
			WebApplicationBuilder.Services.AddAuthServices(WebApplicationBuilder.Configuration);
			WebApplicationBuilder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationIdentityDbContext>();

			var app = WebApplicationBuilder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
