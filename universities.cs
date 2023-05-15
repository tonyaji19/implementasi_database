using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace implementasi_database
{
    public class universities
    {
        public int id { get; set; }
        public string name { get; set; }

        private static readonly string connectionString =
         "Data Source=TONYAJI;Database=db_employee;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

        public static int InsertUniversity(universities university)
        {
            int result = 0;
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            SqlTransaction transaction = connection.BeginTransaction();
            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "INSERT INTO tb_m_universities(name) VALUES (@name)";
                command.Transaction = transaction;

                var pName = new SqlParameter();
                pName.ParameterName = "@name";
                pName.SqlDbType = SqlDbType.VarChar;
                pName.Size = 50;
                pName.Value = university.name;
                command.Parameters.Add(pName);

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

        public static List<universities> GetUniversities()
        {
            var universities = new List<universities>();
            using SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM tb_m_universities";
                connection.Open();

                using SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var university = new universities();
                        university.id = reader.GetInt32(0);
                        university.name = reader.GetString(1);

                        universities.Add(university);
                    }
                    return universities;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return new List<universities>();
        }

        public static int UpdateUniversity(universities university)
        {
            int result = 0;
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            SqlTransaction transaction = connection.BeginTransaction();
            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "UPDATE tb_m_universities SET name = (@name) WHERE id = (@id)";
                command.Transaction = transaction;

                var pName = new SqlParameter();
                var pId = new SqlParameter();

                pName.ParameterName = "@name";
                pId.ParameterName = "@id";
                pName.Value = university.name;
                pId.Value = university.id;

                command.Parameters.Add(pName);
                command.Parameters.Add(pId);

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

        public static int DeleteUniversity(universities university)
        {
            int result = 0;
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            SqlTransaction transaction = connection.BeginTransaction();
            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "DELETE FROM tb_m_universities WHERE id = (@id)";
                command.Transaction = transaction;

                var pId = new SqlParameter();
                pId.ParameterName = "@id";
                pId.Value = university.id;

                command.Parameters.Add(pId);
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
