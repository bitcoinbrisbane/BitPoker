# BitPoker.IO

## Abstract
Inspired by OpenBazaar.com, the goal of the project is to design a peer to peer protocol of turn based games, such as online poker, in which no central actor can control the outcome and thus rig the game and is provably fair.  The game uses bitcoin (or other digital tokens) and lightning network to settle bets between actors, and a blockchain to persist the state of the game.

Most blockchains are too slow for turned based games, but not all turns need to persisted back to the blockchain.  For example, in poker, turns can be stored in memory on clients as "mini chains".  Only when the outcome of the game is required such as awarding the pot, is the data required to be persisted back to the blockchain.  Furthermore, players could agree this could be a higher cadence, such as each orbit, to save on fees.

Its hoped, that different clients developed in different programming languages will be built.

### Notation & Conventions

Ids should be represented as GUIDs, and be in lower case
All values represented in base16 (hex) should be lower case
Bitcoin addresses are used the the player identifier
Time stamps are EPOCH
Time durations are in seconds
Amounts should be in smallest crytocurrency unit, such as Satoshis for Bitcoin

### Enumerations 
Deck is represented as an array of bytes.

| Key  | Value | Decimal | Byte |
| -----|------ | --------|----- |
| A  | Ace  | 12 | {0x0C} |
| K  | King  | 11 |{0x0B}|
| Q  | Queen | 10 |{0x0A}|
| J  | Jack  | 9 |{0x09}|
| T  | Ten | 8 |{0x08}|
| 9 | 9 | 7 |{0x07}|
| 8 | 8 | 6 |{0x06}|
| 7 | 7 | 5 |{0x05}|
| 6 | 6 | 4 |{0x04}|
| 5 | 5 | 3 |{0x03}|
| 4 | 4 | 2 |{0x02}|
| 3 | 3 | 1 |{0x01}|
| 2 | 2 | 0 |{0x00}|

Suites

| Key  | Value | Offset | Byte |
| ---- | ----- | ------ | ---- |
| S  | Spade  | +0 | {0x00} |
| C  | Club | +13 | {0x0D} |
| H  | Heart | +26 | {0x1A} |
| D  | Diamond  | +39  | {0x27} |

\*Eg
Ace of clubs = { 0x0D }

Crypto Currencies
BTC Bitcoin
ETH Ethereum
ETC Ethereum Classic

### Poker terminology
- SB = Small Blind
- BB = Big Blind

### Example Keys (Address, Public Key (Not compressed), WIF Private Key)
*Alice* 
- msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv 
- 041FA97EFD760F26E93E91E29FDDF3DDDDD3F543841CF9435BDC156FB73854F4BF22557798BA535A3EE89A62238C5AFC7F8BF1FA0985DC4E1A06C25209BAB78BD1 
- 93Loqe8T3Qn3fCc87AiJHYHJfFFMLy6YuMpXzffyFsiodmAMCZS

*Bob* 
- mhSW3EUNoVkD1ZQV1ZpnxdRMBjo648enyo 
- 04F48396AC675B97EEB54E57554827CC2B937C2DAE285A9198F9582B15C920D91309BC567858DC63357BCD5D24FD8C041CA55DE8BAE62C7315B0BA66FE5F96C20D 
- 91yMBYURGqd38spSA1ydY6UjqWiyD1SBGJDuqPPfRWcpG53T672

*Witness* 
- mq1Ctw6xTcomjGgQz5pi8oXdR1tjjZQHYs
- 04C82B8E2D6EA7F17665C4A1070F340E84D4C02DA72AE5018574001841C10E8009A04E2C333D3EB90102E71B324BFE595430D4C654BBFF0F66EDBFE63798C7A271
- 93C4fbYtv8VXWDnbJLzQiVfBGuQgfz1hBF1QwQeJxQepe9oE876

\*2 of 3 address
2NCSTuV27SC1BF122Xe1wmkNkjo4MJw4W85
https://testnet.blockexplorer.com/address/2NCSTuV27SC1BF122Xe1wmkNkjo4MJw4W85

*Redeem Script* 524104c82b8e2d6ea7f17665c4a1070f340e84d4c02da72ae5018574001841c10e8009a04e2c333d3eb90102e71b324bfe595430d4c654bbff0f66edbfe63798c7a2714104f48396ac675b97eeb54e57554827cc2b937c2dae285a9198f9582b15c920d91309bc567858dc63357bcd5d24fd8c041ca55de8bae62c7315b0ba66fe5f96c20d4104f48396ac675b97eeb54e57554827cc2b937c2dae285a9198f9582b15c920d91309bc567858dc63357bcd5d24fd8c041ca55de8bae62c7315b0ba66fe5f96c20d53ae

Created from http://ms-brainwallet.org

https://coinb.in/?verify=524104c82b8e2d6ea7f17665c4a1070f340e84d4c02da72ae5018574001841c10e8009a04e2c333d3eb90102e71b324bfe595430d4c654bbff0f66edbfe63798c7a2714104f48396ac675b97eeb54e57554827cc2b937c2dae285a9198f9582b15c920d91309bc567858dc63357bcd5d24fd8c041ca55de8bae62c7315b0ba66fe5f96c20d4104f48396ac675b97eeb54e57554827cc2b937c2dae285a9198f9582b15c920d91309bc567858dc63357bcd5d24fd8c041ca55de8bae62c7315b0ba66fe5f96c20d53ae#verify


