using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Projekt
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InspoPage : ContentPage
    {
        public InspoPage()
        {
            InitializeComponent();
        }


        private void ButtonFav_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ShowFavPage());
        }

        private void ButtonLoupe_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ShufflePage());
        }
    }
}