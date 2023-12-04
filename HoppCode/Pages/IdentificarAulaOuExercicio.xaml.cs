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
        aulasPage json = new aulasPage();

        //Pega o numero de quantos botões vai criar 
        int quantButtons = await json.ReturnNumOfAulas(Convert.ToInt32(classe));
        string type;

        if (quantButtons-1 < Convert.ToInt32(aula)) { type = "Passar"; }
        else
        {
           type = await identifyType.JsonReturnType(classe, aula);
        }
        
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
            case "Passar":
                //Envia par AulasPage
                await Navigation.PushAsync(new Pages.ClassesPage());
                break;
        }
        



    }

}