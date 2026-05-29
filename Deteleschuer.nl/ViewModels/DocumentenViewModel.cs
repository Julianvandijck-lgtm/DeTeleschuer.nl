using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Deteleschuer.nl.ViewModels;

public class DocumentenViewModel
{
    [Required] public IFormFile FotoLegitimatie { get; set; }
    [Required] public IFormFile FotoBankpas { get; set; }
    [Required] public string DigitaleHandtekening { get; set; }

}  