
    using UnityEngine;

    public class User
    {
        [SerializeField] private string name;
        [SerializeField] private int gold;

        public User(string name, int gold)
        {
            this.name = name;
            this.gold = gold;
        }
    }
