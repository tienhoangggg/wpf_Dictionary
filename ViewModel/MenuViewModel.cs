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
        public ICommand dictionary { get; set; }
        public ICommand learn { get; set; }
        public ICommand addUnitTopic { get; set; }
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
            dictionary = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) =>
            {
                FrameworkElement window = GetWindowParent(p);
                var w = window as Window;
                if (w != null)
                {
                    closeAllGrid(w);
                    Grid dic = w.FindName("dictionary") as Grid;
                        dic.Visibility = Visibility.Visible;
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
            addUnitTopic = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) =>
            {
                FrameworkElement window = GetWindowParent(p);
                var w = window as Window;
                if (w != null)
                {
                    closeAllGrid(w);
                    Grid addUnitTopicG = w.FindName("addUnitTopic") as Grid;
                        addUnitTopicG.Visibility = Visibility.Visible;
                }
            }
            );
        }
        private void closeAllGrid(Window w)
        {
            Grid addVocab = w.FindName("addVocabulary") as Grid;
            Grid addUnitTopicG = w.FindName("addUnitTopic") as Grid;
            Grid dic = w.FindName("dictionary") as Grid;
            Grid learnG = w.FindName("learn") as Grid;
            addVocab.Visibility = Visibility.Collapsed;
            dic.Visibility = Visibility.Collapsed;
            learnG.Visibility = Visibility.Collapsed;
            addUnitTopicG.Visibility = Visibility.Collapsed;
        }
    }
}
