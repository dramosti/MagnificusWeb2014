using HLP.Comum.Model.Componentes;
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
using System.Windows.Shapes;

namespace HLP.Comum.Model.StaticModels
{
    public static class lCamposSqlNotNull
    {
        public static List<CamposSqlNotNullModel> _lCamposSql = new List<CamposSqlNotNullModel>();

        public static void AddCampoSql(CamposSqlNotNullModel objCamposSqlNotNull)
        {
            _lCamposSql.Add(item: objCamposSqlNotNull);
        }
    }

    public class CamposSqlNotNullModel
    {
        public string xTabela { get; set; }
        public List<PesquisaPadraoModelContract> lCamposSqlModel { get; set; }
    }
}
