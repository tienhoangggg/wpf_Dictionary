using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Google.Api.Gax.Grpc;
using learnVocabulary.model;

namespace learnVocabulary.ViewModel
{
    public class defineViewModel : BaseViewModel
    {
        #region command
        public ICommand btFind { get; set; }
        public ICommand btAdd { get; set; }
        public ICommand btRemove { get; set; }
        #endregion
        #region property
        private string _txtFind;
        private string _cbLevelItem;
        private string _txtDefine;
        private string _txtShow;
        #endregion
        public string txtFind
        {
            get => _txtFind;
            set
            {
                _txtFind = value;
                OnPropertyChanged(nameof(txtFind));
            }
        }
        firebase fb = firebase.getFirebase();
        public class partDetailVocab
        {
            public string level { get; set; }
            public string define { get; set; }
            public string index { get; set; }
        }
        public List<partDetailVocab> detailVocab { get; set; }
        public List<string> levels { get; set; }
        public string cbLevelItem
        {
            get => _cbLevelItem;
            set
            {
                _cbLevelItem = value;
                OnPropertyChanged(nameof(cbLevelItem));
            }
        }
        public string txtDefine
        {
            get => _txtDefine;
            set
            {
                _txtDefine = value;
                OnPropertyChanged(nameof(txtDefine));
            }
        }
        public string txtShow
        {
            get => _txtShow;
            set
            {
                _txtShow = value;
                OnPropertyChanged(nameof(txtShow));
            }
        }
        public defineViewModel()
        {
            btFind = new RelayCommand<object>((p) => { return true; }, (p) => { showDetailVocab(); });
            btAdd = new RelayCommand<object>((p) => { return true; }, (p) => { addVocab(); });
            btRemove = new RelayCommand<object>((p) => { return true; }, (p) => { removeCommand(p as string); });
            levels = new List<string>() {"A1", "A2", "B1", "B2", "C1", "C2"};
        }
        public Task removeCommand(string index)
        {
            return Task.Run(() =>
            {
                fb.removeVocab(txtShow, index);
                detailVocab.Where(x => x.index == index).ToList().ForEach(x => detailVocab.Remove(x));
                List<partDetailVocab> temp = detailVocab;
                detailVocab = new List<partDetailVocab>();
                foreach(var item in temp)
                {
                    detailVocab.Add(item);
                }
                OnPropertyChanged(nameof(detailVocab));
            });
        }
        public Task showDetailVocab()
        {
            return Task.Run(() => { 
            Dictionary<string, Dictionary<string, string>> temp = fb.getDetailVocabulary(txtFind);
            detailVocab = new List<partDetailVocab>();
            foreach (var item in temp)
            {
                detailVocab.Add(new partDetailVocab() { level = item.Value["level"], define = item.Value["define"], index = item.Key });
            }
            txtShow = txtFind;
            OnPropertyChanged(nameof(detailVocab));
            });
        }
        public async void addVocab()
        {
            string temp =await fb.addVocabulary("", "", txtShow, cbLevelItem, txtDefine);
            List<partDetailVocab> tempV = detailVocab;
            tempV.Add(new partDetailVocab() { level = cbLevelItem, define = txtDefine, index = temp });
            cbLevelItem = txtDefine = null;
            detailVocab = new List<partDetailVocab>();
            foreach (var item in tempV)
            {
                detailVocab.Add(item);
            }
            OnPropertyChanged(nameof(detailVocab));
        }
    }
}
