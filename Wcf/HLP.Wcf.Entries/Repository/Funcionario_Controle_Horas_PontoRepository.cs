using HLP.Wcf.Entries.Infrastructure;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Windows;
using HLP.Comum.Infrastructure.Static;

namespace HLP.Wcf.Entries.Repository
{
    public class Funcionario_Controle_Horas_PontoRepository
    {
        [Inject]
        public UnitOfWorkBase UndTrabalho { get; set; }

        public Funcionario_Controle_Horas_PontoRepository()
        {
            IKernel kernel = new StandardKernel(new MagnificusDependenciesModule());
            kernel.Settings.ActivationCacheDisabled = false;
            kernel.Inject(this);
        }

        private DataAccessor<HLP.Entries.Model.RecursosHumanos.Parametro_Cartao_PontoModel> regParametrosAccessor;
        private DataAccessor<HLP.Entries.Model.RecursosHumanos.FuncionarioModel> regFuncionarioAccessor;
        private DataAccessor<HLP.Entries.Model.RecursosHumanos.Funcionario_Controle_Horas_PontoModel> regAllFuncionario_Controle_Horas_PontoAccessor;

        public HLP.Entries.Model.RecursosHumanos.Funcionario_Controle_Horas_PontoModel Save(HLP.Entries.Model.RecursosHumanos.Funcionario_Controle_Horas_PontoModel objFuncionario_Controle_Horas_Ponto, int idEmpresa)
        {
            // VERIFICAR SE O PONTO ESTÁ DENTRO DA RANGE DE ENTRADA / SAÍDA
            List<HLP.Entries.Model.RecursosHumanos.Calendario_DetalheModel> lRegistros = this.GetCalendarioDia((int)objFuncionario_Controle_Horas_Ponto.idFuncionario, objFuncionario_Controle_Horas_Ponto.dRelogioPonto);

            if (lRegistros.Count() > 0)
            {
                TimeSpan tsEntrada = lRegistros.FirstOrDefault().tHoraInicio;
                TimeSpan tsSaida = lRegistros.LastOrDefault().tHoraTermino;

                HLP.Entries.Model.RecursosHumanos.Funcionario_Controle_Horas_PontoModel lastPonto;
                HLP.Entries.Model.RecursosHumanos.Parametro_Cartao_PontoModel param = this.GetParametroPontoEmpresa(idEmpresa);
                if (param.tToleranciaHoraExtra != null)
                {
                    #region Entrada
                    // verifica se o ponto a ser registrado é entrada
                    if (objFuncionario_Controle_Horas_Ponto.hRelogioPonto <= tsEntrada.Add((TimeSpan)param.tToleranciaHoraExtra) &&
                        objFuncionario_Controle_Horas_Ponto.hRelogioPonto >= tsEntrada.Subtract((TimeSpan)param.tToleranciaHoraExtra))
                    {
                        objFuncionario_Controle_Horas_Ponto.hRelogioPonto = tsEntrada;// HORARIO PADRÃO
                        lastPonto = this.GetLastFuncionario_Controle_Horas_Ponto((int)objFuncionario_Controle_Horas_Ponto.idFuncionario, objFuncionario_Controle_Horas_Ponto.dRelogioPonto);
                        if (lastPonto != null)
                        {
                            // verifica se o ultimo ponto foi entrada.
                            if (lastPonto.hRelogioPonto <= tsEntrada.Add((TimeSpan)param.tToleranciaHoraExtra) &&
                            lastPonto.hRelogioPonto >= tsEntrada.Subtract((TimeSpan)param.tToleranciaHoraExtra))
                            {
                                objFuncionario_Controle_Horas_Ponto.idFuncionarioControleHorasPonto = lastPonto.idFuncionarioControleHorasPonto;
                            }
                        }
                    }
                    #endregion

                    #region Saída
                    // verifica se o ponto a ser registrado é entrada
                    if (objFuncionario_Controle_Horas_Ponto.hRelogioPonto <= tsSaida.Add((TimeSpan)param.tToleranciaHoraExtra) &&
                        objFuncionario_Controle_Horas_Ponto.hRelogioPonto >= tsSaida.Subtract((TimeSpan)param.tToleranciaHoraExtra))
                    {
                        objFuncionario_Controle_Horas_Ponto.hRelogioPonto = tsSaida; // HORARIO PADRÃO
                        lastPonto = this.GetLastFuncionario_Controle_Horas_Ponto((int)objFuncionario_Controle_Horas_Ponto.idFuncionario, objFuncionario_Controle_Horas_Ponto.dRelogioPonto);
                        // verifica se o ultimo ponto foi entrada.
                        if (lastPonto.hRelogioPonto <= tsSaida.Add((TimeSpan)param.tToleranciaHoraExtra) &&
                        lastPonto.hRelogioPonto >= tsSaida.Subtract((TimeSpan)param.tToleranciaHoraExtra))
                        {
                            objFuncionario_Controle_Horas_Ponto.idFuncionarioControleHorasPonto = lastPonto.idFuncionarioControleHorasPonto;
                        }
                    }
                    #endregion
                }
            }

            if (objFuncionario_Controle_Horas_Ponto.idFuncionarioControleHorasPonto == null)
            {
                objFuncionario_Controle_Horas_Ponto.idFuncionarioControleHorasPonto = (int)UndTrabalho.dbPrincipal.ExecuteScalar("dbo.Proc_save_Funcionario_Controle_Horas_Ponto",
                ParameterBase<HLP.Entries.Model.RecursosHumanos.Funcionario_Controle_Horas_PontoModel>.SetParameterValue(objFuncionario_Controle_Horas_Ponto));
                return objFuncionario_Controle_Horas_Ponto;
            }
            else
            {
                objFuncionario_Controle_Horas_Ponto.idFuncionarioControleHorasPonto = (int)UndTrabalho.dbPrincipal.ExecuteScalar("dbo.Proc_update_Funcionario_Controle_Horas_Ponto",
                  ParameterBase<HLP.Entries.Model.RecursosHumanos.Funcionario_Controle_Horas_PontoModel>.SetParameterValue(objFuncionario_Controle_Horas_Ponto));
                return objFuncionario_Controle_Horas_Ponto;
            }
        }

