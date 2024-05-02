using Microsoft.Extensions.Configuration;
using OnlineAppointmentSchedulingSystem.Application.Common.Interfaces;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;
using System.Net.Mail;
using System.Net;
using System.Reflection.PortableExecutable;

namespace OnlineAppointmentSchedulingSystem.Infrastructure.Identity
{
	public class EmailService : IEmailService
	{
		private readonly IConfiguration _configuration;

		public EmailService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public async System.Threading.Tasks.Task SendEmailAsync(string toEmail, string subject, string content)
		{
			var smtpServer = _configuration["SmtpSettings:Host"];
			var smtpPort = _configuration["SmtpSettings:Port"];
			var smtpUsername = _configuration["SmtpSettings:Username"];
			var smtpPassword = _configuration["SmtpSettings:Password"];

			var from = new MailAddress(smtpUsername, "MED-APSUZ");
			var to = new MailAddress(toEmail);
			var message = new MailMessage(from, to)
			{
				Subject = subject,
				Body = content,
				IsBodyHtml = true
			};

			using (var client = new SmtpClient(smtpServer, int.Parse(smtpPort)))
			{
				client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
				client.EnableSsl = true;
				await client.SendMailAsync(message);
			}
		}
	}
}
