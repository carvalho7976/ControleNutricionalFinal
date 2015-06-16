using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControleNutricionalFinal.Models
{
    public class Grupo
    {
        public Grupo()
        {
            this.Alimentos = new List<Alimento>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }

        public virtual List<Alimento> Alimentos { get; set; }
    }
}