namespace HoppCode;
using Newtonsoft;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;
using Newtonsoft.Json.Linq;
using HoppCode.Classes;

public partial class MainPage : ContentPage
{
    
    public MainPage()
    {
        InitializeComponent();
        
        //funções para verificar e criar pasta de arquivo locar
        CreateLocalStorageFolder createFolder = new CreateLocalStorageFolder();
        createFolder.CreateStorage();

        //manda para a pagina ClassPage
        Shell.Current.GoToAsync("ClassesPage");

    }


}






