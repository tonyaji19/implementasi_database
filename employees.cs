﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace implementasi_database
{
    public class employees
    {
        private static readonly string connectionString =
    "Data Source=TONYAJI;Database=db_employee;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
        public string id { get; set; }
        public string nik { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public DateTime birthdate { get; set; }
        public string gender { get; set; }
        public DateTime hiring_date { get; set; }
        public string email { get; set; }
        public string phone_number { get; set; }
        public string department { get; set; }

        public static int InsertEmployee(employees employees)
        {
            int result = 0;
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            SqlTransaction transaction = connection.BeginTransaction();
            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "INSERT INTO tb_m_employees(nik, first_name, last_name, birthdate, gender, hiring_date, email, phone_number, department_id) " +
                    "VALUES (@NIK, @FirstName, @LastName, @Birthdate, @Gender, @HiringDate, @Email, @PhoneNumber, @DepartmentId)";
                command.Transaction = transaction;

                var pNIK = new SqlParameter();
                pNIK.ParameterName = "@NIK";
                pNIK.SqlDbType = SqlDbType.VarChar;
                pNIK.Size = 6;
                pNIK.Value = employees.nik;
                command.Parameters.Add(pNIK);

                var pFname = new SqlParameter();
                pFname.ParameterName = "@FirstName";
                pFname.SqlDbType = SqlDbType.VarChar;
                pFname.Size = 50;
                pFname.Value = employees.first_name;
                command.Parameters.Add(pFname);

                var pLname = new SqlParameter();
                pLname.ParameterName = "@LastName";
                pLname.SqlDbType = SqlDbType.VarChar;
                pLname.Size = 50;
                pLname.Value = employees.last_name;
                command.Parameters.Add(pLname);

                var pBday = new SqlParameter();
                pBday.ParameterName = "@Birthdate";
                pBday.Value = employees.birthdate;
                pBday.SqlDbType = SqlDbType.DateTime;
                command.Parameters.Add(pBday);

                var pGender = new SqlParameter();
                pGender.ParameterName = "@Gender";
                pGender.SqlDbType = SqlDbType.VarChar;
                pGender.Size = 10;
                pGender.Value = employees.gender;
                command.Parameters.Add(pGender);

                var pHdate = new SqlParameter();
                pHdate.ParameterName = "@HiringDate";
                pBday.SqlDbType = SqlDbType.DateTime;
                pHdate.Value = employees.hiring_date;
                command.Parameters.Add(pHdate);

                var pEmail = new SqlParameter();
                pEmail.ParameterName = "@Email";
                pEmail.SqlDbType = SqlDbType.VarChar;
                pEmail.Size = 50;
                pEmail.Value = employees.email;
                command.Parameters.Add(pEmail);

                var pNumber = new SqlParameter();
                pNumber.ParameterName = "@PhoneNumber";
                pNumber.SqlDbType = SqlDbType.VarChar;
                pNumber.Size = 20;
                pNumber.Value = employees.phone_number;
                command.Parameters.Add(pNumber);

                var pDepid = new SqlParameter();
                pDepid.ParameterName = "@DepartmentId";
                pDepid.SqlDbType = SqlDbType.Int;
                pDepid.Size = 4;
                pDepid.Value = employees.department;
                command.Parameters.Add(pDepid);

                result = command.ExecuteNonQuery();
                transaction.Commit();

                return result;
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                transaction.Rollback();
            }
            finally
            {
                connection.Close();
            }
            return result;
        }
        

        public static string GetEmpId(string NIK)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT id FROM tb_m_employees WHERE nik=(@NIK)", connection);

            var niks = new SqlParameter();
            niks.ParameterName = "@NIK";
            niks.Value = NIK;
            command.Parameters.Add(niks);

            string lastEmpId = Convert.ToString(command.ExecuteScalar());
            connection.Close();

            return lastEmpId;
        }

        public static int GetUnivEduId(int choice)
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            if (choice == 1)
            {
                SqlCommand command = new SqlCommand("SELECT TOP 1 id FROM tb_m_universities ORDER BY id DESC", connection);

                int id = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();

                return id;
            }
            else
            {
                SqlCommand command = new SqlCommand("SELECT TOP 1 id FROM tb_m_educations ORDER BY id DESC", connection);

                int id = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();

                return id;
            }
        }
        public static void PrintOutEmployee()
        {
            var employee = new employees();
            var profiling = new profilings();
            var education = new Educations();
            var university = new universities();

            Console.Write("NIK : ");
            var niks = Console.ReadLine();
            employee.nik = niks;

            Console.Write("First Name : ");
            employee.first_name = Console.ReadLine();

            Console.Write("Last Name : ");
            employee.last_name = Console.ReadLine();

            Console.Write("Birthdate : ");
            employee.birthdate = DateTime.Parse(Console.ReadLine());

            Console.Write("Gender : ");
            employee.gender = Console.ReadLine();

            Console.Write("Hiring Date : ");
            employee.hiring_date = DateTime.Parse(Console.ReadLine());

            Console.Write("Email : ");
            employee.email = Console.ReadLine();

            Console.Write("Phone Number : ");
            employee.phone_number = Console.ReadLine();

            Console.Write("Department ID : ");
            employee.department = Console.ReadLine();

            //EDUCATION
            Console.Write("Major : ");
            education.major = Console.ReadLine();

            Console.Write("Degree : ");
            education.degree = Console.ReadLine();

            Console.Write("GPA : ");
            education.gpa = Console.ReadLine();

            Console.Write("University Name : ");
            university.name = Console.ReadLine();

            var result = InsertEmployee(employee);
            if (result > 0)
            {
                Console.WriteLine("INSERT Success");
            }
            else
            {
                Console.WriteLine("INSERT Failed");
            }

            universities.InsertUniversity(university);
            education.university_id = GetUnivEduId(1);
            Educations.InsertEducation(education);

            profiling.employee_id = GetEmpId(niks);
            profiling.education = GetUnivEduId(2);
            profilings.InsertProfiling(profiling);

        }
    }

}
