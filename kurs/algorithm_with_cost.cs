namespace Knapsack
{
    internal class algorithm_with_cost
    {
        // Массив для хранения максимальной стоимости для заданного веса рюкзака
        public static int[,] arr;

        // Массив для хранения набора предметов, дающего максимальную стоимость
        public static string[,] arr_items;

        /// <summary>
        /// Основной метод для вычисления максимальной стоимости рюкзака.
        /// </summary>
        /// <param name="items">Массив предметов.</param>
        /// <param name="maxCapacity">Максимальная вместимость рюкзака.</param>
        /// <param name="c3">Флаг задачи: предметы в единственном экземпляре.</param>
        /// <param name="c2">Флаг задачи: предметы в неограниченном количестве.</param>
        /// <param name="c4">Флаг задачи: предметы в ограниченном количестве.</param>
        /// <returns>Максимальная стоимость рюкзака.</returns>
        public static int max_cost(Item[] items, int maxCapacity, bool c2, bool c3, bool c4)
        {
            int n = items.Length; // Количество предметов
            arr = new int[n + 1, maxCapacity + 1]; // Массив для хранения максимальных стоимостей
            arr_items = new string[n + 1, maxCapacity + 1]; // Массив для хранения выбранных предметов

            // Инициализация массивов
            for (int i = 0; i <= n; i++)
            {
                for (int j = 0; j <= maxCapacity; j++)
                {
                    arr[i, j] = 0; // Все стоимости по умолчанию равны 0
                    arr_items[i, j] = ""; // Изначально набор предметов пуст
                }
            }

            // Основной цикл по предметам
            for (int i = 1; i <= n; i++) // Обрабатываем каждый предмет
            {
                var currentItem = items[i - 1]; // Текущий предмет (индекс в массиве на 1 меньше i)
                for (int j = 1; j <= maxCapacity; j++) // Цикл по всем возможным весам рюкзака
                {
                    // Если текущий предмет не помещается в рюкзак
                    if (currentItem.Weight > j)
                    {
                        // Сохраняем значения из предыдущей строки (без учета текущего предмета)
                        arr[i, j] = arr[i - 1, j];
                        arr_items[i, j] = arr_items[i - 1, j];
                        continue;
                    }

                    // Рассчитываем максимальную стоимость для текущего состояния
                    int prev = arr[i - 1, j]; // Стоимость без учета текущего предмета
                    int byFormula = 0; // Переменная для расчета стоимости с учетом текущего предмета
                    string newItemSet = ""; // Набор предметов, дающий максимальную стоимость

                    if (c3) // Задача: каждый предмет в единственном экземпляре
                    {
                        byFormula = currentItem.Cost + arr[i - 1, j - currentItem.Weight]; // Формула для добавления предмета
                        newItemSet = arr_items[i - 1, j - currentItem.Weight] + " " + currentItem.Name; // Обновляем набор
                    }
                    else if (c2) // Задача: каждый предмет в неограниченном количестве
                    {
                        byFormula = currentItem.Cost + arr[i, j - currentItem.Weight]; // Формула для добавления предмета
                        newItemSet = arr_items[i, j - currentItem.Weight] + " " + currentItem.Name; // Обновляем набор
                    }
                    else if (c4) // Задача: каждый предмет в ограниченном количестве
                    {
                        // Перебираем количество предметов от 1 до максимального доступного количества
                        for (int k = 1; k <= currentItem.Quantity && currentItem.Weight * k <= j; k++)
                        {
                            int tempByFormula = currentItem.Cost * k + arr[i - 1, j - currentItem.Weight * k]; // Формула для k предметов
                            if (tempByFormula > byFormula) // Если текущая стоимость больше предыдущей
                            {
                                byFormula = tempByFormula; // Обновляем максимальную стоимость
                                newItemSet = arr_items[i - 1, j - currentItem.Weight * k] +
                                             $" {currentItem.Name}({k} шт.)"; // Обновляем набор предметов
                            }
                        }
                    }

                    // Сравниваем стоимость с учетом текущего предмета и без него
                    if (byFormula > prev)
                    {
                        arr[i, j] = byFormula; // Сохраняем максимальную стоимость
                        arr_items[i, j] = newItemSet; // Сохраняем набор предметов
                    }
                    else
                    {
                        arr[i, j] = prev; // Оставляем стоимость без учета текущего предмета
                        arr_items[i, j] = arr_items[i - 1, j]; // Оставляем набор без текущего предмета
                    }
                }
            }

            // Возвращаем максимальную стоимость из правой нижней ячейки
            return arr[n, maxCapacity];
        }
    }
}
