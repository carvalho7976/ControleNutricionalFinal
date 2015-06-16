using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControleNutricionalFinal.Models
{
    public class Refeicao
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime dataDeCriacao { get; set; }
    }
}