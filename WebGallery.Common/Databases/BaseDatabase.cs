using SQLite;
using System;
using System.Diagnostics;
using System.Linq;
using WebGallery.Common.Databases.Interfaces;
using WebGallery.Common.Helpers.Interfaces;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WebGallery.Common.Databases
{
    public abstract class BaseDatabase : IDatabase
    {
        private readonly IFileHelper fileHelper;
        private readonly object _lock = new object();

        private SQLiteConnection connection;

        protected SQLiteConnection Connection => this.GetConnection();

        protected virtual CreateFlags CreateFlags => CreateFlags.AllImplicit | CreateFlags.AutoIncPK;

        protected abstract string DatabaseName { get; }

        protected virtual string DatabasePath => this.fileHelper.GetFilePath(Enums.DirectoryType.Database, this.DatabaseName + ".db3");

        protected abstract Type[] EntityTypes { get; }

        public BaseDatabase(IFileHelper fileHelper)
        {
            this.fileHelper = fileHelper;
            this.Initialise();
        }

        public void Initialise()
        {
            this.Connection.CreateTables(this.CreateFlags, this.EntityTypes);
        }

        public SQLiteConnection GetConnection()
        {
            lock (this._lock)
            {
                if (this.connection == null)
                {
                    this.connection = new SQLiteConnection(this.DatabasePath, true);
                }
            }

            return this.connection;
        }

        public void RunInTransaction(Action<SQLiteConnection> action)
        {
            lock (this._lock)
            {
                try
                {
                    Action<SQLiteConnection> newaction = (connection) =>
                    {
                        connection.RunInTransaction(() => action(connection));
                    };

                    var connection = this.GetConnection();
                    newaction(connection);
                }
                catch (Exception e) 
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}
