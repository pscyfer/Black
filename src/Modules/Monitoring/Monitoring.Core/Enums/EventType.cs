using System.ComponentModel.DataAnnotations;

namespace Monitoring.Core.Enums;
public enum EventType
{
    [Display(Name ="Up")]
    Up = 0,
    [Display(Name = "Down")]
    Down = 1,
    [Display(Name = "Pause")]
    Pause = 2,
    [Display(Name = "Start")]
    Start = 3
}