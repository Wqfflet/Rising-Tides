using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public string shipName;
    public float maxHealth;
    public float currHealth;
    public float defense;
    public float strength;
    public float attackSpeed;

    public List<Tile> tiles = new List<Tile>();

    public void CreateShip(string shipName, float maxHealth, float currHealth, float defense, float strength, float attackSpeed, List<Tile> tiles)
    {
        this.shipName = shipName;
        this.maxHealth = maxHealth;
        this.currHealth = currHealth;
        this.defense = defense;
        this.strength = strength;
        this.attackSpeed = attackSpeed;
        this.tiles = tiles;
    }


}
