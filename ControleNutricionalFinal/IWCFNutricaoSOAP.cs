using ControleNutricionalFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace ControleNutricionalFinal
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWCFNutricao" in both code and config file together.
    [ServiceContract]
    public interface IWCFNutricaoSOAP
    {
        //CRUD para Alimento
        [OperationContract]
        List<Alimento> findAllAlimento();

        [OperationContract]
        Alimento findAlimento(string id);

        [OperationContract]
        bool createAlimento(Alimento alimento);

        [OperationContract]
        bool editAlimento(Alimento alimento);

        [OperationContract]
        bool deleteAlimento(Alimento alimento);

        //Grupo
        [OperationContract]
        List<Grupo> findAllGrupo();

        //Crud para AlimentoRefeicao
        [OperationContract]
        List<Refeicao> findAllRefeicao();

        [OperationContract]
        Refeicao findRefeicao(string id);

        [OperationContract]
        Refeicao createRefeicao(Refeicao refeicao);//mudei de bool para refeicao

        [OperationContract]
        bool editRefeicao(Refeicao refeicao);

        [OperationContract]
        bool deleteRefeicao(Refeicao refeicao);


        //Crud para AlimentoRefeicao
        [OperationContract]
        List<AlimentoRefeicao> findAllAlimentoRefeicao();

        [OperationContract]
        List<AlimentoRefeicao> relatorioValorNutricionalTotalDiario();

        [OperationContract]
        List<AlimentoRefeicao> listaAlimentosPorRefeicao(string dia, string mes, string ano);

        [OperationContract]
        AlimentoRefeicao findAlimentoRefeicao(string id);

        [OperationContract]
        bool createAlimentoRefeicao(AlimentoRefeicao alimentoRefeicao);

        [OperationContract]
        bool editAlimentoRefeicao(AlimentoRefeicao alimentoRefeicao);

        [OperationContract]
        bool deleteAlimentoRefeicao(AlimentoRefeicao alimentoRefeicao);

        //[OperationContract]
        //bool registration(Usuario user);

        [OperationContract]
        bool login(string nome, string pwd);

    }
}

