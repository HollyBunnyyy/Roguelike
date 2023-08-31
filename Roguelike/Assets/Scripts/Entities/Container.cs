using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : Entity
{
    public Inventory<Item> Inventory = new Inventory<Item>( 6 );

}
