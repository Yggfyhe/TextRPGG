using System;
using System.Collections.Generic;

namespace TextRPG
{
    class Player
    {
        public string Name { get; private set; }
        public string Job { get; private set; }
        public int Level { get; private set; }
        public int Health { get; set; }
        public int AttackPower { get; private set; }
        public int DefensePower { get; private set; }
        public int Gold { get; private set; }
        public Inventory Inventory { get; private set; }
        public List<Item> EquippedItems { get; private set; }

        public Player(string name, string job, int level, int health, int attackPower, int defensePower, int gold)
        {
            Name = name;
            Job = job;
            Level = level;
            Health = health;
            AttackPower = attackPower;
            DefensePower = defensePower;
            Gold = gold;
            Inventory = new Inventory();
            EquippedItems = new List<Item>();
        }

   
        public void EquipOrUnequipItem(Item item)
        {
            if (EquippedItems.Contains(item))
            {
    
                EquippedItems.Remove(item);
                AttackPower -= item.AttackBoost;
                DefensePower -= item.DefenseBoost;
                item.IsEquipped = false;
                Console.WriteLine($"{item.Name}(을)를 장착 해제했습니다! 공격력: {AttackPower}, 방어력: {DefensePower}");
            }
            else
            {
         
                EquippedItems.Add(item);
                AttackPower += item.AttackBoost;
                DefensePower += item.DefenseBoost;
                item.IsEquipped = true;
                Console.WriteLine($"{item.Name}(을)를 장착했습니다! 공격력: {AttackPower}, 방어력: {DefensePower}");
            }
        }

        public void Attack(Enemy enemy)
        {
            Console.WriteLine($"{Name}(이)가 {enemy.Name}에게 {AttackPower}의 피해를 입혔습니다.");
            enemy.Health -= AttackPower;
        }

        public void LevelUp()
        {
            Level++;
            Health += 20;
            AttackPower += 5;
            DefensePower += 3;
            Console.WriteLine($"{Name}(이)가 레벨업했습니다! 레벨: {Level}, 체력: {Health}, 공격력: {AttackPower}, 방어력: {DefensePower}");
        }

        public void GainGold(int amount)
        {
            Gold += amount;
            Console.WriteLine($"{Name}(이)가 {amount} 골드를 얻었습니다. 현재 소지금: {Gold}");
        }

        public bool SpendGold(int amount)
        {
            if (Gold >= amount)
            {
                Gold -= amount;
                return true;
            }
            Console.WriteLine("골드가 부족합니다.");
            return false;
        }
    }
}
