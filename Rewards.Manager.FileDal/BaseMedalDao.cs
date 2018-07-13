﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rewards.Manager.DalContracts;
using Rewards.Manager.Entities;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Rewards.Manager.FileDal
{
    /// <summary>
    /// реализация работы с данными Medal
    /// </summary>
    public class BaseMedalDao:IMedalDao
    {
        static string connectionString = "";
        static BaseMedalDao()
        {
            ConnectionStringSettingsCollection settings =
     ConfigurationManager.ConnectionStrings;
            connectionString = settings["Remote"].ConnectionString;
        }

        public Medal Add(Medal medal)
        {
            string ProcName = "AddMedal";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(ProcName, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                //// параметр для ввода имени
                //SqlParameter medalMameParam = new SqlParameter
                //{
                //    ParameterName = "@Name",
                //    Value = medal.Name
                //};
                //// добавляем параметр
                //command.Parameters.Add(medalMameParam);

                //SqlParameter materialNameParam = new SqlParameter
                //{
                //    ParameterName = "@Material",
                //    Value = medal.Material
                //};
                //command.Parameters.Add(materialNameParam);

                var medalNameParam = new SqlParameter("@Name", SqlDbType.VarChar)
                {
                    Value = medal.Name
                };

                var materialNameParam = new SqlParameter("@Material", SqlDbType.VarChar)
                {
                    Value = medal.Material
                };

                command.Parameters.AddRange(new SqlParameter[] { medalNameParam, materialNameParam });

                var medalId = command.ExecuteScalar();

                medal.id = Convert.ToInt32(medalId);

                return medal;
            }
        }


        public bool Update(Medal medal)
        {
            string ProcName = "UpdateMedal";

            using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(ProcName, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                //// параметр для ввода имени
                //SqlParameter medalMameParam = new SqlParameter
                //{
                //    ParameterName = "@Name",
                //    Value = medal.Name
                //};
                //// добавляем параметр
                //command.Parameters.Add(medalMameParam);

                //SqlParameter materialNameParam = new SqlParameter
                //{
                //    ParameterName = "@Material",
                //    Value = medal.Material
                //};
                //command.Parameters.Add(materialNameParam);

                //SqlParameter medalIdParam = new SqlParameter
                //{
                //    ParameterName = "@id",
                //    Value = medal.id
                //};
                //command.Parameters.Add(medalIdParam);

                var medalNameParam = new SqlParameter("@Name", SqlDbType.VarChar)
                {
                    Value = medal.Name
                };

                var materialNameParam = new SqlParameter("@Material", SqlDbType.VarChar)
                {
                    Value = medal.Material
                };

                var medalIdParam = new SqlParameter("@id", SqlDbType.Int)
                {
                    Value = medal.id
                };

                command.Parameters.AddRange(new SqlParameter[] { medalNameParam, materialNameParam, medalIdParam });


                command.ExecuteNonQuery();
                    return true;
                }
        }

        public bool Delete(int id)
        {
            string ProcName = "DeleteMedal";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(ProcName, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                //SqlParameter medalIdParam = new SqlParameter
                //{
                //    ParameterName = "@id",
                //    Value = id
                //};
                //command.Parameters.Add(medalIdParam);

                var medalIdParam = new SqlParameter("@id", SqlDbType.Int)
                {
                    Value = id
                };

                command.Parameters.Add(medalIdParam);

                try
                {
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {

                    throw new Exception($"{ex.TargetSite}: Конфликт инструкции DELETE с ограничением REFERENCE [FK_Reward_Medal]. Конфликт произошел в базе данных [nagrady], таблица [dbo.Reward], [column] 'medalId'.");
                }
            }
        }

        public Medal GetById(int id)
        {
            return GetAll().FirstOrDefault(medal => medal.id == id);
        }

        public IEnumerable<Medal> GetAll()
        {
            string ProcName = "ReadMedal";
            List<Medal> medals = new List<Medal>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(ProcName, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            DataTable table = new DataTable();
                            table.Load(reader);
                            foreach (DataRow row in table.Rows)
                            {
                                Medal p = new Medal();
                                p.id = (int)row["id"];
                                p.Name = row["Name"].ToString();
                                p.Material = row["Material"].ToString();
                                medals.Add(p);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return medals;
        }
    }
}

