using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Zelda
{
    [Serializable]
    public class Dungeon
    {
        private static int ROOMS_LAYOUT = 5;

        [NonSerialized] private Random random;
        private EntityPlayer player;

        private Room[,] rooms;
        private Room currentRoom;

        public Dungeon(Random random, int width, int height, EntityPlayer player)
        {
            this.player = player;
            this.random = random;

            this.rooms = new Room[width, height];
            this.currentRoom = null;

            this.GenerateRooms();
            this.currentRoom.Spawn(this.player, 5, 3);
            this.player.CurrentRoom = this.currentRoom;
        }

        public void Initialize()
        {
            for (int y = 0; y < this.rooms.GetLength(1); ++y)
            {
                for (int x = 0; x < this.rooms.GetLength(0); ++x)
                {
                    this.rooms[x, y]?.Initialize();
                }
            }
        }

        public void Save()
        {
            for (int y = 0; y < this.rooms.GetLength(1); ++y)
            {
                for (int x = 0; x < this.rooms.GetLength(0); ++x)
                {
                    this.rooms[x, y]?.Save();
                }
            }
        }

        private void GenerateRooms()
        {
            int width = this.rooms.GetLength(0);
            int height = this.rooms.GetLength(0);
            char[,] map = new char[width, height];

            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    map[x, y] = 'x';
                }
            }
            
            int startX = this.random.Next(width);
            int startY = this.random.Next(height);

            map[startX, startY] = 's';

            GenerateRoomsAround(ref map, startX, startY);
            
            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    Debug.Write(map[x, y]);
                    if (map[x, y] == 's')
                    {
                        this.currentRoom = new Room(true);
                        this.currentRoom.RoomID = map[x, y];
                        this.rooms[x, y] = this.currentRoom;
                        //this.currentRoom.SpawnItem(new ItemWeapon(Weapon.ForestSword), 3, 3);
                    }
                    else if (map[x, y] < 'x')
                    {
                        Room room = Room.LoadRoom("room" + map[x, y]);
                        room.RoomID = map[x, y];
                        this.rooms[x, y] = room;
                    }
                }
                Debug.WriteLine("");
            }

            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    Room room = this.rooms[x, y];
                    if (room != null)
                    {
                        Random rand = new Random();
                        //ADD DOORS
                        if (GetRoom(map, x + 1, y) < 'x')
                            room.AddDoor(this, Direction.RIGHT, DoorType.OPEN, this.rooms[x + 1, y]);
                        else
                        {
                            if (0 == rand.Next(10) && (x+1) < this.rooms.GetLength(0))
                                room.AddDoor(this, Direction.RIGHT, DoorType.CLOSED, this.rooms[x + 1, y]);
                        }

                        if (GetRoom(map, x - 1, y) < 'x')
                            room.AddDoor(this, Direction.LEFT, DoorType.OPEN, this.rooms[x - 1, y]);
                        else
                        {
                            if (0 == rand.Next(10) && (x - 1) > this.rooms.GetLength(0))
                                room.AddDoor(this, Direction.LEFT, DoorType.CLOSED, this.rooms[x - 1, y]);
                        }

                        if (GetRoom(map, x, y + 1) < 'x')
                            room.AddDoor(this, Direction.DOWN, DoorType.OPEN, this.rooms[x, y + 1]);
                        else
                        {
                            if (0 == rand.Next(10) && (y + 1) > this.rooms.GetLength(1))
                                room.AddDoor(this, Direction.DOWN, DoorType.CLOSED, this.rooms[x, y + 1]);
                        }

                        if (GetRoom(map, x, y - 1) < 'x')
                            room.AddDoor(this, Direction.UP, DoorType.OPEN, this.rooms[x, y - 1]);
                        else
                        {
                            if (0 == rand.Next(10) && (y - 1) > this.rooms.GetLength(1))
                                room.AddDoor(this, Direction.UP, DoorType.CLOSED, this.rooms[x, y - 1]);
                        }
                    }
                }
            }

        }

        private char GetRoom(char[,] map, int x, int y)
        {
            if (x >= 0 && x < map.GetLength(0) && y >= 0 && y < map.GetLength(1))
                return map[x, y];

            return 'y';
        }

        private void GenerateRoomsAround(ref char[,] map, int x, int y)
        {
            bool end = true;

            foreach (char c in map)
            {
                if (c == 'x')
                    end = false;
                    break;
            }

            if (end)
                return;


            List<Point> freePlaces = new List<Point>();

            if (GetRoom(map, x + 1, y) == 'x')
                freePlaces.Add(new Point(x + 1, y));
            if (GetRoom(map, x, y + 1) == 'x')
                freePlaces.Add(new Point(x, y + 1));
            if (GetRoom(map, x - 1, y) == 'x')
                freePlaces.Add(new Point(x - 1, y));
            if (GetRoom(map, x, y - 1) == 'x')
                freePlaces.Add(new Point(x, y - 1));

            foreach (Point point in freePlaces)
            {
                int create = this.random.Next(5);

                if (create > 1)
                {
                    int roomLayout = this.random.Next(ROOMS_LAYOUT);
                    map[point.X, point.Y] = roomLayout.ToString()[0];
                    GenerateRoomsAround(ref map, point.X, point.Y);
                }
                else
                {
                    map[point.X, point.Y] = 'y';
                }
            }
        }

        public void Transport(EntityDoor door, EntityPlayer player)
        {
            this.currentRoom.Remove(player);
            this.currentRoom = door.TargetRoom;

            int x = 0;
            int y = 0;

            switch(door.Direction)
            {
                case Direction.UP:
                    x = Room.ROOM_OFFSET_X + 88;
                    y = Settings.SCREEN_HEIGHT - 24 - EntityBlock.HEIGHT;
                    this.currentRoom.SpawnProjectile(player, x, y);
                    this.player.CurrentRoom = this.currentRoom;
                    break;
                case Direction.LEFT:
                    this.currentRoom.Spawn(player, 11, 3);
                    this.player.CurrentRoom = this.currentRoom;
                    break;
                case Direction.RIGHT:
                    this.currentRoom.Spawn(player, 0, 3);
                    this.player.CurrentRoom = this.currentRoom;
                    break;
                case Direction.DOWN:
                    x = Room.ROOM_OFFSET_X + 88;
                    y = Room.ROOM_OFFSET_Y - 10 + EntityBlock.HEIGHT;
                    this.currentRoom.SpawnProjectile(player, x, y);
                    this.player.CurrentRoom = this.currentRoom;
                    break;
            }
        }

        public void Update(GameTime gameTime, Input input)
        {
            this.currentRoom.Update(gameTime, input);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            this.currentRoom.Draw(spriteBatch);
        }
    }
}
