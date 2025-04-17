using ETicaretAPI.Infrastructure;
using ETicaretAPI.Infrastructure.Operations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Services
{
    public class FileService
    {
        readonly IWebHostEnvironment _environment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _environment = webHostEnvironment;
        }

        public async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
            try
            {
                using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);

                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        async Task<string> FileRenameAsync(string path, string fileName)
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

        public async Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(_environment.WebRootPath, path);
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            List<(string fileName, string path)> datas = new();
            List<bool> results = new();
            foreach (IFormFile file in files)
            {
                string fileNewName = await FileRenameAsync(uploadPath,file.FileName);

                bool result = await CopyFileAsync($"{uploadPath}\\{fileNewName}",file);
                datas.Add((fileNewName, $"{path}\\{fileNewName}"));
                results.Add(result);
            }

            if (results.TrueForAll(r => r.Equals(true)))
                return datas;
            return null;
        }
    }
}
