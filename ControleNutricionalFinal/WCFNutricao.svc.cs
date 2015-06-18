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
        public List<AlimentoRefeicao> listaAlimentosPorRefeicao(string dia, string mes, string ano)
        {
            using (NutricaoContext mde = new NutricaoContext())
            {

                int nDia = Convert.ToInt32(dia);
                int nMes = Convert.ToInt32(mes);
                int nAno = Convert.ToInt32(ano);
                DateTime converteDate = new DateTime(nAno, nMes, nDia);
                //converteDate = Convert.ToDateTime(data);
                //converteDate = '17/06/2015';
                var queryAlimentoRefeicao = mde.AlimentoRefeicao.Select(column =>
                    new
                    {
                        Refeicao = column.Refeicao,
                        Alimento = column.Alimento,
                    }).ToList();

                return queryAlimentoRefeicao.Where(r => r.Refeicao.dataDeCriacao == converteDate).Select(column =>
                    new AlimentoRefeicao
                    {
                        Refeicao = column.Refeicao,
                        Alimento = column.Alimento
                    }).ToList();

            };
        }

        public List<AlimentoRefeicao> relatorioValorNutricionalTotalDiario()
        {
            using (NutricaoContext mde = new NutricaoContext())
            {
                
                var queryAlimentoRefeicao = mde.AlimentoRefeicao.Select(column =>
                    new
                    {
                        Alimento = column.Alimento,
                        Refeicao = column.Refeicao,
                        Quantidade = column.Quantidade,
                        ValorCaloricoTotal = ((column.Quantidade * column.Alimento.Valor_calorico) / 100),
                        Cho_valorTotal = (column.Quantidade * column.Alimento.Cho)/100,
                        Proteinas_valorTotal = (column.Quantidade * column.Alimento.Proteinas) / 100,
                        Gorduras_totais_valorTotal = (column.Quantidade * column.Alimento.Gorduras_totais) / 100,
                        Gorduras_saturadas_valorTotal = (column.Quantidade * column.Alimento.Gorduras_saturadas) / 100,
                        Colesterol_valorTotal  = (column.Quantidade * column.Alimento.Colesterol) / 100,
                        Fosforo_valorTotal  = (column.Quantidade * column.Alimento.Fosforo) / 100,
                        Poliinsaturados_valorTotal  = (column.Quantidade * column.Alimento.Poliinsaturados) / 100,
                        Monoinsaturados_valorTotal  = (column.Quantidade * column.Alimento.Monoinsaturados) / 100,
                        Vitamina_b1_valorTotal  = (column.Quantidade * column.Alimento.Vitamina_b1) / 100,
                        Vitamina_b2_valorTotal  = (column.Quantidade * column.Alimento.Vitamina_b2) / 100,
                        Vitamina_b3_valorTotal  = (column.Quantidade * column.Alimento.Vitamina_b3) / 100,
                        Vitamina_b6_valorTotal  = (column.Quantidade * column.Alimento.Vitamina_b6) / 100,
                        Gordura_trans_valorTotal  = (column.Quantidade * column.Alimento.Gordura_trans) / 100,
                        Fibra_alimentar_valorTotal  = (column.Quantidade * column.Alimento.Fibra_alimentar) / 100,
                        Acucar_valorTotal  = (column.Quantidade * column.Alimento.Acucar) / 100,
                        Sodio_valorTotal  = (column.Quantidade * column.Alimento.Sodio) / 100,
                        Selenio_valorTotal  = (column.Quantidade * column.Alimento.Selenio) / 100,
                        Calcio_valorTotal  = (column.Quantidade * column.Alimento.Calcio) / 100,
                        Ferro_valorTotal  = (column.Quantidade * column.Alimento.Ferro) / 100,
                        Potassio_valorTotal  = (column.Quantidade * column.Alimento.Potassio) / 100,
                        Zinco_valorTotal  = (column.Quantidade * column.Alimento.Zinco) / 100,
                        Magnesio_valorTotal  = (column.Quantidade * column.Alimento.Magnesio) / 100,
                        Vitamina_a_valorTotal  = (column.Quantidade * column.Alimento.Vitamina_a) / 100,
                        Vitamina_b_valorTotal  = (column.Quantidade * column.Alimento.Vitamina_b) / 100,
                        Vitamina_c_valorTotal  = (column.Quantidade * column.Alimento.Vitamina_c) / 100,
                        Vitamina_d_valorTotal  = (column.Quantidade * column.Alimento.Vitamina_d) / 100,
                        Vitamina_e_valorTotal  = (column.Quantidade * column.Alimento.Vitamina_e) / 100,
                        Vitamina_b9_valorTotal  = (column.Quantidade * column.Alimento.Vitamina_b9) / 100,
                        Vitamina_b12_valorTotal  = (column.Quantidade * column.Alimento.Vitamina_b12) / 100
                    }).ToList();

                return queryAlimentoRefeicao.Where(r => r.Refeicao.dataDeCriacao == DateTime.Today).Select(column =>
                    new AlimentoRefeicao
                    {
                        Alimento = column.Alimento,
                        Quantidade = column.Quantidade,
                        ValorCaloricoTotal = (double)column.ValorCaloricoTotal,
                        Cho_valorTotal = (double)column.Cho_valorTotal,
                        Proteinas_valorTotal = (double)column.Proteinas_valorTotal,
                        Gorduras_totais_valorTotal = (double)column.Gorduras_totais_valorTotal,
                        Gorduras_saturadas_valorTotal = (double)column.Gorduras_saturadas_valorTotal,
                        Colesterol_valorTotal = (double)column.Colesterol_valorTotal,
                        Fosforo_valorTotal = (double)column.Fosforo_valorTotal,
                        Poliinsaturados_valorTotal = (double)column.Poliinsaturados_valorTotal,
                        Monoinsaturados_valorTotal = (double)column.Monoinsaturados_valorTotal,
                        Vitamina_b1_valorTotal = (double)column.Vitamina_b1_valorTotal,
                        Vitamina_b2_valorTotal = (double)column.Vitamina_b2_valorTotal,
                        Vitamina_b3_valorTotal = (double)column.Vitamina_b3_valorTotal,
                        Vitamina_b6_valorTotal = (double)column.Vitamina_b6_valorTotal,
                        Gordura_trans_valorTotal = (double)column.Gordura_trans_valorTotal,
                        Fibra_alimentar_valorTotal = (double)column.Fibra_alimentar_valorTotal,
                        Acucar_valorTotal = (double)column.Acucar_valorTotal,
                        Sodio_valorTotal = (double)column.Sodio_valorTotal,
                        Selenio_valorTotal = (double)column.Selenio_valorTotal,
                        Calcio_valorTotal = (double)column.Calcio_valorTotal,
                        Ferro_valorTotal = (double)column.Ferro_valorTotal,
                        Potassio_valorTotal = (double)column.Potassio_valorTotal,
                        Zinco_valorTotal = (double)column.Zinco_valorTotal,
                        Magnesio_valorTotal = (double)column.Magnesio_valorTotal,
                        Vitamina_a_valorTotal = (double)column.Vitamina_a_valorTotal,
                        Vitamina_b_valorTotal = (double)column.Vitamina_b_valorTotal,
                        Vitamina_c_valorTotal = (double)column.Vitamina_c_valorTotal,
                        Vitamina_d_valorTotal = (double)column.Vitamina_d_valorTotal,
                        Vitamina_e_valorTotal = (double)column.Vitamina_e_valorTotal,
                        Vitamina_b9_valorTotal = (double)column.Vitamina_b9_valorTotal,
                        Vitamina_b12_valorTotal = (double)column.Vitamina_b12_valorTotal                        
                    }).ToList();

            };
        }

        public List<AlimentoRefeicao> relatorioTotalNutrientesMensal()
        {
            using (NutricaoContext mde = new NutricaoContext())
            {
                var dataAtual = DateTime.Today;
                var dataComecoDoMes = new DateTime (dataAtual.Year, dataAtual.Month, 1);

                var queryAlimentoRefeicao = mde.AlimentoRefeicao.Select(column =>
                     new
                     {
                         Alimento = column.Alimento,
                         Refeicao = column.Refeicao,
                         Quantidade = column.Quantidade,
                         ValorCaloricoTotal = mde.AlimentoRefeicao.Sum(a => a.Alimento.Valor_calorico) / dataAtual.Day

                     }).ToList().Take(1);

                return queryAlimentoRefeicao.Where(r => r.Refeicao.dataDeCriacao>= dataComecoDoMes && r.Refeicao.dataDeCriacao <= dataAtual).Select(column =>
                    new AlimentoRefeicao
                    {
                        Alimento = column.Alimento,
                        Quantidade = column.Quantidade,
                        ValorCaloricoTotal = Math.Round((double)column.ValorCaloricoTotal,3)
                    }).ToList();
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
                    mde.Refeicao.Attach(alimentoRefeicao.Refeicao);
                    mde.Alimentos.Attach(alimentoRefeicao.Alimento);
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
