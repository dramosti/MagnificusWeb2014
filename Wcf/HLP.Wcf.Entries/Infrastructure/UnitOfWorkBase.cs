using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HLP.Wcf.Entries
{
    public abstract class UnitOfWorkBase : IDisposable
    {
        
        private int porcentagem;

        public int pPorcentagem
        {
            get
            {
                return porcentagem;
            }
        }

        public abstract Microsoft.Practices.EnterpriseLibrary.Data.Database dbPrincipal { get; }
        public DbTransaction dbTransaction
        {
            get
            {
                return _dbTransaction;
            }
        }

        private DbTransaction _dbTransaction;
        private DbConnection _transactConnection;

        public UnitOfWorkBase()
        {
        }

        public void AddParameterToCommand(DbCommand cmd, string parameterName, DbType type, object value)
        {
            DbParameter param = cmd.CreateParameter();

            param.ParameterName = parameterName;
            param.DbType = type;
            param.Value = value;

            cmd.Parameters.Add(param);
        }

        public void BeginTransaction()
        {
            if (this._dbTransaction != null ||
                this._transactConnection != null)
            {
                throw new Exception("Já existe uma transação aberta no banco de dados!");
            }

            this._transactConnection = this.dbPrincipal.CreateConnection();
            this._transactConnection.Open();
            this._dbTransaction = this._transactConnection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            if (this._dbTransaction == null ||
                this._transactConnection == null)
            {
                throw new Exception("Não existe uma transação aberta no banco de dados!");
            }

            this._dbTransaction.Commit();
            this._dbTransaction = null;

            this._transactConnection.Close();
            this._transactConnection = null;
        }

        public void RollBackTransaction()
        {
            if (this._dbTransaction != null)
            {
                this._dbTransaction.Rollback();
                this._dbTransaction = null;

                this._transactConnection.Close();
                this._transactConnection = null;
            }
        }

        public void Dispose()
        {
            if (this._dbTransaction != null)
            {
                this._dbTransaction.Rollback();
                this._dbTransaction = null;
            }

            if (this._transactConnection != null)
            {
                this._transactConnection.Close();
                this._transactConnection = null;
            }
        }

        /// <summary>
        /// Verifica se uma Tabela ou uma View existe na base de dados
        /// </summary>
        /// <param name="nm_Table"></param>
        /// <returns></returns>
        public bool TableExistis(string nm_Table)
        {
            int iCount = (int)this.dbPrincipal.ExecuteScalar(
              "dbo.Proc_ExistsView", nm_Table);

            if (iCount == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }



        /// <summary>
        /// Verifica se uma Tabela ou uma View existe na base de dados
        /// </summary>
        /// <param name="nm_Table"></param>
        /// <returns></returns>
        public bool ViewExistis(string nm_View)
        {
            int iCount = (int)this.dbPrincipal.ExecuteScalar(
              "dbo.Proc_ExistsView", nm_View);

            if (iCount == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool FunctionExistis(string nm_Function)
        {
            int iCount = (int)this.dbPrincipal.ExecuteScalar(
              "dbo.Proc_ExistisFunction", nm_Function);

            if (iCount == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Verifica se uma coluna existe na Tabela/View
        /// </summary>
        /// <param name="nm_Table"></param>
        /// <param name="nm_Coluna"></param>
        /// <returns></returns>
        public bool ColunaExistis(string nm_Table, string nm_Coluna)
        {
            int iCount = (int)this.dbPrincipal.ExecuteScalar(
               "dbo.Proc_Verifica_Coluna_Existe", nm_Table, nm_Coluna);

            if (iCount == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        //    BACKUP DATABASE [YourDatabase] TO  DISK = N'C:\YourPathAndFile.bak' 
        //WITH NOFORMAT, NOINIT,  
        //NAME = N'Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10
        public void BackupDatabase(string xPath, string xNameBackup)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = this.dbPrincipal.ConnectionString;

            Backup sqlBackup = new Backup();

            sqlBackup.Action = BackupActionType.Database;

            sqlBackup.BackupSetDescription = "ArchiveDataBase:" + DateTime.Now.ToShortDateString();

            sqlBackup.BackupSetName = "Archive";

            sqlBackup.Database = con.Database;

            BackupDeviceItem deviceItem = new BackupDeviceItem(xPath + "\\" + xNameBackup, DeviceType.File);

            //ServerConnection connection = new ServerConnection(con.DataSource, con.Credential.UserId, con.Credential.Password);
            ServerConnection connection = new ServerConnection(con);
            Server sqlServer = new Server(connection);

            Microsoft.SqlServer.Management.Smo.Database db = sqlServer.Databases[con.Database];

            sqlBackup.Initialize = true;

            sqlBackup.Checksum = true;

            sqlBackup.ContinueAfterError = true;

            sqlBackup.Devices.Add(deviceItem);

            sqlBackup.Incremental = false;

            sqlBackup.ExpirationDate = DateTime.Now.AddDays(3);

            sqlBackup.LogTruncation = BackupTruncateLogType.Truncate;

            sqlBackup.FormatMedia = false;

            sqlBackup.Complete += new ServerMessageEventHandler(sqlRestore_Complete);

            // AQUI VC SETA O VALOR PARA 1;

            sqlBackup.PercentCompleteNotification = 1;

            sqlBackup.PercentComplete += new PercentCompleteEventHandler(sqlRestore_PercentComplete);

            try
            {
                sqlBackup.SqlBackup(sqlServer);
            }
            catch (Exception ex)
            {

                throw ex;
            }


            #region Backup Método 2
            //Podia ser feito desta forma, porém foi optado pela classe de Backup do .NET

            //SqlConnection con = new SqlConnection();
            //con.ConnectionString = this.dbPrincipal.ConnectionString;
            //con.FireInfoMessageEventOnUserErrors = true;
            //con.Open();
            //SqlCommand cmd = new SqlCommand(string.Format(
            //    "backup database [{0}] to disk = N'{1}' with NOFORMAT, NOINIT, name = N'Full Database Backup', stats = 1",
            //    con.Database,
            //    xPath+"\\"+xNameBackup), con);

            //try
            //{
            //    cmd.ExecuteNonQuery();
            //}
            //catch (Exception ex)
            //{

            //    throw;
            //}             
            //con.Close();
            //con.FireInfoMessageEventOnUserErrors = false;
            #endregion Backup Método 2
        }

        private void sqlRestore_PercentComplete(object sender, PercentCompleteEventArgs e)
        {
            // E A CADA PORCENTO QUE O COMANDO SqlBackup DEVOLVER, VOCÊ CARREGA SUA VARIAVEL!
            // AQUI MANDEI UMA MENSAGEM PARA VOCÊ ENTENDER!!!
            porcentagem = e.Percent;
        }

        public event EventHandler<ServerMessageEventArgs> Complete;

        void sqlRestore_Complete(object sender, ServerMessageEventArgs e)
        {

            if (Complete != null)
                Complete(sender, e);
        }
    }
}