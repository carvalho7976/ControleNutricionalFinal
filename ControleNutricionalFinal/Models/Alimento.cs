using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControleNutricionalFinal.Models
{
    public class Alimento
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Grupo { get; set; }
        public Nullable<int> Porcao { get; set; }
        public Nullable<double> Valor_calorico { get; set; }
        public Nullable<double> Cho { get; set; }
        public Nullable<double> Proteinas { get; set; }
        public Nullable<double> Gorduras_totais { get; set; }
        public Nullable<double> Gorduras_saturadas { get; set; }
        public Nullable<double> Colesterol { get; set; }
        public Nullable<double> Fosforo { get; set; }
        public Nullable<double> Poliinsaturados { get; set; }
        public Nullable<double> Monoinsaturados { get; set; }
        public Nullable<double> Vitamina_b1 { get; set; }
        public Nullable<double> Vitamina_b2 { get; set; }
        public Nullable<double> Vitamina_b3 { get; set; }
        public Nullable<double> Vitamina_b6 { get; set; }
        public Nullable<double> Gordura_trans { get; set; }
        public Nullable<double> Fibra_alimentar { get; set; }
        public Nullable<double> Acucar { get; set; }
        public Nullable<double> Sodio { get; set; }
        public Nullable<double> Selenio { get; set; }
        public Nullable<double> Calcio { get; set; }
        public Nullable<double> Ferro { get; set; }
        public Nullable<double> Potassio { get; set; }
        public Nullable<double> Zinco { get; set; }
        public Nullable<double> Magnesio { get; set; }
        public Nullable<double> Vitamina_a { get; set; }
        public Nullable<double> Vitamina_b { get; set; }
        public Nullable<double> Vitamina_c { get; set; }
        public Nullable<double> Vitamina_d { get; set; }
        public Nullable<double> Vitamina_e { get; set; }
        public Nullable<double> Vitamina_b9 { get; set; }
        public Nullable<double> Vitamina_b12 { get; set; }

        public virtual Grupo Grupo1 { get; set; }
    }
}