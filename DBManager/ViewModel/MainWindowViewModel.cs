﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using DataRepository;
using DBManager.Infrastructure;
using DataViewModels;
using DBManager.Logic;
using DBManager.Model;

namespace DBManager.ViewModel
{
    public class MainWindowViewModel: ViewModelBase
    {
        #region Data
        //collection with elements from data base
        ObservableCollection<HeroModel> _heroes;
        public IEnumerable<HeroModel> Heroes 
        {
            get
            {
                if (_heroes == null)
                {
                    _heroes = HeroesManager.GetAll();
                    _selectedHeroes = _heroes.First();   
                    OnPropertyChanged("Heroes");
                }
                return _heroes;
            }
            protected set{}
        }

        private HeroModel _selectedHeroes;

        public HeroModel SelectedHeroes
        {
            get
            {   return _selectedHeroes; }

            set
            {
                _selectedHeroes = value;
                OnPropertyChanged("SelectedHeroes");
            }
        }
        #endregion

        #region Command

        #region ChooseHeroImage
        RelayCommand _chooseImageCommamd;

        public ICommand ChooseImage
        {
            get
            {
                if (_chooseImageCommamd == null)
                {
                    _chooseImageCommamd = new RelayCommand(ExecuteChooseImageCommand, CanExecuteChooseImageCommand);
                }
                return _chooseImageCommamd;
            }
        }

