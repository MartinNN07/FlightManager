using FlightManager.Services.Services.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace FlightManager.Services.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendReservationConfirmationAsync(
            string toEmail,
            string flightNumber,
            string departureCity,
            string arrivalCity,
            DateTime departureTime,
            string seatClass,
            IEnumerable<string> passengerNames)
        {
            var emailConfig = _config.GetSection("Email");
            var host = emailConfig["Host"]!;
            var port = int.Parse(emailConfig["Port"]!);
            var user = emailConfig["Username"]!;
            var pass = emailConfig["Password"]!;
            var from = emailConfig["From"]!;
            var fromName = emailConfig["FromName"] ?? "FlightManager";

            var passengerList = passengerNames.ToList();
            var passengerRows = string.Join("", passengerList.Select(
                (name, i) => $"<tr><td style='padding:4px 8px'>{i + 1}</td><td style='padding:4px 8px'>{name}</td></tr>"));

            var body = $"""
                <!DOCTYPE html>
                <html lang="bg">
                <head><meta charset="utf-8"/></head>
                <body style="font-family:Arial,sans-serif;background:#f4f6f8;padding:20px">
                  <div style="max-width:600px;margin:auto;background:white;border-radius:8px;overflow:hidden;box-shadow:0 2px 8px rgba(0,0,0,.1)">
                    <div style="background:#0d6efd;color:white;padding:24px 32px">
                      <h1 style="margin:0;font-size:22px">✈️ Потвърждение на резервация</h1>
                    </div>
                    <div style="padding:24px 32px">
                      <p>Здравейте,</p>
                      <p>Вашата резервация за полет <strong>{flightNumber}</strong> беше приета успешно.</p>

                      <table style="width:100%;border-collapse:collapse;margin:16px 0;background:#f8f9fa;border-radius:6px">
                        <tr>
                          <td style="padding:8px 16px;font-weight:bold">Полет</td>
                          <td style="padding:8px 16px">{flightNumber}</td>
                        </tr>
                        <tr style="background:#e9ecef">
                          <td style="padding:8px 16px;font-weight:bold">От</td>
                          <td style="padding:8px 16px">{departureCity}</td>
                        </tr>
                        <tr>
                          <td style="padding:8px 16px;font-weight:bold">До</td>
                          <td style="padding:8px 16px">{arrivalCity}</td>
                        </tr>
                        <tr style="background:#e9ecef">
                          <td style="padding:8px 16px;font-weight:bold">Дата и час</td>
                          <td style="padding:8px 16px">{departureTime:dd.MM.yyyy HH:mm}</td>
                        </tr>
                        <tr>
                          <td style="padding:8px 16px;font-weight:bold">Клас</td>
                          <td style="padding:8px 16px">{seatClass}</td>
                        </tr>
                      </table>

                      <h3 style="margin-top:24px">Пътници ({passengerList.Count})</h3>
                      <table style="width:100%;border-collapse:collapse;border:1px solid #dee2e6">
                        <thead style="background:#0d6efd;color:white">
                          <tr>
                            <th style="padding:8px">#</th>
                            <th style="padding:8px;text-align:left">Три имена</th>
                          </tr>
                        </thead>
                        <tbody>
                          {passengerRows}
                        </tbody>
                      </table>

                      <p style="margin-top:24px;color:#6c757d;font-size:13px">
                        Благодарим ви, че използвате FlightManager. Приятен полет! 🛫
                      </p>
                    </div>
                  </div>
                </body>
                </html>
                """;

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(fromName, from));
            message.To.Add(MailboxAddress.Parse(toEmail));
            message.Subject = $"✈️ Резервация за полет {flightNumber} – потвърждение";
            message.Body = new TextPart("html") { Text = body };

            using var client = new SmtpClient();
            await client.ConnectAsync(host, port, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(user, pass);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
