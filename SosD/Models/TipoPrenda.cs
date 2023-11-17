using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SosD.Models
{
    [Table("Tipo Prenda")]
    public class TipoPrenda
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Ingresar la descripción para continuar.")]
        [Display(Name = "Descripción")]
        [StringLength(50)]
        public string? Descripcion { get; set; }

        [Column(TypeName = "smalldatetime")]
        [DataType(DataType.Date)]
        public DateTime? FechaRegistro { get; set; }

        [Required(ErrorMessage = "Ingresar el tipo de tela para continuar.")]
        [Display(Name = "TipoTela")]
        public int? TipoTelaId { get; set; }
        [ForeignKey("TipoTelaId")]
        public virtual TipoTela? TipoTela { get; set; }
    }
}
