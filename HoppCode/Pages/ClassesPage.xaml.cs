using HoppCode.Classes;

namespace HoppCode.Pages;

public partial class ClassesPage : ContentPage
{
    
    public ClassesPage()
	{
		InitializeComponent();


        try
        {
            //Cria objeto do CreateButtons
            CreateButtonsClasses classButons = new CreateButtonsClasses();

            //A var buttons vira uma list contendo os objetos dos botões
            dynamic buttons = classButons.CreatingButtonsToPage();

            foreach (var button in buttons)
            {   //Adiciona no stackLayout os botões com o valor dentro do button
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
}