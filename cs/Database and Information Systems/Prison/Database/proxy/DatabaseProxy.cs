using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;
using System.Data.Common;

namespace PrisonORM.Database.proxy
{
    abstract class DatabaseProxy
    {
        public abstract bool Connect();
        public abstract bool Connect(String conString);
        public abstract void Close();
        public abstract void BeginTransaction();
        public abstract void EndTransaction();
        public abstract void Rollback();
    }
}
