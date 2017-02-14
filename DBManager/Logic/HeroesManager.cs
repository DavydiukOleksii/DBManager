using System.Collections.Generic;
using System.Collections.ObjectModel;
using DataRepository;
using DataViewModels;
using DBManager.Model;

namespace DBManager.Logic
{
    public static class HeroesManager
    {
        public static ObservableCollection<HeroModel> GetAll()
        {
            ObservableCollection<HeroModel> resoultList = new ObservableCollection<HeroModel>();

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

        public static List<int> AddNewHero(HeroModel newHero)
        {
            List<int> idList = new List<int>();

            HeroDb tmpHero = new HeroDb();
            tmpHero.Name = newHero.Name;
            tmpHero.Descriptions = newHero.Descriptions;
            tmpHero.ImagePath = newHero.ImagePath;

            List<SkillDb> tmpSkillsList = new List<SkillDb>();

            for (int i = 0; i < newHero.Skills.Count; i++)
            {
                SkillDb tmpSkillDb = new SkillDb();

                StatsDb tmpStatsDb = new StatsDb();
                tmpStatsDb.Armor = newHero.Skills[i].Stats.Armor;
                tmpStatsDb.Attack = newHero.Skills[i].Stats.Attack;
                tmpStatsDb.Health = newHero.Skills[i].Stats.Health;
                tmpStatsDb.Miss = newHero.Skills[i].Stats.Miss;

                tmpSkillDb.StatId = StatsRepository.Instance.Insert(tmpStatsDb);

                tmpSkillDb.Name = newHero.Skills[i].Name;
                tmpSkillDb.Descriptions = newHero.Skills[i].Descriptions;
                tmpSkillDb.ImagePath = newHero.Skills[i].ImagePath;

                tmpSkillsList.Add(tmpSkillDb);

                idList.Add(tmpSkillDb.StatId);
            }

            tmpHero.Skill1Id = SkillsRepository.Instance.Insert(tmpSkillsList[0]);
            tmpHero.Skill2Id = SkillsRepository.Instance.Insert(tmpSkillsList[1]);
            tmpHero.Skill3Id = SkillsRepository.Instance.Insert(tmpSkillsList[2]);
            tmpHero.Skill4Id = SkillsRepository.Instance.Insert(tmpSkillsList[3]);

            StatsDb tmpHeroStat = new StatsDb();
            tmpHeroStat.Armor = newHero.Stats.Armor;
            tmpHeroStat.Attack = newHero.Stats.Attack;
            tmpHeroStat.Health = newHero.Stats.Health;
            tmpHeroStat.Miss = newHero.Stats.Miss;

            tmpHero.StatsId = StatsRepository.Instance.Insert(tmpHeroStat);
            tmpHero.Id = HeroesRepository.Instance.Insert(tmpHero);

            idList.AddRange(new int[] 
                { 
                    tmpHero.Id,
                    tmpHero.StatsId,
                    tmpHero.Skill1Id,
                    tmpHero.Skill2Id,
                    tmpHero.Skill3Id,
                    tmpHero.Skill4Id
                });

            return idList;
        }

        public static void ChangeHero(HeroModel changeHero)
        {
            HeroDb tmpHero = new HeroDb
            {
                Name = changeHero.Name,
                Descriptions = changeHero.Descriptions,
                ImagePath = changeHero.ImagePath,
                Id = changeHero.Id
            };

            StatsDb tmpHeroStat = new StatsDb
            {
                Armor = changeHero.Stats.Armor,
                Attack = changeHero.Stats.Attack,
                Health = changeHero.Stats.Health,
                Miss = changeHero.Stats.Miss,
                Id = changeHero.Stats.Id
            };

            StatsRepository.Instance.Update(tmpHeroStat);
            HeroesRepository.Instance.Update(tmpHero);
        }

        public static void DeleteHero(HeroModel delHero)
        {
            StatsRepository.Instance.Delete(delHero.Stats.Id);

            for (int i = 0; i < delHero.Skills.Count; i++)
            {
                StatsRepository.Instance.Delete(delHero.Skills[i].Stats.Id);
                SkillsRepository.Instance.Delete(delHero.Skills[i].Id);
            }

            HeroesRepository.Instance.Delete(delHero.Id);
        }

        public static void ChangeSkill(SkillModel changSkill)
        {
            SkillDb tmpSkill = new SkillDb
            {
                Name = changSkill.Name,
                Descriptions = changSkill.Descriptions,
                ImagePath = changSkill.ImagePath,
                Id = changSkill.Id
            };

            StatsDb tmpHeroStat = new StatsDb
            {
                Armor = changSkill.Stats.Armor,
                Attack = changSkill.Stats.Attack,
                Health = changSkill.Stats.Health,
                Miss = changSkill.Stats.Miss,
                Id = changSkill.Stats.Id
            };

            StatsRepository.Instance.Update(tmpHeroStat);
            SkillsRepository.Instance.Update(tmpSkill);
        }
    }
}
