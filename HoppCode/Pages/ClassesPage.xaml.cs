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

            //A var buttons vira uma list contendo os objetos dos bot�es
            dynamic buttons = classButons.CreatingButtonsToPage();

            foreach (var button in buttons)
            {   //Adiciona no stackLayout os bot�es com o valor dentro do button
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
    //Daixa o bot�o de voltar n�o funcional
    protected override bool OnBackButtonPressed()
    {
        return true;
    }
}