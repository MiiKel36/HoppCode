using HoppCode.Classes;
using HoppCode.ViewModels;

namespace HoppCode.Pages;

public partial class ClassesPage : ContentPage
{
    public ClassesPage()
	{
		InitializeComponent();

        AddButtonsToStack();
    }

    public async void AddButtonsToStack()
    {
        //Cria objeto do CreateButtons
        CreateButtonsClasses classButons = new CreateButtonsClasses();

        //A var buttons vira uma list contendo os objetos dos bot�es
        dynamic buttons = await classButons.CreatingButtonsToPage();

        foreach (Button button in buttons)
        {
            button.Clicked += (sender, e) =>
            {
                Button clickedButton = (Button)sender;
                string ButtonId = clickedButton.ClassId;

                // Executa a fun��o
                ChangePage(ButtonId);
            };

            //Adiciona no stackLayout os bot�es com o valor dentro do button
            stackClasses.Add(button);
        }
    }

    //Daixa o bot�o de voltar n�o funcional
    protected override bool OnBackButtonPressed()
    {
        return true;
    }

    public void ChangePage(string ClassesEscolhida)
    {
        Navigation.PushAsync(new AulasPage(ClassesEscolhida));
    }
}