using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dal.Ado
{
    public class AdoTemplate
    {
        private readonly IConnectionFactory connectionFactory;

        public AdoTemplate(IConnectionFactory connectionFactory)
        {
            // Beacuse this variable is readonly it can only be set in the constructor
            // When this check succeeds, it cannot be null anywhere else
            this.connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public IEnumerable<T> Query<T>(string sql, RowMapper<T> rowMapper, params Parameter[] parameters)
        {

            var items = new List<T>();

            // Create Connection to DB
            using (DbConnection connection = this.connectionFactory.CreateConnection())
            {
                // Create Commend/Query
                using (DbCommand command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    AddParameters(command, parameters);

                    // Read reveived data
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(rowMapper(reader));
                        }
                    }
                }
            }

            return items;
        }

        private void AddParameters(DbCommand command, Parameter[] parameters)
        {
           foreach(var parameter in parameters)
            {
                DbParameter dbParameter = command.CreateParameter();
                dbParameter.ParameterName = parameter.Name;
                dbParameter.Value = parameter.Value;
                command.Parameters.Add(dbParameter);
            }
        }

        public async Task<int> ExecuteAsync(string sql, params Parameter[] parameters)
        {
            using (DbConnection connection = connectionFactory.CreateConnection())
            using (DbCommand command = connection.CreateCommand())
            {
                command.CommandText = sql;
                this.AddParameters(command, parameters);

                return await command.ExecuteNonQueryAsync();

            }
        }
    }
}
