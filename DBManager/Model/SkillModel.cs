using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DBManager.Infrastructure;
using DBManager.ViewModel;

namespace DBManager.Model
{
    public class SkillModel: ViewModelBase
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
        public string Name
        {
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

        private byte[] _imagePath;
        public byte[] ImagePath
        {
            get { return _imagePath; }
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

        public SkillModel()
        {
            Name = "Skill name";
            Descriptions = "Skill descr";
            ImagePath = ImageConverter.GetImage(Path.GetFullPath("../../Resources/notFound.png"));

            Stats = new StatsModel();
        }

        public SkillModel(int id, string name, string desc, byte[] imagePath, StatsModel stat)
        {
            Id = id;
            Name = name;
            Descriptions = desc;
            ImagePath = imagePath;

            Stats = stat;
        }
    }
}
