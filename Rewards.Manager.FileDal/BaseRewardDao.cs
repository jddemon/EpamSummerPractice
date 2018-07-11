using System;
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
    /// реализует доступа к данным Reward
    /// </summary>
    public class BaseRewardDao:IRewardDao
    {
        static string connectionString = "";
        static BaseRewardDao()
        {
            ConnectionStringSettingsCollection settings =
     ConfigurationManager.ConnectionStrings;
            connectionString = settings["Remote"].ConnectionString;
        }

        public Reward Add(Reward reward)
        {
            string ProcName = "AddNagrady";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(ProcName, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlParameter personIdParam = new SqlParameter
                    {
                        ParameterName = "@personId",
                        Value = reward.awarded.id
                    };
                    command.Parameters.Add(personIdParam);

                    SqlParameter medalIdParam = new SqlParameter
                    {
                        ParameterName = "@medalId",
                        Value = reward.medal.id
                    };
                    command.Parameters.Add(medalIdParam);
                    command.ExecuteNonQuery();
                    return reward;
                }
        }
        public bool Delete(Reward reward)
        {
            string ProcName = "DeleteNagrady";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(ProcName, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlParameter personIdParam = new SqlParameter
                    {
                        ParameterName = "@personId",
                        Value = reward.awarded.id
                    };

                    command.Parameters.Add(personIdParam);
                    SqlParameter medalIdParam = new SqlParameter
                    {
                        ParameterName = "@medalId",
                        Value = reward.medal.id
                    };
                    command.Parameters.Add(medalIdParam);
                    command.ExecuteNonQuery();
                    return true;
                }
        }

        public IEnumerable<int[]> GetAll()
        {
            string ProcName = "ReadNagrady";

            List<int[]> pair = new List<int[]>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(ProcName, connection);
                    // указываем, что команда не представляет хранимую процедуру
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        DataTable table = new DataTable();
                        table.Load(reader);
                        foreach (DataRow row in table.Rows)
                        {
                            int[] p = new int[2];
                            p[0] = (int)row["personId"];
                            p[1] = (int)row["medalId"];
                            pair.Add(p);
                        }
                    }
                }
            }
            catch (Exception ex1)
            {

            }
            return pair;
        }
    }
}
