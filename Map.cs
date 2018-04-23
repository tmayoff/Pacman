using System;
using System.Collections.Generic;
using PacMan.Ghosts;

namespace PacMan {
    public class Map {
        public static Map Instance;

        public Tile[,] Tiles;

        public Vector2 MapSize;

        public int Level;

        private readonly string[] _levels = new string[] {
            @"##############################
##############################
##            ##            ##
## #### ##### ## ##### #### ##
## #### ##### ## ##### #### ##
## #### ##### ## ##### #### ##
##                          ##
## #### ## ######## ## #### ##
## #### ## ######## ## #### ##
##      ##    ##    ##      ##
####### ##### ## ##### #######
####### ##### ## ##### #######
####### ##    B     ## #######
####### ## ###--### ## #######
####### ## #      # ## #######
#T  @      # IP C #         T#
####### ## #      # ## #######
####### ## ######## ## #######
####### ##          ## #######
####### ## ######## ## #######
####### ## ######## ## #######
##            ##            ##
## #### ##### ## ##### #### ##
## #### ##### ## ##### #### ##
##   ##                ##   ##
#### ## ## ######## ## ## ####
#### ## ## ######## ## ## ####
##      ##    ##    ##      ##
## ########## ## ########## ##
##                          ##
##############################
##############################"};

        private readonly List<Teleport> _teleports = new List<Teleport>();

        public Map() {
            Instance = this;

            Level = 0;

            GenerateMap();
            LinkTeleports();
        }

        private void LinkTeleports() {
            if (_teleports.Count < 2) return;
            _teleports[0].TeleportTo = _teleports[1];
            _teleports[1].TeleportTo = _teleports[0];
        }

        private void GenerateMap() {
            string s = _levels[Level];
            string[] lines = s.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

            MapSize = new Vector2(lines[0].Length, lines.Length);
            Tiles = new Tile[MapSize.X, MapSize.Y];

            for (int x = 0; x < MapSize.X; x++) {

                for (int y = 0; y < MapSize.Y; y++) {

                    switch (lines[y][x]) {
                        case 'I':

                            new Inky(new Chixel('%', ConsoleColor.Cyan), new Vector2(x, y));
                            CreateTile(new Vector2(x, y), new Chixel(' '), TileType.Space);
                            break;

                        case 'B':

                            new Blinky(new Chixel('%', ConsoleColor.Red), new Vector2(x, y));
                            CreateTile(new Vector2(x, y), new Chixel(' '), TileType.Space);
                            break;

                        case 'P':
                            new Pinky(new Chixel('%', ConsoleColor.Magenta), new Vector2(x, y));
                            CreateTile(new Vector2(x, y), new Chixel(' '), TileType.Space);
                            break;

                        case 'C':
                            new Clyde(new Chixel('%', ConsoleColor.Yellow), new Vector2(x, y));
                            CreateTile(new Vector2(x, y), new Chixel(' '), TileType.Space);
                            break;

                        case ' ': {
                                CreateTile(new Vector2(x, y), new Chixel(' '), TileType.Space);
                                break;
                            }

                        case '#': { //Wall
                                Chixel ch = new Chixel(lines[y][x], ConsoleColor.Blue, ConsoleColor.Blue);
                                CreateTile(new Vector2(x, y), ch, TileType.Wall);
                                break;
                            }

                        case '-': {
                                CreateTile(new Vector2(x, y), new Chixel(' '), TileType.Door);
                                break;
                            }

                        case 'T': { //Teleport
                                Chixel ch = new Chixel(' ', ConsoleColor.Green);
                                Teleport tile = new Teleport(ch, new Vector2(x, y), TileType.Teleport);
                                FrameBuffer.Instance.SetChixel(tile.Position, tile.Chixel, FrameBuffer.BufferLayers.Obstacles);
                                Game.Instance.Tiles.Add(tile);
                                _teleports.Add(tile);
                                Tiles[x, y] = tile;
                                break;
                            }

                        case '@': //Player
                            Game.Instance.Player = new Pacman(new Chixel('@', ConsoleColor.Yellow), new Vector2(x, y));
                            CreateTile(new Vector2(x, y), new Chixel(' '), TileType.Door);
                            break;
                    }
                }
            }
        }

        private void CreateTile(Vector2 pos, Chixel ch, TileType type) {
            Tile tile = new Tile(ch, new Vector2(pos.X, pos.Y), type);
            FrameBuffer.Instance.SetChixel(tile.Position, tile.Chixel, FrameBuffer.BufferLayers.Obstacles);
            Game.Instance.Tiles.Add(tile);
            Tiles[pos.X, pos.Y] = tile;
        }

        public Tile GetTile(Vector2 pos) {
            return Tiles[pos.X, pos.Y];
        }
    }
}
