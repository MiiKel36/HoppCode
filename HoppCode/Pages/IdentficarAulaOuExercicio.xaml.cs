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
                Shell.Current.GoToAsync("SubAulasPage");
                break;

			case "Exercicio":
                //Envia par AulasPage
                Shell.Current.GoToAsync("ExercisesPage");
                break;
		}


    }

    protected override bool OnBackButtonPressed()
    {
        Shell.Current.GoToAsync("AulasPage");
        return true;
    }

}