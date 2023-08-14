using HoppCode.Classes;
using Newtonsoft.Json;
namespace HoppCode;


public partial class AulasPage : ContentPage
{

    public AulasPage()
	{
        
        InitializeComponent();

        //Pega o valor da classe guardade no json
        aulasPage jsonModifierAulas = new aulasPage();
		string valueAulas = jsonModifierAulas.jsonRead();

        CreateButtonsAulas createButtons = new CreateButtonsAulas();
        dynamic buttons = createButtons.CreatingButtonsToPage(valueAulas);

        foreach (var button in buttons)
        {   //Adiciona no stackLayout os botões com o valor dentro do button
            stackAulas.Add(button);
        }


    }
}