using Microsoft.Extensions.Configuration;
using OnlineAppointmentSchedulingSystem.Application.Common.Interfaces;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace OnlineAppointmentSchedulingSystem.Infrastructure.Identity
{
	public class EmailService : IEmailService
	{
		private readonly IConfiguration _configuration;

		public EmailService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public async Task SendEmailAsync(string toEmail, string subject, string content)
		{
			var apiKey = _configuration["EmailSender"];
			var client = new SendGridClient(apiKey);
			var from = new EmailAddress(_configuration["EmailSenderSendGrid"], "OnlineAppSystem");
			var to = new EmailAddress(toEmail);
			var htmlContent = $"<strong>{content}</strong>";
			var msg = MailHelper.CreateSingleEmail(from, to, subject, content, htmlContent);
			var response = await client.SendEmailAsync(msg);
		}
	}
}
