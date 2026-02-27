using System;

CardDeck deck = new CardDeck();
bool playAgain = true;
int chip = 1000; // 초기 칩 수
int betAmount = 0; // 베팅 금액

while (chip > 0 && playAgain)
{
    Console.WriteLine("=== 블랙잭 게임 ===\n");

    Console.WriteLine($"보유 칩: {chip}");
    Console.Write("베팅 금액을 입력하세요: ");

    if (!int.TryParse(Console.ReadLine(), out betAmount))
    {
        Console.WriteLine("숫자만 입력해주세요.\n");
        continue;
    }
    if (betAmount > chip)
    {
        Console.WriteLine("베팅 금액이 보유 칩보다 많습니다. 다시 입력하세요.\n");
        continue;
    }

    Console.WriteLine("\n카드를 섞는 중...\n");

    string[] playerHand = new string[52];
    string[] dealerHand = new string[52];

    int handCount = 0;
    int dealerCount = 0;

    int sum_d = 0;  // 딜러 초기 점수
    int sum_p = 0;  // 플레이어 초기 점수


    CardDeck.AddCard(deck, playerHand, ref handCount); // 플레이어 2장 추가
    CardDeck.AddCard(deck, playerHand, ref handCount);

    CardDeck.AddCard(deck, dealerHand, ref dealerCount); // 딜러 2장 추가
    CardDeck.AddCard(deck, dealerHand, ref dealerCount);


    Console.WriteLine("=== 초기 패 ===");
    Console.Write("딜러 패:");
    ShowDeck.ShowDealerinit(dealerHand, dealerCount, deck, ref sum_d);    // 딜러 패 보여주기 (첫 번째 카드 숨김)

    Console.Write("플레이어의 패:");
    ShowDeck.ShowPlayerHand(playerHand, handCount, deck, ref sum_p);    // 플레이어 패 보여주기


    int inputFlag = 0;
    do
    {
        Console.Write("H(Hit) 또는 S(Stand)를 선택하세요:");
        string input = Console.ReadLine() ?? "";
        if (input.ToLower() == "h")
        {
            CardDeck.AddCard(deck, playerHand, ref handCount);
            Console.WriteLine($"플레이어가 카드를 받았습니다: [{playerHand[handCount - 1]}]");
            Console.Write("플레이어의 패:");
            ShowDeck.ShowPlayerHand(playerHand, handCount, deck, ref sum_p);
        }
        else if (input.ToLower() == "s")
        {
            Console.WriteLine("플레이어가 stand를 선택했습니다.");
            inputFlag = 1;
        }
        else    // 잘못된 입력 처리
        {
            Console.WriteLine("잘못된 입력입니다. H 또는 S를 입력하세요.\n");
        }

        if (sum_p > 21)
        {
            Console.WriteLine("버스트! 21을 초과했습니다.");
            inputFlag = 2;
        }
    } while (inputFlag == 0);


    Console.WriteLine();

    Console.WriteLine($"딜러의 숨겨진 카드: [{dealerHand[0]}]");
    Console.Write("딜러의 패:");
    ShowDeck.ShowDealerHand(dealerHand, dealerCount, deck, ref sum_d);

    int dFlag = 0;
    if (inputFlag != 2)
    {
        while (sum_d < 17)
        {
            CardDeck.AddCard(deck, dealerHand, ref dealerCount);
            Console.WriteLine($"딜러가 카드를 받습니다: [{dealerHand[dealerCount - 1]}]");
            Console.Write("딜러의 패:");
            ShowDeck.ShowDealerHand(dealerHand, dealerCount, deck, ref sum_d);
            if (sum_d >= 17)
            {
                if (sum_d > 21)
                {
                    Console.WriteLine("버스트! 21을 초과했습니다.\n");
                    dFlag = 2;
                }
                else
                {
                    dFlag = 1;
                }
            }
        }
    }
    ShowResult.ShowGameResult(sum_p, sum_d, inputFlag, dFlag, betAmount, ref chip);   // 게임 결과 보여주기

    if (chip == 0)
    {
        Console.WriteLine("\n칩이 모두 소진되었습니다. 게임을 종료합니다.");
        break;
    }
    else
    {
        playAgain = GameRestart.RestartGame();  // 게임 재시작 여부 묻기
    }

    Console.WriteLine();
}