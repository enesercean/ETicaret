using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Operations
{
    public static class NameOperation
    {
        public static string CharacterRegulatory(string name)
        {
            return name.Replace("\"", "")
                .Replace("!", "")
                .Replace("'", "")
                .Replace("^", "")
                .Replace("+", "")
                .Replace("%", "")
                .Replace("&", "")
                .Replace("/", "")
                .Replace("(", "")
                .Replace(")", "")
                .Replace("=", "")
                .Replace("?", "")
                .Replace("_", "")
                .Replace("@", "")
                .Replace("Ö", "o")
                .Replace("ö", "o")
                .Replace("Ü", "u")
                .Replace("ü", "u")
                .Replace("İ", "i")
                .Replace("ı", "i")
                .Replace("Ğ", "g")
                .Replace("ğ", "g")
                .Replace("Ş", "s")
                .Replace("ş", "s")
                .Replace("Ç", "c")
                .Replace("ç", "c")
                .Replace(" ", "-");
        }
    }
}
