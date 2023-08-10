using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Maui.Controls;

namespace HoppCode.Classes
{
    public class CreateLocalStorageFolder
    {
        //Nome do usuario
        static string User = Environment.UserName;

        //Diretorio da pasta e do arquivo json
        string dir = $"C:\\Users\\{User}\\HoppCode\\JsonFiles\\";
        public string dirJsonFile = $"C:\\Users\\{User}\\HoppCode\\JsonFiles\\changePage.json";

        public void CreateStorage()
        {         
            //Se a pasta HoppCode não existe
            if (!Directory.Exists(dir))
            {
                //Cria o diretorio
                Directory.CreateDirectory(dir);
                
                //Transforma a calasse JsonFile em objetor e deixa seus valores para null
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
