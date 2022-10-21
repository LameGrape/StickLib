using BepInEx;
using UnityEngine;

namespace StickLib
{
    public class Player
    {
        public int id;
        public Controller controller;

        public Player(Controller con)
        {
            controller = con;
            id = con.playerID;
        }

        public static Player Get(int index)
        {
            Controller[] players = GameObject.FindObjectsOfType<Controller>();
            foreach (Controller player in players)
            {
                if (player.playerID == index)
                {
                    return new Player(player);
                }
            }
            return null;
        }
        public static Player[] GetAll()
        {
            Controller[] players = GameObject.FindObjectsOfType<Controller>();
            Player[] playerList = new Player[players.Length];
            for (int i = 0; i < players.Length; i++)
            {
                playerList[i] = new Player(players[i]);
            }
            return playerList;
        }
        public static Player Yellow
        {
            get
            {
                return Get(0);
            }
        }
        public static Player Blue
        {
            get
            {
                return Get(1);
            }
        }
        public static Player Red
        {
            get
            {
                return Get(2);
            }
        }
        public static Player Green
        {
            get
            {
                return Get(3);
            }
        }
    }
}