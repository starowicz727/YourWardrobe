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
    public partial class CalendarPage : ContentPage
    {
        public CalendarPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            SelectingClothes(myDatePickerCalendar.Date.ToString());
        }

        async private void SelectingClothes(String dateToSelect) //odczyt ubrań z bazy które przypisane są do danego dnia
        {
            List<ClothesOnTheDay> clothesOnTheDayDB = await App.Database.GetItemsOfDateAsync(dateToSelect);
            List<Clothes> clothesToShow = new List<Clothes>();

            for (int i = 0; i < clothesOnTheDayDB.Count; i++) 
            {
                clothesToShow.Add(await App.Database.GetItemOfID(clothesOnTheDayDB[i].ClothesID));
            }
            collectionViewCalendar.ItemsSource = clothesToShow;
        }
        private void collectionViewCalendar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private async void ButtonBinCallendar_Clicked(object sender, EventArgs e)
        {
            if (collectionViewCalendar.SelectedItems.Count > 0)
            {
                string actionDelete = await DisplayActionSheet("Are you sure you want to remove this?", "Cancel", "Remove");
                if (actionDelete.Equals("Remove"))
                {
                    for (int i = 0; i < collectionViewCalendar.SelectedItems.Count; i++)
                    {
                        Clothes itemSelected = collectionViewCalendar.SelectedItems[i] as Clothes;
                        await App.Database.DeleteItemsOfDateAsync(itemSelected.ID, myDatePickerCalendar.Date.ToString());
                    }
                    SelectingClothes(myDatePickerCalendar.Date.ToString()); // ponowne wczytanie widoku kolekcji 
                    await DisplayAlert(null, "Successfully removed", "ok");
                }
            }
            else
            {
                await DisplayAlert(null, "You must pick an item first", "ok");
            }
        }

        private void myDatePickerCalendar_DateSelected(object sender, DateChangedEventArgs e)
        {
            SelectingClothes(myDatePickerCalendar.Date.ToString());
        }
    }
}