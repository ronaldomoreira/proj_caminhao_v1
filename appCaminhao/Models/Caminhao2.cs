using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace appCaminhao.Models
{
    public class Caminhao2
    {
        public Caminhao2()
        {
            Fabricante = String.Empty;
            Modelo = String.Empty;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "Bigint")]
        public long Id { get; set; }


        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Fabricante { get; set; }

        [Required]
        [Column(TypeName = "varchar(2)")]
        [RegularExpression("FH|FM")]
        public string Modelo { get; set; } //Poderá aceitar apenas FH e FM

        [Required]
        public int AnoFabricacao { get; set; }

        [Required]
        public int AnoModelo { get; set; }
    }
}
