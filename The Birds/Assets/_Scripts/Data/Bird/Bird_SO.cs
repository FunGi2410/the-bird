using UnityEngine;

[CreateAssetMenu(menuName = "Bird SO", order = 1)]
public class Bird_SO : ScriptableObject
{
    public string birdName;
    public float timming;
    public int price;

    public float fireRate;
    public float damage;
    public float health;
}
