using System.Data.SqlClient;
using System.Data;
using static System.Net.Mime.MediaTypeNames;
using System;
using implementasi_database;

public class Program
{
    private static readonly string connectionString =
        "Data Source=TONYAJI;Database=db_employee;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

    public static void Menu()
    {
        Console.WriteLine("=====================");
        Console.WriteLine("\tCRUD AJAH");
        Console.WriteLine("=====================");
        Console.WriteLine("1. Insert");
        Console.WriteLine("2. Select");
        Console.WriteLine("3. Update");
        Console.WriteLine("4. Delete");
        Console.WriteLine("5. Insert All");
        Console.WriteLine("6. Exit");
        Console.WriteLine("=====================");
        Console.Write("Pilih menu : ");
    }
    public static void Main()
    {
        int choice;
        do
        {
            Menu();
            choice = Convert.ToInt32(Console.ReadLine());
            //var education = new Educations();
            //var university = new Universities();

            switch (choice)
            {
                case 1:
                    Console.WriteLine("1. University");
                    Console.WriteLine("2. Education");
                    Console.WriteLine("3. Employee");

                    Console.Write("Pilih tabel yang akan di insert data : ");
                    int tabel = Convert.ToInt32(Console.ReadLine());
                    if (tabel == 1)
                    {
                        var university = new universities();
                        Console.Write("Masukkan nama : ");
                        string nama = Console.ReadLine();
                        university.name = nama;

                        var result = universities.InsertUniversity(university);
                        if (result > 0)
                        {
                            Console.WriteLine("Insert success.");
                        }
                        else
                        {
                            Console.WriteLine("Insert failed.");
                        }
                    }
                    else if (tabel == 2)
                    {
                        var education = new Educations();
                        Console.Write("Masukkan Major : ");
                        var major = Console.ReadLine();
                        education.major = major;

                        Console.Write("Masukkan Degree : ");
                        var degree = Console.ReadLine();
                        education.degree = degree;

                        Console.Write("Masukkan GPA : ");
                        var gpa = Console.ReadLine();
                        education.gpa = gpa;

                        Console.Write("University ID : ");
                        var university_id = Convert.ToInt32(Console.ReadLine());
                        education.university_id = university_id;

                        var result = Educations.InsertEducation(education);
                        if (result > 0)
                        {
                            Console.WriteLine("Insert success.");
                        }
                        else
                        {
                            Console.WriteLine("Insert failed.");
                        }
                    }
                    else if (tabel == 3)
                    {
                        employees.PrintOutEmployee();
                    }
                    break;

                case 2:
                    Console.WriteLine("1. University");
                    Console.WriteLine("2. Education");
                    Console.WriteLine("3. Employee");
                    Console.Write("Pilih tabel yang akan di tampilkan : ");
                    int tabel2 = Convert.ToInt32(Console.ReadLine());
                    if (tabel2 == 1)
                    {
                        Console.WriteLine("SELECT ALL FROM UNIVERSITY");
                        var results = universities.GetUniversities();
                        foreach (var result in results)
                        {
                            Console.WriteLine("Id: " + result.id);
                            Console.WriteLine("Name: " + result.name);
                            Console.WriteLine("-----------------------------------------");
                        }
                    }
                    if (tabel2 == 2)
                    {
                        Console.WriteLine("SELECT ALL FROM EDUCATION");
                        var results = Educations.GetEducation();
                        foreach (var result in results)
                        {
                            Console.WriteLine("Id: " + result.id);
                            Console.WriteLine("Major: " + result.major);
                            Console.WriteLine("Degree: " + result.degree);
                            Console.WriteLine("GPA: " + result.gpa);
                            Console.WriteLine("Universty Id : " + result.university_id);
                            Console.WriteLine("-----------------------------------------");
                        }
                    }
                    Console.ReadKey();
                    Console.Clear();
                    break;

                case 3:
                    Console.WriteLine("1. University");
                    Console.WriteLine("2. Education");
                    Console.Write("Pilih tabel yang akan di Update : ");
                    int tabel3 = Convert.ToInt32(Console.ReadLine());
                    if (tabel3 == 1)
                    {
                        var university1 = new universities();
                        Console.Write("Masukkan ID : ");
                        int id = Convert.ToInt32(Console.ReadLine());
                        university1.id = id;

                        Console.Write("Masukkan Nama : ");
                        var name3 = Console.ReadLine();
                        university1.name = name3;

                        var result = universities.UpdateUniversity(university1);
                        if (result > 0)
                        {
                            Console.WriteLine("Update success");
                        }
                        else
                        {
                            Console.WriteLine("Update Failed");
                        }
                    }

                    if (tabel3 == 2)
                    {
                        var education1 = new Educations();
                        Console.Write("Masukkan ID : ");
                        int id = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Major: ");
                        string major = Console.ReadLine();
                        Console.Write("Degree: ");
                        string degree = Console.ReadLine();
                        Console.Write("GPA: ");
                        string gpa = Console.ReadLine();
                        Console.Write("Universty Id : ");
                        int univ_id = Convert.ToInt32(Console.ReadLine());

                        education1.id = id;
                        education1.major = major;
                        education1.degree = degree;
                        education1.gpa = gpa;
                        education1.university_id = univ_id;

                        var results = Educations.UpdateEducation(education1);
                        if (results > 0)
                        {
                            Console.WriteLine("Update success");
                        }
                        else
                        {
                            Console.WriteLine("Update Failed");
                        }
                    }
                    break;

                case 4:
                    Console.WriteLine("1. University");
                    Console.WriteLine("2. Education");
                    Console.Write("Pilih tabel yang akan di hapus : ");

                    int tabel4 = Convert.ToInt32(Console.ReadLine());
                    if (tabel4 == 1)
                    {
                        var university2 = new universities();
                        Console.Write("Masukkan ID : ");
                        int id = Convert.ToInt32(Console.ReadLine());
                        university2.id = id;

                        var result = universities.DeleteUniversity(university2);
                        if (result > 0)
                        {
                            Console.WriteLine("Delete success");
                        }
                        else
                        {
                            Console.WriteLine("Delete Failed");
                        }
                    }

                    else if (tabel4 == 2)
                    {
                        var education2 = new Educations();
                        Console.Write("Masukkan ID : ");
                        int id = Convert.ToInt32(Console.ReadLine());
                        education2.id = id;

                        var result = Educations.DeleteEducation(education2);
                        if (result > 0)
                        {
                            Console.WriteLine("Delete success");
                        }
                        else
                        {
                            Console.WriteLine("Delete Failed");
                        }
                    }
                    break;

                case 5:
                    employees.PrintOutEmployee();
                    break;

                default:
                    Console.WriteLine("Input Invalid");
                    break;
            }
        } while (choice != 6);
    }
}