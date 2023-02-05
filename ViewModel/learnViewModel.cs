using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using learnVocabulary.model;

namespace learnVocabulary.ViewModel
{
    public class learnViewModel : BaseViewModel
    {
        firebase fb = firebase.getFirebase();
        #region command
        public ICommand showDetail { get; set; }
        public ICommand btRem { get; set; }
        public ICommand btPre { get; set; }
        public ICommand btShow { get; set; }
        public ICommand btNext { get; set; }
        #endregion
        #region property
        private ArrayList _units;
        private ArrayList _topics;
        private string _cbUnitItem;
        private string _cbTopicItem;
        private string _txtCur;
        public class partDetailVocab
        {
            public string level { get; set; }
            public string define { get; set; }
        }
        public class vocab
        {
            public string vocabulary { get; set; }
        }
        public List<partDetailVocab> detailVocab { get; set; }
        public List<vocab> vocabularies { get; set; }
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
        public string cbUnitItem
        {
            get { return _cbUnitItem; }
            set { _cbUnitItem = value; OnPropertyChanged(nameof(cbUnitItem)); topics = fb.getTopics(cbUnitItem); }
        }
        public string cbTopicItem
        {
            get { return _cbTopicItem; }
            set 
            {
                _cbTopicItem = value; OnPropertyChanged(nameof(cbTopicItem));
                changedVocabularies();
            }
        }
        private Dictionary<string, string> dicVocabs;
        public string txtCur
        {
            get { return _txtCur; }
            set { _txtCur = value; OnPropertyChanged(nameof(txtCur)); }
        }
        private Random rd = new Random();
        public learnViewModel()
        {
            units = fb.getUnits();
            showDetail = new RelayCommand<object>((p) => { return true; }, (p) => { showDetailVocab(p as string); });
            btRem = new RelayCommand<object>((p) => { return true; }, (p) => { btRemCommand(p as UserControl); });
            btPre = new RelayCommand<object>((p) => { return true; }, (p) => { btPreCommand(p as UserControl); });
            btNext = new RelayCommand<object>((p) => { return true; }, (p) => { btNextCommand(p as UserControl); });
            btShow = new RelayCommand<object>((p) => { return true; }, (p) => { btShowCommand(p as UserControl); });
        }
        public void btPreCommand(UserControl p)
        {
            if (vocabularies.Count > 0)
            {
                int index = vocabularies.FindIndex(x => x.vocabulary == txtCur);
                if (index > 0)
                {
                    txtCur = vocabularies[index - 1].vocabulary;
                }
                else
                {
                    txtCur = vocabularies[vocabularies.Count - 1].vocabulary;
                }
                ItemsControl bg = p.FindName("controlBG") as ItemsControl;
                bg.Visibility = Visibility.Collapsed;
            }
        }
        public void btNextCommand(UserControl p)
        {
            if(vocabularies.Count>0)
            {
                int index = vocabularies.FindIndex(x => x.vocabulary == txtCur);
                if (index < vocabularies.Count - 1)
                {
                    txtCur = vocabularies[index + 1].vocabulary;
                }
                else
                {
                    txtCur = vocabularies[0].vocabulary;
                }
                ItemsControl bg = p.FindName("controlBG") as ItemsControl;
                bg.Visibility = Visibility.Collapsed;
            }
        }
        public void btShowCommand(UserControl p)
        {
            if (txtCur != null && txtCur != "")
            {
                ItemsControl bg = p.FindName("controlBG") as ItemsControl;
                if (bg.Visibility == Visibility.Collapsed)
                {
                    bg.Visibility = Visibility.Visible;
                    showDetailVocab(txtCur);
                }
                else
                {
                    bg.Visibility = Visibility.Collapsed;
                }
            }
        }
        public void btRemCommand(UserControl p)
        {
            ItemsControl ct1 = p.FindName("control1") as ItemsControl;
            Grid ct2 = p.FindName("control2") as Grid;
            ItemsControl bg = p.FindName("controlBG") as ItemsControl;
            if (ct1.Visibility==Visibility.Visible)
            {
                ct1.Visibility = Visibility.Collapsed;
                ct2.Visibility = Visibility.Visible;
                bg.Visibility = Visibility.Collapsed;
            }
            else
            {
                ct1.Visibility = Visibility.Visible;
                ct2.Visibility = Visibility.Collapsed;
                bg.Visibility = Visibility.Visible;
            }
            
        }
        public Task showDetailVocab(string p)
        {
            return Task.Run(() =>
            {
                Dictionary<string, Dictionary<string, string>> temp = fb.getDetailVocabulary(p);
                detailVocab = new List<partDetailVocab>();
                foreach (var item in temp)
                {
                    if (item.Key != dicVocabs[p])
                    { detailVocab.Add(new partDetailVocab() { level = item.Value["level"], define = item.Value["define"] }); }
                    else
                    {
                        detailVocab.Insert(0, new partDetailVocab() { level = item.Value["level"], define = item.Value["define"] });
                    }
                }
                OnPropertyChanged(nameof(detailVocab));
            });
        }
        private Task changedVocabularies()
        {
            return Task.Run(() =>
            {
                dicVocabs = fb.getVocabularys(cbUnitItem, cbTopicItem);
                vocabularies = new List<vocab>();
                foreach (var item in dicVocabs)
                {
                    vocabularies.Insert(rd.Next(0, vocabularies.Count), new vocab() { vocabulary = item.Key });
                }
                txtCur = vocabularies[0].vocabulary;
                OnPropertyChanged(nameof(vocabularies));
            });
        }
    }
}
