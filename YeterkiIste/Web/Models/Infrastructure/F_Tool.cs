using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.Infrastructure
{
    public class F_Tool
    {
        public static string URLDuzelt(string psrtKelime)
        {
            //Bu metodumuzlada Türkçe karakterleri temizleyip ingilizceye uyarlıyoruz
            string Temp = psrtKelime;
            Temp = Temp.Replace(" ", "-"); Temp = Temp.Replace("ı", "i");
            Temp = Temp.Replace("ç", "c"); Temp = Temp.Replace("Ç", "C");
            Temp = Temp.Replace("ğ", "g"); Temp = Temp.Replace("Ğ", "G");
            Temp = Temp.Replace("ı", "i"); Temp = Temp.Replace("ö", "o");
            Temp = Temp.Replace("ş", "s"); Temp = Temp.Replace("ü", "u");
            Temp = Temp.Replace("Ç", "c"); Temp = Temp.Replace("Ğ", "g");
            Temp = Temp.Replace("İ", "i"); Temp = Temp.Replace("Ö", "o");
            Temp = Temp.Replace("Ş", "s"); Temp = Temp.Replace("Ü", "u");
            Temp = Temp.Replace("\"", ""); Temp = Temp.Replace("/", ""); Temp = Temp.Replace("|", "");
            Temp = Temp.Replace("(", ""); Temp = Temp.Replace(")", "");
            Temp = Temp.Replace("{", ""); Temp = Temp.Replace("}", "");
            Temp = Temp.Replace("%", ""); Temp = Temp.Replace("&", ""); Temp = Temp.Replace("~", "");
            Temp = Temp.Replace("+", ""); Temp = Temp.Replace(",", ""); Temp = Temp.Replace("`", "");
            Temp = Temp.Replace("?", ""); Temp = Temp.Replace(".", "-");
            Temp = Temp.Replace(" - ", ""); Temp = Temp.Replace(";", "");
            Temp = Temp.Replace("--", "-"); Temp = Temp.Replace("_-", "_");
            Temp = Temp.Replace("'", ""); Temp = Temp.Replace("<", "-"); Temp = Temp.Replace(">", "-");
            Temp = Temp.Replace("%20", "-"); Temp = Temp.Replace(":", "");
            Temp = Temp.Replace("%", "-"); Temp = Temp.Replace("!", "");

            return Temp;
        }

    }
}
