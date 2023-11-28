using HoppCode.Classes;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;

namespace HoppCode.Pages;

public partial class SubAulasPage : ContentPage
{
    static SubAulaPage subAula = new SubAulaPage();
    static ReadSubAula read_Sub_Aula = new ReadSubAula();

    //List de frames que serão colocados no stackLayout                                                  
    List<List<dynamic>> styleList = null;

    //Cria uma lista que dentro, tenha os textos de cada style na lista acima
    List<List<string>> listaDosTextos = null;

    //Variaveis para manipulação dos frames, labels, e subAulas
    int subAulasPage = 0, maxSubAulasPage = 0;
    int labelId = 0;

    public string webApiKey = "AIzaSyB1m5xiuM-tOk0GUHnhrcJ2uVmkJr1ogwE"; // Não é dado sensível 👍

    string classe, aula;

    Image mascotImage = new Image
    {
        Source = ImageSource.FromFile("mascot.png"),
        WidthRequest = 150,
        HeightRequest = 237,
        HorizontalOptions = LayoutOptions.End,
    };

    public SubAulasPage(string classeFromOtherPage, string aulaFromOtherPage)
	{
		InitializeComponent();

        classe = classeFromOtherPage;
        aula = aulaFromOtherPage;        

        AddButtonToStack();
        
    }
    public async void AddButtonToStack()
    {
        //Cria uma lista com duas dimensões, dentro, os styles criados na função ReadJsonAndReturnStyle                                                       
        styleList = await read_Sub_Aula.ReadJsonAndReturnStyle(classe, aula);

        //Cria uma lista que dentro, tem os textos de cada style na lsita acima
        listaDosTextos = await read_Sub_Aula.ReadJsonAndReturnTexts(classe, aula);

        //Desabilita os botões de ir para frente e ir para tras
        IsEnableOrNotButton(false, BtnPassarTras);
        IsEnableOrNotButton(false, BtnPassarFrente);
        IsEnableOrNotButton(true, BtnPassarSubAula);

        BtnPassarFrente.Text = ">";
        BtnPassarTras.Text = "<";

        //Adiciona as styles na primeira dimesão da lista
        foreach (Frame frame in styleList[subAulasPage])
        { StackSubAulas.Add(frame); }

        StackMascote.Add(mascotImage);
    }

    //Botão para mudar de pagina para frente
    public void MaisSubAulasPage(object sender, EventArgs e)
    {
        //Quando passar para frente, o botão de ir para trás sempre será habilitado
        IsEnableOrNotButton(true, BtnPassarTras);
        subAulasPage++;

        //Quando o subAulasPage atingir o número completo de aulas
        if (subAulasPage == (styleList.Count - 1))
        {
            //Finaliza a aula e a coloca como completa          
            BtnPassarFrente.Text = "Avançar";

            BtnPassarFrente.WidthRequest = 100;
            BtnPassarFrente.FontSize = 15;

        }
        if(subAulasPage > (styleList.Count - 1))
        {
            string proximaAula = (1 + Convert.ToInt32(aula)).ToString();

            //subAula.WriteJson(proximaAula.ToString());

            //Envia para AulasPage
            Navigation.PushAsync(new Pages.IdentificarAulaOuExercicio(classe, proximaAula));

        }
        else
        {//Caso seja a primeira vez na subAula
            if (maxSubAulasPage < subAulasPage)
            {
                //Texto do botão no meio fica como começar
                BtnPassarSubAula.Text = "Começar";

                //Deixa botão de passar e voltar desabilitados
                IsEnableOrNotButton(false, BtnPassarFrente);
                IsEnableOrNotButton(false, BtnPassarTras);

                maxSubAulasPage++;

                labelId = 0;
                IsEnableOrNotButton(true, BtnPassarSubAula);
                BtnPassarSubAula.IsVisible = true;

                StackSubAulas.Children.Clear();
                StackMascote.Children.Clear();

                //Adiciona as styles na primeira dimensão da lista
                foreach (Frame frame in styleList[subAulasPage])
                { StackSubAulas.Add(frame); }

                mascotImage.IsVisible = true;
                StackMascote.Add(mascotImage);
            }
            //Caso não seja a primeira vez na subAula
            else
            {
                IsEnableOrNotButton(true, BtnPassarFrente);
                StackSubAulas.Children.Clear();
                StackMascote.Children.Clear();

                //Adiciona os estilos da pagina do valor subAulasPage
                foreach (Frame frame in styleList[subAulasPage])
                {
                    StackSubAulas.Add(frame);
                    frame.IsVisible = true;
                }

                mascotImage.IsVisible = true;
                StackMascote.Add(mascotImage);
            }
            //Se o numero subAulasPage atingir o limite (quantas paginas de subAulas tem)
        }


    }
    
