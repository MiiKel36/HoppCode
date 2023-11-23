using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HoppCode.Classes
{
    public abstract class JsonModifier
    {
        
        protected static CreateLocalStorageFolder json_path = new CreateLocalStorageFolder();
        //Armazena onde fica a pasta dos json
        protected static string jsonFolderPath = json_path.dirJsonFile;

    }
    class JsonClassesPage : JsonModifier
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

        public string jsonRead()
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

        public async Task<int> ReturnNumOfClasses()
        {
            //Provisiorio
            CreateLocalStorageFolder Path = new CreateLocalStorageFolder();

            //Caminho do json
            string JsonAulas = await Path.PushAulaJson();

            //Le o json e transforma em objeto
            //string data = File.ReadAllText(JsonAulas);
            dynamic objJson = JsonConvert.DeserializeObject(JsonAulas);

            //Trasnforma em JArray para contar quantos elementos possui
            var classsOrAulaLength = objJson.cSharp.classes as JArray;

            return classsOrAulaLength.Count();
        }
        public  async Task<string[]> ArrayClassesNames(int numOfButtons) 
        { 
            //Provisiorio
            CreateLocalStorageFolder Path = new CreateLocalStorageFolder();

            //Caminho do json
            string JsonAulas = await Path.PushAulaJson();

            //Le o json e transforma em objeto
            //string data = File.ReadAllText(JsonAulas);
            dynamic objJson = JsonConvert.DeserializeObject(JsonAulas);

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

        public  string JsonReadReturnClasse()
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

        public async Task<int> ReturnNumOfAulas(int classesId)
        {
            //Provisiorio
            CreateLocalStorageFolder Path = new CreateLocalStorageFolder();

            //Caminho do json
            string JsonAulas = await Path.PushAulaJson();

            //Le o json e transforma em objeto
            //string data = File.ReadAllText(JsonAulas);
            dynamic objJson = JsonConvert.DeserializeObject(JsonAulas);

            //Trasnforma em JArray para contar quantos elementos possui no botão selecionado na page ClassesPage
            var classsOrAulaLength = objJson.cSharp.classes[classesId].aulas as JArray;

            return classsOrAulaLength.Count();
        }
        public async Task<string[]> ArrayNames(int numOfButtons, int classId)
        {
            //Provisiorio
            CreateLocalStorageFolder Path = new CreateLocalStorageFolder();

            //Caminho do json
            string JsonAulas = await Path.PushAulaJson();

            //Le o json e transforma em objeto
            //string data = File.ReadAllText(JsonAulas);
            dynamic objJson = JsonConvert.DeserializeObject(JsonAulas);
            
            //Cria um array para armazenar os nomes dos botões
            string[] namesArray = new string[numOfButtons];

            for (int i = 0; i < numOfButtons; i++)
            {
                namesArray[i] = objJson.cSharp.classes[classId].aulas[i].nomeAulaOuExercicio;
            }

            return namesArray;
        }

        
    }

    class SubAulaPage : JsonModifier
    {
        public void WriteJson(string valueToModfy)
        {
            //Cria o objeto e puxa o PATH do json
            CreateLocalStorageFolder Path = new CreateLocalStorageFolder();
            jsonFolderPath = Path.dirJsonFile;

            //Transforma o json em objeto
            string jsonFile = File.ReadAllText(jsonFolderPath);
            dynamic ObjJson = JsonConvert.DeserializeObject(jsonFile);

            ObjJson.Aula = valueToModfy;

            //Transforma em json denovo e escreve na pasta do json
            string ChangedJsonFile = JsonConvert.SerializeObject(ObjJson);
            File.WriteAllText(jsonFolderPath, ChangedJsonFile);
        }
        public async Task<string[]> JsonReturnSubAulasTexto(string classe, string aula)
        {
            //Provisiorio
            CreateLocalStorageFolder Path = new CreateLocalStorageFolder();

            //Caminho do json
            string JsonAulas = await Path.PushAulaJson();

            //Le o json e transforma em objeto
            //string data = File.ReadAllText(JsonAulas);
            dynamic objJson = JsonConvert.DeserializeObject(JsonAulas);
            
            //Quantas subAulas tem no json com os valores de classe e aula
            dynamic subAulasObj = objJson.cSharp.classes[Convert.ToInt32(classe)].aulas[Convert.ToInt32(aula)].subAulas as JArray;
            string[] jsonSubAula =  new string[subAulasObj.Count];
            
            //Coloca todos os textos em um array e o retorna
            for(int i = 0; i < subAulasObj.Count; i++)
            { jsonSubAula[i] = objJson.cSharp.classes[Convert.ToInt32(classe)].aulas[Convert.ToInt32(aula)].subAulas[i].texto; }
            
            return jsonSubAula;

        }
        public string[] JsonReadReturnClasseAula()
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

    class IdentificarAulaOuExercicioPage : JsonModifier
    {
        public async Task<string> JsonReturnExercicioTexto()
        {
            //Provisiorio
            CreateLocalStorageFolder Path = new CreateLocalStorageFolder();

            //Classe que possui a função qu le o json e retorna a classe e a aula
            IdentificarAulaOuExercicioPage identificarAulaOuExercicio = new IdentificarAulaOuExercicioPage();

            //Contem o valor "Classe" e "Aulas" do json
            string[] ClasseAula = identificarAulaOuExercicio.JsonReadReturnClasseAula();

            //Converte os valores Aula e classe para int
            int classe = Convert.ToInt32(ClasseAula[0]);
            int aula = Convert.ToInt32(ClasseAula[1]);


            //Caminho do json
            string JsonAulas = await Path.PushAulaJson();

            //Le o json e transforma em objeto
            //string data = File.ReadAllText(JsonAulas);
            dynamic objJson = JsonConvert.DeserializeObject(JsonAulas);

            //Texto do exercicio atual
            string exercicioTexto = objJson.cSharp.classes[classe].aulas[aula].Texto;

            return exercicioTexto;
        }
        public string[] JsonReadReturnClasseAula()
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

        //Depois modificar para se enccaixar com exercicios
        public async Task<string> JsonReturnType()
        {
            //Classe que possui o camiho do json
            CreateLocalStorageFolder JSON_PATH = new CreateLocalStorageFolder();

            //Classe que possui a função qu le o json e retorna a classe e a aula
            IdentificarAulaOuExercicioPage identificarAulaOuExercicio = new IdentificarAulaOuExercicioPage();

            //Cria o objeto e puxa o PATH do json
            jsonFolderPath = JSON_PATH.dirJsonFile;

            //Caminho do json
            string JsonAulas = await JSON_PATH.PushAulaJson();

            //Le o json e transforma em objeto
            //string data = File.ReadAllText(JsonAulas);
            dynamic objJson = JsonConvert.DeserializeObject(JsonAulas);

            //Contem o valor "Classe" e "Aulas" do json
            string[] ClasseAula = identificarAulaOuExercicio.JsonReadReturnClasseAula();

            //Converte os valores Aula e classe para int
            int classe = Convert.ToInt32(ClasseAula[0]);
            int aula = Convert.ToInt32(ClasseAula[1]);

            //Retorna o tipo do botão clicado dentro do json
            string classsOrAulaLength = objJson.cSharp.classes[classe].aulas[aula].Type;

            return classsOrAulaLength;
        }

    }

}

