using HoppCode.Classes;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;


namespace HoppCode.Pages;

public partial class SubAulasPage : ContentPage
{

    SubAulaPage subAula = new SubAulaPage();
    ReadSubAula read_Sub_Aula = new ReadSubAula();

    //List de frames que serão colocados no stackLayout                                                  
    List<List<dynamic>> styleList = null;

    //Cria uma lista que dentro, tenha os textos de cada style na lista acima
    List<List<string>> listaDosTextos = null;

    //Variaveis para manipulação dos frames, labels, e subAulas
    int subAulasPage = 0, maxSubAulasPage = 0;
    int labelId = 0;


    public SubAulasPage()
	{
		InitializeComponent();

        //Contem o valor "Classe" e "Aulas" do json       
        string[] ClasseAula = subAula.jsonRead();

        //Cria uma lista com duas dimensões, dentro, os styles criados na função ReadJsonAndReturnStyle                                                       
        styleList = read_Sub_Aula.ReadJsonAndReturnStyle(ClasseAula[0], ClasseAula[1], true);
        
        //Cria uma lista que dentro, tem os textos de cada style na lsita acima
        listaDosTextos = read_Sub_Aula.ReadJsonAndReturnTexts(ClasseAula[0], ClasseAula[1]);

        //Desabilita os botões de ir para frente e ir para tras
        BtnPassarTras.IsEnabled = false; BtnPassarFrente.IsEnabled = false;

        //Adiciona as styles na primeira dimesão da lista
        foreach (var frame in styleList[subAulasPage])
        { StackSubAulas.Add(frame); }
       
    }

    //Botão para mudar de pagina para frente
    public void MaisSubAulasPage(object sender, EventArgs e)
    {
        //Quando passar para frente, o botão de ir para tras sempre será habilitado
        BtnPassarTras.IsEnabled = true;
        subAulasPage++;

        //Quando o subAulasPage atingir o numero completo de auas
        if (subAulasPage == (styleList.Count - 1))
        {
            //Finaliza a aula e a coloca como completa           
            BtnPassarFrente.Text = "Finalizar";

        }
        //Caso seja a primeira vez na subAula
        if (maxSubAulasPage < subAulasPage)
        {
            //Deixa botão de passar e voltar desabilitados
            BtnPassarFrente.IsEnabled = false;
            BtnPassarTras.IsEnabled = false;

            maxSubAulasPage++;

            labelId = 0;
            BtnPassarSubAula.IsEnabled = true;
            BtnPassarSubAula.IsVisible = true;

            StackSubAulas.Children.Clear();

            //Adiciona as styles na primeira dimesão da lista
            foreach (var frame in styleList [subAulasPage])
            {StackSubAulas.Add(frame);}

        }
        //Caso não seja a primeira vez na subAula
        else
        {
            BtnPassarFrente.IsEnabled = true;
            StackSubAulas.Children.Clear();

            //Adiciona os estilos da pagina do valor subAulasPage
            foreach (var frame in styleList[subAulasPage])
            {
                StackSubAulas.Add(frame);
                frame.IsVisible = true;
            }
        }
        //Se o numero subAulasPage atingir o limite (quantas paginas de subAulas tem)
 
    }
    
    //Botão para mudar de pagina para atrás
    public void MenosSubAulasPage(object sender, EventArgs e)
    {
        BtnPassarFrente.Text = "-)";

        if (subAulasPage != 0)
        {   //Limpa a stack
            StackSubAulas.Children.Clear();

            subAulasPage--;
            labelId = 0;

            //Se for a primeira pagina, não se pode voltar
            if (subAulasPage == 0)
            { BtnPassarTras.IsEnabled = false; }

            //Adiciona os estilos da pagina do valor subAulasPage
            foreach (var frame in styleList[subAulasPage])
            {
                StackSubAulas.Add(frame);
                
                Label label = frame.Content as Label;
                string frameId = frame.ClassId;

                if (frameId.Substring(0, 1) == "C")
                {
                    label.FormattedText = returnFormatedText(listaDosTextos[subAulasPage][labelId]);
                }
                frame.IsVisible = true;
                labelId++;
            }
        }
        
    }

