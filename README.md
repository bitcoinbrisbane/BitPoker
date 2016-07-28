# BitPoker.IO

## Abstract
Inspired by OpenBazaar.com, the goal of the project is to design a peer to peer protocol of games, such as online poker, in which no central actor can control the outcome and thus rig the game and is proovably fair.  The game uses bitcoin and lightning network to settle bets between actors.

Different clients developed in different programming languages are encouraged.

### Notation

Ids should be represented as GUIDs, and be in lower case
All values represented in base16 (hex) should be lower case

Deck is represented as an array of Bytes.

| Key  | Value | Decimal | Byte |
| -----|------ | -------|------ |
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
| H  | Heart | +26 | |
| D  | Diamond  | +39  ||

*Eg
Ace of clubs = { 0x0D }

Poker terminology
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

*2 of 3 address
2NCSTuV27SC1BF122Xe1wmkNkjo4MJw4W85
https://testnet.blockexplorer.com/address/2NCSTuV27SC1BF122Xe1wmkNkjo4MJw4W85

*Redeem Script* 524104c82b8e2d6ea7f17665c4a1070f340e84d4c02da72ae5018574001841c10e8009a04e2c333d3eb90102e71b324bfe595430d4c654bbff0f66edbfe63798c7a2714104f48396ac675b97eeb54e57554827cc2b937c2dae285a9198f9582b15c920d91309bc567858dc63357bcd5d24fd8c041ca55de8bae62c7315b0ba66fe5f96c20d4104f48396ac675b97eeb54e57554827cc2b937c2dae285a9198f9582b15c920d91309bc567858dc63357bcd5d24fd8c041ca55de8bae62c7315b0ba66fe5f96c20d53ae

References:
http://ms-brainwallet.org

https://coinb.in/?verify=524104c82b8e2d6ea7f17665c4a1070f340e84d4c02da72ae5018574001841c10e8009a04e2c333d3eb90102e71b324bfe595430d4c654bbff0f66edbfe63798c7a2714104f48396ac675b97eeb54e57554827cc2b937c2dae285a9198f9582b15c920d91309bc567858dc63357bcd5d24fd8c041ca55de8bae62c7315b0ba66fe5f96c20d4104f48396ac675b97eeb54e57554827cc2b937c2dae285a9198f9582b15c920d91309bc567858dc63357bcd5d24fd8c041ca55de8bae62c7315b0ba66fe5f96c20d53ae#verify

### Test PGP Private and Public Keys (NOTE BITCOIN MESSAGE SIGNING TO BE USED INSTEAD)

*Alice*
-----BEGIN PGP PRIVATE KEY BLOCK-----
Version: OpenPGP.js v.1.20130420
Comment: http://openpgpjs.org

xcA4BFb6E1MBAf9pyMRXuXc9A7DR8dDe7zkVEksMfOIDuYAE/Kd4cfbpRLRJ
5Ph0iVi69XU3GV1KS+1ThkqrlU94p8/WKAQRjdt7ABEBAAEAAf4gqwavjLAd
IJmnjEUTw87eCgMxPRPTUKMPZzMUyCUXTmFs3q+DGVnIDs928fgNS0RIovSB
GXZnx6uH74qKqg8BAQCxcsy4qvapXHAHmHVgxvgkQFa7xec/nXNKcCJ8xZiq
gQEAmJyuUqmFxaa9FtOOz+9lHqVG5L6E/1V0jupaWHsRL/sA/RDQgcmIZlo3
jGDX1zK6kt5H65pUKjZqFSFdS5vwT1HXUInNJFRlc3QgTWNUZXN0aW5ndG9u
IDx0ZXN0QGV4YW1wbGUuY29tPsJcBBABCAAQBQJW+hNTCRBx4oH3PIZldwAA
DO8B/0Lf1eqIfbBCIhqq4n7ge/qy5ITQKtMT1+P2pSqGj2+6/hUQhmHcvqBq
Sp07i29QBZiup2mExktQMaMvCLD40Vo=
=tIvp
-----END PGP PRIVATE KEY BLOCK-----

