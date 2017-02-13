using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManager.ViewModel;

namespace DBManager.Model
{
    public class HeroModel: ViewModelBase
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        private string _name;
        public string Name {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        private string _descriptions;
        public string Descriptions
        {
            get { return _descriptions; }
            set
            {
                _descriptions = value;
                OnPropertyChanged("Descriptions");
            }
        }

        private string _imagePath;
        public string ImagePath 
        { 
            get{return _imagePath;}
            set { _imagePath = value; OnPropertyChanged("ImagePath"); }
        }

        private StatsModel _stats;
        public StatsModel Stats
        {
            get { return _stats; }
            set
            {
                _stats = value;
                OnPropertyChanged("Stats");
            }
        }

        private List<SkillModel> _skills; 
        public List<SkillModel> Skills {
            get { return _skills; }
            set
            {
                _skills = value;
                OnPropertyChanged("Skills");
            }
        }

        public HeroModel()
        {
            Name = "Meme name";
            Descriptions = "Meme descriptions.";
            ImagePath = "../Resources/imageNotFound.png";

            Stats = new StatsModel();

            Skills = new List<SkillModel>();

            Skills.Add(new SkillModel());
            Skills.Add(new SkillModel());
            Skills.Add(new SkillModel());
            Skills.Add(new SkillModel());
        }

        public HeroModel(int id, string name, string descriptions, string imagePath)
        {
            Name = name;
            Descriptions = descriptions;
            Id = id;
            ImagePath = "../Resources/" + imagePath + ".jpg";
        }
    }
}
