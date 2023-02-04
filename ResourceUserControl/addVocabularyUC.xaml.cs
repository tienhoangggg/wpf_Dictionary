﻿using System;
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
using learnVocabulary.ViewModel;

namespace learnVocabulary.ResourceUserControl
{
    /// <summary>
    /// Interaction logic for addVocabulary.xaml
    /// </summary>
    public partial class addVocabularyUC : UserControl
    {
        public addVocabularyViewModel Viewmodel { get; set; }
        public addVocabularyUC()
        {
            InitializeComponent();
            this.DataContext = Viewmodel = new addVocabularyViewModel();
        }
    }
}