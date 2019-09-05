using DynamicEntities.Entities;
using DynamicEntities.Entities.StaticEntities;
using DynamicEntities.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DynamicEntities.Test
{
    [TestClass]
    public class ApiHelperTest
    {
        [TestMethod]
        public void ListApiTest()
        {
            var result = ApiHelper.GetData<List<DynamicDictionary>>("https://ezanvakti.herokuapp.com/ulkeler").Result;
        }

        [TestMethod]
        public void ModelApiTest()
        {
            var result = ApiHelper.GetData<DynamicDictionary>("https://ezanvakti.herokuapp.com/bayram?ilce=9541").Result;
        }

        [TestMethod]
        public void StaticListModelApiTest()
        {
            var resultList = new List<Test1Entity>();
            var result = ApiHelper.GetData<List<DynamicDictionary>>("https://ezanvakti.herokuapp.com/vakitler?ilce=9541").Result;
            foreach (var item in result)
            {
                var model = new Test1Entity();
                model.SetDictionary(item);
                resultList.Add(model);
            }
        }
    }
}