-----BEGIN PGP PUBLIC KEY BLOCK-----
Version: OpenPGP.js v.1.20130420
Comment: http://openpgpjs.org

xk0EVvoTUwEB/2nIxFe5dz0DsNHx0N7vORUSSwx84gO5gAT8p3hx9ulEtEnk
+HSJWLr1dTcZXUpL7VOGSquVT3inz9YoBBGN23sAEQEAAc0kVGVzdCBNY1Rl
c3Rpbmd0b24gPHRlc3RAZXhhbXBsZS5jb20+wlwEEAEIABAFAlb6E1MJEHHi
gfc8hmV3AAAM7wH/Qt/V6oh9sEIiGqrifuB7+rLkhNAq0xPX4/alKoaPb7r+
FRCGYdy+oGpKnTuLb1AFmK6naYTGS1Axoy8IsPjRWg==
=cWad
-----END PGP PUBLIC KEY BLOCK-----

*Bob*
-----BEGIN PGP PRIVATE KEY BLOCK-----
Version: OpenPGP.js v.1.20130420
Comment: http://openpgpjs.org

xcA4BFb6FKoBAgC5MgyxxJSELbTHZpN6T8H5ncblOmZkXrFlXtxC9acdReg/
15PWiB0omxzDRI0PvUsRHIcgJvd9h97Nq2F6eUITABEBAAEAAf9nk9SBwD6I
Rr/rHjWxnNYSc+n/3s/Rpxx0Y7+xO49B0RmLVBEt8QljZIkc/tXC4HqwzpwW
FmLQFrPqUWoKtkWhAQDmINi1D92m3B4zjgqQ94ZPgiWrGsN+hoPJlWj0JyPc
eQEAzgQGc5iRhOSQJ+35Rw0ma0h/kAOHB6nUYWywE7P3F+sBAIOkByxHtuQi
z2PmIxN9pRa6VQrm84HBpOWfzXEqS2J0UULNJFRlc3QgTWNUZXN0aW5ndG9u
IDx0ZXN0QGV4YW1wbGUuY29tPsJcBBABCAAQBQJW+hSrCRAT+yaNasjJggAA
1W0CAJRnrk8vd6PJhoBrH22U8ninyQahVmdmQ31jwdvVuENUNNPmxVD2UPQJ
nuiaFDXyQsyWs/nnbliJdS4F1HtGOCA=
=B2vt
-----END PGP PRIVATE KEY BLOCK-----

-----BEGIN PGP PUBLIC KEY BLOCK-----
Version: OpenPGP.js v.1.20130420
Comment: http://openpgpjs.org

xk0EVvoUqgECALkyDLHElIQttMdmk3pPwfmdxuU6ZmResWVe3EL1px1F6D/X
k9aIHSibHMNEjQ+9SxEchyAm932H3s2rYXp5QhMAEQEAAc0kVGVzdCBNY1Rl
c3Rpbmd0b24gPHRlc3RAZXhhbXBsZS5jb20+wlwEEAEIABAFAlb6FKsJEBP7
Jo1qyMmCAADVbQIAlGeuTy93o8mGgGsfbZTyeKfJBqFWZ2ZDfWPB29W4Q1Q0
0+bFUPZQ9Ame6JoUNfJCzJaz+eduWIl1LgXUe0Y4IA==
=Amq+
-----END PGP PUBLIC KEY BLOCK-----


## The protocol
Each client connects to one another in the "lobby".  They can then look for players who are looking to start a game, or request to join a running game.  Messages are sent to all players, signed, and referencing the existing message.  Thus like a block chain of messages.

- Table reaches consensus on whos turn to act based off the game contract
- Table reaches conesnsus on the legal moves / actions a player can make
- Table waits for a signed message from that player
- All other players validate that message
- Repeat

### Sample Message types (See below for more on messages)
- Buy In
- Quit
- Sit Out
- Action (CALL, BET, RAISE, FOLD, MUCK)
- Shuffle

