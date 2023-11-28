using HoppCode.Classes;
using HoppCode.ViewModels;
using Newtonsoft.Json;
namespace HoppCode;


public partial class AulasPage : ContentPage
{
    string valueClasse;
    public AulasPage(string valueClasseFromOtherPage)
	{        
        InitializeComponent();
        valueClasse = valueClasseFromOtherPage;
        AddButtonsToStack();

    }
    public async void AddButtonsToStack()
    {

        //Pega o valor da classe guardade no json
        aulasPage jsonModifierAulas = new aulasPage();

        CreateButtonsAulas createButtons = new CreateButtonsAulas();
        dynamic buttons = await createButtons.CreatingButtonsToPage(valueClasse);

        foreach (Button button in buttons)
        {
            button.Clicked += (sender, e) =>
            {
                Button clickedButton = (Button)sender;
                string ButtonId = clickedButton.ClassId;

                // Executa a função
                ChangePage(ButtonId);
            };

            //Adiciona no stackLayout os botões com o valor dentro do button
            stackAulas.Add(button);
        }
    }
    public void ChangePage(string valueAula)
    {
        //Envia para IdentficarAulaOuExercicio e lá, verifica se é exercício ou aula
        Navigation.PushAsync(new Pages.IdentificarAulaOuExercicio(valueClasse, valueAula));
    }
    protected override bool OnBackButtonPressed()
    {
        Navigation.PushAsync(new Pages.ClassesPage());
        return true;
    }
}