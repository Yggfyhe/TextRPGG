using System;
using System.Collections.Generic;
using static TextRPG.Game;

namespace TextRPG
{
    class Inventory
    {
        public List<Item> items { get; private set; }

        public Inventory()
        {
            items = new List<Item>();
        }

        public void AddItem(Item item)
        {
            items.Add(item);
            Console.WriteLine($"{item.Name}(이)가 인벤토리에 추가되었습니다.");
        }

        public void RemoveItem(Item item)
        {
            if (items.Contains(item))
            {
                items.Remove(item);
                Console.WriteLine($"{item.Name}(이)가 인벤토리에서 제거되었습니다.");
            }
            else
            {
                Console.WriteLine("아이템이 인벤토리에 없습니다.");
            }
        }

        public void ShowItems()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            VOICE("\n소지품:",25);
            if (items.Count == 0)
            {
                Console.WriteLine("인벤토리가 비어 있습니다.");
            }
            else
            {
                for (int i = 0; i < items.Count; i++)
                {
                    string equippedIndicator = items[i].IsEquipped ? "[E] " : "";  
                   VOICE($"{i + 1}. {equippedIndicator}{items[i].Name} - {items[i].Description}",10);
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void UseItem(Player player)
        {
            ShowItems();
            if (items.Count > 0)
            {
                Console.WriteLine("\n\n사용할 아이템의 번호를 입력하세요:");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int choice) && choice > 0 && choice <= items.Count)
                {
                    Item selectedItem = items[choice - 1];
                    if (selectedItem != null)
                    {
                        selectedItem.Use(player); 
                    }
                    else
                    {
                        Console.WriteLine("선택된 아이템이 없습니다.");
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 선택입니다. 다시 시도하세요.");
                }
            }

            Anythingelse(player);
        }


        private void Anythingelse(Player player)
        {
            Console.WriteLine("\n추가로 할 일이 있습니까?");
            Console.WriteLine("1. 다른 아이템 사용");
            Console.WriteLine("2. 돌아가기");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    UseItem(player); 
                    break;
                case "2":
                    Console.WriteLine("메뉴로 돌아갑니다.");
                    break;
                default:
                    Console.WriteLine("잘못된 선택입니다. 메뉴로 돌아갑니다.");
                    break;
            }
        }
    }
}
