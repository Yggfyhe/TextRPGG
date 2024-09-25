using System;

namespace TextRPG
{
    class Shop
    {
        private List<Item> shopItems;

        public Shop()
        {
            shopItems = new List<Item>
            {
                new Item("스파르타의 창", "ㅣ공격력 +7ㅣ 스파르타의 전사들이 사용했다는 전설의 창입니다.", 0, 50, attackBoost: 7),
                new Item("무쇠갑옷", "ㅣ방어력 +5ㅣ 무쇠로 만들어져 튼튼한 갑옷입니다.", 0, 30, defenseBoost: 5)
            };
        }

        public void ShowItems()
        {
            Console.WriteLine("\n상점 상품 목록:");
            for (int i = 0; i < shopItems.Count; i++)
            {
                Console.WriteLine($"{(i + 1)} {shopItems[i].Name} - {shopItems[i].Description} (가격: {shopItems[i].Price} 골드)");
            }
        }

        public void BuyItem(Player player)
        {
            ShowItems();
            Console.WriteLine("\n구매할 아이템 번호를 선택하세요 (0을 입력하면 취소):");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int choice) && choice > 0 && choice <= shopItems.Count)
            {
                Item selectedItem = shopItems[choice - 1];
                if (player.SpendGold(selectedItem.Price))
                {
                    player.Inventory.AddItem(selectedItem);
                    Console.WriteLine($"{selectedItem.Name}(을)를 구매했습니다!");
                }
                else
                {
                    Console.WriteLine("골드가 부족합니다.");
                }
            }
            else
            {
                Console.WriteLine("구매가 취소되었습니다.");
            }


            Anythingelse(player);
        }


        private void Anythingelse(Player player)
        {
            Console.WriteLine("\n추가로 할 일이 있습니까?");
            Console.WriteLine("1. 다른 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("3. 상점 나가기");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    BuyItem(player); 
                    break;
                case "2":
                    SellItem(player); 
                    break;
                case "3":
                    Console.WriteLine("상점을 나갑니다.");
                    break;
                default:
                    Console.WriteLine("잘못된 선택입니다. 상점을 나갑니다.");
                    break;
            }
        }

        public void SellItem(Player player)
        {
            player.Inventory.ShowItems();
            Console.WriteLine("\n판매할 아이템 번호를 선택하세요 (0을 입력하면 취소):");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int choice) && choice > 0 && choice <= player.Inventory.items.Count)
            {
                Item selectedItem = player.Inventory.items[choice - 1];
                player.GainGold(selectedItem.Price / 2); 
                player.Inventory.RemoveItem(selectedItem);
                Console.WriteLine($"{selectedItem.Name}(을)를 판매했습니다.");
            }
            else
            {
                Console.WriteLine("판매가 취소되었습니다.");
            }
      
            Anythingelse(player);
        }
    }
}