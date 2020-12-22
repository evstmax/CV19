using System.Windows;
using CV19.Ifrastructure.Commands.Base;

namespace CV19.Ifrastructure.Commands
{
   internal class CloseApplicationCommand : Command
   {
       public override bool CanExecute(object parameter) => true;



       public override void Execute(object parameter) => Application.Current.Shutdown();

   }
}
