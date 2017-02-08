using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBManager.Model
{
    public class SkillModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Descriptions { get; set; }
        public string ImagePath { get; set; }

        public StatsModel Stats { get; set; }

        public SkillModel(int id, string name, string desc, string imagePath, StatsModel stat)
        {
            Id = id;
            Name = name;
            Descriptions = desc;
            ImagePath = imagePath;

            Stats = stat;
        }
    }
}
