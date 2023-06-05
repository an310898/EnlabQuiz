using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EnlabQuiz.Controllers
{
    
    [Route("/[controller]")]
    [ApiController]
    public class CallProcedure : ControllerBase
    {
        private readonly string connectionString;

        public CallProcedure(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("EnlabQuizDB");
        }


        [HttpPost("{name}")]
        public IActionResult CallStoreProcedure(string name, [FromBody] Dictionary<string, object> parameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(name, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        foreach (KeyValuePair<string, object> parameter in parameters)
                        {
                            SqlParameter sqlParameter = new SqlParameter(parameter.Key, parameter.Value.ToString());
                            command.Parameters.Add(sqlParameter);
                        }

                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {

                        try
                        {
                            DataTable resultTable = new DataTable();
                            adapter.Fill(resultTable);

                            List<Dictionary<string, dynamic>> resultList = new List<Dictionary<string, dynamic>>();
                            foreach (DataRow row in resultTable.Rows)
                            {
                                Dictionary<string, dynamic> resultDict = new Dictionary<string, dynamic>();
                                foreach (DataColumn col in resultTable.Columns)
                                {
                                    resultDict[col.ColumnName] = row.IsNull(col) ? null : row[col]; ;
                                }
                                resultList.Add(resultDict);
                            }

                            return Ok(resultList);
                        }
                        catch (Exception ex)
                        {
                            return BadRequest(ex.Message);
                        }
                    }
                }
            }
        }
    }
}
