pragma solidity ^0.4.8;

import "Chip";

//ICO contract
contract Cashier as Chip {
    uint256 public totalSupply = 10000000;
    uint256 public startDate;
    //decimals decimalUnits = 4;

    /* This creates an array with all balances */
    mapping (address=>uint256) public balanceOf;

    /* This generates a public event on the blockchain that will notify clients */
    event Transfer(address indexed from, address indexed to, uint256 value);

    function Cashier()
    {
        startDate = now;
    }

    function buy () payable {
        //Sliding scale of ICO
        uint256 amount = 0;
        if (now < startDate + 30 days)
        {
            amount = msg.value * 10000;
        }
        else
        {
            amount = msg.value * 5000;
        }

        balanceOf[msg.sender] += amount;
    }

    /* Send chips */
    function transfer(address _to, uint256 _value) {
        if (balanceOf[msg.sender] < _value) throw;           // Check if the sender has enough
        if (balanceOf[_to] + _value < balanceOf[_to]) throw; // Check for overflows
        balanceOf[msg.sender] -= _value;                     // Subtract from the sender
        balanceOf[_to] += _value;                            // Add the same to the recipient
        Transfer(msg.sender, _to, _value);                   // Notify anyone listening that this transfer took place
    }      

    /* A contract attempts to get the chips */
    function transferFrom(address _from, address _to, uint256 _value) returns (bool success) {
        if (balanceOf[_from] < _value) throw;                 // Check if the sender has enough
        if (balanceOf[_to] + _value < balanceOf[_to]) throw;  // Check for overflows
        if (_value > allowance[_from][msg.sender]) throw;   // Check allowance
        balanceOf[_from] -= _value;                          // Subtract from the sender
        balanceOf[_to] += _value;                            // Add the same to the recipient
        allowance[_from][msg.sender] -= _value;
        Transfer(_from, _to, _value);
        return true;
    }
}