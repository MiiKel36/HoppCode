using HoppCode.Classes;

namespace HoppCode.Pages;

public partial class IdentficarAulaOuExercicio : ContentPage
{
	public IdentficarAulaOuExercicio()
	{
		InitializeComponent();

		IdentificarAulaOuExercicioPage identfyType = new IdentificarAulaOuExercicioPage();
		string type = identfyType.JsonReturnType();
        
        switch (type)
		{
			case "Aula":
                //Envia par AulasPage
                Navigation.PushAsync(new Pages.SubAulasPage());
                break;

			case "Exercicio":
                //Envia par AulasPage
                Navigation.PushAsync(new Pages.ExercisesPage());
                break;
		}


    }

    protected override bool OnBackButtonPressed()
    {
        Navigation.PushAsync(new AulasPage());
        return true;
    }

}