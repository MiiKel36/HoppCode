using HoppCode.Classes;

namespace HoppCode.Pages;

public partial class IdentificarAulaOuExercicio : ContentPage
{
    public IdentificarAulaOuExercicio() 
    {
        InitializeComponent();
        teste();
    }
    public async void teste()
    {
        IdentificarAulaOuExercicioPage identifyType = new IdentificarAulaOuExercicioPage();
        string type = await identifyType.JsonReturnType();

        switch (type)
        {
            case "Aula":
                //Envia par AulasPage
                await Navigation.PushAsync(new Pages.SubAulasPage());
                break;

            case "Exercicio":
                //Envia par AulasPage
                await Navigation.PushAsync(new Pages.ExercisesPage());
                break;
        }
    }

    protected override bool OnBackButtonPressed()
    {
        Navigation.PushAsync(new AulasPage());
        return true;
    }

}