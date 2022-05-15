using Microsoft.AspNetCore.Mvc;
using MimeKit;

using MailKit.Net.Smtp;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobPlatformUIService.Features.Mail;

[Route("api/[controller]")]
[ApiController]
public class SendMailController : ControllerBase
{
    // GET: api/<SendMailController>
    [HttpGet]
    public bool Get()
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Sandu Ilie Cristian", "crystisandu3@gmail.com"));
        message.To.Add(new MailboxAddress("Cristi Sandu", "ilie.cristian.sandu@gmail.com"));
        message.Subject = "How you doin'?";

        message.Body = new TextPart("plain")
        {
            Text = @"Hey Chandler,

I just wanted to let you know that Monica and I were going to go play some paintball, you in?

-- Joey"
        };

        using (var client = new SmtpClient())
        {
            client.Connect("smtp.friends.com", 587, false);

            // Note: only needed if the SMTP server requires authentication
            client.Authenticate("joey", "password");

            client.Send(message);
            client.Disconnect(true);
        }

        return true;
    }

    // GET api/<SendMailController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<SendMailController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<SendMailController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<SendMailController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