        public List<HLP.Entries.Model.RecursosHumanos.Funcionario_Controle_Horas_PontoModel> GetAllFuncionario_Controle_Horas_Ponto(int idFuncionario, DateTime dRelogioPonto)
        {
            try
            {
                if (regAllFuncionario_Controle_Horas_PontoAccessor == null)
                {
                    regAllFuncionario_Controle_Horas_PontoAccessor = UndTrabalho.dbPrincipal.CreateSqlStringAccessor("SELECT * FROM Funcionario_Controle_Horas_Ponto where idFuncionario = @idFuncionario and dRelogioPonto = @dRelogioPonto",
                                    new Parameters(UndTrabalho.dbPrincipal)
                                    .AddParameter<int>("idFuncionario")
                                    .AddParameter<DateTime>("dRelogioPonto"),
                                    MapBuilder<HLP.Entries.Model.RecursosHumanos.Funcionario_Controle_Horas_PontoModel>.MapAllProperties().Build());
                }
                return regAllFuncionario_Controle_Horas_PontoAccessor.Execute(idFuncionario, dRelogioPonto).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int FuncionarioIsExist(string xCodigoAlternativo, int idEmpresa)
        {
            try
            {
                DbCommand comand = UndTrabalho.dbPrincipal.GetSqlStringCommand
                                  (
                                  string.Format("select top 1 idFuncionario from Funcionario f" +
                                                " where" +
                                                " f.xCodigoAlternativo = '{0}' AND f.idEmpresa = {1} AND f.Ativo = '1'", xCodigoAlternativo, idEmpresa)
                                  );
                int? ireturn = (int)UndTrabalho.dbPrincipal.ExecuteScalar(comand);

                return (int)ireturn;

            }
            catch (Exception)
            {
                return 0;
            }

        }

        public HLP.Entries.Model.RecursosHumanos.FuncionarioModel GetFuncionario(string xCodigoAlternativo)
        {
            try
            {
                if (regFuncionarioAccessor == null)
                {
                    regFuncionarioAccessor = UndTrabalho.dbPrincipal.CreateSprocAccessor("dbo.Proc_sel_FuncionarioPonto",
                                     new Parameters(UndTrabalho.dbPrincipal)
                                     .AddParameter<string>("xCodigoAlternativo"),
                                     MapBuilder<HLP.Entries.Model.RecursosHumanos.FuncionarioModel>.MapAllProperties()
                                     .DoNotMap(c => c.dHorasAtrabalhar)
                                     .DoNotMap(c => c.dHorasTrabalhadas)
                                     .DoNotMap(c => c.xObservacao)
                                     .DoNotMap(c => c.sMessageFull)
                                     .DoNotMap(c => c.sMessageSimples)
                                     .DoNotMap(c => c.idSequenciaInterna)
                                     .DoNotMap(c => c.dSaldo)
                                     .DoNotMap(c => c.imgFoto)
                                     .Build());
                }

                HLP.Entries.Model.RecursosHumanos.FuncionarioModel funcRet = regFuncionarioAccessor.Execute(xCodigoAlternativo).FirstOrDefault();

                if (funcRet != null)
                {
                    funcRet.dHorasAtrabalhar = new TimeSpan();
                    foreach (TimeSpan item in this.GetHorasAtrabalharDia(funcRet.idFuncionario, DateTime.Today))
                    {
                        funcRet.dHorasAtrabalhar = funcRet.dHorasAtrabalhar.Add(item);
                    }
                    funcRet.idSequenciaInterna = this.NextSequencia(funcRet.idFuncionario, DateTime.Today);
                    funcRet.imgFoto = this.GetImgFoto(funcRet.idFuncionario);

                }

                return funcRet;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<TimeSpan> GetHorasAtrabalharDia(int idFuncionario, DateTime dtDia)
        {

            DbCommand command = UndTrabalho.dbPrincipal.GetSqlStringCommand
                (
                string.Format("select Calendario_Detalhe.tHoraInicio, Calendario_Detalhe.tHoraTermino from Funcionario inner join Calendario "
                + "on Funcionario.idCalendario = Calendario.idCalendario inner join Calendario_Detalhe "
                + "on Calendario.idCalendario = Calendario_Detalhe.idCalendario "
                + "where Funcionario.idFuncionario = {0} and Calendario_Detalhe.dCalendario = '{1}' ", idFuncionario, dtDia.Date.ToString("yyyy-MM-dd"))
                );

            IDataReader reader = UndTrabalho.dbPrincipal.ExecuteReader(command);

            List<TimeSpan> lReturn = new List<TimeSpan>();
            TimeSpan dInicio;
            TimeSpan dFinal;
            while (reader.Read())
            {
                dInicio = new TimeSpan();
                dFinal = new TimeSpan();
                if (reader["tHoraInicio"] != null && reader["tHoraTermino"] != null)
                {
                    dInicio = reader["tHoraInicio"].ToString().ToTimeSpan();
                    dFinal = reader["tHoraTermino"].ToString().ToTimeSpan();
                    lReturn.Add(dFinal.Subtract(dInicio));
                }
            }
            return lReturn;
        }

        public TimeSpan GetHoraEntradaDia(int idFuncionario, DateTime dtDia)
        {
            try
            {

                DbCommand command = UndTrabalho.dbPrincipal.GetSqlStringCommand
                    (
                    string.Format("select TOP 1 Calendario_Detalhe.tHoraInicio from Funcionario inner join Calendario "
                    + "on Funcionario.idCalendario = Calendario.idCalendario inner join Calendario_Detalhe "
                    + "on Calendario.idCalendario = Calendario_Detalhe.idCalendario "
                    + "where Funcionario.idFuncionario = {0} and Calendario_Detalhe.dCalendario = '{1}' ", idFuncionario, dtDia.Date.ToString("yyyy-MM-dd"))
                    );

                TimeSpan ret = ((string)UndTrabalho.dbPrincipal.ExecuteScalar(command)).ToTimeSpan();
                return ret;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int NextSequencia(int idFuncionario, DateTime dtDia)
        {
            DbCommand comand = UndTrabalho.dbPrincipal.GetSqlStringCommand
                              (
                              string.Format("select (count(*) + 1) sequencia from Funcionario_Controle_Horas_Ponto p " +
                                            "where " +
                                            "p.idFuncionario = {0} and p.dRelogioPonto = '{1}'", idFuncionario, dtDia.ToString("yyyy-MM-dd"))
                              );

            int ireturn = (int)UndTrabalho.dbPrincipal.ExecuteScalar(comand);

            return ireturn;
        }

        public byte[] GetImgFoto(int idFuncionario)
        {
            try
            {
                DbCommand comand = UndTrabalho.dbPrincipal.GetSqlStringCommand
                              (
                              string.Format("select imgFoto from Funcionario " +
                                            "where " +
                                            "idFuncionario = {0}", idFuncionario)
                              );

                var img = UndTrabalho.dbPrincipal.ExecuteScalar(comand);

                if (img.ToString() != "")
                {
                    return (byte[])img;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HLP.Entries.Model.RecursosHumanos.Parametro_Cartao_PontoModel GetParametroPontoEmpresa(int idEmpresa)
        {
            try
            {
                if (regParametrosAccessor == null)
                {
                    regParametrosAccessor = UndTrabalho.dbPrincipal.CreateSqlStringAccessor("SELECT * FROM Parametro_Cartao_Ponto where idEmpresa = @idEmpresa",
                                    new Parameters(UndTrabalho.dbPrincipal)
                                    .AddParameter<int>("idEmpresa"),
                                    MapBuilder<HLP.Entries.Model.RecursosHumanos.Parametro_Cartao_PontoModel>.MapAllProperties().Build());
                }
                return regParametrosAccessor.Execute(idEmpresa).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        private List<HLP.Entries.Model.RecursosHumanos.Calendario_DetalheModel> GetCalendarioDia(int idFuncionario, DateTime dtDia)
        {
            DataAccessor<HLP.Entries.Model.RecursosHumanos.Calendario_DetalheModel> regCalendario_DetalheModelAccessorDia = UndTrabalho.dbPrincipal.CreateSqlStringAccessor(
                      string.Format("select * from Funcionario inner join Calendario "
                                      + "on Funcionario.idCalendario = Calendario.idCalendario inner join Calendario_Detalhe "
                                      + "on Calendario.idCalendario = Calendario_Detalhe.idCalendario "
                                      + "where Funcionario.idFuncionario = {0} and Calendario_Detalhe.dCalendario = '{1}' ", idFuncionario, dtDia.Date.ToString("yyyy-MM-dd")),
                                  MapBuilder<HLP.Entries.Model.RecursosHumanos.Calendario_DetalheModel>.MapAllProperties()
                                  .DoNotMap(i => i.idCalendarioDetalhe)
                                  .DoNotMap(i => i.dCalendario)
                                  .DoNotMap(i => i.idCalendario)
                                  .Build());
            return regCalendario_DetalheModelAccessorDia.Execute().ToList();
        }

        /// <summary>
        /// Retorna ultimo ponto registrado
        /// </summary>
        /// <param name="idFuncionario"></param>
        /// <param name="dRelogioPonto"></param>
        /// <returns></returns>
        private HLP.Entries.Model.RecursosHumanos.Funcionario_Controle_Horas_PontoModel GetLastFuncionario_Controle_Horas_Ponto(int idFuncionario, DateTime dRelogioPonto)
        {
            try
            {
                if (regAllFuncionario_Controle_Horas_PontoAccessor == null)
                {
                    regAllFuncionario_Controle_Horas_PontoAccessor = UndTrabalho.dbPrincipal.CreateSqlStringAccessor("SELECT top 1 * FROM Funcionario_Controle_Horas_Ponto where idFuncionario = @idFuncionario and dRelogioPonto = @dRelogioPonto order by idFuncionarioControleHorasPonto desc ",
                                    new Parameters(UndTrabalho.dbPrincipal)
                                    .AddParameter<int>("idFuncionario")
                                    .AddParameter<DateTime>("dRelogioPonto"),
                                    MapBuilder<HLP.Entries.Model.RecursosHumanos.Funcionario_Controle_Horas_PontoModel>.MapAllProperties().Build());
                }
                return regAllFuncionario_Controle_Horas_PontoAccessor.Execute(idFuncionario, dRelogioPonto).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}