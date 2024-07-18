using Microsoft.Extensions.Options;
using MimeKit;
using OrderManagementSystem.Core.Services.Contract;
using OrderManagementSystem.Core.Utilities.EmailStampModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
namespace OrderManagementSystem.Service.EmailService
{
	public class EmailSender : IEmailSender
	{
		private readonly IOptions<EmailStamp> _options;

		public EmailSender(IOptions<EmailStamp> options)
        {
			_options = options;
		}
        public async Task SendEmailAsync(string toEmail, string subject, string body)
		{
			var mail = new MimeMessage()
			{
				Sender = MailboxAddress.Parse(_options.Value.FromEmail),
				Subject = subject,

			};
			mail.From.Add(new MailboxAddress(_options.Value.Username, _options.Value.FromEmail));



			mail.To.Add(MailboxAddress.Parse(toEmail));

			var builder = new BodyBuilder();

			builder.TextBody = body;


			mail.Body = builder.ToMessageBody();

			var smtp = new SmtpClient();

			smtp.Connect(_options.Value.Host, _options.Value.Port, _options.Value.EnableSsl);

			smtp.Authenticate(_options.Value.FromEmail, _options.Value.Password);

			smtp.Send(mail);

			smtp.Disconnect(true);
			mail.To.Clear();
		}
	}
}
