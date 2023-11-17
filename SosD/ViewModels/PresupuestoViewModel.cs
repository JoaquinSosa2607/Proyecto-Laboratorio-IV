using SosD.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SosD.ViewModels
{
    public class PresupuestoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor, ingresar la descripción.")]
        [Display(Name = "Descripción")]
        public string? Descripcion { get; set; }

        [Display(Name = "Cantidad")]
        public int? Cantidad { get; set; }

        [Display(Name = "Tipo de Prenda")]
        public int? TipoPrendaId { get; set; }
        [ForeignKey("TipoPrendaId")]
        public virtual TipoPrenda? TipoPrenda { get; set; }

        [NotMapped]
        [Display(Name = "Imagen Prenda")]
        public IFormFile Imagen { get; set; }

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
        [DataType(DataType.Date)]
        public DateTime? FechaRegistro { get; set; }

        
    }
}