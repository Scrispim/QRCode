using System.ComponentModel.DataAnnotations;

namespace QRCode.Models
{
    public class QRCodeModel
    {
        [Display(Name = "Informe o texto")]
        public string? QRCodeTexto { get; set; }
    }
}
