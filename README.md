# BitPoker

## Abstract
The goal of the project is to design a peer to peer protocol of games, such as online poker, in which no central actor can control the deck and thus rig the game.  The game uses bitcoin and lightning network to settle bets between actors.

Different clients developed in different programming languages are encouraged.

### Notation
| Key  | Value |
| ------------- | ------------- |
| A  | Ace  |
| K  | King  |
| Q  | Queen |
| J  | Jack  |

- S = Spades
- C = Clubs
- H = Hearts
- D = Diamonds

- SB = Small Blind
- BB = Big Blind

### Example Keys (Address, Public Key, WIF Private Key)
*Alice* 1LgogfdwKv5m9jDLNr3neogWr1y67oVJLF 03c507bbc4cfaf5f5febaba63a80fec2327a9fcba3ffcd5c925adbfb6308539f75 KzWvEbX8aysrcoeW8ucCnqnDDkWbWF45xEWmKjQuhN2DBZtQ7Lp2

*Bob* 1PGq12ixSJiyq5hSwm2aX7q64pcnDzbX4G 036120d79f2962fed22d5ed8c6a9c4ac60e00bcbe55c76058498da548823700972 Kyp68W4Lq6w78MnWzVCC1zj8pwm6VjUNCTxMY1EEMv5guH3XfquX

*Witness* 135iuRV5GWKjRMhWbSnSyPLYHy3pNHLYKa 027d095e4af2a82c68466587406a2bfed119b7eac31f70582085cc24fc0e36033e KwzvU7vjSWiuXDv7ohhNKa47zJUhHJzsh4LJ19A6yjFyZtLnaZvQ

*Redeem Script* 522103c507bbc4cfaf5f5febaba63a80fec2327a9fcba3ffcd5c925adbfb6308539f7521036120d79f2962fed22d5ed8c6a9c4ac60e00bcbe55c76058498da54882370097221027d095e4af2a82c68466587406a2bfed119b7eac31f70582085cc24fc0e36033e53ae

https://coinb.in/?verify=522103c507bbc4cfaf5f5febaba63a80fec2327a9fcba3ffcd5c925adbfb6308539f7521036120d79f2962fed22d5ed8c6a9c4ac60e00bcbe55c76058498da54882370097221027d095e4af2a82c68466587406a2bfed119b7eac31f70582085cc24fc0e36033e53ae#verify

## The protocol

### Overview
1.  A punter either looks to join a table with game paramaters
2.  A punter can choose to start a table be defining a table contract
3.  Tables should also broad cast their game, status and number of current players to other tables
4.  Leaving the table

## Table Contract
The paramaters are 
1.  Encryption Algorithm (Enum AES-256)
2.  Hash Algorithm (Enum SHA-256)
3.  Currency (Enum)
3.  Blinds
4.  Rake*
5.  Min players
6.  Max players
7.  Game type (Enum, No Limit Texas Holdem)
8.  Other (straddles, "run it twice")
9.  Address for multisig
10.  Consensus
11.  Version
12.  Voting

### Buying in

Example xml serialziation
```
<Table Id="bf368921-346a-42d8-9cb8-621f9cad5e16" Address="3P1c61hiSBuZstuVkECYo8ntDPVsnG2EQh" AddressType"2-3">
  <Encryption>AES-265</Encryption>
  <Hash>SHA-256</Hash>
  <Currency>BTC</Currency>
  <Blinds>
    <SmallBlind>0.001</SmallBlind>
    <BigBlind>0.002</BigBlind>
  </Blinds>
  <BuyIn>
    <Min>0.1</Min>
    <Max<0.2</Max>
  </BuyIn>
  <Game>
    <Type>Texas Holdem</Type>
    <Limit>No Limit</Limit>
  </Game>
  <Clock>30</Clock>
  <TimeOuts>60</TimeOut>
</Table>
```

## Witness nodes
Game witness can also be allowed or chosen to arbitrate a game.  The witness could also help network propigation.  A witness would be choose by the table starter and a small rake paid to the witness.

There will become a market for reputable witnesses based off a https dns endpoint and earn small revenues for witnessing hands.

## Hand Contract
1.  Number and position of players
2.  Private Key.  Each hand also includes some entropy so hands can not be pre computed.  The dealer creates the GUID.

```
<Hand Key="d0033f6f-9f24-4bf2-b280-4832a278c771">
  <Seat Number="1" Position="SB">1PGq12ixSJiyq5hSwm2aX7q64pcnDzbX4G</Seat>
  <Seat Number="2" Position="Dealer">1LgogfdwKv5m9jDLNr3neogWr1y67oVJLF</Seat>
  <Witness>135iuRV5GWKjRMhWbSnSyPLYHy3pNHLYKa</Witness>
<Hand>
```

## The Shuffle
In this example, we will use a "Heads up" game of No Limit Texas Holdem.  In this case, Alice is the dealer.  Bob the small blind, and Alice the big blind.

-Alice = 1LgogfdwKv5m9jDLNr3neogWr1y67oVJLF
-Bob = 1PGq12ixSJiyq5hSwm2aX7q64pcnDzbX4G

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

*Note:  The deck could also be shuffled by a witness.

## Pre flop
We know how the distribution of cards that will be dealt.  In Holdem, each card is dealt one at a time, starting left of the dealer (small blind) [Citation 1]
- Card[0] => Bob
- Card[1] => Alice
- Card[2] => Bob
- Card[3] => Alice

## Flop, Turn and River
The client software co-ordinates the game, based off agreed game rules. 

1.  Enforces action rules of its own player, such as check, bet or fold
2.  If the action involves money, creates the tx
3.  Creates a signed message and broad casts to each player
4.  Waits for next action message
5.  Validates the message

Example action message from Bob.  A call from the small blind.
```
<Action Position="1" Address="1PGq12ixSJiyq5hSwm2aX7q64pcnDzbX4G">
  <PreviousHash></PreviousHash>
  <Call>
    <Amount>0.001</Amount>
    <Tx></Tx>
  </Call>
  <MessageSignature>
  </MessageSignature>
<Action>
<Hash>

</Hash>
```

## Post hand consensus
Once the hand has been played, the table then reaches consensus.   The signed game history could then be persistend into an Ethereum block chain, referencing previous hands.  

Fee vs Payouts.  The table would also include a paramater when to commite the hand, or hand history, to a chain.  The more frequently it is done, the more fees it will incure. 

## Cashing out

## Network Topology

### Dealing with disconnects

## Betting via lightning channels
Each bet is a signature from the punter that is not broad cast to the network.   For example, in the heads up game, if both Alice and Bob post blinds, the net transfer result = 0.

### Settlement


### References
https://lightning.network/lightning-network-paper.pdf
