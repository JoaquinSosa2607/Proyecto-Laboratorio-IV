using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SosD.Models
{
    [Table("Promociones")]
    public class Promociones
    {

        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Ingresar la descripción para continuar.")]
        [Display(Name = "Descripción")]
        [StringLength(50)]
        public string? Descripcion { get; set; }

        [Display(Name = "Cantidad Mínima")]
        public int? CantMin { get; set; }

        [Display(Name = "Descuento")]
        public int? Descuento { get; set; }

        [Display(Name = "Medio de Pago")]
        public int? MedioPagoId { get; set; }
        [ForeignKey("MedioPagoId")]
        public virtual MedioPago? MedioPago { get; set; }

        [Column(TypeName = "smalldatetime")]
        [DataType(DataType.Date)]
        public DateTime? FechaRegistro { get; set; }
    }
}