## The protocol
Each client connects to one another in the "lobby".  They can then look for players who are looking to start a game, or request to join a running game.  Messages are sent to all players, signed, and referencing the existing message.  Thus like a block chain of messages.

- Table reaches consensus on who’s turn to act based off the game contract
- Table reaches consensus on the legal moves / actions a player can make
- Table waits for a signed message from that player
- All other players validate that message
- Repeat

### Valid methods (See below for sample messages)
Methods are of two types.  Game play / action messages that determin a players turn intent.  Methods that do not, such as join a table.

Non action methods
- BuyIn
- Deal
- Quit
- SitOut
- Shuffle

Action methods
- SmallBlind
- BigBlind
- Bet
- Raise
- Fold
- Shuffle

### Overview
If the game is to be developed using Ethereum contracts:

1.  The game is defined as an Ethereum contract
2.  Players agree to the table contract
3.  Each players actions are defined as inputs for the hand contract
4.  After the hand has ended, each player verifies the integrity of the hand contract.  Its in everyones best interest to verify correctly [Game Theory Citation]
5.  The hand message chain is then executed on the Ethereum network for the pot to be awarded

Less use of Ethereum

1.  Players connect to each other via a P2P network protocol.
2.  A player either looks to join a table and reviews the contract.
3.  A player can choose to start a table be defining a table contract.
4.  Tables should also broad cast their game, status and number of current .players to other tables for better network propagation.
5.  Leaving the table (closing the channel)
6.  Lightning network will facilitate micro payments "off chain".  The table can agree to bring them "on chain" after n hands are dealt.

### Aside:  Lightning Network

*How it Works.* 

Funds are placed into a two-party, multi signature "channel" bitcoin address. This channel is represented as an entry on the bitcoin public ledger. In order to spend funds from the channel, both parties must agree on the new balance. The current balance is stored as the most recent transaction signed by both parties, spending from the channel address. To make a payment, both parties sign a new exit transaction spending from the channel address. All old exit transactions are invalidated by doing so.

The Lightning Network does not require cooperation from the counterparty to exit the channel. Both parties have the option to unilaterally close the channel, ending their relationship. Since all parties have multiple multi signature channels with many different users on this network, one can send a payment to any other party across this network.

By embedding the payment conditional upon knowledge of a secure cryptographic hash, payments can be made across a network of channels without the need for any party to have unilateral custodial ownership of funds. The Lightning Network enables what was previously not possible with trusted financial systems vulnerable to monopolies—without the need for custodial trust and ownership, participation on the network can be dynamic and open for all.

