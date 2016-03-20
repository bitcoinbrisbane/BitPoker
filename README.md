# BitPoker

## Abstract
The goal of the project is to create peer to peer "mesh" networks of poker games, in which no central actor can control the deck and thus rig the game.  The game uses bitcoin and lightning network to settle bets between actors.

### Notation
- A = Ace
- K = King
- Q = Queen
- J = Jack

- S = Spades
- C = Clubs
- H = Hearts
- D = Diamonds

- SB = Small Blind
- BB = Big Blind

## The protocol

## Table Contract
The paramaters are
1.  Encryption Algorithm
2.  Hash Algorithm
3.  Blinds
4.  Rake*
5.  Min players
6.  Max players
7.  Game type
8.  Other (straddles, "run it twice")
9.  Address for multisig

### Buy in

Example xml serialziation
```
<Table Id="bf368921-346a-42d8-9cb8-621f9cad5e16" Address="1GpgWDapL6WQarq8G3rgMs77doEzVYHi4s">
  <Enc>AES-265</Enc>
  <Hash>SHA-256</Hash>
</Table>
```

## Hand Contract
1.  Number and position of players
2.  Private Key.  Each hand also includes some entropy so hands can not be pre computed.

```
<Hand Key="d0033f6f-9f24-4bf2-b280-4832a278c771">
  <Seat Number="1" Position="SB">14QJhqjdmRJQBsr9zEE4VJi76yj59WhY51</Seat>
  <Seat Number="2" Position="Dealer">1F7TAjU1tEy8EMkv4BHFL5CxfrH5maEXve</Seat>
<Hand>
```

## The Shuffle
In this example, we will use a "Heads up" game of No Limit Texas Holdem.  In this case, Alice is the dealer.  Bob the small blind, and Alice the big blind.

-Alice = 1F7TAjU1tEy8EMkv4BHFL5CxfrH5maEXve
-Bob = 14QJhqjdmRJQBsr9zEE4VJi76yj59WhY51

The deck is represented by an array[52].  

-Card[0] = AH
-Card[1] = KH
-Card[2] = QH
-Card[3] = JH
...
-Card[51] = 2C

### Alice shuffles the deck and does not disclose the un-encrypted result.
-Card[0] = AC
-Card[1] = 3S
-Card[2] = AH
-Card[3] = 2S

Create an array of 52 private keys.  These do not leave Alices computer.
- Key[0]=89fb4cf3-c801-406c-8c65-b4f065e0b23b
- Key[1]=9e1fa9ca-c9c2-481e-96c8-92aaf7bc058b
- Key[2]=3eb5a6a1-3d06-4a6b-a5d4-3f71f589371a
- Key[3]=37c0abe4-742a-48a8-aa9c-9e7ee1af9867

Each card is double encrypted and represented in base64.  First with the table key.  In the example key = d0033f6f-9f24-4bf2-b280-4832a278c771
- Card[0]=Lxda6XQS8+E80rwhx70MqiUdKErWU1VFbyYHIRcGlaw=
- Card[1]=mHlaEvcUb7vC7xe66tAzOAefYIm4NHVtaMOUfFtowq8=
- Card[2]=MDOO3rFc8yAxBjoqjDC5tPSNC/eAtCt18peIhPFyIVY=
- Card[3]=jZ/1pTx60F3kqDSp6jRr14YAaa9UZIH8Rut7OMqvmhE=

Now encrypted with the matching key.
Card[0]=yma39Z60CRkAt3Kr1JDSpRiGE/vWqZRAMy63KSL9wR1INMYW7R95S4A9m3R/wi1KTx+ssY+sDFLGm1TfXSh0fQ==

Encrypt the card value with the corrsponding key value.  Ie, card value in array 0 is encrypted with key 0

### Alice sends the deck to Bob
As the deck is encrypted, and assumed shuffled, Bob has no way to known the contents of the deck.  Bob now double encrypts and shuffles, and sends the result back to Alice.

## Pre flop
We know how the distribution of cards that will be dealt.  In Holdem, each card is dealt one at a time, starting left of the dealer (small blind) [Citation 1]
- Card[0] => Bob
- Card[1] => Alice
- Card[2] => Bob
- Card[3] => Alice

## Flop, Turn and River

## Post hand consensus
Once the hand has been played, the table then reaches consensus..

## Network Topology

## Betting via lightning channels

