using HoppCode.Classes;

namespace HoppCode.Pages;

public partial class SubAulasPage : ContentPage
{

    int subAulasPage = 0;
    public SubAulasPage()
	{
		InitializeComponent();

        //Função que le os valore classes e aulas no json changePage 
        SubAulaPage subAula = new SubAulaPage();
        string[] ClasseAula = subAula.jsonRead();

        //Cria uma lista com duas dimensões, dentro, os styles criados na função ReadJsonAndReturnStyle
        ReadSubAula read_Sub_Aula = new ReadSubAula();
        List<List<dynamic>> styleList = read_Sub_Aula.ReadJsonAndReturnStyle(ClasseAula[0], ClasseAula[1]);

        //Adiciona as styles na primeira dimesão da lista
        for (int j = 0; j < styleList[0].Count; j++)
        { StackSubAulas.Add(styleList[0][j]); }
    }

    //Botão para mudar de pagina para frente
    public void MaisSubAulasPage(object sender, EventArgs e)
    {
        //Função que le os valore classes e aulas no json changePage 
        SubAulaPage subAula = new SubAulaPage();
        string[] ClasseAula = subAula.jsonRead();

        //Cria uma lista com duas dimensões, dentro, os styles criados na função ReadJsonAndReturnStyle
        ReadSubAula read_Sub_Aula = new ReadSubAula();
        List<List<dynamic>> styleList = read_Sub_Aula.ReadJsonAndReturnStyle(ClasseAula[0], ClasseAula[1]);

        //Se o numero subAulasPage atingir o limite (quantas paginas de subAulas tem)
        if (subAulasPage < (styleList.Count - 1))
        {
            subAulasPage++;
            StackSubAulas.Children.Clear();

            //Adiciona os estilos da pagina do valor subAulasPage
            for (int j = 0; j < styleList[subAulasPage].Count; j++)
            {
                StackSubAulas.Add(styleList[subAulasPage][j]);
            }
        }
        //Finaliza a aula e a coloca como completa
        else
        {

        }
    }
    //Botão para mudar de pagina para atrás
    public void MenosSubAulasPage(object sender, EventArgs e)
    {
        //Função que le os valore classes e aulas no json changePage 
        SubAulaPage subAula = new SubAulaPage();
        string[] ClasseAula = subAula.jsonRead();

        //Cria uma lista com duas dimensões, dentro, os styles criados na função ReadJsonAndReturnStyle
        ReadSubAula read_Sub_Aula = new ReadSubAula();
        List<List<dynamic>> styleList = read_Sub_Aula.ReadJsonAndReturnStyle(ClasseAula[0], ClasseAula[1]);

        if (subAulasPage != 0)
        {
            subAulasPage--;
            StackSubAulas.Children.Clear();

            //Adiciona os estilos da pagina do valor subAulasPage
            for (int j = 0; j < styleList[subAulasPage].Count; j++)
            {
                StackSubAulas.Add(styleList[subAulasPage][j]);
            }
        }
    }
}