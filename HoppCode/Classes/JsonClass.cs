using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoppCode.Classes
{
    class JsonModifier
    {
        string jsonPath;
      
        public void JsonReadAndWriteAulas(string num)
        {
            //Cria o objeto e puxa o PATH do json
            CreateLocalStorageFolder JSON_PATH = new CreateLocalStorageFolder();
            jsonPath = JSON_PATH.dirJsonFile;

            //Transforma o json em ojeto
            string jsonFile = File.ReadAllText(jsonPath);
            dynamic ObjJson = JsonConvert.DeserializeObject(jsonFile);

            //Muda o valor classe do objeto
            ObjJson.Classe = num;

            //Transforma em json denovo e escreve na pasta do json
            string ChangedJsonFile = JsonConvert.SerializeObject(ObjJson);
            File.WriteAllText(jsonPath, ChangedJsonFile);

        }

        public string jsonReadAulas()
        {
            //Cria o objeto e puxa o PATH do json
            CreateLocalStorageFolder JSON_PATH = new CreateLocalStorageFolder();
            jsonPath = JSON_PATH.dirJsonFile;

            //Transforma o json em objeto
            string jsonFile = File.ReadAllText(jsonPath);
            dynamic ObjJson = JsonConvert.DeserializeObject(jsonFile);

            //Retorna o valor classe
            return ObjJson.Classe;
        }
    }
}

