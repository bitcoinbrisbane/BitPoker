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

Suites
| Key  | Value |
| ------------- | ------------- |
| S  | Spade  |
| C  | Club |
| H  | Heart |
| D  | Diamond  |

Poker terminology
- SB = Small Blind
- BB = Big Blind

### Example Keys (Address, Public Key, WIF Private Key)
*Alice* 
- 1LgogfdwKv5m9jDLNr3neogWr1y67oVJLF 
- 03c507bbc4cfaf5f5febaba63a80fec2327a9fcba3ffcd5c925adbfb6308539f75 
- KzWvEbX8aysrcoeW8ucCnqnDDkWbWF45xEWmKjQuhN2DBZtQ7Lp2

*Bob* 
- 1PGq12ixSJiyq5hSwm2aX7q64pcnDzbX4G 
- 036120d79f2962fed22d5ed8c6a9c4ac60e00bcbe55c76058498da548823700972 
- Kyp68W4Lq6w78MnWzVCC1zj8pwm6VjUNCTxMY1EEMv5guH3XfquX

*Witness* 
- 135iuRV5GWKjRMhWbSnSyPLYHy3pNHLYKa
- 027d095e4af2a82c68466587406a2bfed119b7eac31f70582085cc24fc0e36033e 
- KwzvU7vjSWiuXDv7ohhNKa47zJUhHJzsh4LJ19A6yjFyZtLnaZvQ

*Redeem Script* 522103c507bbc4cfaf5f5febaba63a80fec2327a9fcba3ffcd5c925adbfb6308539f7521036120d79f2962fed22d5ed8c6a9c4ac60e00bcbe55c76058498da54882370097221027d095e4af2a82c68466587406a2bfed119b7eac31f70582085cc24fc0e36033e53ae

https://coinb.in/?verify=522103c507bbc4cfaf5f5febaba63a80fec2327a9fcba3ffcd5c925adbfb6308539f7521036120d79f2962fed22d5ed8c6a9c4ac60e00bcbe55c76058498da54882370097221027d095e4af2a82c68466587406a2bfed119b7eac31f70582085cc24fc0e36033e53ae#verify

## The protocol
Each client connects to the table starter, who then gives a list of players IPs.  They then connect to each other.  As new players join, the table starter.

If the table starter disconnects, the next joined player becomes the table starter.

Messages are sent to all players, signed, and referencing the existing message.  Thus like a block chain of messages.  

### Message types
- Join
- Quit
- Action
- Shuffle Request

### Overview
1.  A punter either looks to join a table with game paramaters
2.  A punter can choose to start a table be defining a table contract
3.  Tables should also broad cast their game, status and number of current players to other tables
4.  Leaving the table (closing the channel)
5.  Lightning network will facilitate micro payments "off chain".  The table can agree to bring them "on chain" after n hands are dealt.

### Aside:  Lightning Network

*How it Works.* 

Funds are placed into a two-party, multisignature "channel" bitcoin address. This channel is represented as an entry on the bitcoin public ledger. In order to spend funds from the channel, both parties must agree on the new balance. The current balance is stored as the most recent transaction signed by both parties, spending from the channel address. To make a payment, both parties sign a new exit transaction spending from the channel address. All old exit transactions are invalidated by doing so.

The Lightning Network does not require cooperation from the counterparty to exit the channel. Both parties have the option to unilaterally close the channel, ending their relationship. Since all parties have multiple multisignature channels with many di erent users on this network, one can send a payment to any other party across this network.

By embedding the payment conditional upon knowledge of a secure cryptographic hash, payments can be made across a network of channels without the need for any party to have unilateral custodial ownership of funds. The Lightning Network enables what was previously not possible with trusted financial systems vulnerable to monopolies—without the need for custodial trust and ownership, participation on the network can be dynamic and open for all.

