using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        #region ChooseImage
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
                SelectedHeroes.ImagePath = filename;
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
                HeroesManager.DeleteHero(SelectedHeroes.Id);

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
            //if (_heroes.Count != 0)
            //{
            //    HeroesManager.DeleteHero(SelectedHeroes.Id);

            //    int delIndex = 0;

            //    foreach (var hero in _heroes)
            //    {
            //        if (hero.Id == SelectedHeroes.Id)
            //        {
            //            delIndex = _heroes.IndexOf(hero);
            //        }
            //    }

            //    _heroes.RemoveAt(delIndex);

            //    if (_heroes.Count != 0)
            //        SelectedHeroes = _heroes.First();
            //}

            HeroesManager.ChangeHero(SelectedHeroes);
            //OnPropertyChanged("Heroes");
            //OnPropertyChanged("SelectedHeroes");
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
                SelectedHeroes.ImagePath = "../Resources/imageNotFound.png";
                SelectedHeroes.Stats = new StatsModel();
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
                SelectedHeroes.Skills[int.Parse(parameter.ToString())].ImagePath = "../Resources/imageNotFound.png";

                SelectedHeroes.Skills[int.Parse(parameter.ToString())].Stats = new StatsModel();

            }



            OnPropertyChanged("SelectedHeroes");
        }

        public bool CanExecuteClearSkillCommand(object parameter)
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