namespace Common.Application;

public class SocialNetworkDto
{
    public SocialNetworkDto()
    {
    }

    public SocialNetworkDto(string instagram, string telegram, string youTube, string whatsApp, string linkdine, string gitHub, string email, string discord)
    {
        Instagram = instagram;
        Telegram = telegram;
        YouTube = youTube;
        WhatsApp = whatsApp;
        Linkdine = linkdine;
        GitHub = gitHub;
        Email = email;
        Discord = discord;
    }
    public string Instagram { get;  set; }
    public string Telegram { get;  set; }
    public string YouTube { get;  set; }
    public string WhatsApp { get; set; }
    public string Linkdine { get; set; }
    public string GitHub { get;  set; }
    public string Email { get;  set; }
    public string Discord { get;  set; }
}