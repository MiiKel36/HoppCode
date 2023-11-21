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

/* Alteração não mesclada do projeto 'HoppCode (net7.0-android)'
Antes:
using HoppCode.Classes;



namespace HoppCode.Classes
Após:
using HoppCode.Classes;
using HoppCode;
using HoppCode.ViewModels;

namespace HoppCode.Classes
*/
using HoppCode.Classes;
using System.ComponentModel;

namespace HoppCode.ViewModels
{

    public abstract class CreateButtons 
    {
        //Não possui funções abstratas pois algumas funções necessitam de parametros, e outras não
    }



    //Classe para a pagina ClassesPage
    public class CreateButtonsClasses : CreateButtons 
    {

        public dynamic CreatingButtonsToPage()
        {
            JsonClassesPage json = new JsonClassesPage();

            //Pega o numero de quantos botões vai criar 
            int quantButtons = json.ReturnNumOfClasses();
            //Cria um array com os nomes dos botões
            string[] buttonsNames = json.ArrayClassesNames(quantButtons);


            //Cria uma list para armazenar os objetos dos botões
            List<dynamic> botaoList = new List<dynamic>();

            //O loop roda com a quantidade de botões armazenados no json
            for (int i = 0; i < quantButtons; i++)
            {
                bool BotãoDireitaEsquerda = i % 2 == 0;
                Button botao = new Button()
                {
                    Text = $"{buttonsNames[i]}",
                    WidthRequest = 220,
                    HeightRequest = 160,
                    BackgroundColor = Color.FromRgb(99, 50, 155),
                    TextColor = Colors.White,
                    FontSize = 15,
                    FontAttributes = FontAttributes.Bold,
                    ClassId = i.ToString(),
                    LineBreakMode = LineBreakMode.WordWrap,


                    //Foi o modo que eu achei para fazer if e else dentro dos parametros do botão
                    //Aaso o BotãoDireitaEsquerda seja true, o botão fica no final, se não no começo
                    HorizontalOptions = BotãoDireitaEsquerda ? new LayoutOptions(LayoutAlignment.Start, true) /*true*/    :
                                                               new LayoutOptions(LayoutAlignment.End, true)/*false*/

                };

                botao.Shadow = new Shadow()
                {
                    Brush = Color.FromRgb(57, 33, 93),
                    Offset = new Point(-10, 10),
                    Opacity = 0.5f,
                    Radius = 1,

                };

                //Adiciona o botão para a list botao
                botaoList.Add(botao);
            }
            //Retorna a lista inteira, para o foreach no ClassesPage
            return botaoList;
        }
    }

    //Classe para a pagina AulasPage
    public class CreateButtonsAulas : CreateButtons
    {
        public dynamic CreatingButtonsToPage(string JsonClasseId)
        {
            aulasPage json = new aulasPage();

            //Pega o numero de quantos botões vai criar 
            int quantButtons = json.ReturnNumOfAulas(Convert.ToInt32(JsonClasseId));

            //Cria um array com os nomes dos botões
            string[] buttonsNames = json.ArrayNames(Convert.ToInt32(quantButtons), Convert.ToInt32(JsonClasseId));


            //Cria uma list para armazenar os objetos dos botões
            List<dynamic> botaoList = new List<dynamic>();

            //O loop roda com a quantidade de botões armazenados no json
            for (int i = 0; i < quantButtons; i++)
            {
                bool BotãoDireitaEsquerda = i % 2 == 0;
                Button botao = new Button()
                {
                    Text = $"{buttonsNames[i]}",
                    WidthRequest = 300,
                    HeightRequest = 200,
                    ClassId = i.ToString(), //define o id do botão junto se é aula ou exercicio
                    LineBreakMode = LineBreakMode.WordWrap,

                    //Os bõtoes ficam apenas no centro
                    HorizontalOptions = new LayoutOptions(LayoutAlignment.Center, true)

                };

                //Adiciona o botão para a list botao
                botaoList.Add(botao);
            }
            //Retorna a lista inteira, para o foreach no AulasPage
            return botaoList;
        }
    }


}