        public void ExecuteChooseImageCommand(object parameter)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".png";
            dlg.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                SelectedHeroes.ImagePath = ImageConverter.GetImage(filename);
                OnPropertyChanged("SelectedHeroes");
                OnPropertyChanged("ImagePath");
            }
        }

        public bool CanExecuteChooseImageCommand(object parameter)
        {
            return true;
        }
        #endregion

        #region AddNewHero
        RelayCommand _addNewHeroCommand;

        public ICommand AddNewHero
        {
            get
            {
                if (_addNewHeroCommand == null)
                {
                    _addNewHeroCommand = new RelayCommand(ExecuteAddNewHehoCommand, CanExecuteAddNewHeroCommand);
                }
                return _addNewHeroCommand;
            }
        }

        public void ExecuteAddNewHehoCommand(object parameter)
        {
            HeroModel newModel = new HeroModel();
            List<int> idList = HeroesManager.AddNewHero(newModel);

            //change id from db

            for (int i = 0; i < newModel.Skills.Count; i++)
            {
                newModel.Skills[i].Stats.Id = idList[i];
            }
            newModel.Stats.Id = idList[5];
            newModel.Id = idList[4];
            for (int i = 0; i < newModel.Skills.Count; i++)
            {
                newModel.Skills[i].Id = idList[i + 6];
            }

            _heroes.Add(newModel);
            SelectedHeroes = newModel;

            OnPropertyChanged("Heroes");
            OnPropertyChanged("SelectedHeroes");
        }

        public bool CanExecuteAddNewHeroCommand(object parameter)
        {
            return true;
        }
        #endregion

        #region DeleteHero
        RelayCommand _deleteHeroCommand;

        public ICommand DeleteHero
        {
            get
            {
                if (_deleteHeroCommand == null)
                {
                    _deleteHeroCommand = new RelayCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
                }
                return _deleteHeroCommand;
            }
        }

        public void ExecuteDeleteCommand(object parameter)
        {
            if (_heroes.Count != 0)
            {
                HeroesManager.DeleteHero(SelectedHeroes);

                int delIndex = 0;

                foreach (var hero in _heroes)
                {
                    if (hero.Id == SelectedHeroes.Id)
                    {
                        delIndex = _heroes.IndexOf(hero);
                    }
                }

                _heroes.RemoveAt(delIndex);

                if (_heroes.Count != 0)
                    SelectedHeroes = _heroes.First();
            }
            OnPropertyChanged("Heroes");
            OnPropertyChanged("SelectedHeroes");
        }

        public bool CanExecuteDeleteCommand(object parameter)
        {
            return true;
        }
        #endregion

        #region SaveHero
        RelayCommand _saveHeroCommand;

        public ICommand SaveHero
        {
            get
            {
                if (_saveHeroCommand == null)
                {
                    _saveHeroCommand = new RelayCommand(ExecuteSaveHeroCommand, CanExecuteSaveHeroCommand);
                }
                return _saveHeroCommand;
            }
        }

        public void ExecuteSaveHeroCommand(object parameter)
        {
            HeroesManager.ChangeHero(SelectedHeroes);
            OnPropertyChanged("Heroes");
            OnPropertyChanged("SelectedHeroes");
        }

        public bool CanExecuteSaveHeroCommand(object parameter)
        {
            return true;
        }
        #endregion

        #region ClearHero
        RelayCommand _clearHeroCommand;

        public ICommand ClearHero
        {
            get
            {
                if (_clearHeroCommand == null)
                {
                    _clearHeroCommand = new RelayCommand(ExecuteClearHeroCommand, CanExecuteClearHeroCommand);
                }
                return _clearHeroCommand;
            }
        }

        public void ExecuteClearHeroCommand(object parameter)
        {
            if (_heroes.Count != 0)
            {
                SelectedHeroes.Name = "Meme name.";
                SelectedHeroes.Descriptions = "Meme descriptions.";
                SelectedHeroes.ImagePath = ImageConverter.GetImage(Path.GetFullPath("../../Resources/notFound.png"));
                SelectedHeroes.Stats = new StatsModel(SelectedHeroes.Stats.Id);
            }

            //OnPropertyChanged("Heroes");

            //HeroesManager.ChangeHero(SelectedHeroes);
            OnPropertyChanged("SelectedHeroes");
        }

        public bool CanExecuteClearHeroCommand(object parameter)
        {
            return true;
        }
        #endregion

        #region ClearSkill
        RelayCommand _clearSkillCommand;

        public ICommand ClearSkill
        {
            get
            {
                if (_clearSkillCommand == null)
                {
                    _clearSkillCommand = new RelayCommand(ExecuteClearSkillCommand, CanExecuteClearSkillCommand);
                }
                return _clearSkillCommand;
            }
        }

        public void ExecuteClearSkillCommand(object parameter)
        {
            

            if (_heroes.Count != 0)
            {
                SelectedHeroes.Skills[int.Parse(parameter.ToString())].Name = "Skill name.";
                SelectedHeroes.Skills[int.Parse(parameter.ToString())].Descriptions = "Skill descriptions.";
                SelectedHeroes.Skills[int.Parse(parameter.ToString())].ImagePath = ImageConverter.GetImage("../Resources/imageNotFound.png");

                SelectedHeroes.Skills[int.Parse(parameter.ToString())].Stats = new StatsModel(SelectedHeroes.Skills[int.Parse(parameter.ToString())].Stats.Id);

            }



            OnPropertyChanged("SelectedHeroes");
        }

        public bool CanExecuteClearSkillCommand(object parameter)
        {
            return true;
        }
        #endregion

        #region SaveSkill
        RelayCommand _saveSkillCommand;

        public ICommand SaveSkill
        {
            get
            {
                if (_saveSkillCommand == null)
                {
                    _saveSkillCommand = new RelayCommand(ExecuteSaveSkillCommand, CanExecuteSaveSkillCommand);
                }
                return _saveSkillCommand;
            }
        }

        public void ExecuteSaveSkillCommand(object parameter)
        {
            HeroesManager.ChangeSkill(SelectedHeroes.Skills[int.Parse(parameter.ToString())]);
            OnPropertyChanged("Heroes");
            OnPropertyChanged("SelectedHeroes");
        }

        public bool CanExecuteSaveSkillCommand(object parameter)
        {
            return true;
        }
        #endregion

        #region ChooseSkillImage
        RelayCommand _chooseSkillImageCommamd;

        public ICommand ChooseSkillImage
        {
            get
            {
                if (_chooseSkillImageCommamd == null)
                {
                    _chooseSkillImageCommamd = new RelayCommand(ExecuteChooseSkillImageCommand, CanExecuteChooseSkillImageCommand);
                }
                return _chooseSkillImageCommamd;
            }
        }

        public void ExecuteChooseSkillImageCommand(object parameter)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".png";
            dlg.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                SelectedHeroes.Skills[int.Parse(parameter.ToString())].ImagePath = ImageConverter.GetImage(filename);
                OnPropertyChanged("SelectedHeroes");
                OnPropertyChanged("ImagePath");
            }
        }

        public bool CanExecuteChooseSkillImageCommand(object parameter)
        {
            return true;
        }
        #endregion
        
        #endregion

        #region FreeData
        protected override void OnDispose() 
        {
            //delete collection
            Heroes = Heroes.Except(Heroes);
        }
        #endregion

    }
}