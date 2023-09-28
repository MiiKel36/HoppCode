using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoppCode.Classes
{
    public abstract class JsonModifier
    {
        
        protected static CreateLocalStorageFolder json_path = new CreateLocalStorageFolder();
        //Armazena onde fica a pasta dos json
        protected static string jsonFolderPath = json_path.dirJsonFile;

    }
    class ClassesPage : JsonModifier
    {
        public  void JsonReadAndWrite(string num)
        {
            //Cria o objeto e puxa o PATH do json
            CreateLocalStorageFolder JSON_PATH = new CreateLocalStorageFolder();
            jsonFolderPath = JSON_PATH.dirJsonFile;

            //Transforma o json em ojeto
            string jsonFile = File.ReadAllText(jsonFolderPath);
            dynamic ObjJson = JsonConvert.DeserializeObject(jsonFile);

            //Muda o valor classe do objeto
            ObjJson.Classe = num;

            //Transforma em json denovo e escreve na pasta do json
            string ChangedJsonFile = JsonConvert.SerializeObject(ObjJson);
            File.WriteAllText(jsonFolderPath, ChangedJsonFile);
        }

        public  string jsonRead()
        {
            //Cria o objeto e puxa o PATH do json
            CreateLocalStorageFolder JSON_PATH = new CreateLocalStorageFolder();
            jsonFolderPath = JSON_PATH.dirJsonFile;

            //Transforma o json em objeto
            string jsonFile = File.ReadAllText(jsonFolderPath);
            dynamic ObjJson = JsonConvert.DeserializeObject(jsonFile);

            //Retorna o valor classe
            return ObjJson.Classe;
        }

        public  int GetNumOfJson()
        {
            //Provisiorio
            CreateLocalStorageFolder Path = new CreateLocalStorageFolder();

            //Caminho do json
            string pathJsonTeste = Path.dir + "teste.json";

            //Le o json e transforma em objeto
            string data = File.ReadAllText(pathJsonTeste);
            dynamic objJson = JsonConvert.DeserializeObject(data);

            //Trasnforma em JArray para contar quantos elementos possui
            var classsOrAulaLength = objJson.cSharp.classes as JArray;

            return classsOrAulaLength.Count();
        }
        public  string[] ArrayNames(int numOfButtons) 
        { 
            //Provisiorio
            CreateLocalStorageFolder Path = new CreateLocalStorageFolder();

            //Caminho do json
            string pathJsonTeste = Path.dir + "teste.json";

            //Le o json e transforma em objeto
            string data = File.ReadAllText(pathJsonTeste);
            dynamic objJson = JsonConvert.DeserializeObject(data);

            //Cria um array para armazenar os nomes
            string[] namesArray = new string[numOfButtons];

            //Pesquisa os nomes no json e coloca no array 
            for (int i = 0; i< numOfButtons; i++)
            {
                namesArray[i] = objJson.cSharp.classes[i].nome;
            }

            return namesArray;
        }


    }

    class aulasPage : JsonModifier
    {
        public  void JsonReadAndWrite(string num)
        {
            //Cria o objeto e puxa o PATH do json
            CreateLocalStorageFolder JSON_PATH = new CreateLocalStorageFolder();
            jsonFolderPath = JSON_PATH.dirJsonFile;

            //Transforma o json em ojeto
            string jsonFile = File.ReadAllText(jsonFolderPath);
            dynamic ObjJson = JsonConvert.DeserializeObject(jsonFile);

            //Muda o valor classe do objeto
            ObjJson.Aula = num;

            //Transforma em json denovo e escreve na pasta do json
            string ChangedJsonFile = JsonConvert.SerializeObject(ObjJson);
            File.WriteAllText(jsonFolderPath, ChangedJsonFile);
        }

        public  string jsonRead()
        {
            //Cria o objeto e puxa o PATH do json
            CreateLocalStorageFolder JSON_PATH = new CreateLocalStorageFolder();
            jsonFolderPath = JSON_PATH.dirJsonFile;

            //Transforma o json em objeto
            string jsonFile = File.ReadAllText(jsonFolderPath);
            dynamic ObjJson = JsonConvert.DeserializeObject(jsonFile);

            //Retorna o valor classe
            return ObjJson.Classe;
        }

        public  int GetNumOfJson(int classesId)
        {
            //Provisiorio
            CreateLocalStorageFolder Path = new CreateLocalStorageFolder();

            //Caminho do json
            string pathJsonTeste = Path.dir + "teste.json";

            //Le o json e transforma em objeto
            string data = File.ReadAllText(pathJsonTeste);
            dynamic objJson = JsonConvert.DeserializeObject(data);

            //Trasnforma em JArray para contar quantos elementos possui no botão selecionado na page ClassesPage
            var classsOrAulaLength = objJson.cSharp.classes[classesId].aulas as JArray;

            return classsOrAulaLength.Count();
        }
        public  string[] ArrayNames(int numOfButtons, int classId)
        {
            //Provisiorio
            CreateLocalStorageFolder Path = new CreateLocalStorageFolder();

            //Caminho do json
            string pathJsonTeste = Path.dir + "teste.json";

            //Le o json e transforma em objeto
            string data = File.ReadAllText(pathJsonTeste);
            dynamic objJson = JsonConvert.DeserializeObject(data);
            
            //Cria um array para armazenar os nomes dos botões
            string[] namesArray = new string[numOfButtons];

            for (int i = 0; i < numOfButtons; i++)
            {
                namesArray[i] = objJson.cSharp.classes[classId].aulas[i].aula;
            }

            return namesArray;
        }
    }

    class SubAulaPage : JsonModifier
    {
        public string[] jsonReturnSubAulasTexto(string classe, string aula)
        {
            //Provisiorio
            CreateLocalStorageFolder Path = new CreateLocalStorageFolder();

            //Caminho do json
            string pathJsonTeste = Path.dir + "teste.json";

            //Le o json e transforma em objeto
            string data = File.ReadAllText(pathJsonTeste);
            dynamic objJson = JsonConvert.DeserializeObject(data);
            
            //Quantas subAulas tem no json com os valores de classe e aula
            dynamic subAulasObj = objJson.cSharp.classes[Convert.ToInt32(classe)].aulas[Convert.ToInt32(aula)].subAulas as JArray;
            string[] jsonSubAula =  new string[subAulasObj.Count];
            
            //Coloca todos os textos em um array e o retorna
            for(int i = 0; i < subAulasObj.Count; i++)
            { jsonSubAula[i] = objJson.cSharp.classes[Convert.ToInt32(classe)].aulas[Convert.ToInt32(aula)].subAulas[i].texto; }
            
            return jsonSubAula;

        }
        public string[] jsonRead()
        {
            //Cria o objeto e puxa o PATH do json
            CreateLocalStorageFolder JSON_PATH = new CreateLocalStorageFolder();
            jsonFolderPath = JSON_PATH.dirJsonFile;

            //Transforma o json em objeto
            string jsonFile = File.ReadAllText(jsonFolderPath);
            dynamic ObjJson = JsonConvert.DeserializeObject(jsonFile);

            //Retorna o valor classe
            string[] ClasseEAula = { ObjJson.Classe, ObjJson.Aula };
            return ClasseEAula;
        }

    }

}

