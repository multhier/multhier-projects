using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace Web.Helper
{
    public static class TempDataExtension
    {
        public static void Show<T>(this ITempDataDictionary tempData, string key, T value) where T:class
        {
            tempData[key] = JsonConvert.SerializeObject(value);
        }

        public static T Get<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            object o;
            tempData.TryGetValue(key, out o);
            return o == null ? null : JsonConvert.DeserializeObject<T>((string)o);
        }

//Başka tipte bir sınıfı atmak isteyebiliriz, o yüzden Generic yaparız. public static void Metot&lt;T> ()
//Extension metotların ilk parametresi this olur.Neyi extend yapıyorsak onu yazarız (ThisExpressionSyntax ITempDataDictionart) 
//TempDatanın implemente ettiği sınıftır.

        //ikinci alanımız Mesaj alanı olucak yani string key diyebiliriz
        //sonra T sınıf türü bir değer olucak.
        //En sonunda bir kısıtlama vermek istersek where T class diyebiliriz (zorunlu değildir.)
        //tempDatanın Key içerisine serialize etmemiz gerekir. JsonConvert deriz. (Core da direk eklenebiliyor)

        //TempData içerisine attığımız tipi geri çevirmeliyiz
        //object tanımlayalım ve temdatanın keyine göre value almaya çalışalım.
        //    Return diyerek obje nul ise null değil ise desirialize edeceğiz.


    }
}

