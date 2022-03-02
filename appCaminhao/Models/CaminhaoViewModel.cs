using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace appCaminhao.Models
{
    [Table("Caminhao")]
    public class CaminhaoViewModel
    {
        public CaminhaoViewModel()
        {
            Fabricante = String.Empty;
            Modelo = String.Empty;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name ="Cód.")]
        public long Id { get; set; }


        [Required(ErrorMessage = "Campo {0} é requerido!", AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "Campo {0}, máximo de {1} caracteres.")]
        [Display(Name = "Fabricante")]
        public string Fabricante { get; set; }

        [Required(ErrorMessage = "Campo {0} é requerido!", AllowEmptyStrings = false)]
        [StringLength(2, ErrorMessage = "Campo {0}, máximo de {1} caracteres.")]
        [Display(Name = "Modelo")]
        [RegularExpression("FH|FM")]
        public string Modelo { get; set; } //Poderá aceitar apenas FH e FM

        [Required(ErrorMessage = "Campo {0} é requerido!", AllowEmptyStrings = false)]
        [Range(1900, 2023, ErrorMessage = "Ano de fabricação deve ser entre: 1900 até 2023")]
        [Display(Name = "Ano Fabricação")]
        public int AnoFabricacao { get; set; }

        [Required(ErrorMessage = "Campo {0} é requerido!", AllowEmptyStrings = false)]
        [Range(1900, 2024, ErrorMessage = "Ano do modelo deve ser entre: 1900 até 2024")]
        [Display(Name = "Ano Modelo")]
        public int AnoModelo { get; set; }
    }

}
