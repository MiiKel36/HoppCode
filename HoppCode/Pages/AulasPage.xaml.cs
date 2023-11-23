using HoppCode.Classes;
using HoppCode.ViewModels;
using Newtonsoft.Json;
namespace HoppCode;


public partial class AulasPage : ContentPage
{

    public AulasPage()
	{        
        InitializeComponent();

        //Pega o valor da classe guardade no json
        aulasPage jsonModifierAulas = new aulasPage();
		string valueAulas = jsonModifierAulas.JsonReadReturnClasse();

        CreateButtonsAulas createButtons = new CreateButtonsAulas();
        dynamic buttons = createButtons.CreatingButtonsToPage(valueAulas);


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
            stackAulas.Add(button);
        }


    }
    public void ChangePage(string JsonClasseId)
    {
        //Muda o valor classe para a classe no qual clicamos
        aulasPage jsonModifierAulas = new aulasPage();
        jsonModifierAulas.JsonReadAndWrite(JsonClasseId);

        //Envia para IdentficarAulaOuExercicio e l�, verifica se � exerc�cio ou aula
        Navigation.PushAsync(new Pages.IdentificarAulaOuExercicio());
    }
    protected override bool OnBackButtonPressed()
    {
        Navigation.PushAsync(new Pages.ClassesPage());
        return true;
    }
}