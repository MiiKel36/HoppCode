namespace HoppCode;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		// Caminhos para outras paginas
		Routing.RegisterRoute("AulasPage", typeof(AulasPage));
		Routing.RegisterRoute("ClassesPage", typeof(Pages.ClassesPage));
        Routing.RegisterRoute("SubAulasPage", typeof(Pages.SubAulasPage));
        Routing.RegisterRoute("ExercisesPage", typeof(Pages.ExercisesPage));
        Routing.RegisterRoute("LoginPage", typeof(Pages.LoginPage));
        Routing.RegisterRoute("CadastroPage", typeof(Pages.CadastroPage));
        Routing.RegisterRoute("IdentficarAulaOuExercicio", typeof(Pages.IdentficarAulaOuExercicio));
		
    }
}
