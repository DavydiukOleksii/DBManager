namespace DataViewModels
{
    public class HeroDb
    {
        public int Id { get; set; }
        public int StatsId { get; set; }

        public string Name { get; set; }
        public string Descriptions { get; set; }

        public string ImagePath { get; set; }

        public int Skill1Id { get; set; }
        public int Skill2Id { get; set; }
        public int Skill3Id { get; set; }
        public int Skill4Id { get; set; }
    }
}