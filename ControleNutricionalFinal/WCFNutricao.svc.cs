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

        //mudando de bool para refeicao
        public Refeicao createRefeicao(Refeicao refeicao)
        {
            using (NutricaoContext mde = new NutricaoContext())
            {
                try
                {

                    Debug.Write("Entrou no create");

                    refeicao.dataDeCriacao = DateTime.Today;

                    mde.Refeicao.Add(refeicao);
                    mde.SaveChanges();
                    return refeicao;
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.ToString());
                    return null;
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

        //ListaDeAlimentoPorRefeicao
        public List<AlimentoRefeicao> listaAlimentosPorRefeicao()
        {
            using (NutricaoContext mde = new NutricaoContext())
            {
                var queryAlimentoRefeicao = mde.AlimentoRefeicao.Select(column => new 
                { Alimento = column.Alimento, Refeicao = column.Refeicao, Quantidade = column.Quantidade, ValorCaloricoTotal = (column.Quantidade * column.Alimento.Valor_calorico)/100 }).ToList();
                return queryAlimentoRefeicao.Where(r => r.Refeicao.Id == 2).Select(column => new AlimentoRefeicao { Alimento = column.Alimento, Quantidade = column.Quantidade, ValorCaloricoTotal = (double)column.ValorCaloricoTotal}).ToList();
                
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


        //public bool registration(Usuario user)
        //{
        //    using (NutricaoContext mde = new NutricaoContext())
        //    {
        //        try
        //        {
        //            mde.Usuario.Add(user);
        //            mde.SaveChanges();
        //            return true;
        //        }
        //        catch (Exception ex)
        //        {
        //            Debug.Write(ex.ToString());
        //            return false;
        //        }
        //    };
        //}

        public bool login(string nome, string pwd)
        {
            //using (NutricaoContext mde = new NutricaoContext())
            //{
            //    var messages = from user in mde.Usuario
            //                   where user.Nome == nome && user.Senha == pwd
            //                   select user;

            //    if (messages.Count() > 0)
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //};

            if (nome == null || pwd == null)
            {
                return false;
            }
            if (nome == "admin" && pwd == "admin")
            {
                return true;
            }
            return false;
        }
    }
}
