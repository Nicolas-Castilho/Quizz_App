using System.ComponentModel.DataAnnotations;

namespace QuizzApp.Models
{
    public class Pergunta
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(70)]
        public string Texto { get; set; }
        [Required]
        [StringLength(30)]
        public string Resposta1 { get; set; }
        [Required]
        [StringLength(30)]
        public string Resposta2 { get; set; }
        [Required]
        [StringLength(30)]
        public string Resposta3 { get; set; }
        [Required]
        [StringLength(30)]
        public string Resposta4 { get; set; }
        [Required]
        public int RespostaCorreta { get; set; }
        [StringLength(255)]
        public string ImagemPath { get; set; }


        //Quizz
        public int quizzid { get; set; }
        public Quizz? quizz { get; set; }

    }
}