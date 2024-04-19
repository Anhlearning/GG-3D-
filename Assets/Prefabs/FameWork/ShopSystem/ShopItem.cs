using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Object = UnityEngine.Object;
[CreateAssetMenu(menuName ="Shop/ShopItem")]
public class ShopItem : ScriptableObject
{
    public String title;
    public int price;
    public Object item;
    public Sprite Itemicon;
    [TextArea(10,10)]
    public string description;

}
