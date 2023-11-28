using HoppCode.Classes;

namespace HoppCode.Pages;

public partial class IdentificarAulaOuExercicio : ContentPage
{
    public IdentificarAulaOuExercicio(string classe, string aula)
    {
        InitializeComponent();
        VerificarAulaOuExercicio(classe, aula);
    }
    public async void VerificarAulaOuExercicio(string classe, string aula)
    {
        IdentificarAulaOuExercicioPage identifyType = new IdentificarAulaOuExercicioPage();
        string type = await identifyType.JsonReturnType(classe, aula);

        switch (type)
        {
            case "Aula":
                //Envia par AulasPage
                await Navigation.PushAsync(new Pages.SubAulasPage(classe, aula));
                break;

            case "Exercicio":
                //Envia par AulasPage
                await Navigation.PushAsync(new Pages.ExercisesPage(classe, aula));
                break;
        }


    }

}