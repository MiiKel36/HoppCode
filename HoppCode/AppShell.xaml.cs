namespace HoppCode;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		//Caminhos para outras paginas

		Routing.RegisterRoute("AulasPage", typeof(AulasPage));
		Routing.RegisterRoute("ClassesPage", typeof(Pages.ClassesPage));
    }
}
