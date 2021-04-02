using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Navigation;
using CV19.Ifrastructure.Commands;
using CV19.Models.Decanat;
using CV19.ViewModels.Base;
using OxyPlot;
using DataPoint = CV19.Models.DataPoint;

namespace CV19.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {


        //#region SelectedGroup : Выбранная группа

        ///// <summary>DESCRIPTION</summary>
        //private Group _SelectedGroup;

        ///// <summary>DESCRIPTION</summary>
        //public Group SelectedGroup
        //{
        //    get => _SelectedGroup;
        //    set
        //    {
        //        if(!Set(ref _SelectedGroup, value)) return;
        //        _SelectedGroupStudents.Source = value?.Students;
        //        OnPropertyChanged(nameof(SelectedGroupStudents));
        //    }
        //}

        //#endregion

        #region StudentFilterText : string - Текст фильтра студентов

        /// <summary>Текст фильтра студентов</summary>
        private string _StudentFilterText;
         /// <summary>Текст фильтра студентов</summary>
        public string StudentFilterText
        {
            get => _StudentFilterText;
            set
            {
                if (!Set(ref _StudentFilterText, value)) return;
                _SelectedGroupStudents.View.Refresh();

            }
        }

        #endregion


        private readonly CollectionViewSource _SelectedGroupStudents = new CollectionViewSource();


        private void OnStudentFiltred(object Sender, FilterEventArgs E)
        {
            if (!(E.Item is Student student))
            {
                E.Accepted = false;
                return;
                
            }
            if(string.IsNullOrWhiteSpace(_StudentFilterText))
                return;
            if (student.Name is null || student.Surname is null || student.Patronymic is null)
            {
                E.Accepted = false;
                return;
            }

            if(student.Name.Contains(_StudentFilterText, StringComparison.OrdinalIgnoreCase)) return;
            if(student.Surname.Contains(_StudentFilterText, StringComparison.OrdinalIgnoreCase)) return;
            if(student.Patronymic.Contains(_StudentFilterText, StringComparison.OrdinalIgnoreCase)) return;
            E.Accepted = false;
        }
            


        public ICollectionView SelectedGroupStudents => _SelectedGroupStudents?.View;
        
        






        #region TestDataPoints : IEnumerable<DataPoint> - Тестовый набор данных для визуализации графиков

        /// <summary>Тестовый набор данных для визуализации графиков</summary>
        private IEnumerable<DataPoint> _TestDataPoints;

        /// <summary>Тестовый набор данных для визуализации графиков</summary>
        public IEnumerable<DataPoint> TestDataPoints
        {
            get => _TestDataPoints;
            set => Set(ref _TestDataPoints, value);
        }
        #endregion

        

        
        #region Заголовок окна
        private string _Title = "Анализ статистики CV19";
        /// <summary>
        /// Title : string - Заголовок окна
        /// </summary>
        public string Title
        {
            get => _Title;
            //set
            //{
            //    if (Equals(_Title, value)) return ; // но у нас есть метод Set
            //    _Title = value;
            //    OnPropertyChanged(); // вызываем метод класса ИЗДАТЕЛЯ  и он в свою очередь через событие дергает подписчиков

            //    Set(ref _Title, value); // и можно написать так 
            //}
            set => Set(ref _Title, value);
        }
        #endregion

        #region Статус
        private string _Status = "Готово";
        /// <summary>
        /// Status : string - Статус
        /// </summary>
        public string Status
        {
            get => _Status;

            set => Set(ref _Status, value);
        }
        #endregion

        public IEnumerable<Student> TestStudents => Enumerable.Range(1, App.InDesignMode ? 10 :10000)
            .Select(i => new Student()
            {
                Name = $"Имя_{i}",
                Surname = $"Фамилия_{i}"
            });

     

        /* --------------------------------------------------------------------------------------------------*/


        #region Команды

        #region CloseApplicationCommand
        public ICommand CloseApplicationCommand { get; } // команда

        private bool CanCloseApplicationCommandExecute(object p) => true; //Может Ли Команда Закрытия Приложения Выполняться

        private void OnCloseApplicationCommandExecuted(object p)  //При Закрытии Приложения Выполняется Команда
        {
            Application.Current.Shutdown();
        }

        #endregion

  

        #endregion

        /* --------------------------------------------------------------------------------------------------*/

        public MainWindowViewModel()
        {
            #region Команды

            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);


            #endregion


            var data_points = new List<DataPoint>((int)(360 / 0.1));
            for (var x = 0d; x <= 360; x += 0.1)
            {
                const double to_rad = Math.PI / 180;
                var y = Math.Exp(x * to_rad);
                data_points.Add(new DataPoint { XValue = x, YValue = y });
            }

            TestDataPoints = data_points;




           

        }

        /* --------------------------------------------------------------------------------------------------*/



    }


}
