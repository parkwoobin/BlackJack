# BlackJack

## 미니 게임 과제: 블랙잭-1 (Blackjack)

간단한 C# 콘솔 기반 블랙잭(Blackjack) 게임입니다.  
플레이어는 배팅 후 히트/스탠드를 선택하고, 딜러는 규칙(17 이상까지 카드 받기)에 따라 진행합니다.

---

## Features

- 콘솔에서 배팅 금액 입력 / 초기 1000
- 플레이어: Hit / Stand
- 딜러: 17 이상(규칙에 따라 Soft 17 처리)까지 자동 진행
- 승/패/무승부(Push) 판정
- 입력값 검증 (`int.TryParse` 사용)

---

## Rules (요약)

- 목표는 **21에 가깝게** 만들되, **21 초과(Bust)** 하면 즉시 패배
- 숫자카드: 숫자 그대로
- J/Q/K: 10
- A(에이스): 1 또는 11 (유리한 값으로 계산)
- 딜러는 **합이 17 이상** 될 때까지 카드 받음  
  - (옵션) Soft 17 규칙은 구현에 따라 다를 수 있음

### 21이 둘 다 나오는 경우
- 일반적으로 **무승부(Push)** → 배팅금 반환
- 단, **블랙잭(첫 2장 21)** 여부에 따라 세부 승패 규칙이 달라질 수 있음

---

## Getting Started

### Requirements
- .NET SDK 6+ (또는 프로젝트에 맞는 버전)

### Run
```bash
dotnet run
```
---

### Project Structure
```/ConsoleBlackjack
  ├─ .gitignore
  ├─ BlackJack.cs
  ├─ BlackJack.csproj
  ├─ BlackJack.sln
  ├─ Deck.cs
  └─ README.md
