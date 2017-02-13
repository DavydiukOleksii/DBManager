using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBManager.ViewModel;

namespace DBManager.Model
{
    public class StatsModel:ViewModelBase
    {
        private int _id;
        public int Id {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            } }

        private int _attack;
        public int Attack
        {
            get { return _attack; }

            set
            {
                _attack = value;
                OnPropertyChanged("Attack");
            } }

        private int _amor;
        public int Armor
        {
            get { return _amor; }

            set
            {
                _amor = value;
                OnPropertyChanged("Armor");
            }
        }

        private int _health;
        public int Health
        {
            get { return _health; }

            set
            {
                _health = value;
                OnPropertyChanged("Health");
            }
        }

        private int _miss;
        public int Miss
        {
            get { return _miss; }

            set
            {
                _miss = value;
                OnPropertyChanged("Miss");
            }
        }

        public StatsModel()
        {
            Armor = 1;
            Attack = 1;
            Health = 1;
            Miss = 25;
        }

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
