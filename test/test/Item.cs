using System;

namespace TextRPG
{
    class Item
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int HealAmount { get; private set; }
        public int Price { get; private set; }
        public int AttackBoost { get; private set; } 
        public int DefenseBoost { get; private set; } 
        public bool IsEquipped { get; set; } 

        public Item(string name, string description, int healAmount, int price, int attackBoost = 0, int defenseBoost = 0)
        {
            Name = name;
            Description = description;
            HealAmount = healAmount;
            Price = price;
            AttackBoost = attackBoost;
            DefenseBoost = defenseBoost;
            IsEquipped = false;
        }

    
        public void Use(Player player)
        {
            if (HealAmount > 0) 
            {
               
                Console.WriteLine($"{player.Name}(이)가 {Name}을(를) 사용하여 체력을 {HealAmount} 회복했습니다.");
                player.Health += HealAmount;
                if (player.Health > 100) player.Health = 100; 
                Console.WriteLine($"{player.Name}의 현재 체력은 {player.Health}입니다.");
            }
            else if (AttackBoost > 0 || DefenseBoost > 0) 
            {
                player.EquipOrUnequipItem(this); 
            }
            else
            {
                Console.WriteLine("이 아이템은 사용할 수 없습니다.");
            }
        }
    }
}
