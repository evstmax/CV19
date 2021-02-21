using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CV19.Ifrastructure.Commands;
using CV19.Models;
using CV19.Models.Decanat;
using CV19.ViewModels.Base;

namespace CV19.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        public object[] CompositeCollection { get; }

        #region SelectedCompositeValue : object - Выбранный непонятный элемент

        /// <summary>Выбранный непонятный элемент</summary>
        private object _SelectedCompositeValue;

        /// <summary>Выбранный непонятный элемент</summary>
        public object SelectedCompositeValue
        {
            get => _SelectedCompositeValue;
            set => Set(ref _SelectedCompositeValue, value);
        }

        #endregion

        #region SelectedGroup : Выбранная группа

        /// <summary>DESCRIPTION</summary>
        private Group _SelectedGroup;

        /// <summary>DESCRIPTION</summary>
        public Group SelectedGroup
        {
            get => _SelectedGroup;
            set => Set(ref _SelectedGroup, value);
        }

        #endregion

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
        public ObservableCollection<Group> Groups { get; }

        



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

        #region CreateGroupCommand
        public ICommand CreateGroupCommand { get; }

        private bool CanCreateGroupCommandExecute(object p) => true;

        private void OnCreateGroupCommandExecuted(object p)
        {
            var group_max_index = Groups.Count + 1; 
            var new_group = new Group()
            {
                Name = $"Группа {group_max_index}",
                Students = new ObservableCollection<Student>()
            };
            Groups.Add(new_group);
        }
        #endregion

        #region DeleteGroupCommand

        public ICommand DeleteGroupCommand { get; }

        private bool CanDeleteGroupCommandExecute(object p) => p is Group group && Groups.Contains(group);

        private void OnDeleteGroupCommandExecuted(object p)
        {
            if (!(p is Group group)) return;
            {
                var group_index = Groups.IndexOf(group);
                Groups.Remove(group);
                if (group_index < Groups.Count)
                    SelectedGroup = Groups[group_index];
                if (group_index == Groups.Count)
                    SelectedGroup = Groups[group_index-1];
            }
        }
        #endregion

        #endregion

        /* --------------------------------------------------------------------------------------------------*/

        public MainWindowViewModel()
        {
            #region Команды

            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            CreateGroupCommand = new LambdaCommand(OnCreateGroupCommandExecuted, CanCreateGroupCommandExecute);
            DeleteGroupCommand = new LambdaCommand(OnDeleteGroupCommandExecuted, CanDeleteGroupCommandExecute);

            #endregion


            var data_points = new List<DataPoint>((int)(360 / 0.1));
            for (var x = 0d; x <= 360; x += 0.1)
            {
                const double to_rad = Math.PI / 180;
                var y = Math.Exp(x * to_rad);
                data_points.Add(new DataPoint { XValue = x, YValue = y });
            }

            TestDataPoints = data_points;




            var student_index = 1;
            var students = Enumerable.Range(1, 10).Select(i => new Student
                {
                    Name = $"Name {student_index}",
                    Surname = $"Surname {student_index}",
                    Patronymic = $"Patronymic {student_index++}",
                    Birthday = DateTime.Now,
                    Rating = 0
                }
            );



            var groups = Enumerable.Range(1, 20).Select(i => new Group
            {
                Name = $"Группа {i}",
                Students = new ObservableCollection<Student>(students)
            });
            Groups = new ObservableCollection<Group>(groups);


            var data_list = new List<object>();

            data_list.Add("Hello Word!");
            data_list.Add(42);
            var group = Groups[1];
            data_list.Add(group);
            data_list.Add(group.Students[0]);

            CompositeCollection = data_list.ToArray();


        }

        /* --------------------------------------------------------------------------------------------------*/



    }


}
