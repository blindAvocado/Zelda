using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda
{
    [Serializable]
    public class MenuGame : MenuBase
    {
        private Dungeon dungeon;
        //private string currentRoomName;
        //[NonSerialized] private Room currentRoom;
        private EntityPlayer player;
        [NonSerialized] private Sprite gui;

        public MenuGame() : base()
        {
            //this.currentRoomName = "room1";
            //this.currentRoom = Room.LoadRoom(currentRoomName);
            this.player = new EntityPlayer(10);
            this.player.SetWeapon(Weapon.ForestBow);
            this.gui = new Sprite("gui", 0, 0);

            this.dungeon = new Dungeon(new Random(), 5, 5, this.player);
            //this.currentRoom.Spawn(player, 1, 1);
            //this.currentRoom.Spawn(new EntityEnemy(new Sprite("enemy_octorok", 0, 0, 4, 2), 1, 1, Weapon.None, 1), 4, 3);
            //this.currentRoom.SpawnItem(new ItemKey(), 4, 5);
            //this.currentRoom.SpawnItem(new ItemBomb(), 8, 3);
            //this.currentRoom.SpawnItem(new ItemWeapon(Weapon.ForestSword), 3, 6);
        }


        public override void Update(GameTime gameTime, Input input)
        {
            //this.currentRoom.Update(gameTime, input);
            this.dungeon.Update(gameTime, input);
        }

        public void Initialize()
        {
            //this.currentRoom = Room.LoadRoom(currentRoomName);
            this.gui = new Sprite("gui", 0, 0);
            //this.currentRoom.SpawnItem(new ItemKey(), 4, 5);
            //this.currentRoom.SpawnItem(new ItemBomb(), 8, 3);
            //this.currentRoom.SpawnItem(new ItemWeapon(Weapon.ForestSword), 3, 6);
            Console.WriteLine(this.player.Weapon);
            this.player.Initialize();
            //this.currentRoom.Spawn(player, 1, 1);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            this.dungeon.Draw(spriteBatch);
            //this.currentRoom.Draw(spriteBatch);
            this.gui.Draw(spriteBatch);

            spriteBatch.DrawString(Resources.Fonts["Font"], this.player.CoinsCount.ToString(), new Vector2(104, 16), Color.White);
            spriteBatch.DrawString(Resources.Fonts["Font"], this.player.KeysCount.ToString(), new Vector2(104, 32), Color.White);
            spriteBatch.DrawString(Resources.Fonts["Font"], this.player.BombsCount.ToString(), new Vector2(104, 40), Color.White);

            int i = 0;
            int offsetY = 0;
            for (; i < this.player.CurrentLife; ++i)
            {
                spriteBatch.Draw(Resources.Images["heart"], new Vector2(176 + ((i % 8) * 8), 40 + offsetY), new Color(248, 56, 0));
                if (i == 7)
                    offsetY = -9;
            }
            for (; i < this.player.MaxLife; ++i)
            {
                spriteBatch.Draw(Resources.Images["heart"], new Vector2(176 + ((i % 8) * 8), 40 + offsetY), new Color(252, 188, 176));
                if (i == 7)
                    offsetY = -9;
            }

            if (this.player.Weapon != Weapon.None)
            {
                this.player.Weapon.Draw(spriteBatch);
            }
        }
    }
}
