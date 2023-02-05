using learnVocabulary.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace learnVocabulary.ViewModel
{
    public class MenuViewModel : BaseViewModel
    {
        #region commands
        public ICommand addVocabulary { get; set; }
        public ICommand define { get; set; }
        public ICommand learn { get; set; }
        #endregion
        public MenuViewModel()
        {
            addVocabulary = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) =>
            {
                FrameworkElement window = GetWindowParent(p);
                var w = window as Window;
                if (w != null)
                {
                    closeAllGrid(w);
                    Grid addVocab = w.FindName("addVocabulary") as Grid;
                        addVocab.Visibility = Visibility.Visible;
                }
            }
            );
            define = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) =>
            {
                FrameworkElement window = GetWindowParent(p);
                var w = window as Window;
                if (w != null)
                {
                    closeAllGrid(w);
                    Grid def = w.FindName("define") as Grid;
                        def.Visibility = Visibility.Visible;
                }
            }
            );
            learn = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) =>
            {
                FrameworkElement window = GetWindowParent(p);
                var w = window as Window;
                if (w != null)
                {
                    closeAllGrid(w);
                    Grid learnG = w.FindName("learn") as Grid;
                        learnG.Visibility = Visibility.Visible;
                }
            }
            );
        }
        private void closeAllGrid(Window w)
        {
            Grid addVocab = w.FindName("addVocabulary") as Grid;
            Grid def = w.FindName("define") as Grid;
            Grid learnG = w.FindName("learn") as Grid;
            addVocab.Visibility = Visibility.Collapsed;
            def.Visibility = Visibility.Collapsed;
            learnG.Visibility = Visibility.Collapsed;
        }
    }
}
