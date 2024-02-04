using System.ComponentModel.DataAnnotations;

namespace QuizzApp.Models
{
    public class Tema
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string nmtema { get; set; }
        public ICollection<Topico> Topicos { get; set; }
    }
}
