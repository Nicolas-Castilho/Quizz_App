using System.ComponentModel.DataAnnotations;

namespace QuizzApp.Models
{
    public class Quizzes
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Titulo { get; set; }
        [Required]
        [StringLength(60)]
        public string Descricao { get; set; }
        //AspNetUsers
        public string iduser { get; set; }
        //Temas
        public int temaid { get; set; }
        public Tema? tema { get; set; }
        //Topicos
        public int topicoid { get; set; }
        public Topico? topico { get; set; }
        public List<Pergunta> Perguntas { get; set; }
    }

    public class Pergunta
    {
        [Key] 
        public int Id { get; set; }
        [Required]
        [StringLength(70)]
        public string Texto { get; set; }
        [Required]
        public List<string> Respostas { get; set; }
    }

}
