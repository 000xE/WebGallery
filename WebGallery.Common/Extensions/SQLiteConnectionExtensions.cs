using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGallery.Common.Models.Interfaces;

namespace WebGallery.Common.Extensions
{
    public static class SQLiteConnectionExtensions
    {
        public static int InsertOrReplaceExisting(this SQLiteConnection connection, IEntity entity)
        {
            if (entity.Id == 0) //New entity
            {
                return connection.Insert(entity);
            }
            else //Existing entity
            {
                return connection.InsertOrReplace(entity);
            }
        }

        public static int InsertOrReplaceExisting(this SQLiteConnection connection, IEnumerable<IEntity> entities)
        {
            var changed = 0;

            foreach (var entity in entities)
            {
                changed += SQLiteConnectionExtensions.InsertOrReplaceExisting(connection, entity);
            }

            return changed;
        }
    }
}
