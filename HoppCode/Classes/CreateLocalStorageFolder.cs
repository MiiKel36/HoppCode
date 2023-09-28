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

        //Nome do usuario
        static string User = Environment.UserName;


#if WINDOWS
        public string dir = $"C:\\Users\\{User}\\HoppCode\\JsonFiles\\";
        public string dirJsonFile = $"C:\\Users\\{User}\\HoppCode\\JsonFiles\\changePage.json";
#elif ANDROID
        public string dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "HoppCode","JsonFile");
        public string dirJsonFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "HoppCode","JsonFile","changePage.json");
#endif

        //Diretorio da pasta e do arquivo json versão windows  
        public void CreateStorage()
        {
                //Se a pasta HoppCode NÂO existe
                if (!Directory.Exists(dir))
                {
                    //Cria o diretorio
                    Directory.CreateDirectory(dir);

                    //Transforma a classe JsonFile em objeto e deixa seus valores para null
                    JsonFile file = new JsonFile();
                    file.Classe = null;
                    file.Aula = null;

                    //Transforma em json e escreve no path do json
                    string json = JsonConvert.SerializeObject(file);
                    File.WriteAllText(dirJsonFile, json);
                }           
        }
    }
    public  class JsonFile
    {
        //Qual classe foi selecionada na tela principal
        public string Classe { get; set; }

        //Qual aula foi clicada na tela de aulas
        public string Aula { get; set; } 

    }

}
