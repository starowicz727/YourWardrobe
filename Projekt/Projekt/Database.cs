using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;

namespace Projekt
{
    public class Database
    {
        readonly SQLiteAsyncConnection myDatabase;

        public Database(string dbPath) //konstruktor potrzebuje ścieżki dostępu do bazy danych
        {
            myDatabase = new SQLiteAsyncConnection(dbPath); //tworzy połączenie asynchroniczne z bazą danych
            myDatabase.CreateTableAsync<Clothes>().Wait();//tworzy tabelę na podstawie klasy Clothes
                                                          //jeśli tabela istnieje już w bazie to nie jest tworzona
            myDatabase.CreateTableAsync<ClothesOnTheDay>().Wait();//tworzy tabelę na podstawie klasy ClothesOnTheDay
        }

        public Task<List<Clothes>> GetAllClothesAsync()
        {
            return myDatabase.Table<Clothes>().ToListAsync();
        }

        public Task<int> SaveClothesItemAsync(Clothes item)
        {
            return myDatabase.InsertAsync(item);
        }
        public Task<Clothes> GetItemOfID(int id)
        {
            return myDatabase.FindAsync<Clothes>(id);
        }
        public Task<List<Clothes>> GetItemsOfCategoryAsync(string categoryName)
        {
            return myDatabase.QueryAsync<Clothes>("SELECT * FROM [Clothes] WHERE [Category] LIKE \"" + categoryName +"\"");
        }

        public Task<List<Clothes>> DeleteItemAsync(Clothes itemToDelete)
        {
            return myDatabase.QueryAsync<Clothes>("DELETE FROM [Clothes] WHERE [ID] =" + itemToDelete.ID);
        }

        public Task<Clothes> GetRandomItemOfCategoryAsync(string categoryName)
        {
            return myDatabase.FindWithQueryAsync<Clothes>("SELECT * FROM [Clothes] WHERE [Category] LIKE \"" + categoryName + "\" ORDER BY RANDOM() LIMIT 1");
        }

        public Task<List<Clothes>> GetAllFavouriteClothesAsync() 
        {
            return myDatabase.Table<Clothes>().Where(x => x.Favourite == true).ToListAsync(); 
        }

        public Task<List<Clothes>> AddToFavouritesAsync(Clothes itemToAdd) //true=1, false=0 w sqlite
        {
            return myDatabase.QueryAsync<Clothes>("UPDATE [Clothes] SET [Favourite]=1 WHERE [ID] =" + itemToAdd.ID);
        }
        public Task<List<Clothes>> RemoveFromFavouritesAsync(Clothes itemToRemove) //true=1, false=0 w sqlite
        {
            return myDatabase.QueryAsync<Clothes>("UPDATE [Clothes] SET [Favourite]=0 WHERE [ID] =" + itemToRemove.ID);
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public Task<int> SaveItemsOfDateAsync(ClothesOnTheDay item)
        {
            return myDatabase.InsertAsync(item);
        }
        public Task<List<ClothesOnTheDay>> GetItemsOfDateAsync(string selectedDate)
        {
            return myDatabase.QueryAsync<ClothesOnTheDay>("SELECT * FROM [ClothesOnTheDay] WHERE [Date] LIKE \"" + selectedDate + "\"");
        }
        public Task<List<ClothesOnTheDay>> DeleteItemsOfDateAsync(int itemToDeleteID, string selectedDate) //usuwanie jednego elementu Clothes w danym dniu z kalendarza 
        {
            return myDatabase.QueryAsync<ClothesOnTheDay>("DELETE FROM [ClothesOnTheDay] WHERE [IDDate] = (SELECT [IDDate] FROM [ClothesOnTheDay] WHERE [Date] LIKE \"" + selectedDate+ "\" AND [ClothesID] =" + itemToDeleteID + " LIMIT 1 )");
        }
        public Task<List<ClothesOnTheDay>> DeleteIfClothesDeletedAsync(int deletedClothesID) //usuwanie ubrań w kelandarzu jeśli zostały usunięte w ShowItemPage
        {
            return myDatabase.QueryAsync<ClothesOnTheDay>("DELETE FROM [ClothesOnTheDay] WHERE [ClothesID] = " + deletedClothesID);
        }

    }
}

