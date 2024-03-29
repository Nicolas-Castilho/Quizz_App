﻿using System.ComponentModel.DataAnnotations;

namespace QuizzApp.Models
{
    public class Quizz
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
        //Perguntas
        public ICollection<Pergunta> ?Perguntas { get; set; }
    }

}
