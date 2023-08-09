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
   
    class CreateButtons
    {

        public static void ChangePage(string num)
        {
            //Muda o valor classe para a classe no qual clicamos
            JsonModifier jsonModifierAulas = new JsonModifier();
            jsonModifierAulas.JsonReadAndWriteAulas(num);

            //Envia par AulasPage
            Shell.Current.GoToAsync("AulasPage");
        }

        public dynamic CreatingButtonsAulasToPage()
        {
            //Cria uma list para armazenar os objetos dos botões
            List<dynamic> botaoList = new List<dynamic>();

            //Agora o loop roda com valores normais, mas depois, ira rodar com base na quantidade de resultados de uma pesquisa no json
            for (int i = 1; i <= 10; i++)
            {
                bool BotãoDireitaEsquerda = i % 2 == 0;
                Button botao = new Button()
                {
                    Text = $"botão {i}",
                    WidthRequest = 200,
                    HeightRequest = 200,
                    ClassId = i.ToString(),

                    //Foi o modo que eu achei para fazer if e else dentro dos parametros do botão
                    //Aaso o BotãoDireitaEsquerda seja true, o botão fica no final, se não no começo
                    HorizontalOptions = BotãoDireitaEsquerda ? new LayoutOptions(LayoutAlignment.End, true) /*true*/    :
                                                               new LayoutOptions(LayoutAlignment.Start, true)/*false*/

                };
                botao.Clicked += (sender, args) =>
                {
                    Button clickedButton = (Button)sender;
                    string ButtonId = clickedButton.ClassId;

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
