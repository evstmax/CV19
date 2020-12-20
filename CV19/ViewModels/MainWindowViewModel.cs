using System.Windows;
using System.Windows.Input;
using CV19.Ifrastructure.Commands;
using CV19.ViewModels.Base;

namespace CV19.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
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
    }
}


}
