using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using DataRepository;
using DBManager.Infrastructure;
using DataViewModels;

namespace DBManager.ViewModel
{
    public class MainWindowViewModel: ViewModelBase
    {
        #region Data
        //collection with elements from data base
        IEnumerable<HeroDb> _heroes;
        public IEnumerable<HeroDb> Heroes 
        {
            get
            {
                if (_heroes == null)
                {
                    try
                    {
                        _heroes = HeroesRepository.Instance.GetAll();
                        Console.WriteLine(_heroes.Count());
                        OnPropertyChanged("Heroes");
                    }
                    catch (Exception e)
                    {
                            
                        Console.WriteLine(e.Message);
                    }
                    
                }
                return _heroes;
            }
            protected set{}
        }
        /*
        //об'єки для збереження поточного елемента
        protected ImageViewModel _selectedImage;
        public ImageViewModel SelectedImage
        {
            get
            {
                return _selectedImage;
            }
            set
            {
                _selectedImage = value;
                OnPropertyChanged("SelectedImage");
            }
        }*/
        #endregion

        #region Command
        /*
        #region NextImage
        RelayCommand _nextImageCommand;
        
        public ICommand NextImage
        {
            get
            {
                if (_nextImageCommand == null)
                {
                    _nextImageCommand = new RelayCommand(ExecuteNextImageCommand, CanExecuteNextImageCommand);
                }
                return _nextImageCommand;
            }
        }

        public void ExecuteNextImageCommand(object parameter)
        {
            if (SelectedImage.ImageId != Images.Count())
            {
                SelectedImage = Images.ElementAt(SelectedImage.ImageId);
            }
            else
            {
                SelectedImage = Images.First();
            }
        }

        public bool CanExecuteNextImageCommand(object parameter)
        {
            if (Images != null)
            {
                return true;
            }
            else return false;
        }
        #endregion

        #region PreviousImage
        RelayCommand _prevImageCommand;

        public ICommand PrevImage
        {
            get
            {
                if (_prevImageCommand == null)
                {
                    _prevImageCommand = new RelayCommand(ExecutePrevImageCommand, CanExecutePrevImageCommand);
                }
                return _prevImageCommand;
            }
        }

        public void ExecutePrevImageCommand(object parameter)
        {
            if (SelectedImage.ImageId != 1)
            {
                SelectedImage = Images.ElementAt(SelectedImage.ImageId - 2);
            }
            else
            {
                SelectedImage = Images.Last();
            }
            
        }

        public bool CanExecutePrevImageCommand(object parameter)
        {
            if (Images != null)
            {
                return true;
            }
            else return false;
        }
        #endregion
        */
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