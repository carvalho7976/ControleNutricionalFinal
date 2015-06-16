using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ControleNutricionalClient.ServiceAlimento;
using System.Diagnostics;
namespace ControleNutricionalClient.Controllers
{
    public class AlimentoController : ApiController
    {
        // GET api/alimento
        public IEnumerable<Alimento> Get()
        {
            try
            {
                ServiceAlimento.ServiceAlimentoClient servico = new ServiceAlimentoClient();

                return servico.findall();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return null;
            }

        }

        // GET api/alimento/5
        public Alimento Get(string id)
        {
            try
            {
                ServiceAlimento.ServiceAlimentoClient servico = new ServiceAlimentoClient();
                return servico.find(id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return null;
            }
        }

        // POST api/alimento
        public HttpResponseMessage Post(Alimento alimento)
        {
            Debug.Write("create ---------------------");
            //if (ModelState.IsValid) {

            try
            {
                ServiceAlimento.ServiceAlimentoClient servico = new ServiceAlimentoClient();
                servico.create(alimento);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, alimento);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = alimento.Id }));
                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

            // }
            //else {
            //Debug.Write("Not valid");

            // return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            //}
        }

        // PUT api/alimento/5
        public HttpResponseMessage Put(int id, Alimento alimento)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != alimento.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            // db.Entry(todo).State = EntityState.Modified;

            try
            {
                ServiceAlimento.ServiceAlimentoClient servico = new ServiceAlimentoClient();
                servico.edit(alimento);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // DELETE api/alimento/5
        public HttpResponseMessage Delete(Alimento alimento)
        {
            //Alimento a = db.Alimento.Find(id);
            if (alimento == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            try
            {
                ServiceAlimento.ServiceAlimentoClient servico = new ServiceAlimentoClient();
                servico.delete(alimento);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, alimento);
        }
    }
}
