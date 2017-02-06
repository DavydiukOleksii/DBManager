using System;
using System.Collections.Generic;
using DataRepository;
using DataViewModels;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<StatsDb> r = StatsRepository.Instance.GetAll();

            for (int i = 0; i < r.Count; i++)
            {
                Console.WriteLine("id = " + r[i].Id + "  attack = " + r[i].Attack + "  armor = " + r[i].Armor + "  health = " + r[i].Health + "  miss = " + r[i].Miss);
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            List<SkillDb> sk = SkillsRepository.Instance.GetAll();

            for (int i = 0; i < sk.Count; i++)
            {
                Console.WriteLine("id = " + sk[i].Id + "  name = " + sk[i].Name + "  descr = " + sk[i].Descriptions + "  imagepath = " + sk[i].ImagePath + "  statsid = " + sk[i].StatId);
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            List<HeroDb> h = HeroesRepository.Instance.GetAll();

            for (int i = 0; i < h.Count; i++)
            {
                Console.WriteLine("id = " + h[i].Id + ",  name = " + h[i].Name + ",  descr = " + h[i].Descriptions + ",  imagepath = " + h[i].ImagePath + ",  statsid = " + h[i].StatsId + ", sk1 = " + h[i].Skill1Id + ", sk2 = " + h[i].Skill2Id + ", sk3 = " + h[i].Skill3Id + ", sk4 = " + h[i].Skill4Id);
            }

            Console.ReadLine();
        }
    }
}
