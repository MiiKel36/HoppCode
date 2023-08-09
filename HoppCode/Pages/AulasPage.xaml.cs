using HoppCode.Classes;
using Newtonsoft.Json;
namespace HoppCode;


public partial class AulasPage : ContentPage
{

    public AulasPage()
	{
        
        InitializeComponent();

        //Pega o valor da classe guardade no json
        JsonModifier jsonModifierAulas = new JsonModifier();
		string valueAulas = jsonModifierAulas.jsonReadAulas();

        //APENAS UM PROTÓTIPO -- (mais tarde será a criação dos botões) -- 
        //Cria label com valor no json e colca no stack

        Label label = new Label()
		{ Text = $"{valueAulas}",
        };
        stackAulas.Add(label);

        

    }
}