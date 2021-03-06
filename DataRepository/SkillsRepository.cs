﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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
                            var memoryStream = new MemoryStream((byte[])reader["Image"]);
                            object result = null;
                            try
                            {
                                memoryStream.Seek(0, SeekOrigin.Begin);
                                result = new BinaryFormatter().Deserialize(memoryStream);
                            }
                            catch{}
                            resoult = new SkillDb
                            {
                                Id = int.Parse(reader["Id"].ToString()),
                                Name = reader["Name"].ToString(),
                                Descriptions = reader["Descriptions"].ToString(),
                                ImagePath = (byte[]) result,
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

        public bool Update(SkillDb newSkill)
        {
            try
            {
                using (SQLiteConnection conn =
                    new SQLiteConnection(@"Data Source=D:\Projects\DBManager\DataRepository\bin\Debug\MemesDB.db"))
                {
                    conn.Open();

                    //using (SQLiteCommand com = new SQLiteCommand(conn))
                    //{



                    //    com.CommandText = "UPDATE Skills SET Name='" + newSkill.Name + "', Descriptions='" + newSkill.Descriptions + "', Image='" + newSkill.ImagePath + "' WHERE id=" + newSkill.Id;
                    //    com.ExecuteNonQuery();
                    //}

                    var sql = "UPDATE Skills SET Name=$Name, Descriptions=$Descriptions, Image=$Image WHERE id= $ID";
                    using (var command = new SQLiteCommand(sql, conn))
                        {
                            command.Parameters.AddWithValue("$Name", newSkill.Name);
                            command.Parameters.AddWithValue("$Descriptions", newSkill.Descriptions);
                            using (var ImagePathBinary = new MemoryStream())
                            {
                                new BinaryFormatter().Serialize(ImagePathBinary, newSkill.ImagePath);
                                ImagePathBinary.Seek(0, SeekOrigin.Begin);
                                command.Parameters.AddWithValue("$Image", ImagePathBinary.ToArray());
                            }
                            command.Parameters.AddWithValue("$ID", newSkill.Id);
                            command.ExecuteNonQuery();
                        }
                    conn.Close();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool Delete(int id)
        {
            try
            {
                using (SQLiteConnection conn =
                    new SQLiteConnection(@"Data Source=D:\Projects\DBManager\DataRepository\bin\Debug\MemesDB.db"))
                {
                    conn.Open();

                    using (SQLiteCommand com = new SQLiteCommand(conn))
                    {
                        com.CommandText = "DELETE FROM Skills WHERE id=" + id;
                        com.ExecuteNonQuery();
                    }

                    conn.Close();
                }
            }
            catch 
            {
                return false;
            }
            return true;
        }

        public int Insert(SkillDb newSkill)
        {
            int id = 0;
            using (SQLiteConnection conn =
                    new SQLiteConnection(@"Data Source=D:\Projects\DBManager\DataRepository\bin\Debug\MemesDB.db"))
            {
                conn.Open();

                using (SQLiteCommand com = new SQLiteCommand(conn))
                {
                    //com.CommandText = "INSERT INTO Skills (Name, Descriptions, Image, StatsId) VALUES('" + newSkill.Name + "', '" + newSkill.Descriptions + "', '" + newSkill.ImagePath.ToString() + "', ' " + newSkill.StatId + "')";
                    //com.ExecuteNonQuery();

                    var sql = "INSERT INTO Skills (Name, Descriptions, Image, StatsId) VALUES($Name, $Descriptions, $Image, $StatId)";
                    using (var command = new SQLiteCommand(sql, conn))
                    {
                        command.Parameters.AddWithValue("$Name", newSkill.Name);
                        command.Parameters.AddWithValue("$Descriptions", newSkill.Descriptions);
                        using (var ImagePathBinary = new MemoryStream())
                        {
                            try
                            {
                                new BinaryFormatter().Serialize(ImagePathBinary, newSkill.ImagePath);
                                ImagePathBinary.Seek(0, SeekOrigin.Begin);
                            }
                            catch { }
                            command.Parameters.AddWithValue("$Image", ImagePathBinary.ToArray());
                        }
                        command.Parameters.AddWithValue("$StatId", newSkill.StatId);
                        command.ExecuteNonQuery();
                    }

                    com.CommandText = "SELECT last_insert_rowid()";

                    using (SQLiteDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            id = reader.GetInt32(0);
                        }
                    }

                }



                conn.Close();
            }
            return id;
        }
    }
}
