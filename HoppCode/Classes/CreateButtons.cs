using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;
using static System.Reflection.Metadata.BlobBuilder;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static Microsoft.Maui.ApplicationModel.Permissions;



namespace HoppCode.Classes
{
   
    public abstract class CreateButtons
    {
    //Não possui funções abtratas pois algumas funções necessitam de parametros, e outras não
    }

    //Classe para a pagina ClassesPage
    public class CreateButtonsClasses : CreateButtons
    {
        public void ChangePage(string num)
        {
            //Muda o valor classe para a classe no qual clicamos
            ClassesPage jsonModifierAulas = new ClassesPage();
            jsonModifierAulas.JsonReadAndWrite(num);

            //Envia par AulasPage
            Shell.Current.GoToAsync("AulasPage");
        }
        public  dynamic CreatingButtonsToPage()
        {
            ClassesPage json = new ClassesPage();

            //Parte de extraçã dos conteudos do json
            int buttonsNums = json.GetNumOfJson();
            string[] buttonsNames = json.ArrayNames(buttonsNums);

            //Cria uma list para armazenar os objetos dos botões
            List<dynamic> botaoList = new List<dynamic>();

            //Agora o loop roda com valores normais, mas depois, ira rodar com base na quantidade de resultados de uma pesquisa no json
            for (int i = 0; i < buttonsNums; i++)
            {
                bool BotãoDireitaEsquerda = i % 2 == 0;
                Button botao = new Button()
                {
                    Text = $"{buttonsNames[i]}",
                    WidthRequest = 200,
                    HeightRequest = 200,
                    ClassId = i.ToString(),

                    //Foi o modo que eu achei para fazer if e else dentro dos parametros do botão
                    //Aaso o BotãoDireitaEsquerda seja true, o botão fica no final, se não no começo
                    HorizontalOptions = BotãoDireitaEsquerda ? new LayoutOptions(LayoutAlignment.Start, true) /*true*/    :
                                                               new LayoutOptions(LayoutAlignment.End, true)/*false*/

                };
                botao.Clicked += (sender, args) =>
                {
                    //Pega ID do botão clicado
                    Button clickedButton = (Button)sender;
                    string ButtonId = clickedButton.ClassId;

                    //Muda de pagina
                    ChangePage(ButtonId);
                };

                //Adiciona o botão para a list botao
                botaoList.Add(botao);
            }
            //Retorna a lista inteira, para o foreach no MainPage
            return botaoList;
        }
    }
    
    //Classe para a pagina AulasPage
    public class CreateButtonsAulas : CreateButtons
    {
        public  void ChangePage(string JsonClasseId)
        {
            //Muda o valor classe para a classe no qual clicamos
            aulasPage jsonModifierAulas = new aulasPage();
            jsonModifierAulas.JsonReadAndWrite(JsonClasseId);

            //Envia par AulasPage
            Shell.Current.GoToAsync("SubAulasPage");
        }
        public  dynamic CreatingButtonsToPage(string JsonClasseId)
        {
            aulasPage json = new aulasPage();

            //Pega o numero de quantos botões vai criar 
            int buttonsNums = json.GetNumOfJson(Convert.ToInt32(JsonClasseId));
            //Cria um array com os nomes dos bot~es
            string[] buttonsNames = json.ArrayNames(Convert.ToInt32(buttonsNums), Convert.ToInt32(JsonClasseId));
            

            //Cria uma list para armazenar os objetos dos botões
            List<dynamic> botaoList = new List<dynamic>();

            //Agora o loop roda com valores normais, mas depois, ira rodar com base na quantidade de resultados de uma pesquisa no json
            for (int i = 0; i < buttonsNums; i++)
            {
                bool BotãoDireitaEsquerda = i % 2 == 0;
                Button botao = new Button()
                {
                    Text = $"{buttonsNames[i]}",
                    WidthRequest = 200,
                    HeightRequest = 200,
                    ClassId = i.ToString(),

                    //Os bõtoes ficam apenas no centro
                    HorizontalOptions =  new LayoutOptions(LayoutAlignment.Center, true)

                };
                botao.Clicked += (sender, args) =>
                {
                    //Pega o id do botão clicado
                    Button clickedButton = (Button)sender;
                    string ButtonId = clickedButton.ClassId;

                    //Muda de pagina
                    ChangePage(ButtonId);
                };

                //Adiciona o botão para a list botao
                botaoList.Add(botao);
            }
            //Retorna a lista inteira, para o foreach no MainPage
            return botaoList;
        }
    }


}

