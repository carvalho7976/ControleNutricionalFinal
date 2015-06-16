using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ControleNutricionalFinal.Models;
using System.Diagnostics;
using System.Data.Entity;

namespace ControleNutricionalFinal
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WCFNutricao" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WCFNutricao.svc or WCFNutricao.svc.cs at the Solution Explorer and start debugging.
    public class WCFNutricao : IWCFNutricaoREST, IWCFNutricaoSOAP
    {        
        public List<Alimento> findAllAlimento()
        {
            using (NutricaoContext mde = new NutricaoContext())
            {
                return mde.Alimentos.ToList();
            };
        }

        public Alimento findAlimento(string id)
        {
            using (NutricaoContext mde = new NutricaoContext())
            {
                int nid = Convert.ToInt32(id);
                return mde.Alimentos.Where(ae => ae.Id == nid).First();
            };
        }

        public bool createAlimento(Alimento alimento)
        {
            using (NutricaoContext mde = new NutricaoContext())
            {
                try
                {                    
                    mde.Alimentos.Add(alimento);
                    mde.SaveChanges();

                    return true;
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.ToString());
                    return false;
                }
            };
        }

        public bool editAlimento(Alimento alimento)
        {
            using (NutricaoContext mde = new NutricaoContext())
            {
                var result = mde.Alimentos.SingleOrDefault(a => a.Id == alimento.Id);

                if (result != null)
                {
                    //mde.Refeicao.Add(refeicao);                    
                    mde.Entry(alimento).State = EntityState.Modified;
                    mde.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            };
        }

        public bool deleteAlimento(Alimento alimento)
        {
            using (NutricaoContext mde = new NutricaoContext())
            {
                var result = mde.Alimentos.Where(a => a.Id == alimento.Id).SingleOrDefault();
                if (result != null)
                {
                    mde.Alimentos.Remove(result);
                    mde.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            };
        }

        public List<Grupo> findAllGrupo()
        {
            using (NutricaoContext mde = new NutricaoContext())
            {
                return mde.Grupos.ToList();
            };
        }

        public List<Refeicao> findAllRefeicao()
        {
            using (NutricaoContext mde = new NutricaoContext())
            {
                return mde.Refeicao.ToList();
            };
        }

        public Refeicao findRefeicao(string id)
        {
            using (NutricaoContext mde = new NutricaoContext())
            {
                int nid = Convert.ToInt32(id);
                return mde.Refeicao.Where(ae => ae.Id == nid).First();
            };
        }

        public bool createRefeicao(Refeicao refeicao)
        {
            using (NutricaoContext mde = new NutricaoContext())
            {
                try
                {
                    Debug.Write("Entrou no create");
                    mde.Refeicao.Add(refeicao);
                    mde.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.ToString());
                    return false;
                }
            };
        }

        public bool editRefeicao(Refeicao refeicao)
        {
            using (NutricaoContext mde = new NutricaoContext())
            {
                var result = mde.Refeicao.SingleOrDefault(r => r.Id == refeicao.Id);

                if (result != null)
                {
                    //mde.Refeicao.Add(refeicao);                    
                    mde.Entry(refeicao).State = EntityState.Modified;
                    mde.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            };
        }

        public bool deleteRefeicao(Refeicao refeicao)
        {
            using (NutricaoContext mde = new NutricaoContext())
            {
                var result = mde.Refeicao.Where(rf => rf.Id == refeicao.Id).SingleOrDefault();
                if (result != null)
                {
                    mde.Refeicao.Remove(result);
                    mde.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            };
        }

        public List<AlimentoRefeicao> findAllAlimentoRefeicao()
        {
            using (NutricaoContext mde = new NutricaoContext())
            {
                return mde.AlimentoRefeicao.ToList();
            };
        }

        public List<AlimentoRefeicao> relatorioConsumoDia()
        {
            using (NutricaoContext mde = new NutricaoContext())
            {
                //int nid = Convert.ToInt32(id);
                //Funcionando modo 1
                //var queryAlimentoRefeicao = mde.AlimentoRefeicao.Select(column => new { Quantidade = column.Quantidade, Id = column.Id}).ToList();
                //return queryAlimentoRefeicao.Select(column => new AlimentoRefeicao { Quantidade = column.Quantidade, Id = column.Id }).ToList();

                //Funcionando modo 2
                //var anyms_AlimentoRefeicao = mde.AlimentoRefeicao.Select(column => new { Quantidade = column.Quantidade}).ToList();
                //List<AlimentoRefeicao> result = new List<AlimentoRefeicao>();
                //foreach (var currentAlimentoRefeicao in anyms_AlimentoRefeicao)
                //{
                //    result.Add(new AlimentoRefeicao { Quantidade = currentAlimentoRefeicao.Quantidade });
                //}
                //return result;
                var queryAlimentoRefeicao = mde.AlimentoRefeicao.Select(column => new { Quantidade = column.Quantidade * column.Alimento.Valor_calorico }).ToList();
                return queryAlimentoRefeicao.Select(column => new AlimentoRefeicao { Quantidade = column.Quantidade.HasValue ? column.Quantidade.Value : 0 }).ToList();
            };
        }

        public AlimentoRefeicao findAlimentoRefeicao(string id)
        {
            using (NutricaoContext mde = new NutricaoContext())
            {
                int nid = Convert.ToInt32(id);
                return mde.AlimentoRefeicao.Where(ae => ae.Id == nid).First();
            };
        }

        public bool createAlimentoRefeicao(AlimentoRefeicao alimentoRefeicao)
        {
            using (NutricaoContext mde = new NutricaoContext())
            {
                try
                {
                    mde.AlimentoRefeicao.Add(alimentoRefeicao);
                    mde.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.ToString());
                    return false;
                }
            };
        }

        public bool editAlimentoRefeicao(AlimentoRefeicao alimentoRefeicao)
        {
            using (NutricaoContext mde = new NutricaoContext())
            {
                var result = mde.AlimentoRefeicao.SingleOrDefault(r => r.Id == alimentoRefeicao.Id);

                if (result != null)
                {
                    //mde.Refeicao.Add(refeicao);                    
                    mde.Entry(alimentoRefeicao).State = EntityState.Modified;
                    mde.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            };
        }

        public bool deleteAlimentoRefeicao(AlimentoRefeicao alimentoRefeicao)
        {
            using (NutricaoContext mde = new NutricaoContext())
            {
                var result = mde.AlimentoRefeicao.Where(rf => rf.Id == alimentoRefeicao.Id).SingleOrDefault();
                if (result != null)
                {
                    mde.AlimentoRefeicao.Remove(result);
                    mde.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            };
        }
    }
}
