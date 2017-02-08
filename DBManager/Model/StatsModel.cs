using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBManager.Model
{
    public class StatsModel
    {
        public int Id { get; set; }

        public int Attack { get; set; }
        public int Armor { get; set; }
        public int Health { get; set; }
        public int Miss { get; set; }

        public StatsModel() { }

        public StatsModel(int id, int attack, int armor, int health, int miss)
        {
            Id = id;
            Armor = armor;
            Attack = attack;
            Health = health;
            Miss = miss;
        }
    }
}
