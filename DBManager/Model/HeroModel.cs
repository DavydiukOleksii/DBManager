using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBManager.Model
{
    public class HeroModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Descriptions { get; set; }
        public string ImagePath { get; set; }

        public StatsModel Stats { get; set; }
        public List<SkillModel> Skills { get; set; }

        public HeroModel() { }

        public HeroModel(int id, string name, string descriptions, string imagePath)
        {
            Name = name;
            Descriptions = descriptions;
            Id = id;
            ImagePath = "../Resources/" + imagePath + ".jpg";
        }
    }
}
