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
        message.From.Add(new MailboxAddress("PWeb_smtp_service", "gogoseldulcic90@gmail.com"));
        message.To.Add(new MailboxAddress("User's name here", "User's email address here"));
        message.Subject = "How you doin'?";

        message.Body = new TextPart("plain")
        {
            Text = @"Hey Chandler,

I just wanted to let you know that Monica and I were going to go play some paintball, you in?

-- Joey"
        };

        using (var client = new SmtpClient())
        {
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate("gogoseldulcic90@gmail.com", "sgjbarfdrkskdrvd");

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
