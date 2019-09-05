using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicEntities.Entities.StaticEntities
{
    public class Test1Entity : DynamicEntitiy
    {
        public string Gunes { get { return base.DynamicProperties["Gunes"].ToString(); } }
        public string Aksam { get { return base.DynamicProperties["Aksam"].ToString(); } }
        public string Imsak { get { return base.DynamicProperties["Imsak"].ToString(); } }
        public string KibleSaati { get { return base.DynamicProperties["KibleSaati"].ToString(); } }

        public Test1Entity()
        {
            ChangeKeyNames.Add("KibleSaati", "Kible");
            ChangeKeyNames.Add("MiladiTarihUzunIso8601", "MiladiUzunZaman");

            SortKeys.Add("Gunes", 0);
            SortKeys.Add("Ogle", 1);
            SortKeys.Add("Ikindi", 2);
            SortKeys.Add("Aksam", 3);
            SortKeys.Add("Yatsi", 4);
            SortKeys.Add("Imsak", 5);
        }

        private object IsExist(string key)
        {
            DynamicProperties.TryGetValue(key, out var result);
            return result;
        }

        /// <summary>
        /// Eğer kural tanımlanmışsa ona göre düzelterek ver. Tanımlanmamışsa direkt listeden ver.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected override object GetValue(string key)
        {
            object result;
            var property = this.GetType().GetProperty(key);
            if (property == null)
            {
                result = base.DynamicProperties[key];
            }
            else
            {
                result = property.GetValue(this, null);
            }
            return result;
        }

        protected override void SetValue(string key, object model)
        {
            var propertyInfo = this.GetType().GetProperty(key);
            propertyInfo.SetValue(this, Convert.ChangeType(model, propertyInfo.PropertyType), null);
        }

        /// <summary>
        /// Proxy ile mevcut dictionary i istediğimiz kurallarca veriyoruz.
        /// </summary>
        /// <returns></returns>
        public override DynamicDictionary GetDictionary()
        {
            var proxyDictionary = new DynamicDictionary();
            foreach (var item in DynamicProperties)
            {
                proxyDictionary.Add(GetKeyNameProxy(item.Key), GetValue(item.Key));
            }

            return Sort(proxyDictionary);
        }

        /// <summary>
        /// Dictionary i ilk atama için beslemeye yarar.
        /// </summary>
        /// <param name="model"></param>
        public override void SetDictionary(DynamicDictionary model)
        {
            DynamicProperties = model;
        }

        protected override string GetKeyNameProxy(string key)
        {
            ChangeKeyNames.TryGetValue(key, out var result);
            return string.IsNullOrWhiteSpace(result) ? key : result;
        }

        protected override DynamicDictionary Sort(DynamicDictionary proxyDictionary)
        {
            var newProxyDictionary = new DynamicDictionary();
            var sortedList = (from entry in SortKeys
                              orderby entry.Value ascending
                              select entry).ToDictionary(c => c.Key, t => t.Value);

            foreach (var item in sortedList.Keys)
            {
                var checkItem = proxyDictionary.TryGetValue(item, out var value);
                if (checkItem)
                {
                    newProxyDictionary.Add(item, value);
                    proxyDictionary.Remove(item);
                }
            }

            foreach (var item in proxyDictionary)
            {
                newProxyDictionary.Add(item.Key, item.Value);
            }

            return newProxyDictionary;
        }
    }
}