### Overview
If the game is to be developed using Etherum contracts:

1.  The game is defined an an Etherum contract
2.  Players agree to the table contract
3.  Each players actions are defined as inputs for the hand contract
4.  After the hand has ended, each player verifies the integrety of the hand contract.  Its in everyones best intrest to verify correctly [Game Theory Citation]
5.  The hand inputs are then excuted on the Etherum network for the pot to be awarded

Less use of Etherum

1.  Players connect to each other via P2P
2.  A player either looks to join a table and reviews the contract
3.  A palyer can choose to start a table be defining a table contract
4.  Tables should also broad cast their game, status and number of current players to other tables
5.  Leaving the table (closing the channel)
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

## Messages
All actions are sent as messages.  They must include a public key be signed.  The payload must also reference there previous message hash.

| Property  | Eg |
| --------- | -- |
| Version  | 1  |
| Public Key Hash | msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv |
| Action | Enum (TABLE, ACTION, BUYIN, SHUFFLE, DECK) |
| Payload | |
| Message Signature | |
| Pervious Hash | |

Example action message (payload)

| Property  | Eg |
| ------------- | ------------- |
| Id | 4BC7F305-AA16-450A-A3BE-AAD8FBA7F425 |
| Hand | 398b5fe2-da27-4772-81ce-37fa615719b5 |
| Index | 2 |
| Action | CALL 5000000 |
| TX | |
| Previous Hash | |

The hash (SHA-256) is of the property values concantinated, thus seriliazation format agnostic.

Eg of above message
```
1msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv398b5fe2-da27-4772-81ce-37fa615719b52CALL 5000000
```

Eg in XML
```
<Message Version="1" Type="Action">
  <PublicKeyHash>mhSW3EUNoVkD1ZQV1ZpnxdRMBjo648enyo</PublicKeyHash>
  <Action Position="1">
    <Id>4BC7F305-AA16-450A-A3BE-AAD8FBA7F425</Id>
    <HandId>398b5fe2-da27-4772-81ce-37fa615719b5</HandId>
    <Index>2</Index>
    <Action>CALL 5000000<</Action>
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
  <MessageSignature>Gztf4/3oNvanh51g11W4NlPOEyAhTCURaPTOF13Yl3iYHHh+SUvGk/5dtuZDKQdteuLAwIt8K5uthLTYsyf90rI=</MessageSignature>
  <Hash Algorithm="SHA256"></Hash>
</Message>
```

## Table Contract
The paramaters for a table are defined in the following schema.  Developers are encouraged to create their own algorithms, such as voting or anti-collusion.

1.  Encryption Algorithm (Enum AES-256)
2.  Hash Algorithm (Enum SHA-256)
3.  Table ID (GUID)
3.  Currency (Enum)
3.  Blinds
4.  Rake*
5.  Min players
6.  Max players
7.  Game type (Enum, No Limit Texas Holdem) *
8.  Other (straddles, "run it twice") *
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
      <SmallBlind>0.001</SmallBlind>
      <BigBlind>0.002</BigBlind>
    </Blinds>
    <BuyIn>
      <Min>0.1</Min>
      <Max<0.5</Max>
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

## Witness nodes
Game witness can also be allowed or chosen to arbitrate a game.  The witness could also help network propigation.  A witness would be choose by the table starter and a small rake paid to the witness.

There will become a market for reputable witnesses based off a https dns endpoint and earn small revenues for witnessing hands.

## Buying in
A player buying in opens a lightning payment channel with all players.

