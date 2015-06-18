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

        //ListaDeAlimentoPorRefeicao pela data
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
                DateTime data = new DateTime(2015, 06, 17);
                var queryAlimentoRefeicao = mde.AlimentoRefeicao.Select(column =>
                    new
                    {
                        Alimento = column.Alimento,
                        Refeicao = column.Refeicao,
                        Quantidade = column.Quantidade,
                        ValorCaloricoTotal = ((column.Quantidade * column.Alimento.Valor_calorico) / column.Alimento.Porcao),
                        Cho_valorTotal = (column.Quantidade * column.Alimento.Cho) / column.Alimento.Porcao,
                        Proteinas_valorTotal = (column.Quantidade * column.Alimento.Proteinas) / column.Alimento.Porcao,
                        Gorduras_totais_valorTotal = (column.Quantidade * column.Alimento.Gorduras_totais) / column.Alimento.Porcao,
                        Gorduras_saturadas_valorTotal = (column.Quantidade * column.Alimento.Gorduras_saturadas) / column.Alimento.Porcao,
                        Colesterol_valorTotal = (column.Quantidade * column.Alimento.Colesterol) / column.Alimento.Porcao,
                        Fosforo_valorTotal = (column.Quantidade * column.Alimento.Fosforo) / column.Alimento.Porcao,
                        Poliinsaturados_valorTotal = (column.Quantidade * column.Alimento.Poliinsaturados) / column.Alimento.Porcao,
                        Monoinsaturados_valorTotal = (column.Quantidade * column.Alimento.Monoinsaturados) / column.Alimento.Porcao,
                        Vitamina_b1_valorTotal = (column.Quantidade * column.Alimento.Vitamina_b1) / column.Alimento.Porcao,
                        Vitamina_b2_valorTotal = (column.Quantidade * column.Alimento.Vitamina_b2) / column.Alimento.Porcao,
                        Vitamina_b3_valorTotal = (column.Quantidade * column.Alimento.Vitamina_b3) / column.Alimento.Porcao,
                        Vitamina_b6_valorTotal = (column.Quantidade * column.Alimento.Vitamina_b6) / column.Alimento.Porcao,
                        Gordura_trans_valorTotal = (column.Quantidade * column.Alimento.Gordura_trans) / column.Alimento.Porcao,
                        Fibra_alimentar_valorTotal = (column.Quantidade * column.Alimento.Fibra_alimentar) / column.Alimento.Porcao,
                        Acucar_valorTotal = (column.Quantidade * column.Alimento.Acucar) / column.Alimento.Porcao,
                        Sodio_valorTotal = (column.Quantidade * column.Alimento.Sodio) / column.Alimento.Porcao,
                        Selenio_valorTotal = (column.Quantidade * column.Alimento.Selenio) / column.Alimento.Porcao,
                        Calcio_valorTotal = (column.Quantidade * column.Alimento.Calcio) / column.Alimento.Porcao,
                        Ferro_valorTotal = (column.Quantidade * column.Alimento.Ferro) / column.Alimento.Porcao,
                        Potassio_valorTotal = (column.Quantidade * column.Alimento.Potassio) / column.Alimento.Porcao,
                        Zinco_valorTotal = (column.Quantidade * column.Alimento.Zinco) / column.Alimento.Porcao,
                        Magnesio_valorTotal = (column.Quantidade * column.Alimento.Magnesio) / column.Alimento.Porcao,
                        Vitamina_a_valorTotal = (column.Quantidade * column.Alimento.Vitamina_a) / column.Alimento.Porcao,
                        Vitamina_b_valorTotal = (column.Quantidade * column.Alimento.Vitamina_b) / column.Alimento.Porcao,
                        Vitamina_c_valorTotal = (column.Quantidade * column.Alimento.Vitamina_c) / column.Alimento.Porcao,
                        Vitamina_d_valorTotal = (column.Quantidade * column.Alimento.Vitamina_d) / column.Alimento.Porcao,
                        Vitamina_e_valorTotal = (column.Quantidade * column.Alimento.Vitamina_e) / column.Alimento.Porcao,
                        Vitamina_b9_valorTotal = (column.Quantidade * column.Alimento.Vitamina_b9) / column.Alimento.Porcao,
                        Vitamina_b12_valorTotal = (column.Quantidade * column.Alimento.Vitamina_b12) / column.Alimento.Porcao
                    }).ToList();

                return queryAlimentoRefeicao.Where(r => r.Refeicao.dataDeCriacao == data).Select(column =>
                    new AlimentoRefeicao
                    {
                        Alimento = column.Alimento,
                        Quantidade = column.Quantidade,
                        ValorCaloricoTotal = column.ValorCaloricoTotal.HasValue ? column.ValorCaloricoTotal.Value : 0,
                        Cho_valorTotal = Math.Round(column.Cho_valorTotal.HasValue ? column.Cho_valorTotal.Value : 0, 3),
                        Proteinas_valorTotal = column.Proteinas_valorTotal.HasValue ? column.Proteinas_valorTotal.Value : 0,
                        Gorduras_totais_valorTotal = column.Gorduras_totais_valorTotal.HasValue ? column.Gorduras_totais_valorTotal.Value : 0,
                        Gorduras_saturadas_valorTotal = column.Gorduras_saturadas_valorTotal.HasValue ? column.Gorduras_saturadas_valorTotal.Value : 0,
                        Colesterol_valorTotal = column.Colesterol_valorTotal.HasValue ? column.Colesterol_valorTotal.Value : 0,
                        Fosforo_valorTotal = column.Fosforo_valorTotal.HasValue ? column.Fosforo_valorTotal.Value : 0,
                        Poliinsaturados_valorTotal = column.Poliinsaturados_valorTotal.HasValue ? column.Poliinsaturados_valorTotal.Value : 0,
                        Monoinsaturados_valorTotal = column.Monoinsaturados_valorTotal.HasValue ? column.Monoinsaturados_valorTotal.Value : 0,
                        Vitamina_b1_valorTotal = column.Vitamina_b1_valorTotal.HasValue ? column.Vitamina_b1_valorTotal.Value : 0,
                        Vitamina_b2_valorTotal = column.Vitamina_b2_valorTotal.HasValue ? column.Vitamina_b2_valorTotal.Value : 0,
                        Vitamina_b3_valorTotal = column.Vitamina_b3_valorTotal.HasValue ? column.Vitamina_b3_valorTotal.Value : 0,
                        Vitamina_b6_valorTotal = column.Vitamina_b6_valorTotal.HasValue ? column.Vitamina_b6_valorTotal.Value : 0,
                        Gordura_trans_valorTotal = column.Gordura_trans_valorTotal.HasValue ? column.Gordura_trans_valorTotal.Value : 0,
                        Fibra_alimentar_valorTotal = column.Fibra_alimentar_valorTotal.HasValue ? column.Fibra_alimentar_valorTotal.Value : 0,
                        Acucar_valorTotal = column.Acucar_valorTotal.HasValue ? column.Acucar_valorTotal.Value : 0,
                        Sodio_valorTotal = column.Sodio_valorTotal.HasValue ? column.Sodio_valorTotal.Value : 0,
                        Selenio_valorTotal = column.Selenio_valorTotal.HasValue ? column.Selenio_valorTotal.Value : 0,
                        Calcio_valorTotal = column.Calcio_valorTotal.HasValue ? column.Calcio_valorTotal.Value : 0,
                        Ferro_valorTotal = column.Ferro_valorTotal.HasValue ? column.Ferro_valorTotal.Value : 0,
                        Potassio_valorTotal = column.Potassio_valorTotal.HasValue ? column.Potassio_valorTotal.Value : 0,
                        Zinco_valorTotal = column.Zinco_valorTotal.HasValue ? column.Zinco_valorTotal.Value : 0,
                        Magnesio_valorTotal = column.Magnesio_valorTotal.HasValue ? column.Magnesio_valorTotal.Value : 0,
                        Vitamina_a_valorTotal = column.Vitamina_a_valorTotal.HasValue ? column.Vitamina_a_valorTotal.Value : 0,
                        Vitamina_b_valorTotal = column.Vitamina_b_valorTotal.HasValue ? column.Vitamina_b_valorTotal.Value : 0,
                        Vitamina_c_valorTotal = column.Vitamina_c_valorTotal.HasValue ? column.Vitamina_c_valorTotal.Value : 0,
                        Vitamina_d_valorTotal = column.Vitamina_d_valorTotal.HasValue ? column.Vitamina_d_valorTotal.Value : 0,
                        Vitamina_e_valorTotal = column.Vitamina_e_valorTotal.HasValue ? column.Vitamina_e_valorTotal.Value : 0,
                        Vitamina_b9_valorTotal = column.Vitamina_b9_valorTotal.HasValue ? column.Vitamina_b9_valorTotal.Value : 0,
                        Vitamina_b12_valorTotal = column.Vitamina_b12_valorTotal.HasValue ? column.Poliinsaturados_valorTotal.Value : 0
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
                        ValorCaloricoTotal = Math.Round((column.ValorCaloricoTotal.HasValue ? column.ValorCaloricoTotal.Value : 0) ,3)
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
