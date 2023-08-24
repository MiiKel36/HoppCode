using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoppCode.Classes
{
    internal class ReadSubAula
    {
        public dynamic ReadJsonAndReturnStyle(string classe, string aula)
        {
            //Retorn um array com os textos da subAula no json
            SubAulaPage subAula = new SubAulaPage();
            string[] arrayTextsFromJson = subAula.jsonReturnSubAulasTexto(classe, aula);

            //Variaveis para o loop da formatação
            bool openClose = false;
            string text = null;

            //Criação de uma lista de duas dimensões para colocar os estilos XAMLs
            List<List<dynamic>> styleList = new List<List<dynamic>> { };

                           
            //Codigo para formatação do texto
            //Primeiro loop para verificar cada string no array arrayTextsFromJson
            for (int i = 0; i < arrayTextsFromJson.Length; i++)
            {
                //Adiciona uma list dentro do styleList
                styleList.Add(new List<dynamic>());

                //Verifica cada caracter do valor do array selecinado com o primeiro loop
                for (int j = 0; j < arrayTextsFromJson[i].Length; j++)
                {
                    //Formatação para a criação de uma caixa de texto normal
                    if (arrayTextsFromJson[i][j] == '@')
                    {
                        if (openClose)
                        {
                            openClose = false;
                            styleList[i].Add(CreateBox(text));
                            text = null;
                        }
                        else
                        {
                            openClose = true;
                        }

                    }
                    //Formatação para a criação de uma caixa de texto com codigo
                    else if (arrayTextsFromJson[i][j] == ':')
                    {
                        if (openClose)
                        {
                            openClose = false;
                            styleList[i].Add(CreateCodeBox(text));
                            text = null;
                        }
                        else
                        {
                            openClose = true;
                        }
                    }
                    else
                    {
                        text += arrayTextsFromJson[i][j];
                    }
                }


            }
            return styleList;
        }
        private dynamic CreateBox(string text)
        {
            var label = new Label
            {
                Text = $"{text}",
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };

            var stackLayout = new StackLayout
            {
                Children = { label },
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                BackgroundColor = Colors.Blue,
            };

            return stackLayout;
        }
        private dynamic CreateCodeBox(string text)
        {
            var label = new Label
            {
                Text = $"{text}",
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center

            };

            var stackLayout = new StackLayout
            {
                Children = { label },
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                BackgroundColor = Colors.Red,
            };

            return stackLayout;
        }
    }
}
