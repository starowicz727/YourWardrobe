using SQLite;

public class Clothes
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
	[NotNull]
    public string Category { get; set; }//Accessories,Shirt,Skirt,Dress,Pants,Jacket,Shoes,Bag
	[NotNull]
	public string Image { get; set; } //ścieżka do pliku

	public bool Favourite { get; set; } // czy dany przedmiot należy do ulubionych 

	//public Clothes(int id, string category, string image)
	//{
	//	this.id = id;
	//	this.category = category;
	//	this.image = image;
	//}
}
