using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
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
        #endregion
        #region base variable
        private ArrayList _units;
        private ArrayList _topics;
        private string _cbUnitItem;
        private string _cbTopicItem;
        public class partDetailVocab
        {
            public string level { get; set; }
            public string define { get; set; }
        }
        public class vocab
        {
            public string vocabulary { get; set; }
        }
        public ObservableCollection<partDetailVocab> detailVocab { get; set; }
        public ObservableCollection<vocab> vocabularies { get; set; }
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
        public learnViewModel()
        {
            units = fb.getUnits();
            showDetail = new RelayCommand<object>((p) => { return true; }, (p) => { showDetailVocab(p as string); });
            btRem = new RelayCommand<object>((p) => { return true; }, (p) => {  });
        }
        public void showDetailVocab(string p)
        {
            Dictionary<string, Dictionary<string, string>> temp = fb.getDetailVocabulary(p);
            detailVocab = new ObservableCollection<partDetailVocab>();
            foreach (var item in temp)
            {
                detailVocab.Add(new partDetailVocab() { level = item.Value["level"], define = item.Value["define"] });
            }
            OnPropertyChanged(nameof(detailVocab));
        }
        private void changedVocabularies()
        {
            Dictionary<string, string> temp = fb.getVocabularys(cbUnitItem, cbTopicItem);
            vocabularies = new ObservableCollection<vocab>();
            foreach (var item in temp)
            {
                vocabularies.Add(new vocab() { vocabulary = item.Key });
            }
            OnPropertyChanged(nameof(vocabularies));
        }
    }
}
