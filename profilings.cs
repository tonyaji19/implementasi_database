using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace implementasi_database
{
    public class profilings
    {
        public string employee_id { get; set; }
        public int education { get; set; }

        private static readonly string connectionString =
            "Data Source=TONYAJI;Database=db_employee;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
        public static int InsertProfiling(profilings profilings)
        {
            int result = 0;
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            var employee = new employees();
            var education = new Educations();

            SqlTransaction transaction = connection.BeginTransaction();
            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "INSERT INTO tb_tr_profilings(employee_id, education_id) VALUES (@EmployeeId, @EducationId)";
                command.Transaction = transaction;

                var pEmpId = new SqlParameter();
                pEmpId.ParameterName = "@EmployeeId";
                pEmpId.Value = profilings.employee_id;
                command.Parameters.Add(pEmpId);

                var pEduId = new SqlParameter();
                pEduId.ParameterName = "@EducationId";
                pEduId.Value = profilings.education;
                command.Parameters.Add(pEduId);

                result = command.ExecuteNonQuery();
                transaction.Commit();

                return result;
            }

            catch (Exception e)
            {
                transaction.Rollback();
            }
            finally
            {
                connection.Close();
            }
            return result;
        }
    }
}
