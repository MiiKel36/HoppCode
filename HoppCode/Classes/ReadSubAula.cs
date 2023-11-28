using Microsoft.Maui.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace HoppCode.Classes
{
    internal class ReadSubAula
    {
        //Dá um id para o frame, usado para o codigo conseguir achar o frame no SubAulaPAge.xaml
        int frameId = 0;

        internal SubAulaPage SubAulaPage
        {
            get => default;
            set
            {
            }
        }

        public async Task<dynamic> ReadJsonAndReturnStyle(string classe, string aula)
        {
            //Retorn um array com os textos da subAula no json
            SubAulaPage subAula = new SubAulaPage();
            string[] arrayTextsFromJson = await subAula.JsonReturnSubAulasTexto(classe, aula);

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
                        //Caso ja estivesse lendo um frase, fechar formatação
                        if (openClose)
                        {   //Fecha formtação                                               
                            openClose = false;

                            //Executa função que retorna frame com label e adiciona para list de estilos
                            styleList[i].Add(CreateBox(text, frameId.ToString()));
                            
                            text = null;
                            frameId++;
                        }
                        //Caso não tiver fromatação aberta, abre uma
                        else
                        {
                            //Abre formtação
                            openClose = true;
                        }

                    }
                    //Formatação para a criação de uma caixa de texto com codigo
                    else if (arrayTextsFromJson[i][j] == '~')
                    {
                        //Caso ja estivesse lendo um frase, fechar formatação
                        if (openClose)
                        {   //Fecha formtação                                               
                            openClose = false;

                            //Executa função que retorna frame com label e adiciona para list de estilos
                            styleList[i].Add(CreateCodeBox(text, frameId.ToString()));

                            text = null;
                            frameId++;
                        }
                        //Caso não tiver fromatação aberta, abre uma
                        else
                        {
                            //Abre formtação
                            openClose = true;
                        }
                    }
                    else
                    {
                        text += arrayTextsFromJson[i][j];
                    }
                }
                frameId = 0;

            }
            frameId = 0;
            return styleList;
        }
        private dynamic CreateBox(string text, string id)
        {
            
            
            Frame frameWithLabel = new Frame
            {
                CornerRadius = 10,           // Set corner radius for the frame
                BorderColor = Color.FromRgb(99, 50, 155),   // Set border color for the frame
                Padding = new Thickness(20), // Set padding for the frame
                IsVisible = false,
                BackgroundColor = Color.FromRgb(99, 50, 155), // Set background color for the frame
                HorizontalOptions = LayoutOptions.Fill,
                ClassId = $"T {id}",

            };
            
            Label lbl = new Label()
            {
                Text = text,            
                FontSize = 17,
            };
            frameWithLabel.Content = lbl;
            



            return frameWithLabel;
        }
        private dynamic CreateCodeBox(string text, string id)
        {
            Frame frameWithLabel = new Frame
            {
                CornerRadius = 10,           // Set corner radius for the frame
                BorderColor = Color.FromRgb(49, 24, 80),   // Set border color for the frame
                BackgroundColor = Color.FromRgb(49, 24, 80), // Set background color for the frame
                ClassId = $"C {id}",
                IsVisible = false,
                HorizontalOptions = LayoutOptions.Fill,
                MinimumHeightRequest = 50,
                Margin = new Thickness(0),

            };
            Label lbl = new Label()
            {
                Text = text,                
                FontSize = 15,                        
            };
            frameWithLabel.Content = lbl;

            return frameWithLabel;
        }

        //Mesmo codigo para criação de frames, porem retorna os textos
        public async Task<List<List<string>>> ReadJsonAndReturnTexts(string classe, string aula)
        {                     
            //Retorn um array com os textos da subAula no json
            SubAulaPage subAula = new SubAulaPage();
            string[] arrayTextsFromJson = await subAula.JsonReturnSubAulasTexto(classe, aula);

            List<List<string>> textsList = new List<List<string>> { };

            bool openClose = false;
            string text = null;

            for (int i = 0; i < arrayTextsFromJson.Length; i++)
            {
                //Adiciona uma list dentro do styleList
                textsList.Add(new List<string>());

                //Verifica cada caracter do valor do array selecinado com o primeiro loop
                for (int j = 0; j < arrayTextsFromJson[i].Length; j++)
                {
                    //Formatação para a criação de uma caixa de texto normal
                    if (arrayTextsFromJson[i][j] == '@')
                    {
                        if (openClose)
                        {
                            openClose = false;
                            textsList[i].Add(text);
                            text = null;
                        }
                        else
                        {
                            openClose = true;
                        }

                    }
                    //Formatação para a criação de uma caixa de texto com codigo
                    else if (arrayTextsFromJson[i][j] == '~')
                    {
                        if (openClose)
                        {
                            openClose = false;
                            textsList[i].Add(text);
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

            return textsList;

        }


    }
}


