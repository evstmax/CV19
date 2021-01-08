using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using CV19.Ifrastructure.Commands;
using CV19.Models;
using CV19.ViewModels.Base;

namespace CV19.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
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
        private string _Title = "Анализ статистики CV19" ;
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


        public MainWindowViewModel()
        {
            #region Команды

        CloseApplicationCommand  = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);

        #endregion


            var data_points = new List<DataPoint>((int)(360 / 0.1));
        for (var x = 0d; x <= 360; x += 0.1)
        {
            const double to_rad = Math.PI / 180;
            var y = Math.Cos(x * to_rad);
           data_points.Add(new DataPoint {XValue = x, YValue = y}); 
        }

        TestDataPoints = data_points;
        }
}


}
