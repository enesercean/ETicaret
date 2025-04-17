using ETicaretAPI.Infrastructure.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Services.Storage
{
    public class Storage
    {
        protected async Task<string> FileRenameAsync(string path, string fileName)
        {
            string extension = Path.GetExtension(fileName);
            string oldName = Path.GetFileNameWithoutExtension(fileName);
            string regulatedFileName = NameOperation.CharacterRegulatory(oldName);
            string newFileName = $"{regulatedFileName}{extension}";

            string fullPath = Path.Combine(path, newFileName);
            int iteration = 1;
            while (File.Exists(fullPath))
            {
                newFileName = $"{regulatedFileName}-{iteration}{extension}";
                fullPath = Path.Combine(path, newFileName);
                iteration++;
            }

            return newFileName;
        }
    }
}
