using System;
using System.Collections.Generic;
using System.Data.SQLite;
using DataViewModels;

namespace DataRepository
{
    public class SkillsRepository: IRepository<SkillDb>
    {
        
        #region Singleton
        protected static SkillsRepository instance = null;
        public static SkillsRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new SkillsRepository();
                return instance;
            }
        }

        #region Constructor
        protected SkillsRepository() { }
        #endregion

        #endregion 


        public List<SkillDb> GetAll()
        {
            List<SkillDb> resoult = new List<SkillDb>();
            List<int> SkillsId = new List<int>();

            using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=D:\Projects\DBManager\DataRepository\bin\Debug\MemesDB.db"))
            {
                conn.Open();

                using (SQLiteCommand com = new SQLiteCommand(conn))
                {

                    com.CommandText = "SELECT Id FROM Skills";
                    using (SQLiteDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SkillsId.Add(reader.GetInt32(0));
                        }
                    }
                }

                conn.Close();

                for (int i = 0; i < SkillsId.Count; i++)
                {
                    resoult.Add(GetById(SkillsId[i]));
                }
            }

            if (resoult.Count != 0)
                return resoult;
            else
            {
                throw new Exception("Not found skills table.");
            }
        }

        public SkillDb GetById(int id)
        {
            SkillDb resoult = null;

            using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=D:\Projects\DBManager\DataRepository\bin\Debug\MemesDB.db"))
            {
                conn.Open();

                using (SQLiteCommand com = new SQLiteCommand(conn))
                {

                    com.CommandText = "SELECT * FROM Skills WHERE id = " + id;

                    using (SQLiteDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            resoult = new SkillDb
                            {
                                Id = int.Parse(reader["Id"].ToString()),
                                Name = reader["Name"].ToString(),
                                Descriptions = reader["Descriptions"].ToString(),
                                ImagePath = reader["Image"].ToString(),
                                StatId = int.Parse(reader["StatsId"].ToString()),
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
                throw new Exception("Not found skills with this id.");
            }
        }

        public bool Change(SkillDb newSkill)
        {
            return false;
        }
    }
}
