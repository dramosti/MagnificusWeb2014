using HLP.Wcf.Entries.Infrastructure;
using HLP.Wcf.Entries.Repository;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HLP.Wcf.Entries.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "wcf_Funcionario_Controle_Horas_Ponto" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select wcf_Funcionario_Controle_Horas_Ponto.svc or wcf_Funcionario_Controle_Horas_Ponto.svc.cs at the Solution Explorer and start debugging.
    public class wcf_Funcionario_Controle_Horas_Ponto : Iwcf_Funcionario_Controle_Horas_Ponto
    {
        public Funcionario_Controle_Horas_PontoRepository ponto;

        public wcf_Funcionario_Controle_Horas_Ponto()
        {
            ponto = new Funcionario_Controle_Horas_PontoRepository();
        }

        public List<HLP.Entries.Model.RecursosHumanos.Funcionario_Controle_Horas_PontoModel> GetAllFuncionario_Controle_Horas_Ponto(int idFuncionario, DateTime dRelogioPonto)
        {
            try
            {
                return ponto.GetAllFuncionario_Controle_Horas_Ponto(idFuncionario, dRelogioPonto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HLP.Entries.Model.RecursosHumanos.Funcionario_Controle_Horas_PontoModel Save(HLP.Entries.Model.RecursosHumanos.Funcionario_Controle_Horas_PontoModel objFuncionario_Controle_Horas_Ponto, int idEmpresa)
        {
            try
            {
                //objFuncionario_Controle_Horas_Ponto.idFuncionario = 2;
                //objFuncionario_Controle_Horas_Ponto.hRelogioPonto = new TimeSpan(7, 36, 0);
                //objFuncionario_Controle_Horas_Ponto.dRelogioPonto = DateTime.Today;                
                return ponto.Save(objFuncionario_Controle_Horas_Ponto, idEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HLP.Entries.Model.RecursosHumanos.FuncionarioModel GetFuncionario(string xCodigoAlternativo)
        {
            try
            {
                return ponto.GetFuncionario(xCodigoAlternativo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string UserCanRegisterPonto(string xCodigoAlternativo, int idEmpresa)
        {
            string sReturn = string.Empty;
            int idFuncionario = 0;
            try
            {
                idFuncionario = ponto.FuncionarioIsExist(xCodigoAlternativo, idEmpresa);
                if (idFuncionario == 0)
                {
                    sReturn = "Funcinario inexistente !!";
                }
                else
                {
                    #region Regras a serem analisadas
                    //List<HLP.Entries.Model.RecursosHumanos.Funcionario_Controle_Horas_PontoModel> lPontos = ponto.GetAllFuncionario_Controle_Horas_Ponto(idFuncionario, DateTime.Today);
                    //if (lPontos != null)
                    //    if (lPontos.Count > 0)
                    //    {
                    //        TimeSpan lastRegister = lPontos.LastOrDefault().hRelogioPonto;

                    //        if (lastRegister.Hours == DateTime.Now.TimeOfDay.Hours && lastRegister.Minutes == DateTime.Now.TimeOfDay.Minutes)
                    //        {
                    //            sReturn = "Aguarde 1 minuto para poder bater o ponto novamente.";
                    //        }
                    //    }
                    #endregion
                }
                return sReturn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
