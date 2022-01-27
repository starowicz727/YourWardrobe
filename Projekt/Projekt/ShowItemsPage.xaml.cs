using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.CommunityToolkit.Extensions;

namespace Projekt
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowItemsPage : ContentPage
    {
        public ShowItemsPage()
        {
            SettingTitle();
            InitializeComponent();
        }

        //Przesłonięcie metody OnAppearing jest wykonywane po określeniu układu obiektu ContentPage,
        //ale tuż przed tym, gdy stanie się on widoczny.
        //Dlatego jest to dobre miejsce do ustawiania zawartości widoków platformy Xamarin.Forms.
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await App.Database.GetItemsOfCategoryAsync(MainPage.category);
        }

        public void SettingTitle()
        {
            string title = "";

            if(MainPage.category== "Accessories" || MainPage.category == "Pants" || MainPage.category == "Shoes")
            {
                title = "Your "+ MainPage.category;
            }
            else if(MainPage.category == "Dress")
            {
                title = "Your Dresses";
            }
            else
            {
                title = "Your " + MainPage.category + "s";
            }
            Device.BeginInvokeOnMainThread(() => {
                lblTitle.Text = title;
            });
        }

        private void collectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

    async private void ButtonBin_Clicked(object sender, EventArgs e) //usuwanie wybranych ubrań z bazy danych 
        {
            if (collectionView.SelectedItems.Count > 0) //jeśli ubrania do usunięcia zostały wybrane 
            {
                string actionDelete = await DisplayActionSheet("Are you sure you want to delete this?", "Cancel", "Delete");
                if (actionDelete.Equals("Delete"))
                {
                    for(int i =0; i< collectionView.SelectedItems.Count; i++) //każde kliknięte ubranie 
                    {
                        Clothes itemSelected = collectionView.SelectedItems[i] as Clothes;
                        await App.Database.DeleteIfClothesDeletedAsync(itemSelected.ID);//usuwanie ubrań najpierw w kelandarzu 
                        await App.Database.DeleteItemAsync(itemSelected);   //usuwanie ubrań w ShowItemsPage
                    }
                    collectionView.ItemsSource = await App.Database.GetItemsOfCategoryAsync(MainPage.category); //ponowne wczytanie widoku kolekcji
                    // await (sender as VisualElement).DisplayToastAsync("Successfully deleted", 2000);
                    await DisplayAlert(null, "Successfully deleted", "ok");
                }
            }
            else
            {
                await DisplayAlert(null,"You must pick an item first", "ok");
            }
        }

        private void ButtonCalendar_Clicked(object sender, EventArgs e) //kontrola widoczności datepickera i przycisku save
        {
            // myDatePicker.IsVisible = true;
            
            if (myDatePicker.IsVisible == true)
            {
                Device.BeginInvokeOnMainThread(() => {
                    myDatePicker.IsVisible = false;
                    ButtonSaveToCalendar.IsVisible = false;
                });
            }
            else
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    myDatePicker.IsVisible = true;
                    ButtonSaveToCalendar.IsVisible = true;
                });
            }
        }

        private void myDatePicker_DateSelected(object sender, DateChangedEventArgs e) 
        {             
        }

        async private void ButtonSaveToCalendar_Clicked(object sender, EventArgs e)  //przypisywanie wybranych ubrań
        {                                                                   //do konkretnego dnia w kalendarzu
            if (collectionView.SelectedItems.Count > 0)
            {
                for (int i = 0; i < collectionView.SelectedItems.Count; i++)
                {
                    Clothes itemSelected = collectionView.SelectedItems[i] as Clothes;
                    await App.Database.SaveItemsOfDateAsync(new ClothesOnTheDay
                    {
                        Date = myDatePicker.Date.ToString(),
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

        private async void ButtonFav_Clicked(object sender, EventArgs e) //dodawanie ubrań do ulubionych
        {
            if (collectionView.SelectedItems.Count > 0)
            {
                int added = 0;
                for (int i = 0; i < collectionView.SelectedItems.Count; i++)
                {
                    Clothes itemSelected = collectionView.SelectedItems[i] as Clothes;
                    if (itemSelected.Favourite) //jeśli przedmiot znajduje się już wśród ulubionych 
                    {
                        await DisplayAlert(null, "At least one picked item is already in your favorites", "ok");
                    }
                    else
                    {
                        await App.Database.AddToFavouritesAsync(itemSelected);
                        added++;
                    }
                }
                // await (sender as VisualElement).DisplayToastAsync("Added to your favourites", 2000);
                if (added > 0)
                {
                    await DisplayAlert(null, "Added to your favorites", "ok");
                }
            }
            else
            {
                await DisplayAlert(null, "You must pick an item first", "ok");
            }
        }
    }
}