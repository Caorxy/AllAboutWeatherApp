using System.ComponentModel.DataAnnotations;

namespace AllAboutWeatherApp.MVVM.Model;

public class SearchedLocationData
{
    [Required]
        public string? Location { get; init; }
}