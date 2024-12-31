
using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;

namespace RentalPropertyAnalyzer.Services
{
    public class StateService
    {
        public string _connectionString;

        public StateService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<SelectListItem> GetStates()
        {
            var states = new List<SelectListItem>();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("sp_GetStates", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            states.Add(new SelectListItem
                            {
                                Value = reader["StateID"].ToString(),
                                Text = reader["State"].ToString()
                            });
                        }
                    }
                }
            }

            return states;
        }


public IEnumerable<SelectListItem> GetCountiesByState(string stateCode)
    {
        var counties = new List<SelectListItem>();

        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = new SqlCommand("sp_GetCounties", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Add the stateCode parameter to the command
                command.Parameters.Add(new SqlParameter("@StateCode", stateCode));

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        counties.Add(new SelectListItem
                        {
                            Value = reader["County"].ToString(), // Use County ID if you have one, for Value
                            Text = reader["County"].ToString()
                        });
                    }
                }
            }
        }

        return counties;
    }


    //public List<string> GetCountiesByState(string stateCode)
    //{
    //    var counties = new List<string>();

    //    using (var connection = new SqlConnection(_connectionString))
    //    {
    //        using (var command = new SqlCommand("sp_GetCounties", connection))
    //        {
    //            command.CommandType = CommandType.StoredProcedure;

    //            // Add the stateCode parameter to the command
    //            command.Parameters.Add(new SqlParameter("@StateCode", stateCode));

    //            connection.Open();
    //            using (var reader = command.ExecuteReader())
    //            {
    //                while (reader.Read())
    //                {
    //                    // Assuming the column name in your database is "CountyName"
    //                    // Change "CountyName" to the actual column name if it's different
    //                    counties.Add(reader["County"].ToString());
    //                }
    //            }
    //        }
    //    }

    //    return counties;
    //}

}
}