    //Botão para mudar de pagina para atrás
    public void MenosSubAulasPage(object sender, EventArgs e)
    {
        //DEixa o botão de passar para frente em seu setup inicial
        BtnPassarFrente.Text = ">";
        BtnPassarFrente.WidthRequest = 50;
        BtnPassarFrente.FontSize = 30;

        if (subAulasPage != 0)
        {   //Limpa as stacks
            StackSubAulas.Children.Clear();
            StackMascote.Children.Clear();

            subAulasPage--;
            labelId = 0;

            //Se for a primeira pagina, não se pode voltar
            if (subAulasPage == 0)
            { IsEnableOrNotButton(true, BtnPassarTras); }

            //Adiciona os estilos da pagina do valor subAulasPage
            foreach (Frame frame in styleList[subAulasPage])
            {              
                StackSubAulas.Add(frame);

                Label label = frame.Content as Label;
                string frameId = frame.ClassId;

                WebView webView = new WebView()
                {
                    Source = "aceeditor2.html",

                };
                webView.Navigated += (o, s) => {
                    string textFromLista = listaDosTextos[subAulasPage][labelId - 1];
                    webView.Eval($"SetTextOnCodeEditor(\"{textFromLista}\");");

                    int numOfLines = textFromLista.Split("\\n").Length - 1;
                    int sizeOfWemView = (numOfLines * 15) + 30;

                    webView.HeightRequest = sizeOfWemView;
                    frame.HeightRequest = sizeOfWemView;
                    //Console.WriteLine(listaDosTextos[10000]);
                };

                if (frameId.Substring(0, 1) == "C")
                {
                    frame.WidthRequest = 100;
                    frame.HeightRequest = 100;
                    frame.Content = webView;
                    frame.IsVisible = true;
                }
                frame.IsVisible = true;
                labelId++;

            }

            mascotImage.IsVisible = true;
            StackMascote.Add(mascotImage);
        }
        
    }

    private async void AnimateMascot()
    {
        // Função que anima o pulinho do mascote (e a saída da tela também)
        double originalTranslationY = mascotImage.TranslationY;
        if (labelId <= 2)
        {
            await mascotImage.TranslateTo(0, originalTranslationY - 50, 200, Easing.CubicOut);
            await mascotImage.TranslateTo(0, originalTranslationY, 200, Easing.CubicIn);
        }
        // Quando tem mais de 3 frames visíveis, manda o mascote pra fora (não dá pra ver nada com ele na frente!)
        else if (mascotImage.IsVisible)
        {
            await mascotImage.TranslateTo(500, originalTranslationY, 400, Easing.CubicOut);
            mascotImage.IsVisible = false;
        }
        else return;
    }

    private async void botao_Clicked(object sender, EventArgs e)
    {
        BtnPassarSubAula.Text = "Passar";

        AnimateMascot();

        //Enquato as subAulas ainda estiverem aparecendo, os todos os botões estarão inacessiveis
        IsEnableOrNotButton(false, BtnPassarSubAula);
        IsEnableOrNotButton(false, BtnPassarFrente);
        IsEnableOrNotButton(false, BtnPassarTras);

        string targetId = $"{labelId}"; // ID do elemento que você deseja modificar

        if (styleList [subAulasPage].Count > labelId)
        {
            foreach (Frame frame in styleList [subAulasPage])
            {
                //Para que a verifiação do ClassId com a substrig seja possivel, preciso definir o class id como uma string
                string frameId = frame.ClassId;
                if (frameId.Substring(2) == targetId)
                {                  
                    Label label = frame.Content as Label;

                    WebView webView = new WebView()
                    {
                        Source = "aceeditor2.html",
                    };
                    webView.Navigated += (o, s) => {
                        string textFromLista = listaDosTextos[subAulasPage][labelId - 1];
                        webView.Eval($"SetTextOnCodeEditor(\"{textFromLista}\");");
                        
                        int numOfLines = textFromLista.Split("\\n").Length - 1;
                        int sizeOfWemView = (numOfLines * 15) + 30;
                        
                        webView.HeightRequest = sizeOfWemView;
                        frame.HeightRequest = sizeOfWemView;
                        //Console.WriteLine(listaDosTextos[10000]);
                    };

                    //Caso o balão de texto seja de codigo
                    if (frameId.Substring(0, 1) == "C")
                    {
                        frame.Content = webView;
                        frame.IsVisible = true;
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
                                await Task.Delay(15); // Aguarde 15 milésimos
                                
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
                IsEnableOrNotButton(true, BtnPassarFrente);
                IsEnableOrNotButton(true, BtnPassarTras);
                IsEnableOrNotButton(false, BtnPassarSubAula);
            }
            else
            {
                IsEnableOrNotButton(true, BtnPassarSubAula);
            }
        }
       
    }
    private void Button_Clicked_1(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AulasPage(classe));
    }

    protected override bool OnBackButtonPressed()
    {
        return true;
    }

    public FormattedString ReturnFormattedText(string code)
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

    public static void IsEnableOrNotButton(bool IsOrNot, Button button)
    {
        button.IsEnabled = IsOrNot;
        button.BackgroundColor = IsOrNot ? Color.FromRgb(74, 51, 189) : Color.FromRgb(54, 34, 150);
    }



}
