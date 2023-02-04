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
                    db.Collection(unit).Document(topic).SetAsync(new Dictionary<string, string>());

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
        public void addVocabulary(string unit, string topic, string vocab, string level, string define)
        {
                Dictionary<string, string> vocabularys = getVocabularys(unit, topic);
                Dictionary<string, Dictionary<string, string>> detailVocabulary = getDetailVocabulary(vocab);
                Dictionary<string, string> detail = new Dictionary<string, string>();
                detail.Add("level", level);
                detail.Add("define", define);
                vocabularys.Add(vocab, detailVocabulary.Count.ToString());
                db.Collection(unit).Document(topic).SetAsync(vocabularys);
                detailVocabulary.Add(detailVocabulary.Count.ToString(), detail);
                db.Collection("#######").Document(vocab).SetAsync(detailVocabulary);
        }
    }
}