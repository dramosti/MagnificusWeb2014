using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HLP.Wcf.Entries.Infrastructure
{
    public class UnitOfWork : UnitOfWorkBase
    {
        private Database _dbPrincipal;

        //private DbTransaction _transaction; **comentei esta linha pq não tem sentido uma variável privada e a mesma não ser utilizada em nenhum lugar

        public override Database dbPrincipal
        {
            get
            {
                return _dbPrincipal;
            }
        }

        public UnitOfWork()
        {
            this._dbPrincipal = EnterpriseLibraryContainer.Current.GetInstance<Database>("dbPrincipal");
        }

    }
}