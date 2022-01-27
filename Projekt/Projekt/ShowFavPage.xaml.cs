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
    public partial class ShowFavPage : ContentPage
    {
        public ShowFavPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionFavView.ItemsSource = await App.Database.GetAllFavouriteClothesAsync();
        }

        private void collectionFavView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void myDateFavPicker_DateSelected(object sender, DateChangedEventArgs e)
        {
        }

        private async void ButtonSaveFavToCalendar_Clicked(object sender, EventArgs e) //przypisywanie ulubionych ubrań do konkretnego dnia w kalendarzu
        {
            if (collectionFavView.SelectedItems.Count > 0)
            {
                for (int i = 0; i < collectionFavView.SelectedItems.Count; i++)
                {
                    Clothes itemSelected = collectionFavView.SelectedItems[i] as Clothes;
                    await App.Database.SaveItemsOfDateAsync(new ClothesOnTheDay
                    {
                        Date = myDateFavPicker.Date.ToString(),
                        ClothesID = itemSelected.ID
                    });
                }
                // await (sender as VisualElement).DisplayToastAsync("Added to your calendar", 2000);
                await DisplayAlert(null, "Added to your calendar", "ok");

            }
            else
            {
                await DisplayAlert(null, "You must pick an item first", "ok");
            }
        }

        private async void ButtonFavBin_Clicked(object sender, EventArgs e) //usuwanie przedmiotow z ulubionych 
        {
            if (collectionFavView.SelectedItems.Count > 0)
            {
                string actionDelete = await DisplayActionSheet("Are you sure you want to remove this?", "Cancel", "Remove");
                if (actionDelete.Equals("Remove"))
                {
                    for (int i = 0; i < collectionFavView.SelectedItems.Count; i++)
                    {
                        Clothes itemSelected = collectionFavView.SelectedItems[i] as Clothes;
                        await App.Database.RemoveFromFavouritesAsync(itemSelected);
                    }
                    collectionFavView.ItemsSource = await App.Database.GetAllFavouriteClothesAsync();
                    await DisplayAlert(null, "Successfully removed", "ok");
                }
            }
            else
            {
                await DisplayAlert(null, "You must pick an item first", "ok");
            }
        }

        private void ButtonFavCalendar_Clicked(object sender, EventArgs e) //kontrola widoczności datepickera i przycisku save
        {
            if (myDateFavPicker.IsVisible == true)
            {
                Device.BeginInvokeOnMainThread(() => {
                    myDateFavPicker.IsVisible = false;
                    ButtonSaveFavToCalendar.IsVisible = false;
                });
            }
            else
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    myDateFavPicker.IsVisible = true;
                    ButtonSaveFavToCalendar.IsVisible = true;
                });

            }
        }
    }
}