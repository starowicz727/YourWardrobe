using System;
using System.Collections.Generic;
using SQLite;

namespace Projekt
{
    public class ClothesOnTheDay
    {
        [PrimaryKey, AutoIncrement]
        public int IDDate { get; set; }
        [NotNull]
        public string Date { get; set; }
        [NotNull]
        public int ClothesID { get; set; }
    }
}
