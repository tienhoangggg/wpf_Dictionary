using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace learnVocabulary.ViewModel
{
    public class ControlBarViewModel : BaseViewModel
    {
        #region commands
        public ICommand CloseWindowCommand { get; set; }
        public ICommand MaximizeWindowCommand { get; set; }
        public ICommand MinimizeWindowCommand { get; set; }
        public ICommand MouseMoveWindowCommand { get; set; }
        public ICommand OpenMenuCommand { get; set; }
        #endregion

        public  ControlBarViewModel()
        {
            CloseWindowCommand = new RelayCommand<UserControl>((p)=> { return p == null? false : true; }, (p)=> {
                FrameworkElement window = GetWindowParent(p);
                var w = window as Window;
                if (w != null)
                {
                    w.Close();
                }
                }
            );
            MaximizeWindowCommand = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) =>
            {
                FrameworkElement window = GetWindowParent(p);
                var w = window as Window;
                if (w != null)
                {
                    if (w.WindowState != WindowState.Maximized)
                        w.WindowState = WindowState.Maximized;
                    else
                        w.WindowState = WindowState.Normal;
                }
            }
            );
            MinimizeWindowCommand = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) =>
            {
                FrameworkElement window = GetWindowParent(p);
                var w = window as Window;
                if (w != null)
                {
                    if (w.WindowState != WindowState.Minimized)
                        w.WindowState = WindowState.Minimized;
                    else
                        w.WindowState = WindowState.Maximized;
                }
            }
            );
            MouseMoveWindowCommand = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) =>
            {                
                FrameworkElement window = GetWindowParent(p);
                var w = window as Window;
                if (w != null)
                {
                    w.DragMove();
                }
            }
           );
            OpenMenuCommand = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) =>
            {
                FrameworkElement window = GetWindowParent(p);
                var w = window as Window;
                if (w != null)
                {
                    Grid menu = w.FindName("Menu") as Grid;
                    if (menu.Visibility == Visibility.Visible)
                        menu.Visibility = Visibility.Collapsed;
                    else
                        menu.Visibility = Visibility.Visible;
                }
            }
           );
        }
    }
}
