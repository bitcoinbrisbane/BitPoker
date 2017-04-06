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

contract TexasHoldem is BitPoker{
    uint256 minBuyIn;
    uint256 maxBuyIn;
    uint256 smallBlind;
    uint256 bigBlind;

    uint8 minPlayers;
    uint8 maxPlayers;

    uint8 dealerIndex = 0;
    uint256 refundDate;

    //move to base
    address owner;
    string contractUrl;

    enum AllowedAction {
        SmallBlind,
        BigBlind,
        Check,
        Bet,
        Call,
        Raise,
        Fold
    }

    struct Player {
        uint256 stack;
        address myAddress;
    }

    struct Action {
        AllowedAction action;
        address player;
        uint256 amount;
    }

    struct Hand {
        Action[] actions;
    }

    //mapping (address => uint256) public players;
    Player[] players;

    //Url to owners game
    function TexasHoldem(string _url, uint8 _minPlayers, uint8 _maxPlayers)
    {
        owner = msg.sender;
        contractUrl = _url;
    }

    //buy in
    function buyIn ()
    {
        //Add players balance
    }

    function awardPot(Action[] actions) internal
    {
        if (actions.length == 0) throw;

        if (action[0].action != smallBlind) throw;
    }
}