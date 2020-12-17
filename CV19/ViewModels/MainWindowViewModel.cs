using CV19.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CV19.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Заголовок окна
        private string _Title = "Анализ статистики" ;
        /// <summary>
        /// Заголовок окна
        /// </summary>
        public string Title
        {
            get => _Title;
            //set
            //{
            //    //if (Equals(_Title, value)) return; // но у нас есть метод Set
            //    //_Title = value;
            //    //OnPropertyChanged();

            //    //Set(ref _Title, value); // и можно написать так 
            //}
            set => Set(ref _Title, value);
        }
        #endregion


    }
}
