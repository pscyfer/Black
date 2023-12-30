using Common.Domain.Exceptions;

namespace Common.Domain.ValueObjects;

public class SocialNetwork : ValueObject
{
    #region Constructor

    public SocialNetwork()
    {

    }

    public SocialNetwork(string instagram, string telegram)
    {
        Instagram = instagram;
        Telegram = telegram;
    }

    public SocialNetwork(string instagram, string telegram, string youTube, string whatsApp, string linkdin,
        string gitHub, string email, string discord)
    {
        Instagram = instagram;
        Telegram = telegram;
        YouTube = youTube;
        WhatsApp = whatsApp;
        Linkdin = linkdin;
        GitHub = gitHub;
        Email = email;
        Discord = discord;
    }

    #endregion

    #region Propertis
    public string Instagram { get; private set; }
    public string Telegram { get; private set; }
    public string YouTube { get; private set; }
    public string WhatsApp { get; private set; }
    public string Linkdin { get; private set; }
    public string GitHub { get; private set; }
    public string Email { get; private set; }
    public string Discord { get; private set; }

    #endregion



    #region Methods
    public SocialNetwork AddSocialNetwork(SocialNetwork socialNetwork)
    {
        var newSocialNetwork = new SocialNetwork
        {
            Telegram = socialNetwork.Telegram,
            Instagram = socialNetwork.Instagram,
            YouTube = socialNetwork.YouTube,
            Discord = socialNetwork.Discord,
            Email = socialNetwork.Email,
            WhatsApp = socialNetwork.WhatsApp,
            Linkdin = socialNetwork.Linkdin,
            GitHub = socialNetwork.GitHub
        };
        return newSocialNetwork;
    }



    public void AddOrUpdateTelegramSocial(string url)
    {
        Garud(url);
        Telegram = url;
    }
    public void AddOrUpdateInstagramSocial(string url)
    {
        Garud(url);
        Instagram = url;
    }
    public void AddOrUpdateYouTubeSocial(string url)
    {
        Garud(url);
        YouTube = url;
    }
    public void AddOrUpdateDiscordSocial(string url)
    {
        Garud(url);
        Discord = url;
    }
    public void AddOrUpdateEmailSocial(string url)
    {
        Garud(url);
        Email = url;
    }
    public void AddOrUpdateWhatsAppSocial(string url)
    {
        Garud(url);
        WhatsApp = url;
    }
    public void AddOrUpdateLinkdinSocial(string url)
    {
        Garud(url);
        Linkdin = url;
    }
    public void AddOrUpdateGitHubSocial(string url)
    {
        Garud(url);
        GitHub = url;
    }

    public void RemoveAll()
    {
        Telegram = string.Empty;
        Instagram = string.Empty;
        WhatsApp = string.Empty;
        GitHub = string.Empty;
        Linkdin = string.Empty;
        YouTube = string.Empty;
    }
    public static SocialNetwork CreateIntance()
    {
        return new SocialNetwork();
    }


    #endregion

    private void Garud(string url)
    {
        if (string.IsNullOrWhiteSpace(url)) NullOrEmptyDomainDataException.CheckString(url, url);
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Telegram;
        yield return WhatsApp;
        yield return Linkdin;
        yield return GitHub;
        yield return Email;
        yield return Discord;
        yield return YouTube;
        yield return Instagram;

    }
}