# Boggle-VB.NET
The Game of Boggle Created in Visual Basic
## Game Logic
- Dice
  - createDice(String diceConfig)
  - getTopLetter() as char
  - scrambleLetter()
- Board
  - getUnusedDice() as String[]
  - setUnusedDice(String[] unused)
  - getBoard() as Char[]
  - scrambleBoard()
  - getNeighbors(int pos) as Dict{}
- Timer
  - getTime() as Int
  - startTime()
  - setTimeLimit (int sec, Default 180)
- Player(s)
  - setName(String name)
  - getName() as String
  - setScore(Integer score)
  - getScore() as Integer
- Valadator(String) as Boolean
- SearchDict(string input) as Boolean
- CompareAnswers(Players[] players)
## Game Window
- Start Screen  - Set # of Players and start game
- Game Screen   - Display the Boggle Board and Timer
- Input Screen  - Each Player Individually Inputs the words they have found
- Score Screen  - Display the scores for each player and allow players to continue playing or start a new game
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
AEE**G**MU	AE**G**MNN	AFIRSY	**B**JKQXZ	CCNSTW
CEIILT	CEILPT	CEIPST	DHHNOT	DHH**L**OR
DHLNOR	DDLNOR	EIIITT	**E**MOTTT	ENSSSU
FIPRSY	GORRVW	HIPRRY	NOOTUW	OOOTTU
