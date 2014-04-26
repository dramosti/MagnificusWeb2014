using HLP.Comum.Model.Componentes;
using HLP.Comum.Resources.RecursosBases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Reflection;
using System.Windows;
using System.Linq;
using HLP.Comum.Model.StaticModels;
using HLP.Comum.Infrastructure.Static;

namespace HLP.Comum.Model.Models
{
      public class modelBase : INotifyPropertyChanged, IDataErrorInfo
    {
        public List<PesquisaPadraoModelContract> lcamposSqlNotNull;
        public statusModel status { get; set; }
        public readonly string[] camposSeremIgnorados = { "Item", "Error" }; //Campos que não devem ser verificados no reflection de iniciaobjeto()
        camposNotNullServices.IcamposBaseDadosServiceClient servico;

        public modelBase()
        {
        }

        public void IniciaObjeto()
        {
            try
            {
                object o;
                foreach (PropertyInfo p in this.GetType().GetProperties())
                {
                    if (camposSeremIgnorados.ToList().Count(i => i.ToString() == p.Name) == 0)
                    {
                        o = p.GetValue(obj: this, index:null);
                        if (o != null)
                            if (p.GetValue(obj: this,index:null).GetType() == typeof(DateTime))
                                p.SetValue(obj: this, value: DateTime.MinValue,index:null);
                        
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public modelBase(string xTabela)
        {
            servico = new camposNotNullServices.IcamposBaseDadosServiceClient();
            if (lCamposSqlNotNull._lCamposSql.Count(i => i.xTabela == xTabela)
                == 0)
            {
                CamposSqlNotNullModel lCampos = new CamposSqlNotNullModel();
                lCampos.xTabela = xTabela;
                lCampos.lCamposSqlModel = new List<PesquisaPadraoModelContract>();

                //foreach (var item in servico.getCamposNotNull(xTabela: xTabela))
                //{
                //    lCampos.lCamposSqlModel.Add(item: new PesquisaPadraoModelContract
                //    {
                //        COLUMN_NAME = item.COLUMN_NAME,
                //        DATA_TYPE = item.DATA_TYPE,
                //        CHARACTER_MAXIMUM_LENGTH = item.CHARACTER_MAXIMUM_LENGTH,
                //        IS_NULLABLE = item.IS_NULLABLE
                //    });
                //}

                lCamposSqlNotNull.AddCampoSql(objCamposSqlNotNull: lCampos);
            }
            lcamposSqlNotNull = lCamposSqlNotNull._lCamposSql.FirstOrDefault(i => i.xTabela
                    == xTabela).lCamposSqlModel;
            PropertyInfo p = this.GetType().GetProperty("idEmpresa");

            if (p != null && !xTabela.Contains("Empresa"))
            {
                if (CompanyData.idEmpresa != 0)
                    p.SetValue(this, CompanyData.idEmpresa, index:null);
            }

            this.IniciaObjeto();
        }

        #region NotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                if (this.status == statusModel.nenhum)
                    this.status = statusModel.alterado;
            }
        }

        #endregion

        #region Validação de Dados

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public virtual string this[string columnName]
        {
            get
            {
                object valor = this.GetType().GetProperty(columnName).GetValue(this, null);
                string sMessage = string.Empty;
                sMessage = this.GetValidationError(columnName: columnName, objeto: this);

                if (sMessage == null)
                    if (columnName.ToUpper().Contains("XEMAIL"))
                        if (valor != null)
                            if (valor != "")
                                if (!(valor.ToString()).IsValidEmailAddress())
                                    sMessage = "Email inválido.";

                return sMessage;
            }
        }

        protected string GetValidationError<T>(string columnName, T objeto) where T : class
        {
            object valor = objeto.GetType().GetProperty(columnName).GetValue(objeto,null);
            if (lcamposSqlNotNull != null)
            {
                PesquisaPadraoModelContract campo = lcamposSqlNotNull.FirstOrDefault(predicate:
                    i => i.COLUMN_NAME == columnName);
                if (campo != null)
                {
                    if (campo.IS_NULLABLE == "NO" && campo.DATA_TYPE == "F ")
                    {
                        try
                        {
                            if (valor == null)
                                return "Necessário que campo possua valor!";
                            else if (valor.ToString() == "0")
                                return "Necessário que campo possua valor!";
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }
                    else if (campo.IS_NULLABLE == "NO" && (campo.DATA_TYPE == null || campo.DATA_TYPE == "UQ")
                        && (valor == "" || valor == null))
                    {
                        return "Necessário que campo possua valor!";
                    }
                    else if (campo.IS_NULLABLE == "NO" && (objeto.GetType().GetProperty(columnName).GetType()
                        == typeof(DateTime) && ((DateTime)valor) < DateTime.MinValue))
                    {
                        return "Necessário uma data maior que " + DateTime.MinValue.ToString();
                    }                   

                    if (valor != null)
                    {
                        if (valor.GetType() == typeof(string) && valor.ToString().Count() > campo.CHARACTER_MAXIMUM_LENGTH && campo.CHARACTER_MAXIMUM_LENGTH > 0)
                            return "Valor deve possuir menos que " + campo.CHARACTER_MAXIMUM_LENGTH.ToString() +
                                " caracteres!";
                    }
                }
            }

            if (valor == null)
            {
                return null;
            }
            if (valor.GetType()
                == typeof(DateTime) && ((DateTime)valor) != DateTime.MinValue && ((DateTime)valor) < ((DateTime)DateTime.MinValue))
            {
                return "Necessário uma data maior que " + DateTime.MinValue.ToString();
            }

            return null;
        }

        #endregion
    }
}
