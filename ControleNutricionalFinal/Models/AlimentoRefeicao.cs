using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControleNutricionalFinal.Models
{
    public class AlimentoRefeicao
    {
        public int Id { get; set; }
        public virtual Alimento Alimento { get; set; }
        public virtual Refeicao Refeicao { get; set; }
        public double Quantidade { get; set; }
        public double ValorCaloricoTotal { get; set; }
        public double Cho_valorTotal { get; set; }
        public double Proteinas_valorTotal { get; set; }
        public double Gorduras_totais_valorTotal { get; set; }
        public double Gorduras_saturadas_valorTotal { get; set; }
        public double Colesterol_valorTotal { get; set; }
        public double Fosforo_valorTotal { get; set; }
        public double Poliinsaturados_valorTotal { get; set; }
        public double Monoinsaturados_valorTotal { get; set; }
        public double Vitamina_b1_valorTotal { get; set; }
        public double Vitamina_b2_valorTotal { get; set; }
        public double Vitamina_b3_valorTotal { get; set; }
        public double Vitamina_b6_valorTotal { get; set; }
        public double Gordura_trans_valorTotal { get; set; }
        public double Fibra_alimentar_valorTotal { get; set; }
        public double Acucar_valorTotal { get; set; }
        public double Sodio_valorTotal { get; set; }
        public double Selenio_valorTotal { get; set; }
        public double Calcio_valorTotal { get; set; }
        public double Ferro_valorTotal { get; set; }
        public double Potassio_valorTotal { get; set; }
        public double Zinco_valorTotal { get; set; }
        public double Magnesio_valorTotal { get; set; }
        public double Vitamina_a_valorTotal { get; set; }
        public double Vitamina_b_valorTotal { get; set; }
        public double Vitamina_c_valorTotal { get; set; }
        public double Vitamina_d_valorTotal { get; set; }
        public double Vitamina_e_valorTotal { get; set; }
        public double Vitamina_b9_valorTotal { get; set; }
        public double Vitamina_b12_valorTotal { get; set; }


    }
}