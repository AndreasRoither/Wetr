using System.Data;

namespace Common.Dal.Ado
{
    public delegate T RowMapper<T>(IDataRecord row);
}