[https://lightning.network/lightning-network-summary.pdf]

## Game as a contract
In the below *table contract* the below game Texas Holdem is defined as an Enum.  The whole rules of the game could be defined as a contract, thus allowing anyone to develop variations of the game, such as the "Seven Duce" rule, other variations of poker such as Ohmaha or even other games.  

These are out side the scope of this paper.

## Table Contract
The paramaters for a table are defined in the following schema.  Developers are encouraged to create their own algorithms, such as voting or anti-collusion.

1.  Encryption Algorithm (Enum AES-256)
2.  Hash Algorithm (Enum SHA-256)
3.  Currency (Enum)
3.  Blinds
4.  Rake*
5.  Min players
6.  Max players
7.  Game type (Enum, No Limit Texas Holdem) *
8.  Other (straddles, "run it twice") **
9.  Address for multisig
10.  Consensus Algorithm
11.  Anti Collusion Algorithm
11.  Version
12.  Voting Algorithm
13.  Channel Address

* Perhaps an entire contract
** Perhaps an entire contract

*Example xml serialziation*
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

## Buying in
A player buying in opens a lightning payment channel with all players.

"Through this network of interconnected payment channels, Lightning provides a scalable, decentralized micropayments solution on top of the Bitcoin blockchain." [https://lightning.network/lightning-network-technical-summary.pdf]

## Hand Contract
1.  Number and position of players
2.  Private Key.  Each hand also includes a private key to add entropy so hands can not be pre computed.  The dealer creates the private key.

```
<Hand Key="HBFwc/qnlFqkxwiXTmNkXw==">
  <Seat Number="1" Position="SB">1PGq12ixSJiyq5hSwm2aX7q64pcnDzbX4G</Seat>
  <Seat Number="2" Position="Dealer">1LgogfdwKv5m9jDLNr3neogWr1y67oVJLF</Seat>
  <Witness>135iuRV5GWKjRMhWbSnSyPLYHy3pNHLYKa</Witness>
<Hand>
```

## The Shuffle
In this example, we will use a "Heads up" game of No Limit Texas Holdem.  In this case, Alice is the dealer.  Bob the small blind, and Alice the big blind.

- Alice = 1LgogfdwKv5m9jDLNr3neogWr1y67oVJLF
- Bob = 1PGq12ixSJiyq5hSwm2aX7q64pcnDzbX4G

The deck is represented by an array[52].  

- Card[0] = AH
- Card[1] = KH
- Card[2] = QH
- Card[3] = JH
...
- Card[51] = 2C

### Alice shuffles the deck and does not disclose the un-encrypted result.
- Card[0] = AC
- Card[1] = 3S
- Card[2] = AH
- Card[3] = 2S

Create an array of 52 private keys, 16 bytes represented as base64.  These do not leave Alices computer.
- Key[0]=VC65FGS6DQpKLzC+65XPbQ==
- Key[1]=r5a9aeztGOAFovYk+SESHg==
- Key[2]=TneaKNlV8fZgVJ61e6Xwwg==
- Key[3]=YhcT8QLRmmcV2Q/CxOkQKQ==

Each card is double encrypted.  First round of encryption with the hand key.  In the example key = HBFwc/qnlFqkxwiXTmNkXw== (1c 11 70 73 fa a7 94 5a a4 c7 08 97 4e 63 64 5f) in hex.

- Card[0]=
- Card[1]=
- Card[2]=
- Card[3]=

Then each card is encrypted again with the matching key and represented as base64.  Eg, card[0] is encrypted with key[0]
Card[0]=

### Alice sends the deck to Bob
As the deck is encrypted, and assumed shuffled, Bob has no way to known the contents of the deck.  Bob the encrypts the deck again and shuffles, and sends the result back to Alice.

*Example message in xml*
```
<Deck>
  <Card Index="0"></Card>
  <Card Index="1"></Card>
  ...
  <Card Index="51"></Card>
  <Signature>
  </Signature>
<Deck>
```

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

*Example action message from Bob.  A call from the small blind.*
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
Closing the channel

## Network Topology

### Dealing with disconnects

## Betting via lightning channels
Each bet is a signature from the punter that is not broad cast to the network.   For example, in the heads up game, if both Alice and Bob post blinds, the net transfer result = 0.

## Settlement


## References
https://lightning.network/lightning-network-paper.pdf
