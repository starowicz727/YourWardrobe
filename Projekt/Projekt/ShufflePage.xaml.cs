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
    public partial class ShufflePage : ContentPage
    {
        private static List<string> AllCategories = new List<string> { "Accessories", "Shirt", "Skirt", "Dress", "Pants", "Jacket", "Shoes", "Bag" }; 

        public ShufflePage()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            collectionShuffleView.ItemsSource = await Shuffle (AllCategories);
        }

        private void collectionShuffleView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void myDateShufflePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
        }

        private async void ButtonSaveShuffleToCalendar_Clicked(object sender, EventArgs e) //przypisywanie zaznaczonych ubrań do wybranego dnia w kalendarzu 
        {
            if (collectionShuffleView.SelectedItems.Count > 0)
            {
                for (int i = 0; i < collectionShuffleView.SelectedItems.Count; i++)
                {
                    Clothes itemSelected = collectionShuffleView.SelectedItems[i] as Clothes;
                   // if(itemSelected != null)
                //    {
                        await App.Database.SaveItemsOfDateAsync(new ClothesOnTheDay
                        {
                            Date = myDateShufflePicker.Date.ToString(),
                            ClothesID = itemSelected.ID
                        });
                //    }
                }
                // await (sender as VisualElement).DisplayToastAsync("Added to your calendar", 2000);
                await DisplayAlert(null, "Added to your calendar", "ok");

            }
            else
            {
                await DisplayAlert(null, "You must pick an item first", "ok");
            }
        }

        private void ButtonShuffleCalendar_Clicked(object sender, EventArgs e) //kontrola widoczności datepickera i przycisku save
        {
            if (myDateShufflePicker.IsVisible == true)
            {
                Device.BeginInvokeOnMainThread(() => {
                    myDateShufflePicker.IsVisible = false;
                    ButtonSaveShuffleToCalendar.IsVisible = false;
                });
            }
            else
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    myDateShufflePicker.IsVisible = true;
                    ButtonSaveShuffleToCalendar.IsVisible = true;
                });
            }
        }

        private async void ButtonShuffle_Clicked(object sender, EventArgs e) // po naciśnięciu tego przycisku losują się ubrania 
        {
            if (collectionShuffleView.SelectedItems.Count == 0) //jeśli nie kliknięto w żaden przedmiot, losuje się na nowo wszystkie przedmioty
            {
                collectionShuffleView.ItemsSource = await Shuffle(AllCategories);
            }
            else if (collectionShuffleView.SelectedItems.Count > 0) // jeśli kliknięto na chociaż jeden przedmiot to losujemy nowy obrazek tylko dla klikniętej kategorii
            {
                List<string> selectedCategories = new List<string>();
                for (int i = 0; i < collectionShuffleView.SelectedItems.Count; i++)
                {
                    Clothes itemSelected = collectionShuffleView.SelectedItems[i] as Clothes;
                    selectedCategories.Add(itemSelected.Category);
                }
                collectionShuffleView.ItemsSource = await Shuffle(selectedCategories);
            }
        }

        private async Task<List<Clothes>> Shuffle(List<string> listOfCategories)
        {
            List<Clothes> listOfRandomClothes = new List<Clothes>();

            for(int i=0; i< listOfCategories.Count; i++)
            {
                Clothes randomItem = await App.Database.GetRandomItemOfCategoryAsync(listOfCategories[i]);
                if (randomItem != null) //jeśli w danej kategorii nie ma żadnych Clothes, to do collectionView i tak był dodawany obiekt null
                {
                    listOfRandomClothes.Add(randomItem);
                }
            }

            return listOfRandomClothes;
        }
    }
}