using System;


class CardDeck  // 카드 덱 클래스
{
    // 카드 모양과 숫자 배열
    private readonly string[] Shapes = { "♥", "◆", "♣", "♠" };
    private readonly string[] Numbers = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
    private readonly string[] Cards;
    private string[] usedCards; // 사용된 카드 배열
    private int usedCardCount = 0; // 사용된 카드 개수
    Random number = new Random();

    public CardDeck()   // 생성자로 카드 덱 초기화
    {
        int k = 0;
        Cards = new string[Shapes.Length * Numbers.Length];
        usedCards = new string[52]; // 최대 52장

        for (int i = 0; i < Shapes.Length; i++)
        {
            for (int j = 0; j < Numbers.Length; j++)
            {
                Cards[k++] = $"{Shapes[i]}{Numbers[j]}";
            }
        }
    }

    public void ResetUsedCards()  // 사용된 카드 리스트 초기화
    {
        usedCardCount = 0;  // 사용된 카드 개수 초기화
    }

    public string RandomCard()  // 랜덤으로 카드 한 장 뽑기
    {
        // 모든 카드를 사용했으면 덱 리셋
        if (usedCardCount >= 52)
        {
            Console.WriteLine("\n=== [알림] 모든 카드를 사용했습니다. 덱을 새로 섞습니다 ===\n");
            ResetUsedCards();
        }

        string card;

        do
        {
            int numbers = number.Next(52);  // 0부터 51까지의 랜덤 인덱스 생성
            card = Cards[numbers];
        } while (IsCardUsed(card)); // 이미 사용된 카드면 다시 뽑기

        usedCards[usedCardCount++] = card;  // 뽑은 카드를 사용된 카드 리스트에 추가
        return card;
    }

    private bool IsCardUsed(string card) // 카드가 이미 사용되었는지 확인
    {
        for (int i = 0; i < usedCardCount; i++)
        {
            if (usedCards[i] == card)   // 이미 사용된 카드면 true 반환
            {
                return true;
            }
        }
        return false;
    }



    // 기존에 있던 것
    public static void AddCard(CardDeck deck, string[] hand, ref int handCount) // 카드 한 장 추가하기
    {
        hand[handCount++] = deck.RandomCard(); // RandomCard()에서 이미 중복 검사를 하므로 바로 추가
    }

    public int GetNumValue(string card, int currentsum)     // 문자 카드 숫자로 변환하기
    {
        string numbercard = card.Substring(1);
        if (numbercard == "A")
        {
            if (currentsum + 11 <= 21)
            {
                return 11;
            }
            else
            {
                return 1;
            }
        }
        if (numbercard == "J" || numbercard == "Q" || numbercard == "K")
        {
            return 10;
        }
        return int.Parse(numbercard);
    }
}


class ShowDeck  // 카드 보여주는 클래스
{
    public static void ShowDealerinit(string[] DealerHand, int dealerCount, CardDeck deck, ref int sum_d)   // 딜러 패 보여주기 (첫 번째 카드 숨김)
    {
        for (int i = 0; i < dealerCount; i++)
        {
            if (i == 0)
            {
                Console.Write($"[?] ");
            }
            else { Console.Write($"[{DealerHand[i]}] "); }
            sum_d += deck.GetNumValue(DealerHand[i], sum_d);
        }
        Console.WriteLine($"\n딜러 점수: ?\n");
    }

    public static void ShowDealerHand(string[] DealerHand, int dealerCount, CardDeck deck, ref int sum_d)   // 딜러 패 보여주기 (첫 번째 카드 포함)
    {
        sum_d = 0;
        for (int i = 0; i < dealerCount; i++)
        {
            Console.Write($"[{DealerHand[i]}] ");
            sum_d += deck.GetNumValue(DealerHand[i], sum_d);
        }
        Console.WriteLine($"\n딜러 점수: {sum_d}\n");
    }

    public static void ShowPlayerHand(string[] PlayerHand, int handCount, CardDeck deck, ref int sum_p) // 플레이어 패 보여주기
    {
        sum_p = 0;
        for (int i = 0; i < handCount; i++)
        {
            Console.Write($"[{PlayerHand[i]}] ");
            sum_p += deck.GetNumValue(PlayerHand[i], sum_p);
        }
        Console.WriteLine($"\n플레이어 점수: {sum_p}\n");
    }
}

class ShowResult    // 게임 결과 보여주는 클래스
{
    public static void ShowGameResult(int sum_p, int sum_d, int inputFlag, int dFlag, int betAmount, ref int chip)   // 게임 결과 보여주기
    {
        Console.WriteLine($"=== 게임 결과 ===\r\n플레이어: {sum_p}점\r\n딜러: {sum_d}점");

        if (inputFlag == 2 || (dFlag != 2 && sum_d > sum_p))    // 플레이어 버스트면서 딜러 버스트 아니고 딜러의 합산이 더 클 때 
        {
            Console.WriteLine($"\n딜러 승리! (-{betAmount}개)");
            chip -= betAmount;
            Console.WriteLine($"보유 칩: {chip}");
        }
        else if (dFlag != 2 && sum_d == sum_p)  // 딜러 버스트 아니고 딜러와 플레이어 합산이 같을 때
        {
            Console.WriteLine($"\n무승부");
            Console.WriteLine($"보유 칩: {chip}");
        }
        else    // 플레이어 버스트 아니고 딜러 버스트거나 플레이어 합산이 더 클 때
        {
            chip += betAmount;
            Console.WriteLine($"\n플레이어 승리! (+{betAmount}개)");
            Console.WriteLine($"보유 칩: {chip}");
        }
    }
}

class GameRestart   // 게임 재시작 클래스
{
    public static bool RestartGame()   // 게임 재시작 여부 묻기
    {
        Console.Write("\n새 게임을 하시겠습니까? (Y/N): ");
        string input = Console.ReadLine() ?? "";
        if (input.ToLower() == "y")
        {
            Console.WriteLine("\n=== 게임을 다시 시작합니다 ===\n");
            return true;
        }
        else if (input.ToLower() == "n")
        {
            Console.WriteLine("\n게임을 종료합니다.");
            return false;
        }
        else
        {
            Console.Write("잘못된 입력입니다.");
            return RestartGame(); // 잘못된 입력 시 다시 묻기
        }

    }
}