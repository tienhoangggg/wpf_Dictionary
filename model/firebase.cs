using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using Google.Cloud.Firestore;

namespace learnVocabulary.model
{
    public class firebase
    {
        private string path = AppDomain.CurrentDomain.BaseDirectory + @"learnvocabulary-74389.json";
        private FirestoreDb db;
        private static firebase instance = null;
        private firebase()
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
            db = FirestoreDb.Create("learnvocabulary-74389");
        }
        public static firebase getFirebase()
        {
            if (instance == null)
            {
                instance = new firebase();
            }
            return instance;
        }
        public ArrayList getUnits()
        {
            ArrayList units = new ArrayList();
            db.ListRootCollectionsAsync().ToListAsync().Result.ForEach((collection) =>
            {
                if(collection.Id!="#######")
                    units.Add(collection.Id);
            });
            return units;
        }
        public ArrayList getTopics(string unit)
        {
            ArrayList topics = new ArrayList();
            if (unit != null)
            {
                db.Collection(unit).ListDocumentsAsync().ToListAsync().Result.ForEach((document) =>
                {
                    topics.Add(document.Id);
                });
            }
            return topics;
        }
        public void addUnitTopic(string unit, string topic)
        {
            db.Collection(unit).Document(topic).SetAsync(new Dictionary<string, string>()).Wait();

        }
        public Dictionary<string,string> getVocabularys(string unit, string topic)
        {
                if(unit != null && unit != "" && topic != null && topic != "")
                    return db.Collection(unit).Document(topic).GetSnapshotAsync().Result.ConvertTo<Dictionary<string, string>>();
            return new Dictionary<string, string>();
        }
        public Dictionary<string,Dictionary<string,string>> getDetailVocabulary(string vocabulary)
        {
            DocumentSnapshot doc = db.Collection("#######").Document(vocabulary).GetSnapshotAsync().Result;
            if (doc.Exists)
                return doc.ConvertTo<Dictionary<string, Dictionary<string, string>>>();
                return new Dictionary<string, Dictionary<string, string>>();

        }
        public Task<string> addVocabulary(string unit, string topic, string vocab, string level, string define)
        {
            Task<string> t = new Task<string>(() =>
            {
                Dictionary<string, string> vocabularys = getVocabularys(unit, topic);
                Dictionary<string, Dictionary<string, string>> detailVocabulary = getDetailVocabulary(vocab);
                Dictionary<string, string> detail = new Dictionary<string, string>();
                detail.Add("level", level);
                detail.Add("define", define);
                string index;
                if (unit != "" && topic != "")
                {
                    vocabularys.Add(vocab, unit + "#######" + topic);
                    db.Collection(unit).Document(topic).SetAsync(vocabularys);
                    detailVocabulary.Add(unit + "#######" + topic, detail);
                    index = unit + "#######" + topic;
                }
                else
                {
                    index = "#######" + DateTime.UtcNow.ToString();
                    detailVocabulary.Add(index, detail);
                }
                db.Collection("#######").Document(vocab).SetAsync(detailVocabulary);
                return index;
            });
            t.Start();
            return t;
        }
        public Task removeVocab(string vocab, string index)
        {
            Task t = Task.Run(() =>
            {
                Dictionary<string, Dictionary<string, string>> detailVocabulary = getDetailVocabulary(vocab);
                detailVocabulary.Remove(index);
                db.Collection("#######").Document(vocab).SetAsync(detailVocabulary);
                if (index.Substring(0, 7) != "#######")
                {
                    string[] unitTopic = index.Split(new string[] { "#######" }, StringSplitOptions.None);
                    Dictionary<string, string> vocabularys = getVocabularys(unitTopic[0], unitTopic[1]);
                    vocabularys.Remove(vocab);
                    db.Collection(unitTopic[0]).Document(unitTopic[1]).SetAsync(vocabularys);
                }
            });
            return t;
        }
    }
}