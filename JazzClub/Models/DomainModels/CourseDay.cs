using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace JazzClub.Model.DomainsModels {
    public class CourseDay
    {
    public int Id { get; set; }
    public int CoursesCourseId { get; set; }
    public int DaysDayId { get; set; }
    [Range(0, 5, ErrorMessage = "Only status from 0 to 5")]
    [Required]
    public int status { get; set; }
     public DateTime CreatedAt { get; set; }
    }
}
