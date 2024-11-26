namespace Knapsack
{
    internal class algorithm_without_cost
    {
        // Массив для хранения максимального веса
        public static int[,] arr;

        // Массив для хранения набора предметов
        public static string[,] arr_items;

        /// <summary>
        /// Основной метод для вычисления максимального веса рюкзака.
        /// </summary>
        /// <param name="items">Массив предметов.</param>
        /// <param name="maxCapacity">Максимальная вместимость рюкзака.</param>
        /// <param name="c3">Флаг задачи: предметы в единственном экземпляре.</param>
        /// <param name="c2">Флаг задачи: предметы в неограниченном количестве.</param>
        /// <param name="c4">Флаг задачи: предметы в ограниченном количестве.</param>
        /// <returns>Максимальный вес, который можно уложить в рюкзак.</returns>
        public static int max_weight(Item[] items, int maxCapacity, bool c2, bool c3, bool c4)
        {
            int n = items.Length; // Количество предметов
            arr = new int[n + 1, maxCapacity + 1]; // Массив для хранения максимального веса
            arr_items = new string[n + 1, maxCapacity + 1]; // Массив для хранения набора предметов

            // Инициализация массивов
            for (int i = 0; i <= n; i++)
            {
                for (int j = 0; j <= maxCapacity; j++)
                {
                    arr[i, j] = 0; // Максимальный вес по умолчанию равен 0
                    arr_items[i, j] = ""; // Набор предметов изначально пуст
                }
            }

            // Основной цикл по предметам
            for (int i = 1; i <= n; i++)
            {
                var currentItem = items[i - 1]; // Текущий предмет
                for (int j = 1; j <= maxCapacity; j++) // Перебор по весам рюкзака
                {
                    // Если предмет не помещается
                    if (currentItem.Weight > j)
                    {
                        arr[i, j] = arr[i - 1, j];
                        arr_items[i, j] = arr_items[i - 1, j];
                        continue;
                    }

                    int prev = arr[i - 1, j]; // Максимальный вес без текущего предмета
                    int byFormula = 0; // Значение при добавлении предмета
                    string newItemSet = ""; // Новый набор предметов

                    // Задача: каждый предмет в единственном экземпляре
                    if (c3)
                    {
                        byFormula = currentItem.Weight + arr[i - 1, j - currentItem.Weight];
                        newItemSet = arr_items[i - 1, j - currentItem.Weight] + " " + currentItem.Name;
                    }

                    // Задача: каждый предмет в неограниченном количестве
                    else if (c2)
                    {
                        byFormula = currentItem.Weight + arr[i, j - currentItem.Weight];
                        newItemSet = arr_items[i, j - currentItem.Weight] + " " + currentItem.Name;
                    }

                    // Задача: каждый предмет в ограниченном количестве
                    else if (c4)
                    {
                        for (int k = 1; k <= currentItem.Quantity && currentItem.Weight * k <= j; k++)
                        {
                            int tempByFormula = currentItem.Weight * k + arr[i - 1, j - currentItem.Weight * k];
                            if (tempByFormula > byFormula)
                            {
                                byFormula = tempByFormula;
                                newItemSet = arr_items[i - 1, j - currentItem.Weight * k] +
                                             $" {currentItem.Name}({k} шт.)";
                            }
                        }
                    }

                    // Сравнение текущего значения и значения с учетом нового предмета
                    if (byFormula > prev)
                    {
                        arr[i, j] = byFormula;
                        arr_items[i, j] = newItemSet;
                    }
                    else
                    {
                        arr[i, j] = prev;
                        arr_items[i, j] = arr_items[i - 1, j];
                    }
                }
            }

            // Возвращаем максимальный вес из правой нижней ячейки
            return arr[n, maxCapacity];
        }
    }
}