    private async void botao_Clicked(object sender, EventArgs e)
    {
        //Enquato as subAulas ainda estiverem aparecendo, os todos os botões estarão inacessiveis
        BtnPassarSubAula.IsEnabled = false;
        BtnPassarFrente.IsEnabled = false;
        BtnPassarTras.IsEnabled = false;

        string targetId = $"{labelId}"; // ID do elemento que você deseja modificar

        if (styleList [subAulasPage].Count > labelId)
        {
            foreach (var frame in styleList [subAulasPage])
            {
                //Para que a verifiação do ClassId com a substrig seja possivel, preciso definir o class id como uma string
                string frameId = frame.ClassId;
                if (frameId.Substring(2) == targetId)
                {
                    Label label = frame.Content as Label;
                    
                    //Caso o balão de texto seja de codigo
                    if (frameId.Substring(0, 1) == "C")
                    {
                        frame.IsVisible = true;
                        label.FormattedText = returnFormatedText(listaDosTextos[subAulasPage][labelId]);
                    }
                    //Caso o balão de texto seja um simples balão de texto
                    else if (frameId.Substring(0, 1) == "T")
                    {
                        if (label != null)
                        {
                            frame.IsVisible = true;
                            int currentIndex = 0;

                            label.Text = String.Empty;
                            while (currentIndex < listaDosTextos[subAulasPage][labelId].Length)
                            {
                                label.Text += listaDosTextos[subAulasPage][labelId][currentIndex];
                                currentIndex++;
                                await Task.Delay(20); // Aguarde meio segundo
                            }
                            break; // Sai do loop após encontrar o Frame desejado
                        }
                    }
                }
            }
           
            labelId++;
            //Caso seja a ultima página
            if (styleList [subAulasPage].Count == labelId)
            {
                BtnPassarFrente.IsEnabled = true;
                BtnPassarTras.IsEnabled = true;
                BtnPassarSubAula.IsVisible = false;    
            }
            
            BtnPassarSubAula.IsEnabled = true;

        }
       
    }

    public FormattedString returnFormatedText(string code)
    {
        FormattedString formattedString = new FormattedString();
        int stringLen = (code.Length - 1);
      
        for (int j = 0; j <= stringLen; j++)
        {
            try
            {
                if (code.Substring(j, 2) == "if")
                {
                    formattedString.Spans.Add(new Span { Text = "if ", TextColor = Colors.Purple });
                    j += 1;
                }
                else if (code.Substring(j, 7) == "Console")
                {
                    formattedString.Spans.Add(new Span { Text = "Console ", TextColor = Colors.Green });
                    j += 6;
                }
                else if (code.Substring(j, 9) == "WriteLine")
                {
                    formattedString.Spans.Add(new Span { Text = "WriteLine ", TextColor = Colors.Yellow });
                    j += 8;
                }
                else if (code.Substring(j, 1) == "\"")
                {
                    string stringToAdd = null;
                    int num = (j + 1);

                    stringToAdd += "\"";
                    while (code[num] != '\"')
                    {
                        stringToAdd += code[num];
                        num++;
                    }
                    stringToAdd += "\"";
                    formattedString.Spans.Add(new Span { Text = stringToAdd, TextColor = Colors.Orange });
                    j = num;
                }
                else
                {
                    formattedString.Spans.Add(new Span { Text = code[j].ToString(), TextColor = Colors.White });
                }
            }


            catch (System.IndexOutOfRangeException)
            {
                formattedString.Spans.Add(new Span { Text = code[j].ToString(), TextColor = Colors.White });
            }
            catch (System.ArgumentOutOfRangeException)
            {
                formattedString.Spans.Add(new Span { Text = code[j].ToString(), TextColor = Colors.White });
            }
        }

        return formattedString;

    }



}