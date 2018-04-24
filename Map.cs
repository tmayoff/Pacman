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
####### ##     B    ## #######
####### ## ###--### ## #######
####### ## #!!!!!!# ## #######
#T         #!IPC!!#         T#
####### ## #!!!!!!# ## #######
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
            GetNeighbors();
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
                    CreateTile(new Vector2(x, y), new Chixel(' '), TileType.Wall);
                }
            }

            for (int x = 0; x < MapSize.X; x++) {
                for (int y = 0; y < MapSize.Y; y++) {
                    char c = lines[y][x];
                    Tile currentTile = Tiles[x, y];
                    switch (c) {
                        case '@':
                            new Pacman(new Chixel(c, ConsoleColor.Yellow), new Vector2(x, y));
                            currentTile.Type = TileType.Space;
                            break;
                        case 'I':
                            new Inky(new Chixel('$', ConsoleColor.DarkYellow), new Vector2(x, y));
                            currentTile.Type = TileType.GhostHouse;
                            break;
                        case 'C':
                            new Clyde(new Chixel('$', ConsoleColor.Cyan), new Vector2(x, y));
                            currentTile.Type = TileType.Space;
                            break;
                        case 'P':
                            new Pinky(new Chixel('$', ConsoleColor.Magenta), new Vector2(x, y));
                            currentTile.Type = TileType.GhostHouse;
                            break;
                        case 'B':
                            new Blinky(new Chixel('$', ConsoleColor.Red), new Vector2(x, y));
                            currentTile.Type = TileType.Space;
                            break;

                        case '#':
                            currentTile.Chixel.BackgroundColor = ConsoleColor.Blue;
                            currentTile.Type = TileType.Wall;

                            break;
                        case '-':
                            currentTile.Chixel.Glyph = c;
                            currentTile.Type = TileType.Door;
                            break;

                        case '!':
                            currentTile.Type = TileType.GhostHouse;
                            currentTile.Chixel.Glyph = c;
                            break;

                        case 'T':
                            currentTile.Type = TileType.Teleport;
                            currentTile.Chixel.BackgroundColor = ConsoleColor.Green;
                            Teleport tel = new Teleport(currentTile);
                            Tiles[x, y] = tel;
                            _teleports.Add(tel);
                            break;
                        default:
                            currentTile.Type = TileType.Space;
                            break;
                    }
                }
            }
        }

        private void GetNeighbors() {
            for (int x = 0; x < Tiles.GetLength(0); x++) {
                for (int y = 0; y < Tiles.GetLength(1); y++) {
                    Tile tile = Tiles[x, y];
                    if (tile.Type == TileType.Wall) continue;

                    tile.Neighbors = new Tile[4];

                    int neighbors = 0;

                    Vector2 topNeighborPos = tile.Position + Vector2.Up;
                    Tile topNeighbor = GetTile(topNeighborPos);
                    if (topNeighbor != null && topNeighbor.Type == TileType.Space) {
                        tile.Neighbors[0] = topNeighbor;
                        neighbors++;
                    }

                    Vector2 rightNeighborPos = tile.Position + Vector2.Right;
                    Tile rightNeighbor = GetTile(rightNeighborPos);
                    if (rightNeighbor != null && rightNeighbor.Type == TileType.Space) {
                        tile.Neighbors[1] = rightNeighbor;
                        neighbors++;
                    }

                    Vector2 downNeighborPos = tile.Position + Vector2.Down;
                    Tile downNeighbor = GetTile(downNeighborPos);
                    if (downNeighbor != null && downNeighbor.Type == TileType.Space) {
                        tile.Neighbors[2] = downNeighbor;
                        neighbors++;
                    }

                    Vector2 leftNeighborPos = tile.Position + Vector2.Left;
                    Tile leftNeighbor = GetTile(leftNeighborPos);
                    if (leftNeighbor != null && leftNeighbor.Type == TileType.Space) {
                        tile.Neighbors[3] = leftNeighbor;
                        neighbors++;
                    }

                    bool vert = (tile.Neighbors[0] != null && tile.Neighbors[2] == null) || (tile.Neighbors[0] == null && tile.Neighbors[2] != null);
                    bool hori = (tile.Neighbors[1] != null && tile.Neighbors[3] == null) || (tile.Neighbors[1] == null && tile.Neighbors[3] != null);
                    tile.Intersection = neighbors >= 3;
                    tile.Corner = vert && hori;
                    //if (tile.Corner)
                    //    tile.Chixel.BackgroundColor = ConsoleColor.Cyan;
                    //if (tile.Intersection)
                    //    tile.Chixel.BackgroundColor = ConsoleColor.Green;
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
            try {
                return Tiles[pos.X, pos.Y];
            } catch {
                return null;
            }
        }
    }
}