using ExoTaskWebApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ExoTaskWebApi.Models.Repositories.Mappers
{
    internal static class DataRecordExtensions
    {
        internal static Task ToTask(this IDataRecord dataRecord)
        {
            return new Task() { Id = (int)dataRecord["Id"], Title = (string)dataRecord["Title"], Done = (bool)dataRecord["Done"], Deleted = (bool)dataRecord["Deleted"], Created = (DateTime)dataRecord["Created"], LastModified = (dataRecord["LastModified"] is DBNull)? null : (DateTime?)dataRecord["LastModified"] };
        }
    }
}
