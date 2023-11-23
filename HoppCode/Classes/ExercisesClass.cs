using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoppCode.Classes
{
    internal class ExercisesClass
    {

        //Cria objeto do identificar aula
        IdentificarAulaOuExercicioPage identificarAulaOuExercicio = new IdentificarAulaOuExercicioPage();

        public async Task<Frame> ReadJsonAndReturStyle(string classe, string aula)
        {
            string exercicioText = await identificarAulaOuExercicio.JsonReturnExercicioTexto();

            Frame frame = new Frame()
            {
                WidthRequest = 200,
                HeightRequest = 200,
                HorizontalOptions = new LayoutOptions(LayoutAlignment.Center, true),
            };
            Label label = new Label()
            {
                Text = exercicioText,
                HorizontalOptions = new LayoutOptions(LayoutAlignment.Center, true),
            };

            frame.Content = label;

            return frame;
        }   
    }
}
