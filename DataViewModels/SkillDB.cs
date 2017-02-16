namespace DataViewModels
{
    public class SkillDb
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Descriptions { get; set; }

        public int StatId { get; set; }

        public byte[] ImagePath { get; set; }
    }
}
