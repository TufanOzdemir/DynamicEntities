using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicEntities.Entities
{
    public abstract class DynamicEntitiy
    {
        protected DynamicDictionary DynamicProperties { get; set; }
        protected Dictionary<string, string> ChangeKeyNames { get; set; }
        protected Dictionary<string, int> SortKeys { get; set; }

        public DynamicEntitiy()
        {
            DynamicProperties = new DynamicDictionary();
            ChangeKeyNames = new Dictionary<string, string>();
            SortKeys = new Dictionary<string, int>();
        }

        protected abstract object GetValue(string key);
        protected abstract void SetValue(string key, object model);
        public abstract DynamicDictionary GetDictionary();
        public abstract void SetDictionary(DynamicDictionary model);
        protected abstract string GetKeyNameProxy(string key);
        protected abstract DynamicDictionary Sort(DynamicDictionary model);
    }
}