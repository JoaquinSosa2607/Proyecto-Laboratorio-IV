using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SosD.Models
{
    [Table("Presupuesto")]
    public class Presupuesto
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Display(Name = "Descripción")]
        [StringLength(50)]
        public string? Descripcion { get; set; }

        [Display(Name = "Cantidad")]
        public int? Cantidad { get; set; }

        [Display(Name = "Tipo de Prenda")]
        public int? TipoPrendaId { get; set; }
        [ForeignKey("TipoPrendaId")]
        public virtual TipoPrenda? TipoPrenda { get; set; }

        [Display(Name = "Imagen")]
        public string? ImagenPrenda { get; set; }

        [Display(Name = "Diseño")]
        public int? DiseñoId { get; set; }
        [ForeignKey("DiseñoId")]
        public virtual Diseño? Diseño { get; set; }

        [Display(Name = "Precio Unitario")]
        public int? PrecioUni { get; set; }

        [Display(Name = "Precio Total")]
        public int PrecioTotal => (PrecioUni ?? 0) * (Cantidad ?? 0);

        [Display(Name = "Fecha Registro")]
        [Column(TypeName = "smalldatetime")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? FechaRegistro { get; set; }
    }
}
