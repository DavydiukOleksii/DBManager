using System;
using System.Collections.Generic;
using System.Data.SQLite;
using DataViewModels;

namespace DataRepository
{
    public class HeroesRepository: IRepository<HeroDb>
    {
        #region Singleton
        protected static HeroesRepository instance = null;
        public static HeroesRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new HeroesRepository();
                return instance;
            }
        }

        #region Constructor
        protected HeroesRepository() { }
        #endregion

        #endregion 

        public List<HeroDb> GetAll()
        {
            List<HeroDb> resoult = new List<HeroDb>();
            List<int> SkillsId = new List<int>();

            using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=D:\Projects\DBManager\DataRepository\bin\Debug\MemesDB.db"))
            {
                conn.Open();

                using (SQLiteCommand com = new SQLiteCommand(conn))
                {

                    com.CommandText = "SELECT Id FROM Heroes";
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

        public HeroDb GetById(int id)
        {
            HeroDb resoult = null;

            using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=D:\Projects\DBManager\DataRepository\bin\Debug\MemesDB.db"))
            {
                conn.Open();

                using (SQLiteCommand com = new SQLiteCommand(conn))
                {

                    com.CommandText = "SELECT * FROM Heroes WHERE id = " + id;

                    using (SQLiteDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            resoult = new HeroDb
                            {
                                Id = int.Parse(reader["Id"].ToString()),
                                StatsId = int.Parse(reader["StatsId"].ToString()),
                                Name = reader["Name"].ToString(),
                                Descriptions = reader["Descriptions"].ToString(),
                                ImagePath = reader["Image"].ToString(),
                                Skill1Id = int.Parse(reader["Skill1Id"].ToString()),
                                Skill2Id = int.Parse(reader["Skill2Id"].ToString()),
                                Skill3Id = int.Parse(reader["Skill3Id"].ToString()),
                                Skill4Id = int.Parse(reader["Skill4Id"].ToString()),
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

        public bool Change(HeroDb hero)
        {
            return false;
        }
    }
}