"Through this network of interconnected payment channels, Lightning provides a scalable, decentralized micropayments solution on top of the Bitcoin blockchain." [https://lightning.network/lightning-network-technical-summary.pdf]

### Process
1.  Alice and Bob create a 2 of 2 address
2.  Alice creates a deposit transaction
3.  Bob creates a deposit transaction
4.  Alice creates a refund transaction but does not broadcast
5.  Bob creates a refund transaction but does not broadcast

*2 of 2 Redeem Script and Address for Alice and Bob
```
2 041fa97efd760f26e93e91e29fddf3ddddd3f543841cf9435bdc156fb73854f4bf22557798ba53
5a3ee89a62238c5afc7f8bf1fa0985dc4e1a06c25209bab78bd1 041fa97efd760f26e93e91e29fd
df3ddddd3f543841cf9435bdc156fb73854f4bf22557798ba535a3ee89a62238c5afc7f8bf1fa098
5dc4e1a06c25209bab78bd1 2 OP_CHECKMULTISIG

2Mx377XSXhvqqVyLaXsPDAAEsJFzGeWunKi
```

###Funding TXs
Both Alice and Bob now deposit their buy in to the address 2Mx377XSXhvqqVyLaXsPDAAEsJFzGeWunKi.  Note:  The table contract could include a minumum confirmation count.

Alice tx  f5c5e008f0cb9fc52487deb7531a8019e2d78c51c3c40e53a45248e0712102a3
Bob tx c60193a33174a1252df9deb522bac3e5532e0c756d053e4ac9999ca17a79c74e

*Sample C# NBitcoin code
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

*Raw TX Alice buy in of 0.5 BTC
```
0100000001a3022171e04852a4530ec4c3518cd7e219801a53b7de8724c59fcbf008e0c5f5000000
008b483045022100c21e5c296d3024f64dbd948b1999933206a3d3d757ff1004ce874fa4b9277acc
02202d0c0115b4f52a7de2a1863141eda25192255015da14765a1409d8d202f096b40141041fa97e
fd760f26e93e91e29fddf3ddddd3f543841cf9435bdc156fb73854f4bf22557798ba535a3ee89a62
238c5afc7f8bf1fa0985dc4e1a06c25209bab78bd1ffffffff02e069f902000000001976a914822f
3782f8d0357cb6fc7b4c5cfc1424b3f0100988ac80f0fa020000000017a914348de5f6c91078c128
4956a88a9322be8d2834148700000000
```

*In JSON
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
The dealer's client is responsible for the orchastration of the game.  As the dealer position rotates, this isn't a centralisation risk.  The intnet is to limit network traffic.
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

1.  The players and seat postions
2.  The stack of each player
3.  A ID as a GUID

*Example hand contract seralized in XML
```
<Message Verison="1">
  <Hand ID="398b5fe2-da27-4772-81ce-37fa615719b5" Table ID="bf368921-346a-42d8-9cb8-621f9cad5e16" TableContract="5ed4565da9b0cf46f8e3b6a5e6353d0c41b7d1b88234de5310315be670c2cf13">
    <Seat Number="1" Position="SB" Stack="1.01">mhSW3EUNoVkD1ZQV1ZpnxdRMBjo648enyo</Seat>
    <Seat Number="2" Position="BB,Dealer" Stack="0.9">msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv</Seat>
    <Witness>mq1Ctw6xTcomjGgQz5pi8oXdR1tjjZQHYs</Witness>
  <Hand>
</Message>
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

Create an array of 52 private keys, 16 bytes represented as base64.  These do not leave Alices computer.  *See test data for full set.
- Key[0]=ro4So+aeT6VJt9/OKTa/Ag==
- Key[1]=GcL2OvzsDg54RIZZ5ruMFA==
- Key[2]=HEKFpbtQnjl715X5P+8Y8g==
- Key[3]=2cXOWr/IQcJ/AyqhF/W/jg==

~~Each card is double encrypted.  First round of encryption with the hand key.  In the example key = HBFwc/qnlFqkxwiXTmNkXw== (1c 11 70 73 fa a7 94 5a a4 c7 08 97 4e 63 64 5f) in hex.~~

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

*Note:  The deck could also be shuffled by a witness.

### Post blinds
In our example, Bob is SB and Alice is BB.  Using the lightning proposal, Bob creates an unsigned TX of 0.001 to Alice.  

```
```

Eg SB message in XML
```
```

### Pre flop
We know how the distribution of cards that will be dealt.  In Holdem, each card is dealt one at a time, starting left of the dealer (small blind) [Citation 1]

- Card[0] => Bob
- Card[1] => Alice
- Card[2] => Bob
- Card[3] => Alice

- Alice -> Action request message to Bob.
- Bob -> Returns signed action message to Alice
- Alice -> Checks signature, and adds action response to the block
- Alice -> Broadcasts the concatinated block to all players
- All players -> Verifiy the block and signature
- All players -> Return verification message

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
Once the hand has been played, the table then reaches consensus.  The signed game history could then be persistend into an Ethereum block chain referencing previous hands.  

Fee vs Payouts.  The table would also include a paramater when to commite the hand, or hand history, to a chain.  The more frequently it is done the more fees it will incure. 

## Cashing out
Closing the channel

## Sample hand
To be used via the mock API.
Table ID = 
Hand ID =

The hand is decribed in Poker Stars Hand History format.
```
PokerStars Hand #GUID:  Hold'em No Limit (0.005/0.01 BTC) - 2015/09/10 7:31:28 ET
Table GUID 10-max Seat #3 is the button
Seat 1: alice ($111.26 in chips) 
Seat 2: bob ($99.49 in chips) 
Sweden_Pound: posts small blind $0.50
sylvian31: is sitting out 
sylvian31 leaves the table
KLOP06031987: posts big blind $1
*** HOLE CARDS ***
Avtovo: raises $1.50 to $2.50
SharingaaN: folds 
koksskrt: folds 
nickgodro: calls $2.50
DarioNo$had: folds 
Monsthand: folds 
Sweden_Pound: folds 
KLOP06031987: folds 
*** FLOP *** [7d Qh 9s]
Avtovo: bets $4
nickgodro: calls $4
*** TURN *** [7d Qh 9s] [4h]
Avtovo: bets $8.31
nickgodro: calls $8.31
*** RIVER *** [7d Qh 9s 4h] [6s]
Avtovo: checks 
nickgodro: bets $10
maxxmeister joins the table at seat #6 
Avtovo: calls $10
*** SHOW DOWN ***
nickgodro: shows [Qs Ks] (a pair of Queens)
Avtovo: mucks hand 
nickgodro collected $48.82 from pot
*** SUMMARY ***
Total pot $51.12 | Rake $2.30 
Board [7d Qh 9s 4h 6s]
Seat 1: koksskrt folded before Flop (didn't bet)
Seat 2: nickgodro showed [Qs Ks] and won ($48.82) with a pair of Queens
Seat 3: DarioNo$had folded before Flop (didn't bet)
Seat 4: Monsthand (button) folded before Flop (didn't bet)
Seat 5: Sweden_Pound (small blind) folded before Flop
Seat 7: KLOP06031987 (big blind) folded before Flop
Seat 8: Avtovo mucked
Seat 9: SharingaaN folded before Flop (didn't bet)
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
An REST API is located at https://bitpoker.azurewebsites.net/api for users to develop clients against.

| Verb  | Uri |
| ------------- | ------------- |
| GET | /Players |
| GET | /Players/<bitcoinaddress> |
| GET | /Tables |

## Coloured coin crowd sale
The following colour coin asset will be sold to raise funds for the development of the project. Asset Id Ua9V5JgADia5zJdSnSTDDenKhPuTVc6RbeNmsJ
http://coloredcoins.org/explorer/asset/Ua9V5JgADia5zJdSnSTDDenKhPuTVc6RbeNmsJ/f5102f4c37a64ede406173ed707d3458c6258dca48f72f0efdb234ce38e2d9f8/0

## References
1. https://lightning.network/lightning-network-paper.pdf
2. http://www.pokerlistings.com/poker-rules-texas-holdem
3. http://ianpurton.com/online-pgp/
4. http://www.codeproject.com/Articles/835098/NBitcoin-Build-Them-All
5. https://www.benjoffe.com/holdem
