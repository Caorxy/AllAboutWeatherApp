using System.ComponentModel.DataAnnotations;

namespace AllAboutWeatherApp.MVVM.Model.DataStorage;

public class SearchedLocationData
{
    [Required]
        public string? Location { get; init; }
}