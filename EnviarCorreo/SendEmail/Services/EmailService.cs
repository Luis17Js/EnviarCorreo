
using SendEmail.Models;
using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;


namespace SendEmail.Services
{
    public class EmailService : IEmailService
    {

        private readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
        }


        public void SendEmail(EmailDTO request)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("Email:UserName").Value));
            email.To.Add(MailboxAddress.Parse(request.Para));
            

            // Leer la plantilla HTML
            string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "htmlpage.html");
            string htmlContent = File.ReadAllText(templatePath);

            // Reemplazar marcadores de posición
            htmlContent = htmlContent.Replace("@Model.Compañia", request.Compañia)
                .Replace("@Model.Telefono", request.Telefono)
                .Replace("@Model.Nombre", request.Nombre)
                                      .Replace("@Model.Apellidos", request.Apellidos?? string.Empty)
                                     .Replace("{{Message}}", request.Contenido);

            email.Body = new TextPart(TextFormat.Html)
            {
                Text = htmlContent
            };

            using var smtp = new SmtpClient();
            smtp.Connect(
                _config.GetSection("Email:Host").Value,
                Convert.ToInt32(_config.GetSection("Email:Port").Value),
                SecureSocketOptions.StartTls
            );

            smtp.Authenticate(_config.GetSection("Email:UserName").Value, _config.GetSection("Email:PassWord").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
