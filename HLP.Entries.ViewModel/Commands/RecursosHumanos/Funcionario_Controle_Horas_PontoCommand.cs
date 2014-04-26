using HLP.Entries.Model.RecursosHumanos;
using HLP.Entries.ViewModel.Commands.RecursosHumanos;
using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HLP.Comum.Resources;
using System.Windows.Browser;
using System.IO;
using System.Linq;
using HLP.Comum.Infrastructure.Static;
using HLP.Entries.Model;
using HLP.Entries.ViewModel.ViewModels.RecursosHumanos;
using HLP.Comum.Viewcs;

namespace HLP.Entries.ViewModel.Commands.RecursosHumanos
{
    public class Funcionario_Controle_Horas_PontoCommand
    {
        public Funcionario_Controle_Horas_PontoViewModel viewModel { get; set; }

        public pontoServicoLocal.Iwcf_Funcionario_Controle_Horas_PontoClient servico;

        Funcionario_Controle_Horas_PontoModel registerPonto;
        public Funcionario_Controle_Horas_PontoCommand(Funcionario_Controle_Horas_PontoViewModel viewModel)
        {
            try
            {
                this.viewModel = viewModel;
                servico = new pontoServicoLocal.Iwcf_Funcionario_Controle_Horas_PontoClient();
                this.viewModel.commandSave = new HLP.Comum.ViewModel.Commands.RelayCommand(exec => SavePonto(),
                    canexec => true);


                this.servico.SaveCompleted += new EventHandler<Entries.ViewModel.pontoServicoLocal.SaveCompletedEventArgs>
                (
                this.servico_SaveCompleted
                );

                this.servico.GetAllFuncionario_Controle_Horas_PontoCompleted += new
                    EventHandler<Entries.ViewModel.pontoServicoLocal.GetAllFuncionario_Controle_Horas_PontoCompletedEventArgs>(this.servico_GetAllFuncionario_Controle_Horas_PontoCompleted);
                this.servico.UserCanRegisterPontoCompleted +=
                    new EventHandler<Entries.ViewModel.pontoServicoLocal.UserCanRegisterPontoCompletedEventArgs>(this.servico_UserCanRegisterPontoCompleted);
                this.servico.GetFuncionarioCompleted +=
                    new EventHandler<Entries.ViewModel.pontoServicoLocal.GetFuncionarioCompletedEventArgs>(this.servico_GetFuncionarioCompleted);


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void SavePonto()
        {
            try
            {
                if (this.viewModel.codigoPonto.Split('-').Count() == 2)
                {
                    string xCodigoAlternativo = this.viewModel.codigoPonto.Split('-')[0];
                    int idEmpresa;
                    int.TryParse(this.viewModel.codigoPonto.Split('-')[1].ToString(), out idEmpresa);

                    CompanyData.idEmpresa = idEmpresa;
                    this.viewModel.codigoPonto = xCodigoAlternativo;

                    servico.UserCanRegisterPontoAsync(this.viewModel.codigoPonto, CompanyData.idEmpresa);
                }
                else
                {
                    ChildWindow winMessage = new MessageWindow("AVISO", "FORMATO DO CÓDIGO É INVÁLIDO.");
                    winMessage.Closed += winMessage_Closed;
                    winMessage.Show();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void servico_UserCanRegisterPontoCompleted(object sender, Entries.ViewModel.pontoServicoLocal.UserCanRegisterPontoCompletedEventArgs e)
        {
            if (e.Result == "")
            {
                servico.GetFuncionarioAsync(this.viewModel.codigoPonto);
            }
            else
            {
                ChildWindow winMessage = new MessageWindow("AVISO", e.Result);
                winMessage.Closed += winMessage_Closed;
                winMessage.Show();
            }
        }

        void winMessage_Closed(object sender, EventArgs e)
        {
            this.viewModel.RefreshPonto();
        }

        public void servico_GetFuncionarioCompleted(object sender, pontoServicoLocal.GetFuncionarioCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                registerPonto = new Funcionario_Controle_Horas_PontoModel();
                registerPonto.idFuncionario = e.Result.idFuncionario;
                registerPonto.hRelogioPonto = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 0);
                registerPonto.dRelogioPonto = DateTime.Today;
                registerPonto.stFeriasAbono = 0;
                registerPonto.idSequenciaInterna = e.Result.idSequenciaInterna;
                this.viewModel.currentModel = e.Result;
                if (DateTime.Now.Hour > 0 &&
                    DateTime.Now.Hour < 12)
                    this.viewModel.currentModel.sMessageFull = "TENHA UM BOM DIA !!!";
                else if (
                    DateTime.Now.Hour > 12 &&
                    DateTime.Now.Hour < 18)
                    this.viewModel.currentModel.sMessageFull = "TENHA UMA BOA TARDE !!!";
                else if (
                    DateTime.Now.Hour > 18 &&
                    DateTime.Now.Hour < 18)
                    this.viewModel.currentModel.sMessageFull = "TENHA UMA BOA NOITE !!!";
                servico.SaveAsync(registerPonto, CompanyData.idEmpresa);
            }
            else
            {

                ChildWindow winMessage = new MessageWindow("AVISO", "Funcionario inexistente no banco de dados.");
                winMessage.Closed += winMessage_Closed;
                winMessage.Show();
                this.viewModel.RefreshPonto();
            }
        }

        public void servico_SaveCompleted(object sender, pontoServicoLocal.SaveCompletedEventArgs e)
        {
            if (e.Result.idFuncionarioControleHorasPonto != null)
            {
                this.viewModel.lponto = new System.Collections.ObjectModel.ObservableCollection<Funcionario_Controle_Horas_PontoModel>();
                this.servico.GetAllFuncionario_Controle_Horas_PontoAsync(this.viewModel.currentModel.idFuncionario, e.Result.dRelogioPonto);
            }
        }

        public void servico_GetAllFuncionario_Controle_Horas_PontoCompleted(object sender, pontoServicoLocal.GetAllFuncionario_Controle_Horas_PontoCompletedEventArgs e)
        {
            this.viewModel.lponto = new System.Collections.ObjectModel.ObservableCollection<Funcionario_Controle_Horas_PontoModel>(e.Result);

            decimal icount = this.viewModel.lponto.Count();

            if ((icount % 2) == 0)
            {
                this.viewModel.currentModel.sMessageSimples = "SAÍDA";
                this.viewModel.bImageEntrada = Visibility.Collapsed;
                this.viewModel.bImageSaida = Visibility.Visible;
            }
            else
            {
                this.viewModel.currentModel.sMessageSimples = "ENTRADA";
                this.viewModel.bImageEntrada = Visibility.Visible;
                this.viewModel.bImageSaida = Visibility.Collapsed;
            }

            this.viewModel.currentModel.dHorasTrabalhadas = new TimeSpan();

            for (int i = 0; i < this.viewModel.lponto.Count(); )
            {
                if ((i + 1) < this.viewModel.lponto.Count())
                {
                    this.viewModel.currentModel.dHorasTrabalhadas = this.viewModel.currentModel.dHorasTrabalhadas.Add(
                        this.viewModel.lponto[i + 1].hRelogioPonto
                                    .Subtract(
                        this.viewModel.lponto[i].hRelogioPonto));
                }
                i = i + 2;
            }
            this.viewModel.timerRefresh.Start();
        }

    }
}
