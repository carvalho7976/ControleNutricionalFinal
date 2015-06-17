using ControleNutricionalFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ControleNutricionalFinal
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWCFNutricao" in both code and config file together.
    [ServiceContract]
    public interface IWCFNutricaoREST
    {
        //CRUD para Alimento
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "findAllAlimento", ResponseFormat = WebMessageFormat.Json)]
        List<Alimento> findAllAlimento();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "findAlimento/{id}", ResponseFormat = WebMessageFormat.Json)]
        Alimento findAlimento(string id);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "createAlimento", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool createAlimento(Alimento alimento);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "editAlimento", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool editAlimento(Alimento alimento);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "deleteAlimento", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool deleteAlimento(Alimento alimento);

        //Grupo
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "findAllGrupo", ResponseFormat = WebMessageFormat.Json)]
        List<Grupo> findAllGrupo();

        //Crud para AlimentoRefeicao
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "findAllRefeicao", ResponseFormat = WebMessageFormat.Json)]
        List<Refeicao> findAllRefeicao();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "findRefeicao/{id}", ResponseFormat = WebMessageFormat.Json)]
        Refeicao findRefeicao(string id);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "createRefeicao", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Refeicao createRefeicao(Refeicao refeicao);//mudei de bool para Refeicao --

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "editRefeicao", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool editRefeicao(Refeicao refeicao);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "deleteRefeicao", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool deleteRefeicao(Refeicao refeicao);
      

        //Crud para AlimentoRefeicao
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "findAllAlimentoRefeicao", ResponseFormat = WebMessageFormat.Json)]
        List<AlimentoRefeicao> findAllAlimentoRefeicao();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "listaAlimentosPorRefeicao/{dia}/{mes}/{ano}", ResponseFormat = WebMessageFormat.Json)]
        List<AlimentoRefeicao> listaAlimentosPorRefeicao(string dia, string mes,string ano);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "findAlimentoRefeicao/{id}", ResponseFormat = WebMessageFormat.Json)]
        AlimentoRefeicao findAlimentoRefeicao(string id);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "createAlimentoRefeicao", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool createAlimentoRefeicao(AlimentoRefeicao alimentoRefeicao);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "editAlimentoRefeicao", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool editAlimentoRefeicao(AlimentoRefeicao alimentoRefeicao);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "deleteAlimentoRefeicao", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool deleteAlimentoRefeicao(AlimentoRefeicao alimentoRefeicao);

    }
}
