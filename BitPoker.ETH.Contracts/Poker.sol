pragma solidity ^0.4.9;

//Base rules etc
contract BitPoker {
    
    enum Rank {
        2,
        3,
        4,
        5,
        6,
        7,
        8,
        9,
        10,
        Jack,
        Queen,
        King,
        Ace
    }

    enum Suit {
        Hearts,
        Diamonds,
        Clubs,
        Spades
    }

    struct Card {
        Suit: suit,
        Rank: rank
    }

    uint16 rake;
}