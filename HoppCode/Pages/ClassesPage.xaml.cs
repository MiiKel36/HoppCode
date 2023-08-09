using HoppCode.Classes;

namespace HoppCode.Pages;

public partial class ClassesPage : ContentPage
{

	public ClassesPage()
	{
		InitializeComponent();

        //Cria objeto do CreateButtons
        CreateButtons classButons = new CreateButtons();

        //A var buttons vira uma list contendo os objetos dos botões
        dynamic buttons = classButons.CreatingButtonsAulasToPage();

        foreach (var button in buttons)
        {   //Adiciona no stackLayout os botões com o valor dentro do button
            stackClasses.Add(button);
        }
    }
}