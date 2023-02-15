using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RadencyService.Lib.File
{
    public  class Json
    {
         public static void jsonSerializer<T>(string pathDirectory, string nameFile, T results)
        {
            var directory = Directory.CreateDirectory($"{pathDirectory}\\{DateTime.Now.ToString("d")}");
            using (FileStream fs = new FileStream($"{directory.FullName}\\{nameFile}.json", FileMode.Create))
            {
                JsonSerializer.Serialize<T>(fs, results);
            }
        }
    }
}
