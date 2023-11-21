using HoppCode.Classes;
using HoppCode.ViewModels;

namespace HoppCode.Pages;

public partial class ClassesPage : ContentPage
{
    public ClassesPage()
	{
		InitializeComponent();
        BindingContext = new CreateButtonsClasses();
        

        try
        {
            //Cria objeto do CreateButtons
            CreateButtonsClasses classButons = new CreateButtonsClasses();

            //A var buttons vira uma list contendo os objetos dos botões
            dynamic buttons = classButons.CreatingButtonsToPage();

            foreach (Button button in buttons)
            {
                button.Clicked += (sender, e) =>
                {
                    Button clickedButton = (Button)sender;
                    string ButtonId = clickedButton.ClassId;

                    // Executa a função
                    ChangePage(ButtonId);
                };

                //Adiciona no stackLayout os botões com o valor dentro do button
                stackClasses.Add(button);
            }
        }
        catch
        {
            CreateLocalStorageFolder JSON_PATH = new CreateLocalStorageFolder();
            string jsonFolderPath = JSON_PATH.dirJsonFile;
            lbl.Text = jsonFolderPath;
        }
    }
    //Daixa o botão de voltar não funcional
    protected override bool OnBackButtonPressed()
    {
        return true;
    }

    public void ChangePage(string num)
    {
        //Muda o valor classe para a classe no qual clicamos
        JsonClassesPage jsonModifierAulas = new JsonClassesPage();
        jsonModifierAulas.JsonReadAndWrite(num);

        Navigation.PushAsync(new AulasPage());
    }
}