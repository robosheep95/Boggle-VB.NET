# Boggle-VB.NET
The Game of Boggle Created in Visual Basic
## Game Logic
- Dice
  - CreateDice(String diceConfig)
  - GetTopLetter() as char
  - ScrambleLetter()
- Board
  - GetBoard() as Char[]
  - ScrambleBoard()
  - GetNeighbors(int pos) as Dict{}
- Timer
  - GetTime() as Int
  - StartTime()
  - SetTimeLimit (int sec, Default 180)
- Players
  - SetName(String name)
  - GetName() as String
  - 
- Valadator(String) as Boolean
- SearchDict(string input) as Boolean
- CompareAnswers(Players[] players)
## Game Windows
- Draw Board
- Input Anwsers
- Number of Players 1-4
## Scoring
- Fewer than 3 Letters: no score
- 3 Letters: 1 point
- 4 Letters: 1 point
- 5 Letters: 2 points
- 6 Letters: 3 points
- 7 Letters: 4 points
- 8 or More Letters: 11 points
- Although the 'Qu' cube occupies a single space in the grid, it counts as two letters for the purpose of scoring.
- You will be penalised 1 point for each guess you make that is not recognised as a valid word.
- Multiplyer per Underlined Letter x2
- If two or more players find the same word, it will not be scored
## Dice
AAAFRS	AAEEEE	AAFIRS	ADENNN	AEEEEM
AEEGMU	AEGMNN	AFIRSY	BJKQXZ	CCNSTW
CEIILT	CEILPT	CEIPST	DHHNOT	DHHLOR
DHLNOR	DDLNOR	EIIITT	EMOTTT	ENSSSU
FIPRSY	GORRVW	HIPRRY	NOOTUW	OOOTTU
