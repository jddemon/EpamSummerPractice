using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rewards.Manager.DalContracts;
using Rewards.Manager.Entities;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Rewards.Manager.FileDal
{
    /// <summary>
    /// реализует доступа к данным Person
    /// </summary>
    public class BasePersonDao:IPersonDao
    {
        static string connectionString = "";

        static BasePersonDao()
        {
            ConnectionStringSettingsCollection settings =
     ConfigurationManager.ConnectionStrings;
            connectionString = settings["Remote"].ConnectionString;
        }

        public Person Add(Person person)
        {
            string ProcName = "AddPerson";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(ProcName, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                //SqlParameter adresParam = new SqlParameter
                //{
                //    ParameterName = "@Adres",
                //    Value = person.Adres
                //};
                //command.Parameters.Add(adresParam);

                //SqlParameter birthdayParam = new SqlParameter
                //{
                //    ParameterName = "@Birthday",
                //    Value = person.Birthday
                //};
                //command.Parameters.Add(birthdayParam);

                //SqlParameter vozrastParam = new SqlParameter
                //{
                //    ParameterName = "@Vozrast",
                //    Value = person.Vozrast
                //};
                //command.Parameters.Add(vozrastParam);

                //SqlParameter imjaParam = new SqlParameter
                //{
                //    ParameterName = "@Imja",
                //    Value = person.Imja
                //};
                //command.Parameters.Add(imjaParam);

                //SqlParameter familiaParam = new SqlParameter
                //{
                //    ParameterName = "@Familia",
                //    Value = person.Familia
                //};
                //command.Parameters.Add(familiaParam);

                var adresParam = new SqlParameter("@Adres", SqlDbType.VarChar)
                {
                    Value = person.Adres
                };

                var birthdayParam = new SqlParameter("@Birthday", SqlDbType.Date)
                {
                    Value = person.Birthday
                };

                var vozrastParam = new SqlParameter("@Vozrast", SqlDbType.Int)
                {
                    Value = person.Vozrast
                };

                var imjaParam = new SqlParameter("@Imja", SqlDbType.VarChar)
                {
                    Value = person.Imja
                };

                var familiaParam = new SqlParameter("@Familia", SqlDbType.VarChar)
                {
                    Value = person.Familia
                };

                command.Parameters.AddRange(new SqlParameter[] { adresParam, birthdayParam,
                    vozrastParam, imjaParam, familiaParam });

                var result = command.ExecuteScalar();
                person.id=Convert.ToInt32(result);
                return person;
            }
        }

        public Person GetById(int id)
        {
            return GetAll().FirstOrDefault(person => person.id == id);
        }

        public bool Update(Person person)
        {
            string ProcName = "UpdatePerson";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(ProcName, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                //SqlParameter idParam = new SqlParameter
                //{
                //    ParameterName = "@id",
                //    Value = person.id
                //};
                //command.Parameters.Add(idParam);

                //SqlParameter adresParam = new SqlParameter
                //{
                //    ParameterName = "@Adres",
                //    Value = person.Adres
                //};
                //command.Parameters.Add(adresParam);

                //SqlParameter birthdayParam = new SqlParameter
                //{
                //    ParameterName = "@Birthday",
                //    Value = person.Birthday
                //};
                //command.Parameters.Add(birthdayParam);

                //SqlParameter vozrastParam = new SqlParameter
                //{
                //    ParameterName = "@Vozrast",
                //    Value = person.Vozrast
                //};
                //command.Parameters.Add(vozrastParam);

                //SqlParameter imjaParam = new SqlParameter
                //{
                //    ParameterName = "@Imja",
                //    Value = person.Imja
                //};
                //command.Parameters.Add(imjaParam);

                //SqlParameter familiaParam = new SqlParameter
                //{
                //    ParameterName = "@Familia",
                //    Value = person.Familia
                //};
                //command.Parameters.Add(familiaParam);

                var idParam = new SqlParameter("@id", SqlDbType.Int)
                {
                    Value = person.id
                };

                var adresParam = new SqlParameter("@Adres", SqlDbType.VarChar)
                {
                    Value = person.Adres
                };

                var birthdayParam = new SqlParameter("@Birthday", SqlDbType.Date)
                {
                    Value = person.Birthday
                };

                var vozrastParam = new SqlParameter("@Vozrast", SqlDbType.Int)
                {
                    Value = person.Vozrast
                };

                var imjaParam = new SqlParameter("@Imja", SqlDbType.VarChar)
                {
                    Value = person.Imja
                };

                var familiaParam = new SqlParameter("@Familia", SqlDbType.VarChar)
                {
                    Value = person.Familia
                };

                command.Parameters.AddRange(new SqlParameter[] { idParam, adresParam, birthdayParam,
                    vozrastParam, imjaParam, familiaParam });

                command.ExecuteNonQuery();
                }
                return true;
        }

        public bool Delete(int id)
        {
            string ProcName = "DeletePerson";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(ProcName, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                //SqlParameter idParam = new SqlParameter
                //{
                //    ParameterName = "@id",
                //    Value = id
                //};
                //command.Parameters.Add(idParam);

                    var idParam = new SqlParameter("@id", SqlDbType.Int)
                    {
                        Value = id
                    };

                    command.Parameters.Add(idParam);

                    command.ExecuteNonQuery();
                }
                return true;
        }


        public IEnumerable<Person> GetAll()
        {
            List<Person> persons = new List<Person>();
            try
            {
                string ProcName = "ReadPerson";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(ProcName, connection);

                    // указываем, что команда представляет хранимую процедуру
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    // запрос возвращает ридер для получения таблицы
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            // для получения данных создаем таблицу
                            DataTable table = new DataTable();
                            //загружаем таблицу из ридера
                            table.Load(reader);

                            // выбираем данные из таблицы и создаем список персон
                            foreach (DataRow row in table.Rows)
                            {
                                Person p = new Person();
                                p.id = (int)row["id"];
                                p.Imja = row["Imja"].ToString();
                                p.Familia = row["Familia"].ToString();
                                p.Birthday = (DateTime)row["Birthday"];
                                p.Vozrast = (int)row["Vozrast"];
                                p.Adres = row["Adres"].ToString();
                                persons.Add(p);
                            }
                        }
                    }
                }
            }
            catch (Exception ex1)
            {

            }
            return persons;
        }
    }
}
