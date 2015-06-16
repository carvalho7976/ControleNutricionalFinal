using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ControleNutricionalClient.ServiceAlimentoRefeicao;
using System.Diagnostics;


namespace ControleNutricionalClient.Controllers
{
    public class AlimentoRefeicaoController : ApiController
    {
        // GET api/alimentorefeicao
        public IEnumerable<AlimentoRefeicao> Get()
        {
            try {
                ServiceAlimentoRefeicao.ServiceAlimentoRefeicaoClient servico = new ServiceAlimentoRefeicao.ServiceAlimentoRefeicaoClient();
                return servico.findall();
            }
            catch (Exception ex) {
                
                return null;
            }
        }

        // GET api/alimentorefeicao/5
        public AlimentoRefeicao Get(string id)
        {
            try {
                ServiceAlimentoRefeicao.ServiceAlimentoRefeicaoClient servico = new ServiceAlimentoRefeicao.ServiceAlimentoRefeicaoClient();
                return servico.find(id);
            }
            catch (Exception ex) {
                //asdfasdfasd
                return null;
                //verificar
            }
        }

        // POST api/alimentorefeicao
        public HttpResponseMessage Post(AlimentoRefeicao alimentoRefeicao) {
            try {                         
                //implementar
                //metodo esta errado
                
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, alimentoRefeicao);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = alimentoRefeicao }));
                return response;
            }
            catch (Exception ex) {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // PUT api/alimentorefeicao/5
        public HttpResponseMessage Put(int id, AlimentoRefeicao alimentoRefeicao) {
            //if (!ModelState.IsValid) {
            //    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            //}

            if (id != alimentoRefeicao.Id) {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            // db.Entry(todo).State = EntityState.Modified;

            try {
                ServiceAlimentoRefeicao.ServiceAlimentoRefeicaoClient servico = new ServiceAlimentoRefeicao.ServiceAlimentoRefeicaoClient(); servico.create(alimentoRefeicao);
                servico.edit(alimentoRefeicao);
            }
            catch (Exception ex) {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // DELETE api/alimentorefeicao/5
        public HttpResponseMessage Delete(AlimentoRefeicao alimentoRefeicao){
            if (alimentoRefeicao == null) {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            try {
             ServiceAlimentoRefeicao.ServiceAlimentoRefeicaoClient servico = new ServiceAlimentoRefeicao.ServiceAlimentoRefeicaoClient(); servico.create(alimentoRefeicao);

                servico.delete(alimentoRefeicao);
            }
            catch (Exception ex) {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, alimentoRefeicao);
        }
        
    }
}
