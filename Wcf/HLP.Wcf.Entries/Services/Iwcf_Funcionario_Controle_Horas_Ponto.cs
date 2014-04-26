using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HLP.Wcf.Entries.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "Iwcf_Funcionario_Controle_Horas_Ponto" in both code and config file together.
    [ServiceContract]
    public interface Iwcf_Funcionario_Controle_Horas_Ponto
    { 
        [OperationContract]
        List<HLP.Entries.Model.RecursosHumanos.Funcionario_Controle_Horas_PontoModel> GetAllFuncionario_Controle_Horas_Ponto(int idFuncionario, DateTime dRelogioPonto);
        [OperationContract]
        HLP.Entries.Model.RecursosHumanos.Funcionario_Controle_Horas_PontoModel Save(HLP.Entries.Model.RecursosHumanos.Funcionario_Controle_Horas_PontoModel objFuncionario_Controle_Horas_Ponto, int idEmpresa);
        [OperationContract]
        HLP.Entries.Model.RecursosHumanos.FuncionarioModel GetFuncionario(string xCodigoAlternativo);
        [OperationContract]
        string UserCanRegisterPonto(string xCodigoAlternativo, int idEmpresa);
    }
}
