using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ControleNutricionalFinal.Models
{
    [DataContract]
    public class Usuario
    {
        [DataMember]
        public int Id { get ; set; }
        [DataMember]
        public string Nome { get; set; }
        [DataMember]
        public string Senha { get; set; }

        public Usuario(string usuario, string senha)
        {

            Nome = usuario;
            Senha = senha;
        }

      
    }
}