using System;
using learnVocabulary.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace learnVocabulary.ResourceUserControl
{
    /// <summary>
    /// Interaction logic for MenuUC.xaml
    /// </summary>
    public partial class MenuUC : UserControl
    {
        public MenuViewModel Viewmodel { get; set; }
        public MenuUC()
        {
            InitializeComponent();
            this.DataContext = Viewmodel = new MenuViewModel();
        }
    }
}
