pragma solidity ^0.4.8;

import "Chip";

//Base rules etc
contract BitPoker is Chip{
    
    enum Rank {
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
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

contract TexasHoldem is BitPoker { 
    uint256 public minBuyIn;
    uint256 public maxBuyIn;
    uint256 public smallBlind;
    uint256 public bigBlind;

    uint8 public minPlayers;
    uint8 public maxPlayers;

    //Hand indexes
    uint8 public dealerIndex = 0;
    uint256 public handIndex = 0;

    //Hours +
    uint256 public refundDate;

    //move to base
    address public owner;
    string public contractUrl;

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
        address name;
    }

    struct Action {
        //AllowedAction action;
        string action;
        address player;
        uint256 amount;
    }

    struct Hand {
        uint8 dealerIndex;
        Action[] actions;
    }

    //mapping (address => uint256) public players;
    Player[] public players;

    Hand[] public hands;

    //Url to owners game
    function TexasHoldem(string _url, uint8 _minPlayers, uint8 _maxPlayers)
    {
        owner = msg.sender;
        contractUrl = _url;
    }

    //buy in
    function buyIn (uint8 seatNumber, uint256 stack)
    {
        //Add players balance
        var player = Player(stack, msg.sender);
        players[seatNumber] = player;
    }

    // function sit(uint8 seatNumber)
    // {
    //     //Ignore checking if seat is filled
    // }

    function addAction(string _action, uint256 _amount)
    {
        //Current hand
        var hand = Hands[handIndex];

        var playerToAct = players[hand.dealerIndex + 1];

        var action = Action(_action, msg.sender, 0);

        if (msg.sender == playerToAct.name)
        {
            if (hand.dealerIndex + 1 == 1)
            {
                if (_action == "SmallBlind")
                {
                    action.amount = smallBlind;
                    Actions[hand.dealerIndex + 1] = action;
                }
                else
                {
                    throw;
                }
            }

            if (hand.dealerIndex + 1 == 2)
            {
                if (_action == "BigBlind")
                {
                    action.amount = bigBlind;
                    Actions[hand.dealerIndex + 1] = action;
                }
                else
                {
                    throw;
                }
            }
        }
        else
        {
            throw;
        }

    }

    function awardPot(string _action, uint256 _amount)
    {

    }
}