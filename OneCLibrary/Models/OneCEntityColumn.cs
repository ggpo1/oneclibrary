using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OneCLibrary.Models
{
    public class OneCEntityColumn
    {

        public OneCEntityColumn(string columnName)
        {
            ColumnName = columnName ?? throw new ArgumentNullException(nameof(columnName));
        }

        public OneCEntityColumn(string columnName, long entityId)
        {
            ColumnName = columnName;
            EntityId = entityId;
        }

        public OneCEntityColumn(long columnId, string columnName, long entityId)
        {
            ColumnId = columnId;
            ColumnName = columnName ?? throw new ArgumentNullException(nameof(columnName));
            EntityId = entityId;
        }

        public long ColumnId { get; set; }
        public string ColumnName { get; set; }
        public long EntityId { get; set; }

    }
}