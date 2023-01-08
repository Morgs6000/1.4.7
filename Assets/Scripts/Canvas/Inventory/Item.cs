using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Item", order = 30)]
public class Item : ScriptableObject {
    public Sprite sprite;

    public int maxStack;
}
