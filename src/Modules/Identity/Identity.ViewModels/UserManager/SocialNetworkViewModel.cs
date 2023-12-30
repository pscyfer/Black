using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Identity.ViewModels.UserManager;

public class SocialNetworkViewModel
{
    public SocialNetworkViewModel()
    {
    }

    public SocialNetworkViewModel(string instagram, string telegram, string youTube, string whatsApp, string linkdine, string gitHub, string email, string discord)
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
    [Display(Name = "اینستاگرام",Prompt = "اینستاگرام")]
    public string? Instagram { get; set; }

    [Display(Name = "تلگرام",Prompt = "تلگرام")]
    public string? Telegram { get; set; }

    [Display(Name = "یوتیوب", Prompt = "یوتیوب")]
    public string? YouTube { get; set; }

    [Display(Name = "واتساپ", Prompt = "واتساپ")]
    public string? WhatsApp { get; set; }

    [Display(Name = "لینکدین", Prompt = "لینکدین")]
    public string? Linkdine { get; set; }

    [Display(Name = "گیت هاب", Prompt = "گیت هاب")]
    public string? GitHub { get; set; }

    [Display(Name = "ایمیل", Prompt = "ایمیل")]
    public string? Email { get; set; }

    [Display(Name = "دیسکورد", Prompt = "دیسکورد")]
    public string? Discord { get; set; }
}