using System;
using System.Numerics;
using static TextRPG.Art;

namespace TextRPG
{
    class Game
    {
        Player player;
        Enemy enemy;
        bool gameRunning;
        Shop shop;


       
        public static void VOICE(string message, int delay)
        {
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
        }




        public void Start()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(titleArt);
            Console.ForegroundColor = ConsoleColor.White;
            VOICE("스파르타 던전에 오신 여러분을 환영합니다!",100);
            Thread.Sleep(1000);
         
            CreatePlayer();


            shop = new Shop(); 
            gameRunning = true;

            while (gameRunning)
            {
                ShowMainMenu();
            }
        }

        void CreatePlayer()
        {
            Console.WriteLine("\n원하시는 이름을 설정해주세요:");
            string name = Console.ReadLine();

            Console.WriteLine("\n직업을 선택하세요:");
            Console.WriteLine("1. 전사 ");
            Console.WriteLine("2. 도적 ");
            Console.WriteLine("3. 궁수 ");
            string jobChoice = Console.ReadLine();
            string job = "";
            int attack = 0, defense = 0;

            switch (jobChoice)
            {
                case "1":
                    job = "전사";
                    attack = 15;
                    defense = 5;
                    break;
                case "2":
                    job = "도적";
                    attack = 12;
                    defense = 3;
                    break;
                case "3":
                    job = "궁수";
                    attack = 10;
                    defense = 7;
                    break;
                default:
                    Console.WriteLine("잘못된 선택입니다. 평민으로 설정됩니다.");
                    job = "평민";
                    attack =  5;
                    defense = 5;
                    break;
            }

            //player = new Player(이름,직업,레벨,체력,공격력,방어력,소지금)
            player = new Player(name, job, 1, 100, attack, defense, 1000); 
            VOICE($"\n\n{player.Name}, {player.Job}의 길을 걷습니다.",100);
            player.Inventory.AddItem(new Item("체력 포션", "체력을 20 회복합니다", 20, 50));
        }

        void ShowMainMenu()
        {
            
            Console.WriteLine("\n1. 마을 방문");
            Console.WriteLine("2. 왕초보사냥터 가기");
            Console.WriteLine("3. 플레이어 상태 확인");
            Console.WriteLine("4. 인벤토리 확인");
            Console.WriteLine("5. 게임 종료");
            Console.WriteLine("");
            VOICE("\n원하시는 행동을 선택하세요: ", 50);
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine(townArt);
                    VisitTown();
                    break;
                case "2":
                    Console.Clear();
                    StartBattle();
                    break;
                case "3":
                    ShowPlayerStats();
                    break;
                case "4":
                    UseItem();
                    break;
                case "5":
                    gameRunning = false;
                    Console.WriteLine("게임을 종료합니다...");
                    break;
                default:
                    Console.WriteLine("잘못된 선택입니다. 다시 시도하세요.");
                    break;
            }
        }

        void VisitTown()
        {
            bool inTown = true;
            while (inTown)
            {
          

                VOICE("\n마을에 도착했습니다.", 100);
                Console.WriteLine("\n1. 휴식 (500 골드 필요)");
                Console.WriteLine("2. 상점 방문");
                Console.WriteLine("3. 마을 떠나기");
                VOICE("\n원하시는 행동을 선택하세요: ", 100);



            string townChoice = Console.ReadLine();

                switch (townChoice)
                {
                    case "1":
                        Rest();
                        break;
                    case "2":
                        VisitShop();
                        break;
                    case "3":
                        inTown = false;
                        Console.WriteLine("마을을 떠났습니다.");
                        break;
                    default:
                        Console.WriteLine("잘못된 선택입니다. 다시 시도하세요.");
                        break;
                }
            }
        }

       

        void Rest()
        {
            if (player.SpendGold(500))
            {
                Console.WriteLine("여관에서 휴식을 취하여 체력을 모두 회복했습니다.");
                player.Health = 100;
                Console.WriteLine($"현재 체력: {player.Health}");
            }
            else
            {
                Console.WriteLine("골드가 부족하여 휴식할 수 없습니다.");
            }
        }

        void StartBattle()
        {
            enemy = new Enemy("다람쥐", 50, 5);
            Console.WriteLine(monsterArt1);
            VOICE($"\n야생의 {enemy.Name}(이)가 나타났습니다!",100);

            while (player.Health > 0 && enemy.Health > 0)
            {
                Console.WriteLine($"\n플레이어 체력: {player.Health}, 적 체력: {enemy.Health}");
                Console.WriteLine("1. 공격");
                Console.WriteLine("2. 도망가기");
                Console.WriteLine("3. 인벤토리 사용");
                VOICE("\n원하시는 행동을 선택하세요: ",50);
                string action = Console.ReadLine();

                switch (action)
                {
                    case "1":
                        player.Attack(enemy);
                        if (enemy.Health > 0)
                        {
                            enemy.Attack(player);
                        }
                        break;
                    case "2":
                        Console.WriteLine("전투에서 도망쳤습니다!");
                        return;
                    case "3":
                        UseItem();
                        break;
                    default:
                        Console.WriteLine("잘못된 행동입니다. 다시 시도하세요.");
                        break;
                }
            }

            if (player.Health > 0)
            {
                Console.WriteLine($"{enemy.Name}(을)를 물리쳤습니다!");
                player.GainGold(20); 
                player.LevelUp();
            }
            else
            {
                Console.WriteLine("패배했습니다.");
                gameRunning = false;
            }
        }

        void ShowPlayerStats()
        {
            Console.ForegroundColor = ConsoleColor.Green;
           
            VOICE($"\n플레이어 정보: \n" +
                              $"이름: {player.Name}\n" +
                              $"직업: {player.Job}\n" +
                              $"Lv.: {player.Level}\n" +
                              $"체력: {player.Health}\n" +
                              $"공격력: {player.AttackPower}\n" +
                              $"방어력: {player.DefensePower}\n" +
                              $"Gold: {player.Gold}G",20);

            Console.ForegroundColor = ConsoleColor.White;
        }



        void UseItem()
        {
            player.Inventory.UseItem(player);
        }

        void VisitShop()
        {
            Console.WriteLine("\n상점에 도착했습니다!");
            shop.ShowItems();
            Console.WriteLine("\n1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("3. 돌아가기");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    shop.BuyItem(player);
                    break;

                case "2":
                    shop.SellItem(player);
                    break;

                case "3":
                    return;

                default:
                    Console.WriteLine("잘못된 선택입니다.");
                    break;
            }
        }
    }
}

