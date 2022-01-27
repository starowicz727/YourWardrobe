using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace Projekt
{
    public partial class MainPage : ContentPage
    {
        public static string category ="main";
        public MainPage()
        {
            InitializeComponent();
        }
        //protected override async void OnAppearing()
        //{
        //    base.OnAppearing();
        //    //listView.ItemsSource = await App.Database.GetOsobyAsync();
        //}

       
        private void ButtonAccessories_Clicked(object sender, EventArgs e)
        {
            category = "Accessories";
            Navigation.PushAsync(new ShowItemsPage());
        }

        private void ButtonShirt_Clicked(object sender, EventArgs e)
        {
            category = "Shirt";
            Navigation.PushAsync(new ShowItemsPage());
        }

        private void ButtonSkirt_Clicked(object sender, EventArgs e)
        {
            category = "Skirt";
            Navigation.PushAsync(new ShowItemsPage());
        }

        private void ButtonDress_Clicked(object sender, EventArgs e)
        {
            category = "Dress";
            Navigation.PushAsync(new ShowItemsPage());
        }

        private void ButtonPants_Clicked(object sender, EventArgs e)
        {
            category = "Pants";
            Navigation.PushAsync(new ShowItemsPage());
        }

        private void ButtonJacket_Clicked(object sender, EventArgs e)
        {
            category = "Jacket";
            Navigation.PushAsync(new ShowItemsPage());
        }

        private void ButtonShoes_Clicked(object sender, EventArgs e)
        {
            category = "Shoes";
            Navigation.PushAsync(new ShowItemsPage());
        }

        private void ButtonBag_Clicked(object sender, EventArgs e)
        {
            category = "Bag";
            Navigation.PushAsync(new ShowItemsPage());
        }

        private void ButtonMainCalendar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CalendarPage());
        }
        private void ButtonAddItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddItemPage());
        }

        private void ButtonInspo_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new InspoPage());
        }
    }
}
