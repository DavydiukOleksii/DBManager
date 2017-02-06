using System;
using System.Collections.Generic;
using System.Data.SQLite;
using DataViewModels;

namespace DataRepository
{
    public class StatsRepository: IRepository<StatsDb>
    {

        #region Singleton
        protected static StatsRepository instance = null;
        public static StatsRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new StatsRepository();
                return instance;
            }
        }

        #region Constructor
        protected StatsRepository() { }
        #endregion

        #endregion 



        public List<StatsDb> GetAll()
        {
            List<StatsDb> resoult = new List<StatsDb>();
            List<int> StatsId = new List<int>();

            //SQLiteConnection.CreateFile("MemesDB.db");

            using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=D:\Projects\DBManager\DataRepository\bin\Debug\MemesDB.db"))
            {
                conn.Open();

                using (SQLiteCommand com = new SQLiteCommand(conn))
                {

                    com.CommandText = "SELECT Id FROM Stats";
                    using (SQLiteDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            StatsId.Add(reader.GetInt32(0));
                        }
                    }
                }

                conn.Close();

                for (int i = 0; i < StatsId.Count; i++)
                {
                    resoult.Add(GetById(StatsId[i]));
                }
            }

            if (resoult.Count != 0)
                return resoult;
            else
            {
                throw new Exception("Not found stats table.");
            }
        }

        public StatsDb GetById(int id)
        {
            StatsDb resoult = null;

            using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=D:\Projects\DBManager\DataRepository\bin\Debug\MemesDB.db"))
            {
                conn.Open();

                using (SQLiteCommand com = new SQLiteCommand(conn))
                {

                    com.CommandText = "SELECT * FROM Stats WHERE id = " + id;

                    using (SQLiteDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            resoult = new StatsDb
                            {
                                Armor = int.Parse(reader["Armor"].ToString()),
                                Attack = int.Parse(reader["Attack"].ToString()),
                                Health = int.Parse(reader["Health"].ToString()),
                                Id = int.Parse(reader["Id"].ToString()),
                                Miss = int.Parse(reader["Miss"].ToString())
                            };
                        }
                    }
                }

                conn.Close();
            }

            if (resoult != null)
                return resoult;
            else
            {
                throw new Exception("Not found stats with this id.");
            }
        }
    }
}
