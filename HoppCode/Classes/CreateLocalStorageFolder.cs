using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration;

namespace HoppCode.Classes
{
    public class CreateLocalStorageFolder
    {

        public async Task<string> PushAulaJson()
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("JsonFile/aulasJson.json");
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        public async Task<string> ReturnTheChangePagePath()
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("JsonFile/changePage.json");
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }




    }


}
