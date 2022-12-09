using SQLite;
using System;

namespace WebGallery.Common.Databases.Interfaces
{
    public interface IDatabase
    {
        SQLiteConnection GetConnection();
        void RunInTransaction(Action<SQLiteConnection> action);
    }
}