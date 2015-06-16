using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ControleNutricionalClient.ServiceGrupo;
using System.Diagnostics;

namespace ControleNutricionalClient.Controllers
{
    public class GrupoController : ApiController
    {
        // GET api/grupo
        public IEnumerable<Grupo> Get(){
            try {
                ServiceGrupo.SeviceGrupoClient servico = new SeviceGrupoClient();

                return servico.findall();
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.ToString());
                return null;
            }
        }

        // GET api/grupo/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/grupo
        public void Post([FromBody]string value)
        {
        }

        // PUT api/grupo/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/grupo/5
        public void Delete(int id)
        {
        }
    }
}
