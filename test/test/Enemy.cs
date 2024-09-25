using System;

namespace TextRPG
{
    class Enemy
    {
        public string Name { get; private set; }
        public int Health { get; set; }
        public int AttackPower { get; private set; }

        public Enemy(string name, int health, int attackPower)
        {
            Name = name;
            Health = health;
            AttackPower = attackPower;
        }

        public void Attack(Player player)
        {
            int damage = AttackPower - player.DefensePower;
            if (damage < 0) damage = 0;
            Console.WriteLine($"{Name}(이)가 {player.Name}에게 {damage}의 피해를 입혔습니다.");
            player.Health -= damage;
        }
    }
}