[https://lightning.network/lightning-network-summary.pdf]

## Game as a contract
In the below *table contract* the below game Texas Holdem is defined as an Enum.  The whole rules of the game could be defined as a contract, thus allowing anyone to develop variations of the game, such as the "Seven Duce" rule, other variations of poker such as Omaha or even other games.  

These are out side the scope of this paper.

## Messages
All actions are sent as JSON RPC.  They must include a public key hash and be signed.  The payload must also reference their previous message hash.

1.  Concatenate the payload the values
2.  Hash the payload of step 1
3.  Sign the output of step 2

General message object as a JSON RPC param

| Property | Eg |
| -------- | -- |
| Version | Message Version |
| Id | GUID |
| Bitcoin Address (Public Key Hash) | msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv |
| Method | Enum (TABLE, ACTION, BUYIN, SHUFFLE, DECK) |
| Payload Hash | SHA256 |
| Message Signature | TODO |
| Pervious Hash | TODO |

Example action message (payload)

| Property  | Eg |
| --------- | -- |
| Id | 47b466e4-c852-49f3-9a6d-5e59c62a98b6 |
| Bitcoin Address (Public Key Hash) | msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv |
| Index | 2 |
| Action | CALL 5000000 |
| TX | TODO |
| Previous Hash | TODO |
| Time stamp | 2016-08-17 00:00:00 |


Eg in Json
```
{"TableId":"bf368921-346a-42d8-9cb8-621f9cad5e16","HandId":"398b5fe2-da27-4772-81ce-37fa615719b5","Index":2,"Action":"CALL","Amount":5000000,"Tx":null,"PreviousHash":"8ab9f91c002d8ccdbd8a49f7e028d27ca6ef01cf1fdaa4eca637868d8e4adf31","HashAlgorithm":"SHA256","Version":1.0,"BitcoinAddress":"msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv","TimeStamp":"2016-08-17T00:00:00"}
```

SHA-256 hash of the JSON RPC method
```
cb19bc14bca61bee174e5d6591530ad72b3ab58e0c5a904baec5b5de85c65e88
```

## Sample messages
### Buy In
```
```

### Call
```
```

## Adding a Table Contract
A client will define the table contract and store that locally.  They become the table starter and thus define the conditions of that game.  The parameters for a table are defined in the following schema.  Developers are encouraged to create their own algorithms, such as voting or anti-collusion.

1.  Encryption Algorithm (Enum AES-256)
2.  Hash Algorithm (Enum SHA-256)
3.  Id (GUID)
3.  Currency (Enum)
3.  Blinds
4.  Rake\*
5.  Min players
6.  Max players
7.  Game type (Enum, No Limit Texas Holdem) \*
8.  Other (straddles, "run it twice") \*
9.  Channel Address / multisig
10.  Consensus Algorithm
11.  Anti Collusion Algorithm / Contract
12.  Version
13.  Voting Algorithm / Contract
14.  Channel Address

* Perhaps an entire contract

*Example xml serialziation*
```
<Message Verion="1" Type="Table">
  <PubKeyHash></PubKeyHash>
  <Table Id="bf368921-346a-42d8-9cb8-621f9cad5e16" AddressType"2-2">
    <Encryption>AES-265</Encryption>
    <Hash>SHA-256</Hash>
    <Currency>BTC</Currency>
    <Blinds>
      <SmallBlind>100000</SmallBlind>
      <BigBlind>200000</BigBlind>
    </Blinds>
    <BuyIn>
      <Min>10000000</Min>
      <Max>50000000</Max>
    </BuyIn>
    <Game>
      <Type>Texas Holdem</Type>
      <Limit>No Limit</Limit>
    </Game>
    <Clock>30</Clock>
    <TimeOuts>120</TimeOut>
    <Version>0.0.1</Version>
  </Table>
  <MessageSignature></MessageSignature>
</Message>
```

```
{
  table : {
    id : "bf368921-346a-42d8-9cb8-621f9cad5e16"
    encryption : "AES-256",
    hash : "SHA-256",
    currency : "BTC",
    blinds : {
      small : 100000,
      big : 200000
    },
    buyIn : {
      min : 10000000
      max : 50000000
    }

  }
}
```

## Joining a Table
Users send their intent to join a table by the JoinTable method.  This is analogous choosing a seat and sitting down at the table.  Once the table reaches the maximum amount of players, or the players vote to start the table, a multi signature address is created.  The required signatures are part of the agreed table contract.

```
{
  method : "JoinTable",
  version : 1
  params {
      publicKey : "04F48396AC675B97EEB54E57554827CC2B937C2DAE285A9198F9582B15C920D91309BC567858DC63357BCD5D24FD8C041CA55DE8BAE62C7315B0BA66FE5F96C20D",
      minStart : 2
  }
}
```

Response
```

```

## Buying in
All players buying in open a lightning payment channel with the multi signature address of the table.  Players must add BTC within the range for the table contract (MinBuyIn, MaxBuyIn)

"Through this network of interconnected payment channels, Lightning provides a scalable, decentralised micropayments solution on top of the Bitcoin blockchain." [https://lightning.network/lightning-network-technical-summary.pdf]

## Witness nodes
Game witness can also be allowed or chosen to arbitrate a game.  The witness could also help network propagation.  A witness would be choose by the table starter and a small rake paid to the witness.

There might become a market for reputable witnesses based off a HTTPS DNS endpoint and earn small revenues for witnessing hands.

### Process
1.  Alice and Bob create a 2 of 2 address
2.  Alice creates a deposit transaction
3.  Bob creates a deposit transaction
4.  Alice creates a refund transaction but does not broadcast
5.  Bob creates a refund transaction but does not broadcast

\*2 of 2 Redeem Script and Address for Alice and Bob
```
2 041fa97efd760f26e93e91e29fddf3ddddd3f543841cf9435bdc156fb73854f4bf22557798ba53
5a3ee89a62238c5afc7f8bf1fa0985dc4e1a06c25209bab78bd1 041fa97efd760f26e93e91e29fd
df3ddddd3f543841cf9435bdc156fb73854f4bf22557798ba535a3ee89a62238c5afc7f8bf1fa098
5dc4e1a06c25209bab78bd1 2 OP_CHECKMULTISIG

2Mx377XSXhvqqVyLaXsPDAAEsJFzGeWunKi
```

### Funding TXs
Both Alice and Bob now deposit their buy in to the address 2Mx377XSXhvqqVyLaXsPDAAEsJFzGeWunKi.  Note:  The table contract could include a minimum confirmation count.

Alice tx  f5c5e008f0cb9fc52487deb7531a8019e2d78c51c3c40e53a45248e0712102a3
Bob tx c60193a33174a1252df9deb522bac3e5532e0c756d053e4ac9999ca17a79c74e

\*Sample C# NBitcoin code
```
const String alice_wif = "93Loqe8T3Qn3fCc87AiJHYHJfFFMLy6YuMpXzffyFsiodmAMCZS";

NBitcoin.BitcoinSecret alice_secret = new NBitcoin.BitcoinSecret (alice_wif, NBitcoin.Network.TestNet);
NBitcoin.BitcoinAddress alice = alice_secret.GetAddress ();

//Create 2 of 2
NBitcoin.Script table = NBitcoin.PayToMultiSigTemplate
                        .Instance
                        .GenerateScriptPubKey(2, new[] { alice_secret.PubKey, alice_secret.PubKey });

Console.WriteLine(table);
Console.WriteLine(table.Hash.GetAddress(NBitcoin.Network.TestNet));

NBitcoin.IDestination msigAddress = table.Hash.GetAddress(NBitcoin.Network.TestNet);

var blockr = new NBitcoin.BlockrTransactionRepository(NBitcoin.Network.TestNet);
NBitcoin.Transaction transaction = blockr.GetAsync(new NBitcoin.uint256("f5c5e008f0cb9fc52487deb7531a8019e2d78c51c3c40e53a45248e0712102a3")).Result;

NBitcoin.Coin[] aliceCoins = transaction
                        .Outputs
                        .Select((o, i) => new NBitcoin.Coin(new NBitcoin.OutPoint(transaction.GetHash(), i), o))
                        .ToArray();

var txBuilder = new NBitcoin.TransactionBuilder();

var tx = txBuilder
    .AddKeys(alice_secret.PrivateKey)
        .AddCoins(aliceCoins)
        .Send(msigAddress, new NBitcoin.Money(50000000))
        .SetChange(alice)
        .SendFees(NBitcoin.Money.Coins(0.001m))
        .BuildTransaction(true);

Boolean ok = txBuilder.Verify(tx);

Console.WriteLine(tx.ToHex());
```

\*Raw TX Alice buy in of 0.5 BTC
```
0100000001a3022171e04852a4530ec4c3518cd7e219801a53b7de8724c59fcbf008e0c5f5000000
008b483045022100c21e5c296d3024f64dbd948b1999933206a3d3d757ff1004ce874fa4b9277acc
02202d0c0115b4f52a7de2a1863141eda25192255015da14765a1409d8d202f096b40141041fa97e
fd760f26e93e91e29fddf3ddddd3f543841cf9435bdc156fb73854f4bf22557798ba535a3ee89a62
238c5afc7f8bf1fa0985dc4e1a06c25209bab78bd1ffffffff02e069f902000000001976a914822f
3782f8d0357cb6fc7b4c5cfc1424b3f0100988ac80f0fa020000000017a914348de5f6c91078c128
4956a88a9322be8d2834148700000000
```

\*In JSON
```
{
  "txid": "0e7ae471ffd578c64b142c232f36c3f7810e1fcb2e31b8c1b02f4c61c07859dc",
  "size": 256,
  "version": 1,
  "locktime": 0,
  "vin": [
    {
      "txid": "f5c5e008f0cb9fc52487deb7531a8019e2d78c51c3c40e53a45248e0712102a3",
      "vout": 0,
      "scriptSig": {
        "asm": "3045022100c21e5c296d3024f64dbd948b1999933206a3d3d757ff1004ce874fa4b9277acc02202d0c0115b4f52a7de2a1863141eda25192255015da14765a1409d8d202f096b4[ALL] 041fa97efd760f26e93e91e29fddf3ddddd3f543841cf9435bdc156fb73854f4bf22557798ba535a3ee89a62238c5afc7f8bf1fa0985dc4e1a06c25209bab78bd1",
        "hex": "483045022100c21e5c296d3024f64dbd948b1999933206a3d3d757ff1004ce874fa4b9277acc02202d0c0115b4f52a7de2a1863141eda25192255015da14765a1409d8d202f096b40141041fa97efd760f26e93e91e29fddf3ddddd3f543841cf9435bdc156fb73854f4bf22557798ba535a3ee89a62238c5afc7f8bf1fa0985dc4e1a06c25209bab78bd1"
      },
      "sequence": 4294967295
    }
  ],
  "vout": [
    {
      "value": 0.499,
      "n": 0,
      "scriptPubKey": {
        "asm": "OP_DUP OP_HASH160 822f3782f8d0357cb6fc7b4c5cfc1424b3f01009 OP_EQUALVERIFY OP_CHECKSIG",
        "hex": "76a914822f3782f8d0357cb6fc7b4c5cfc1424b3f0100988ac",
        "reqSigs": 1,
        "type": "pubkeyhash",
        "addresses": [
          "msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv"
        ]
      }
    },
    {
      "value": 0.5,
      "n": 1,
      "scriptPubKey": {
        "asm": "OP_HASH160 348de5f6c91078c1284956a88a9322be8d283414 OP_EQUAL",
        "hex": "a914348de5f6c91078c1284956a88a9322be8d28341487",
        "reqSigs": 1,
        "type": "scripthash",
        "addresses": [
          "2Mx377XSXhvqqVyLaXsPDAAEsJFzGeWunKi"
        ]
      }
    }
  ]
}
```
Which yields transaction id 0e7ae471ffd578c64b142c232f36c3f7810e1fcb2e31b8c1b02f4c61c07859dc
Bobs transaction id d74b4bfc99dd46adb7c30877cc3ce7ea13feb51a6fab3b9b15f75f4e213ac0da

The address 2Mx377XSXhvqqVyLaXsPDAAEsJFzGeWunKi now contains 1btc https://testnet.blockexplorer.com/address/2Mx377XSXhvqqVyLaXsPDAAEsJFzGeWunKi

*Sample opening lightning channel in c# / NBitcoin*
```
TODO
```

### Create the refund transaction
Alice and Bob must also create a refund transaction to themselves, but *not* broadcast it.

## Game play
The dealer's client is responsible for the orchestration of the game.  As the dealer position rotates, this isn't a centralisation risk.  The intent is to limit network traffic.
1.  Define the hand contract
2.  Shuffle the deck
3.  Post blinds
4.  Pre flop round
5.  Deal the flop
6.  Post flop round
7.  Deal the turn
8.  Post turn round
9.  Deal the river
10.  Post river round
11.  Award the pot

### Hand Contract
At the start of each hand, the dealer defines the hand contract which references the table contract.  

1.  The players and seat positions
2.  The stack of each player
3.  A ID as a GUID

\*Example hand contract serialised in XML Download from test data folder
```
<?xml version="1.0" encoding="utf-8" ?>
<ArrayOfActionMessage xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <ActionMessage>
    <Version>1</Version>
    <Id>47b466e4-c852-49f3-9a6d-5e59c62a98b6</Id>
    <BitcoinAddress>msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv</BitcoinAddress>
    <Signature>HEVF2mU1K0MmgeaP/zlxjCDkgbz43I638QaWwM/ipcrfWbSZwfx96MDxcqDr3dTBzzKMr9EnNqBjJlIQLk6Tdmg=</Signature>
    <TimeStamp>2016-08-17T00:00:00</TimeStamp>
    <TableId>bf368921-346a-42d8-9cb8-621f9cad5e16</TableId>
    <HandId>398b5fe2-da27-4772-81ce-37fa615719b5</HandId>
    <Index>0</Index>
    <Action>SMALL BLIND</Action>
    <Amount>50000</Amount>
    <HashAlgorithm>SHA256</HashAlgorithm>
  </ActionMessage>
  <ActionMessage>
    <Version>1</Version>
    <Id>a29bc370-9492-4b60-ad4f-7c7513064383</Id>
    <BitcoinAddress>mhSW3EUNoVkD1ZQV1ZpnxdRMBjo648enyo</BitcoinAddress>
    <Signature>HATytRG1kIsPUVxELt7/m40EwCn4ryaV2p6Xmr38rijmAsm3pra8vvRPipNdYzAF5fgNr8HuLKZH2wUkpvEJ8CM=</Signature>
    <TimeStamp>2016-08-17T00:00:10</TimeStamp>
    <TableId>bf368921-346a-42d8-9cb8-621f9cad5e16</TableId>
    <HandId>398b5fe2-da27-4772-81ce-37fa615719b5</HandId>
    <Index>1</Index>
    <Action>BIG BLIND</Action>
    <Amount>100000</Amount>
    <PreviousHash>be0c3991bdc569a94e57bec2afbfc7a8283be8c85ab16bb6e009d6f73270f7a0</PreviousHash>
    <HashAlgorithm>SHA256</HashAlgorithm>
  </ActionMessage>
  <ActionMessage>
    <Version>1.0</Version>
    <Id>e299ebc5-b50f-425e-b839-cb69ef69a12e</Id>
    <BitcoinAddress>msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv</BitcoinAddress>
    <Signature>HFJTeey5K6yOyeapiuDelM4WANsHM+CK3etYO8d8qCGlKiMLo9rygkvTGDF/vF3gyMYoK5jVx6/yKgW3yJF5hnc=</Signature>
    <TimeStamp>2016-08-17T00:00:20</TimeStamp>
    <TableId>bf368921-346a-42d8-9cb8-621f9cad5e16</TableId>
    <HandId>398b5fe2-da27-4772-81ce-37fa615719b5</HandId>
    <Index>2</Index>
    <Action>CALL</Action>
    <Amount>50000</Amount>
    <PreviousHash>5739b2f4d3a1fceb7dfaa644e9a392105bccbb1fe17ea00e4979a124f28d81e9</PreviousHash>
    <HashAlgorithm>SHA256</HashAlgorithm>
  </ActionMessage>
  <ActionMessage>
    <Version>1.0</Version>
    <Id>54c5c3c1-306a-4f1b-863c-aba29b22cb5c</Id>
    <BitcoinAddress>mhSW3EUNoVkD1ZQV1ZpnxdRMBjo648enyo</BitcoinAddress>
    <Signature>HJmu7k8u/xDZ1q8U4Kn2a9NGciOfj7oES35VMKoSOC2KJlZuFJ19hxGjV5ARetHG5CjT2QvKEbgfeIM2S+1ye6g=</Signature>
    <TimeStamp>2016-08-17T00:00:30</TimeStamp>
    <TableId>bf368921-346a-42d8-9cb8-621f9cad5e16</TableId>
    <HandId>398b5fe2-da27-4772-81ce-37fa615719b5</HandId>
    <Index>3</Index>
    <Action>CHECK</Action>
    <Amount>0</Amount>
    <PreviousHash>76cbd60a3ac4f3883cf4c5fdc60a8c791856796867dcc16849e102e4d462a793</PreviousHash>
    <HashAlgorithm>SHA256</HashAlgorithm>
  </ActionMessage>
  <ActionMessage>
    <Version>1.0</Version>
    <Id>0e9053eb-288c-44be-81e0-d6ad57e42ded</Id>
    <BitcoinAddress>msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv</BitcoinAddress>
    <Signature>GyDFY/a2DGEHGpsIDuk+TNWotI4eoFFFGcw2Lc+9DageAdRr0E8QTuZIhz6pB5kLMkqcPOiKoF71HT5HPLeCxk8=</Signature>
    <TimeStamp>2016-08-17T00:00:40</TimeStamp>
    <TableId>bf368921-346a-42d8-9cb8-621f9cad5e16</TableId>
    <HandId>398b5fe2-da27-4772-81ce-37fa615719b5</HandId>
    <Index>4</Index>
    <Action>BET</Action>
    <Amount>100000</Amount>
    <PreviousHash>6f71c283fd37a916f99386050b7a3e56f66c86f9def153bdd332bab85f226c0a</PreviousHash>
    <HashAlgorithm>SHA256</HashAlgorithm>
  </ActionMessage>
  <ActionMessage>
    <Version>1.0</Version>
    <Id>93cae6c4-4dbf-4d5d-8df1-bf7e0d6baa71</Id>
    <BitcoinAddress>mhSW3EUNoVkD1ZQV1ZpnxdRMBjo648enyo</BitcoinAddress>
    <Signature>HJ4MSISqKn5PogbvqpQ1Iz7gNBOaXCUbtKOR6scYk7j6aVSOVlzXQeS+ZzE49+PSSzR3aqIYArNgelhWRPSTdPI=</Signature>
    <TimeStamp>2016-08-17T00:00:50</TimeStamp>
    <TableId>bf368921-346a-42d8-9cb8-621f9cad5e16</TableId>
    <HandId>398b5fe2-da27-4772-81ce-37fa615719b5</HandId>
    <Index>5</Index>
    <Action>CALL</Action>
    <Amount>50000</Amount>
    <PreviousHash>ece153165e2e9e45f1789062470a4cd3c0df7b6fbdfe84b3e52d34b3994a302c</PreviousHash>
    <HashAlgorithm>SHA256</HashAlgorithm>
  </ActionMessage>
  <ActionMessage>
    <Version>1.0</Version>
    <Id>3ea0a3de-2595-476f-b1b4-20d37fc25197</Id>
    <BitcoinAddress>msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv</BitcoinAddress>
    <Signature>G6skQA8KhDJV0FHIh/UzPs4TjiCRQNwYlqGcHO19f2bmGbijpo3t2/L0ebzrHtHePsNjJyYuvsnomf2tvTt3CPQ=</Signature>
    <TimeStamp>2016-08-17T00:01:00</TimeStamp>
    <TableId>bf368921-346a-42d8-9cb8-621f9cad5e16</TableId>
    <HandId>398b5fe2-da27-4772-81ce-37fa615719b5</HandId>
    <Index>6</Index>
    <Action>BET</Action>
    <Amount>50000</Amount>
    <PreviousHash>ca980720b8c753d56a65ec045d45b2b1344fdc423c66b566e170c68894d2998d</PreviousHash>
    <HashAlgorithm>SHA256</HashAlgorithm>
  </ActionMessage>
  <ActionMessage>
    <Version>1.0</Version>
    <Id>52cf418b-3b8b-4d91-b2fb-35d7a9ee0d1f</Id>
    <BitcoinAddress>mhSW3EUNoVkD1ZQV1ZpnxdRMBjo648enyo</BitcoinAddress>
    <Signature>G81CNbTkcipasZ9BZg5RwQyR/55uV7cq+kWehcJ0IxfHAMypuyQVSGR9EY/5RqD/E4ttBe7uDU2mfMYKI3LVW14=</Signature>
    <TimeStamp>2016-08-17T00:01:10</TimeStamp>
    <TableId>bf368921-346a-42d8-9cb8-621f9cad5e16</TableId>
    <HandId>398b5fe2-da27-4772-81ce-37fa615719b5</HandId>
    <Index>7</Index>
    <Action>CALL</Action>
    <Amount>50000</Amount>
    <PreviousHash>4320831c682de22a1074751c1a7d8c833dc3f2d268cd775d1ee924eccd04c34d</PreviousHash>
    <HashAlgorithm>SHA256</HashAlgorithm>
  </ActionMessage>
</ArrayOfActionMessage>
```

### The Shuffle
In this example, we will use a "Heads up" game of No Limit Texas Holdem.  In this case the hand contract defines Alice is the dealer, Bob the small blind, and Alice the big blind.

- Alice = msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv
- Bob = mhSW3EUNoVkD1ZQV1ZpnxdRMBjo648enyo

The deck is represented by an array[52] of bytes.  See lookup table.

- Card[0] = AH
- Card[1] = KH
- Card[2] = QH
- Card[3] = JH
...
- Card[51] = 2C

### Encrypting the deck
The deck needs to be encrypted using a commutative algorithm, such as RSA.  Alice shuffles the deck and does not disclose the un-encrypted result.

- Card[0] = AC
- Card[1] = 3S
- Card[2] = AH
- Card[3] = 2S

Create an array of 52 private keys, 16 bytes represented as base64.  These do not leave Alices computer.  \*See test data for full set.
- Key[0]=ro4So+aeT6VJt9/OKTa/Ag==
- Key[1]=GcL2OvzsDg54RIZZ5ruMFA==
- Key[2]=HEKFpbtQnjl715X5P+8Y8g==
- Key[3]=2cXOWr/IQcJ/AyqhF/W/jg==

\~\~Each card is double encrypted.  First round of encryption with the hand key.  In the example key = HBFwc/qnlFqkxwiXTmNkXw== (1c 11 70 73 fa a7 94 5a a4 c7 08 97 4e 63 64 5f) in hex.\~\~

- Card[0]=
- Card[1]=
- Card[2]=
- Card[3]=

Then each card is encrypted again with the matching key and represented as base64.  Eg, card[0] is encrypted with key[0]
- Card[0]=

#### Alice sends the deck to Bob
As the deck is encrypted, and assumed shuffled, Bob has no way to known the contents of the deck.  Bob the encrypts the deck again and shuffles, and sends the result back to Alice.

*Example message in xml*
```
<Message Version="1">
  <Deck>
    <Card Index="0"></Card>
    <Card Index="1"></Card>
    ...
    <Card Index="51"></Card>
  <Deck>
  <Signature></Signature>
</Message>
```

\*Note:  The deck could also be shuffled by a witness.

### Post blinds
In our example, Bob is SB and Alice is BB.  Using the lightning proposal, Bob creates an unsigned TX of 0.001 to Alice.  

```
```

Eg SB message in XML
```
```

### Pre flop
We know how the distribution of cards that will be dealt.  In Holdem, each card is dealt one at a time, starting left of the dealer (small blind) [Citation 1]

- Card[0] =\> Bob
- Card[1] =\> Alice
- Card[2] =\> Bob
- Card[3] =\> Alice

- Alice -\> Action request message to Bob.
- Bob -\> Returns signed action message to Alice
- Alice -\> Checks signature, and adds action response to the block
- Alice -\> Broadcasts the concatenated block to all players
- All players -\> Verify the block and signature
- All players -\> Return verification message

*TODO: CREATE SEQUENCE DIAGRAM*

### Flop, Turn and River
The client software co-ordinates the game, based off agreed game rules. 

1.  Enforces action rules of its own player, such as check, bet or fold
2.  If the action involves money, creates the tx
3.  Creates a signed message and broad casts to each player
4.  Waits for next action message
5.  Validates the message

*Example action message from Bob serialzed in XML.  A call from the small blind.*
```
<Message Version="1">
  <Action Position="1" Address="mhSW3EUNoVkD1ZQV1ZpnxdRMBjo648enyo">
    <HandId>398b5fe2-da27-4772-81ce-37fa615719b5</HandId>
    <Index>2</Index>
    <Action>CALL 10000<</Action>
    <Tx>0100000001a3022171e04852a4530ec4c3518cd7e219801a53b7de8724c59fcbf008e0c5f5000000
008a47304402205530f19e6cad5f2f4e04a92c3d4438907ac29a4ab50e6861088d2ad9e59ee61002
20134b57cce3157f0ccaf47d9928d85713611062521900941460d677ccc884da20014104f48396ac
675b97eeb54e57554827cc2b937c2dae285a9198f9582b15c920d91309bc567858dc63357bcd5d24
fd8c041ca55de8bae62c7315b0ba66fe5f96c20dffffffff0380c6ef05000000001976a9141518ab
b3523718f0231c7c6239a8e5887a4360c888aca08601000000000017a914348de5f6c91078c12849
56a88a9322be8d28341487a08601000000000017a914348de5f6c91078c1284956a88a9322be8d28
34148700000000</Tx>
    <PreviousHash Algorithm="SHA256">d4f235a5f120224ca290c8bd76ba182db67873c04bfddffe13355a0f752f7b37</PreviousHash>
  <Action>
  <Hash Algorithm="SHA256">68b4f0c0955cc947aaf179d212aa80848d7dc41c5ac845447bb504ad595bb8e9</Hash>
  <Signature>G3m66hVXe+gfbkDRS/0MNd6Sxp+Tem7i2czVNdt+aTdQXP7sUrHVOYwDX/70qywfKjEZKPr/FJ4n1kJPZKSHlSI=</Signature>
</Message>
```

### Award the pot. (Post hand consensus)
Once the hand has been played, the table then reaches consensus.  The signed game history could then be persisted into an Ethereum block chain referencing previous hands.  

Fee vs Payouts.  The table would also include a parameter when to commit the hand, or hand history, to a chain.  The more frequently it is done the more fees it will incur. 

## Cashing out
Closing the channel

## Sample hands
To be used via the mock API.
Table ID = 04dd3def-a654-4995-97e4-a1d151ef18ad
Hand ID = ce364246-6c52-40e8-a35f-18a1ea519251

Players (in seats)
2. Daniel my8qxmUcTMGpwaLr5SsGsDwXpv78BGmVuL 
3. Chris n4oaGy1uBZ4J4Pge7ZaGgbRRcJ9795dfdE
4. Tony mmbC1Gs1oGSwoT8F8VLQcwmLrNTV5DdajA
5. Phil mgXqH7yA6Djx5Wqpaspsw25mNLTx2yaPeB
6. Mike mre7fKPYqyKjACfb74rSjVNubE9ZX3t7Cb
7. Tom mxeHrRd239QgwYZtEtNo8eGrnmGeDbCFPB
9. Gus mkcuSxhM5F76vzbAXyhXayXUw4sTitJXhc \* Button

The hand is decribed in Poker Stars Hand History format.
```
PokerStars Hand ce364246-6c52-40e8-a35f-18a1ea519251:  Hold'em No Limit (0.0005/0.001 BTC) - 2015/09/10 7:13:56 ET
Table 04dd3def-a654-4995-97e4-a1d151ef18ad 9-max Seat #9 is the button
Seat 2: Daniel (0.01 in chips) 
Seat 3: Chris (0.01 in chips) 
Seat 4: Tony (0.01 in chips) 
Seat 5: Phil (0.01 in chips) 
Seat 6: Mike (0.01 in chips) 
Seat 7: Tom (0.01 in chips) 
Seat 9: Gus (0.008 in chips) 
Daniel: posts small blind 0.0005
Chris: posts big blind 0.001
*** HOLE CARDS ***
Avtovo joins the table at seat #8 
Tony: folds 
Phil: folds 
Mike: folds 
Tom: raises 0.00126 to 0.0026
Gus: folds 
Daniel: calls 0.00176
Chris: calls 0.00126
*** FLOP *** [9h Td 6h]
Daniel: checks 
Chris: checks 
Tom: bets 0.00485
koksskrt joins the table at seat #1 
Daniel: calls 0.00485
Chris: calls 0.00485
*** TURN *** [9h Td 6h] [Qh]
Daniel: checks 
Chris: checks 
Tom: checks 
*** RIVER *** [9h Td 6h Qh] [Th]
Daniel: checks 
Chris: bets 0.006
Tom: folds 
Daniel: folds 
Uncalled bet (0.006) returned to Chris
Chris collected 0.02037 from pot
Chris: doesn't show hand 
*** SUMMARY ***
Total pot $21.33 | Rake $0.96 
Board [9h Td 6h Qh Th]
Seat 2: Daniel (small blind) folded on the River
Seat 3: Chris (big blind) collected (0.02037)
Seat 4: Tony folded before Flop (didn't bet)
Seat 5: Phil folded before Flop (didn't bet)
Seat 6: sylvian31 folded before Flop (didn't bet)
Seat 7: Tom folded on the River
Seat 9: Gus (button) folded before Flop (didn't bet)
```
Private Keys
2. Daniel 92DurEQEwd5KR4BjeaLfK3xuuSiQEWNc8R3szPH1VM5XorMa3eq 
3. Chris 92ZY3DxX8h9wpueDwSzD8YWKXCi1YT9yrvYDVhfnbqXgCvKQmX6
4. Tony 92WmrGTyfW6uvZdzy7TsoARwtg9QpzNvxz31UTeoD2WSgSLQHHg
5. Phil 92Q53JuV5L3ehi2ML4CbwWAhGrAJUAd2jc39yke4bqqW1XUpgQx
6. Mike 92rXYx7owd2AgUiJZzDUJrNy4EBD92y1x6JSQBiSHkKD6PdL9Zt
7. Tom 91qV38Xgc2KS5zNJfXfi9rK36FUS14yoFPE9iaEpmHckFLCWsTX
9. Gus 92GD9YDCT4koimj3nBQ2gcLZbxdHdkcLPboBjibjJAhP5Gks8An

The above hand as a message chain.
```

```

## Network Topology

### Dealing with disconnects

## Test Data
- Alice keys
- Bob keys
- Sample hand XML
- Sample hand JSON
For full key set, see the test data folder
Cold deck from https://www.benjoffe.com/holdem

### Mock API
An REST API is located at https://www.bitpoker.io/api for users to develop clients against.  The API returns mock data to develop against.

| Verb  | Uri |
| ------------- | ------------- |
| GET | /Peers |
| GET | /Players |
| GET | /Players/bitcoinaddress |
| GET | /Tables |

## Coloured coin crowd sale
The following colour coin asset will be sold to raise funds for the development of the project. Asset Id Ua9V5JgADia5zJdSnSTDDenKhPuTVc6RbeNmsJ

## Coloured coin chip (testnet)
Used as in game currency mhAYHH9nuCzSwp47asR7Yo3wthsXgL9EhR

## Colour coin chip
Used as in game currency 

## References
1. https://lightning.network/lightning-network-paper.pdf
2. http://www.pokerlistings.com/poker-rules-texas-holdem
3. http://ianpurton.com/online-pgp/
4. http://www.codeproject.com/Articles/835098/NBitcoin-Build-Them-All
5. https://www.benjoffe.com/holdem
6. http://ms-brainwallet.org
7. http://json-rpc.org/wiki/specification
8. https://msdn.microsoft.com/en-us/library/bb756931.aspx?tduid=(fc4ef1dccc45eb37dfbbbf748821ac83)(256380)(2459594)(TnL5HPStwNw-fIrr3XMwfP4kdvb3oN5V_A)()
9. http://www.nongnu.org/libtmcg/MentalPoker.pdf
10. https://people.csail.mit.edu/rivest/ShamirRivestAdleman-MentalPoker.pdf
11. http://www.nongnu.org/libtmcg/WEWoRC2005_proc.pdf
12. http://www.cs.technion.ac.il/~ranjit/papers/poker.pdf
13. A c# poker engine https://github.com/NikolayIT/TexasHoldemGameEngine

