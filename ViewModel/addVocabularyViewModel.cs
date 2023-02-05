using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using learnVocabulary.model;

namespace learnVocabulary.ViewModel
{
    public class addVocabularyViewModel : BaseViewModel
    {
        #region command
        public ICommand newUnit { get; set; }
        public ICommand newTopic { get; set; }
        public ICommand send { get; set; }
        public ICommand refresh { get; set; }
        #endregion
        private firebase fb = firebase.getFirebase();
        #region property
        private ArrayList _units;
        private ArrayList _topics;
        private ArrayList _levels;
        private string _cbUnitItem;
        private string _txtUnitItem;
        private string _cbTopicItem;
        private string _txtTopicItem;
        private string _cbLevelItem;
        private string _txtVocabularyItem;
        private string _txtDefineItem;
        #endregion
        public ArrayList units
        {
            get { return _units; }
            set { _units = value; OnPropertyChanged(nameof(units)); }
        }
        public ArrayList topics
        {
            get { return _topics; }
            set { _topics = value; OnPropertyChanged(nameof(topics)); }
        }
        public ArrayList levels
        {
            get { return _levels; }
            set { _levels = value; OnPropertyChanged(nameof(levels)); }
        }
        public string cbUnitItem
        {
            get { return _cbUnitItem; }
            set { _cbUnitItem = value; OnPropertyChanged(nameof(cbUnitItem)); topics = fb.getTopics(cbUnitItem); }
        }
        public string txtUnitItem
        {
            get { return _txtUnitItem; }
            set { _txtUnitItem = value; OnPropertyChanged(nameof(txtUnitItem)); }
        }
        public string cbTopicItem
        {
            get { return _cbTopicItem; }
            set { _cbTopicItem = value; OnPropertyChanged(nameof(cbTopicItem)); }
        }
        public string txtTopicItem
        {
            get { return _txtTopicItem; }
            set { _txtTopicItem = value; OnPropertyChanged(nameof(txtTopicItem)); }
        }
        public string cbLevelItem
        {
            get { return _cbLevelItem; }
            set { _cbLevelItem = value; OnPropertyChanged(nameof(cbLevelItem)); }
        }
        public string txtVocabularyItem
        {
            get { return _txtVocabularyItem; }
            set { _txtVocabularyItem = value; OnPropertyChanged(nameof(txtVocabularyItem)); }
        }
        public string txtDefineItem
        {
            get { return _txtDefineItem; }
            set { _txtDefineItem = value; OnPropertyChanged(nameof(txtDefineItem)); }
        }
        public addVocabularyViewModel()
        {
            newUnit = new RelayCommand<object>((p) => { return true; }, (p) => { newUnitCommand(p as UserControl); });
            newTopic = new RelayCommand<object>((p) => { return true; }, (p) => { newTopicCommand(p as UserControl); });
            send = new RelayCommand<object>((p) => { return true; }, (p) => { sendCommand(p as UserControl); });
            refresh = new RelayCommand<object>((p) => { return true; }, (p) => { refreshCommand(); });
            units = fb.getUnits();
            levels = new ArrayList() { "A1", "A2", "B1", "B2", "C1", "C2" };
        }
        private void newUnitCommand(UserControl w)
        {
            if(w!=null)
            {
                ComboBox cbUnit = w.FindName("cbUnit") as ComboBox;
                TextBox txtUnit = w.FindName("txtUnit") as TextBox;
                ComboBox cbTopic = w.FindName("cbTopic") as ComboBox;
                TextBox txtTopic = w.FindName("txtTopic") as TextBox;
                if (cbUnit.Visibility == Visibility.Visible)
                {
                    cbUnit.Visibility = Visibility.Collapsed;
                    txtUnit.Visibility = Visibility.Visible;
                    cbTopic.Visibility = Visibility.Collapsed;
                    txtTopic.Visibility = Visibility.Visible;
                }
                else
                {
                    cbUnit.Visibility = Visibility.Visible;
                    txtUnit.Visibility = Visibility.Collapsed;
                }
            }
        }
        private void newTopicCommand(UserControl w)
        {
            if (w != null)
            {
                ComboBox cbTopic = w.FindName("cbTopic") as ComboBox;
                TextBox txtTopic = w.FindName("txtTopic") as TextBox;
                if (cbTopic.Visibility == Visibility.Visible)
                {
                    cbTopic.Visibility = Visibility.Collapsed;
                    txtTopic.Visibility = Visibility.Visible;
                }
                else
                {
                    cbTopic.Visibility = Visibility.Visible;
                    txtTopic.Visibility = Visibility.Collapsed;
                }
            }
        }
        private async void sendCommand(UserControl w)
        {
            ComboBox cbUnit = w.FindName("cbUnit") as ComboBox;
            ComboBox cbTopic = w.FindName("cbTopic") as ComboBox;
            string unit, topic;
            if(cbUnit.Visibility == Visibility.Visible)
            {
                unit = cbUnitItem;
            }
            else
            {
                unit = txtUnitItem;
            }
            if(cbTopic.Visibility == Visibility.Visible)
            {
                topic = cbTopicItem;
            }
            else
            {
                topic = txtTopicItem;
            }
            if (unit != null && topic != null && cbLevelItem != null && txtVocabularyItem != null && txtDefineItem != null && unit != "" && topic != "" && cbLevelItem != "" && txtVocabularyItem != "" && txtDefineItem != "")
            {
                if (cbUnit.Visibility == Visibility.Collapsed || cbTopic.Visibility == Visibility.Collapsed)
                {
                    fb.addUnitTopic(unit, topic);
                }
                await fb.addVocabulary(unit, topic, txtVocabularyItem, cbLevelItem, txtDefineItem);
                refreshCommand();
            }
        }
        private void refreshCommand()
        {
            cbUnitItem = txtUnitItem = cbTopicItem = txtTopicItem = cbLevelItem = txtVocabularyItem = txtDefineItem = null;
            units = fb.getUnits();
        }
    }
}
