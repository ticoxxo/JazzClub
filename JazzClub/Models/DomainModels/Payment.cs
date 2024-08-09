using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace JazzClub.Models.DomainModels
{
    public class Payment
    {
      
        public int PaymentId { get; set; }

        [Display(Name ="Cantidad")]
		[DataType(DataType.Currency)]
		[Column(TypeName = "decimal(18, 2)")]
		[Required(ErrorMessage = "Agregue una cantidad")]
		public decimal Amount { get; set; }

		[Display(Name = "Descuento")]
		[DataType(DataType.Currency)]
		[Column(TypeName = "decimal(18, 2)")]
		//[DataType(DataType.Currency)] 
		[Required(ErrorMessage = "Agregue un descuento")]
		public decimal Discount { get; set; }

		[Display(Name = "Total")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        //[DataType(DataType.Currency)] 
        [Required(ErrorMessage = "Agregue un pago")]
        public decimal Total { get; set; }

        [Required(ErrorMessage = "Agregue un estudiante")]
        public int StudentId { get; set; }
		public Student Student { get; set; }

		[Required(ErrorMessage = "Agregue una clase")]
        public int CourseId { get; set; }
		public Course Course { get; set; }

		[Required(ErrorMessage = "Especifique el tipo de pago")]
        public string PaymentType { get; set; }

		[DisplayName("Fecha de pago")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Agregue una fecha de pago")]
        public DateTime PaymentDate { get; set; }

        [DisplayName("Fecha de Vencimiento")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Agregue una fecha de expiracion")]
        public DateTime PaymentExpirationDate { get; set; }

        [Range(0, 5, ErrorMessage = "Only status from 0 to 5")]
        [Required]
        public int status { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
