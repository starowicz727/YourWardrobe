using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Xamarin.CommunityToolkit.Extensions;
using System.IO;


namespace Projekt
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddItemPage : ContentPage
    {
        public string selectedCategory = "Accessories";
        public string selectedSource = "photo.png";
        public AddItemPage()
        {
            InitializeComponent();
            pickerItemType.SelectedIndex = 0;
        }

        private void pickerItemType_SelectedIndexChanged(object sender, EventArgs e) 
        {
            if (pickerItemType.SelectedIndex == 0) { selectedCategory = "Accessories"; }
            else if (pickerItemType.SelectedIndex == 1) { selectedCategory = "Shirt"; }
            else if (pickerItemType.SelectedIndex == 2) { selectedCategory = "Skirt"; }
            else if (pickerItemType.SelectedIndex == 3) { selectedCategory = "Dress"; }
            else if (pickerItemType.SelectedIndex == 4) { selectedCategory = "Pants"; }
            else if (pickerItemType.SelectedIndex == 5) { selectedCategory = "Jacket"; }
            else if (pickerItemType.SelectedIndex == 6) { selectedCategory = "Shoes"; }
            else if (pickerItemType.SelectedIndex == 7) { selectedCategory = "Bag"; }
        }

        private async void ButtonSaveItem_Clicked(object sender, EventArgs e)
        {
            if (selectedSource.Equals("photo.png"))
            {
                await DisplayAlert("", "Please pick an image first", "ok");
            }
            else
            {
                await App.Database.SaveClothesItemAsync(new Clothes
                {
                    Category = selectedCategory,
                    Image = selectedSource,
                    Favourite = false
                });
                resultImage.Source = "photo.png";
                selectedSource = "photo.png";

                await DisplayAlert(null, "Added successfully!", "ok");

                //await (sender as VisualElement).DisplayToastAsync("Added successfully!", 3000);
                //dzięki sender as VisualElement toast pokazuje się nie na samym dole aplikacji
                
            }
        }

        async private void ButtonPickImage_Clicked(object sender, EventArgs e)
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Please pick a photo"
            });

            if (result != null)
            {
                var newFile = Path.Combine(FileSystem.CacheDirectory, result.FileName); //await result.OpenReadAsync();
                using (var stream = await result.OpenReadAsync())
                using (var newStream = File.OpenWrite(newFile))
                    await stream.CopyToAsync(newStream);

                resultImage.Source = newFile;
                selectedSource = newFile;
                //resultImage.Source = ImageSource.FromStream(() => stream);
                //selectedSource = result.FullPath;
                //String myPath = result.FullPath;
                //lblText.Text = myPath;
                //pickedImage.Source = myPath;
            }
        }
    }
}