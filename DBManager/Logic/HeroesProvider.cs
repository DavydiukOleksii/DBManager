using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManager.Model;
using DataRepository;
using DataViewModels;

namespace DBManager.Logic
{
    public static class HeroesProvider
    {
        public static List<HeroModel> GetAll()
        {
            List<HeroModel> resoultList = new List<HeroModel>();

            foreach (var hero in HeroesRepository.Instance.GetAll())
            {
                resoultList.Add(GetHeroById(hero.Id));
            }

            return resoultList;
        }

        public static HeroModel GetHeroById(int id)
        {
            HeroDb tmpHero = HeroesRepository.Instance.GetById(id);
            HeroModel resoult = new HeroModel(tmpHero.Id, tmpHero.Name, tmpHero.Descriptions, tmpHero.ImagePath);

            StatsDb tmpStat = StatsRepository.Instance.GetById(tmpHero.StatsId);
            resoult.Stats = new StatsModel(tmpStat.Id, tmpStat.Attack, tmpStat.Armor, tmpStat.Armor, tmpStat.Miss);

            resoult.Skills = new List<SkillModel>();

            //add sk1
            SkillDb tmpSkill = SkillsRepository.Instance.GetById(tmpHero.Skill1Id);
            tmpStat = StatsRepository.Instance.GetById(tmpSkill.StatId);

            resoult.Skills.Add(new SkillModel(tmpSkill.Id, tmpSkill.Name, tmpSkill.Descriptions, tmpSkill.ImagePath, new StatsModel(tmpStat.Id, tmpStat.Attack, tmpStat.Armor, tmpStat.Armor, tmpStat.Miss)));

            //add sk2
            tmpSkill = SkillsRepository.Instance.GetById(tmpHero.Skill2Id);
            tmpStat = StatsRepository.Instance.GetById(tmpSkill.StatId);

            resoult.Skills.Add(new SkillModel(tmpSkill.Id, tmpSkill.Name, tmpSkill.Descriptions, tmpSkill.ImagePath, new StatsModel(tmpStat.Id, tmpStat.Attack, tmpStat.Armor, tmpStat.Armor, tmpStat.Miss)));
            
            //add sk3
            tmpSkill = SkillsRepository.Instance.GetById(tmpHero.Skill3Id);
            tmpStat = StatsRepository.Instance.GetById(tmpSkill.StatId);

            resoult.Skills.Add(new SkillModel(tmpSkill.Id, tmpSkill.Name, tmpSkill.Descriptions, tmpSkill.ImagePath, new StatsModel(tmpStat.Id, tmpStat.Attack, tmpStat.Armor, tmpStat.Armor, tmpStat.Miss)));
            
            //add sk4
            tmpSkill = SkillsRepository.Instance.GetById(tmpHero.Skill4Id);
            tmpStat = StatsRepository.Instance.GetById(tmpSkill.StatId);

            resoult.Skills.Add(new SkillModel(tmpSkill.Id, tmpSkill.Name, tmpSkill.Descriptions, tmpSkill.ImagePath, new StatsModel(tmpStat.Id, tmpStat.Attack, tmpStat.Armor, tmpStat.Armor, tmpStat.Miss)));

            return resoult;
        }
    }
}
