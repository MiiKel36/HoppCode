using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HoppCode
{
    class CreateButtons
    {
        //funçao que retorna o objeto json
        private dynamic ReturnJson(string jsonPath)
        {
            //lê o arquivo json
            string jsonArchive = File.ReadAllText(jsonPath);
            //transforma o json em objeto
            dynamic obj = JsonConvert.DeserializeObject<dynamic>(jsonArchive);

            return obj;

            //-- ESTA A SER PRODUZIDO --
        }
        public dynamic CreatingButtonsToPage()
        {
            //cria uma list para armazenar os objetos dos botões
            List<dynamic> botaoList = new List<dynamic>();

            //agora o loop roda com valores normais, mas depois, ira rodar com base na quantidade de resultados de uma pesquisa no json
            for (int i = 1; i <= 10; i++)
            {
                bool BotãoDireitaEsquerda = i % 2 == 0;
                Button botao = new Button()
                {
                    Text = $"botão {i}",
                    WidthRequest = 200,
                    HeightRequest = 200,

                    //foi o modo que eu achei para fazer if e else dentro dos parametros do botão
                    //caso o BotãoDireitaEsquerda seja true, o botão fica no final, se não no começo
                    HorizontalOptions = BotãoDireitaEsquerda ? new LayoutOptions(LayoutAlignment.End, true) /*true*/    :
                                                               new LayoutOptions(LayoutAlignment.Start, true)/*false*/

                };
                //adiciona o botão para a list botao
                botaoList.Add(botao);
            }
            //retorna a lista inteira, para o foreach no MainPage
            return botaoList;
        }


    }
}
