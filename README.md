# BattleShip

BattleShip is a turn-based two-player game that simulates a war between ships on an ocean.

Each player gets their own battle area with a certain number of ships placed in non-overlapping positions. The ships might be of different sizes.

Note, players cannot see each others ship's location.
There are two types of ships - type P and type Q. 

Type P ships can be destroyed by a single hit in each of their cells
and type Q ships require 2 hits in each of their cells.
A ship is considered destroyed when all of its cells are destroyed.

The player who destroys all the ships of the other player first wins the game. The game ends in a draw if none of the players can destroy all of the other’s ships using a finite number of missiles.

Constraints:
 - 1 <= Width of Battle area (M’) <= 9,
 - A <= Height of Battle area (N’) <= Z
 - 1 <= Number of battleships <= M’ * N’
 - Type of ship = {‘P’, ‘Q’}
 - 1 <= Width of battleship <= M’
 - A <= Height of battleship <= N’
 - 1 <= X coordinate of ship <= M’
 - A <= Y coordinate of ship <= N’


Input:

    - The first line of the input contains the width and height of the battle area respectively.
    - The second line of the input contains the number of battleships that each player gets.
    - The third line of the input contains the type of the battleship, its dimensions (width and height) and coordinates for Player-1 and Player-2.
    - The fourth line of the input contains the type of the battleship, its dimensions (width and height) and coordinates for Player-1 and Player-2.
    - The fifth line contains the sequence of the target locations of missiles fired by Player-1.
    - The sixth line contains the sequence of the target locations of missiles fired by Player-2.
    
Sample Input:
 - 5  E         
 - 2
 - Q 1 1 A1 B2
 - P 2 1 D4 C3
 - A1 B2 B2 B3
 - A1 B2 B3 A1 D1 E1 D4 D4 D5 D5

Output:
```sh
Player-1 fires a missile with target A1 which got miss
Player-2 fires a missile with target A1 which got hit
Player-2 fires a missile with target B2 which got miss
Player-1 fires a missile with target B2 which got hit
Player-1 fires a missile with target B2 which got hit
Player-1 fires a missile with target B3 which got miss
Player-2 fires a missile with target B3 which got miss
Player-1 has no more missiles left to launch
Player-2 fires a missile with target A1 which got hit
Player-2 fires a missile with target D1 which got miss
Player-1 has no more missiles left to launch
Player-2 fires a missile with target E1 which got miss
Player-1 has no more missiles left to launch
Player-2 fires a missile with target D4 which got hit
Player-2 fires a missile with target D4 which got miss
Player-1 has no more missiles left to launch
Player-2 fires a missile with target D5 which got hit
Player-2 won the battle 
```

#### PDF

See [BattleShip_Game_V3.pdf](https://github.com/deepak-rathi/BattleShip/blob/master/BattleShip_Game_V3.pdf)


### Todos

 - Add Unit Test
 - Improve code and implement new architecture to support different user interface application using same code

License
----

GNU GENERAL PUBLIC LICENSE
Version 3
