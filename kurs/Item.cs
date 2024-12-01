using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knapsack
{
    public class Item
    {
        public string Name { get; set; } // Название предмета
        public int Weight { get; set; } // Вес предмета
        public int Cost { get; set; } // Стоимость предмета (может быть 0 для простого варианта)
        public int Quantity { get; set; } // Количество предметов (0, если неограниченное количество)

        public static Item[] Items { get; set; } // Глобальный массив предметов

        public Item(string name, int weight, int cost, int quantity)
        {
            Name = name;
            Weight = weight;
            Cost = cost;
            Quantity = quantity;
        }
    }
}
