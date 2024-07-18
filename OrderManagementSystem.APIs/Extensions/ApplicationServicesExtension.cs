using MailKit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using OrderManagementSystem.Core.Repositories.Contract;
using OrderManagementSystem.Core.Services.Contract;
using OrderManagementSystem.Repository;
using OrderManagementSystem.Service.AuthService;
using OrderManagementSystem.Service.EmailService;
using OrderManagementSystem.Service.InvoiceService;
using OrderManagementSystem.Service.OrderService;
using OrderManagementSystem.Service.PaymentService;
using OrderManagementSystem.Service.ProductService;
using Org.BouncyCastle.Asn1.X509.Qualified;
using System.Text;

namespace OrderManagementSystem.APIs.Extensions
{
	public static class ApplicationServicesExtension
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
			services.AddScoped(typeof(IPaymentService), typeof(PayamentService));
			services.AddScoped(typeof(IOrderService), typeof(OrderService));
			services.AddScoped(typeof(IProductService), typeof(ProductService));
			services.AddScoped(typeof(IInvoiceService), typeof(InvoiceService));
			services.AddScoped(typeof(IEmailSender), typeof(EmailSender));
			return services;
		}

		public static IServiceCollection AddAuthServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddAuthentication(/*JwtBearerDefaults.AuthenticationScheme*/options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuer = true,
					ValidIssuer = configuration["JWT:ValidIssuer"],
					ValidateAudience = true,
					ValidAudience = configuration["JWT:ValidAudience"],
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:AuthKey"] ?? string.Empty)),
					ValidateLifetime = true,
					ClockSkew = TimeSpan.Zero,
				};
			});

			services.AddScoped(typeof(IAuthService), typeof(AuthService));

			return services;
		}
	}
}
