using Microsoft.Extensions.Configuration;
using OnlineAppointmentSchedulingSystem.Application.Common.Interfaces;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;
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
			var apiInstance = new TransactionalEmailsApi();

			string SenderName = _configuration["UMEAS"]!;
			string SenderEmail = _configuration["BrevoApi:EmailSender"]!;
			SendSmtpEmailSender Email = new SendSmtpEmailSender(SenderName, SenderEmail);

			SendSmtpEmailTo smtpEmailTo = new SendSmtpEmailTo(toEmail, null);
			List<SendSmtpEmailTo> To = new List<SendSmtpEmailTo>();
			To.Add(smtpEmailTo);

			string HtmlContent = $"<strong>{content}</strong>"!;
			string TextContent = null;

			var sendSmtpEmail = new SendSmtpEmail(Email, To, null, null, HtmlContent, TextContent, subject);
			var result = await apiInstance.SendTransacEmailAsync(sendSmtpEmail);
		}
	}
}
