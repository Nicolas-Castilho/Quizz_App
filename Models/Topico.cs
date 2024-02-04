using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizzApp.Models
{
    public class Topico
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string nmtopico { get; set; }
        [ForeignKey("temaid")]
        public int temaid { get; set; }
        public Tema tema { get; set; }

    }
}
