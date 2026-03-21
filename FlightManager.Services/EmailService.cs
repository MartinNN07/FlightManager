using FlightManager.Data.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;

namespace FlightManager.Services
{
    internal class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendReservationConfirmationAsync(Reservations reservations)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(_config["Email:From"]));
            message.To.Add(MailboxAddress.Parse(reservation.ContactEmail));
            message.Subject = $"Потвърждение на резервация #{reservation.Id}";

            var body = new StringBuilder();
            body.AppendLine($"<h2>Резервация #{reservations.Id}</h2>");
            body.AppendLine($"<p><b>Полет:</b> {flight.FlightNumber}</p>");
            body.AppendLine($"<p><b>От:</b> {flight.DepartureLocation} → <b>До:</b> {flight.ArrivalLocation}</p>");
            body.AppendLine($"<p><b>Излитане:</b> {flight.DepartureTime:dd.MM.yyyy HH:mm}</p>");
            body.AppendLine($"<p><b>Кацане:</b> {flight.ArrivalTime:dd.MM.yyyy HH:mm}</p>");
            body.AppendLine("<hr/><h3>Пътници:</h3><ul>");

            foreach (var p in reservations.Passengers)
            {
                body.AppendLine($"<li>{p.FirstName} {p.MiddleName} {p.LastName} " +
                                $"— {(p.TicketType == TicketType.Business ? "Бизнес класа" : "Икономична класа")}</li>");
            }

            body.AppendLine("</ul>");

            message.Body = new TextPart("html") { Text = body.ToString() };

            using var client = new SmtpClient();
            await client.ConnectAsync(
                _config["Email:Host"],
                int.Parse(_config["Email:Port"]!),
                bool.Parse(_config["Email:UseSsl"]!));
            await client.AuthenticateAsync(_config["Email:Username"], _config["Email:Password"]);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
