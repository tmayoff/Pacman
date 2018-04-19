using System;
using System.Collections.Generic;

namespace PacMan {
    public class Map {


        public Vector2 MapSize;

        public int Level;

        private readonly string[] _levels = new string[] {
            @"########################################################
##                        ####                        ##
##  ########  ##########  ####  ##########  ########  ##
##  ########  ##########  ####  ##########  ########  ##
##  ########  ##########  ####  ##########  ########  ##
##                                                    ##
##  ########  ####  ################  ####  ########  ##
##  ########  ####  ################  ####  ########  ##
##            ####        ####        ####            ##
############  ##########  ####  ##########  ############
############  ##########  ####  ##########  ############
############  ####                    ####  ############
############  ####  ######    ######  ####  ############
############  ####  ##            ##  ####  ############
T@                  ##    IBPC    ##          	       T
############  ####  ##            ##  ####  ############
############  ####  ################  ####  ############
############  ####                    ####  ############
############  ####  ################  ####  ############
############  ####  ################  ####  ############
##                        ####                        ##
##  ########  ##########  ####  ##########  ########  ##
##  ########  ##########  ####  ##########  ########  ##
##      ####                                ####      ##
######  ####  ####  ################  ####  ####  ######
######  ####  ####  ################  ####  ####  ######
##            ####        ####        ####            ##
##  ####################  ####  ####################  ##
##  ####################  ####  ####################  ##
##                                                    ##
########################################################"
        };

        List<Teleport> _teleports = new List<Teleport>();

        public Map() {
            Level = 0;

            GenerateMap();
            LinkTeleports();
        }

        private void LinkTeleports() {
            _teleports[0].TeleportTo = _teleports[1];
            _teleports[1].TeleportTo = _teleports[0];
        }

        private void GenerateMap() {
            string s = _levels[Level];
            string[] lines = s.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

            MapSize = new Vector2(lines[0].Length - 1, lines.Length + 1);

            for (int x = 0; x < lines[0].Length - 1; x++) {

                for (int y = 0; y < lines.Length; y++) {
                    //if (x < 46 || y < 10) continue;

                    switch (lines[y][x]) {
                        case ' ':
                            continue;

                        case 'I':

                            new Ghost(new Chixel('%', ConsoleColor.Cyan), new Vector2(x, y));
                            break;

                        case 'B':

                            new Ghost(new Chixel('%', ConsoleColor.Red), new Vector2(x, y));
                            break;

                        case 'P':
                            new Ghost(new Chixel('%', ConsoleColor.Magenta), new Vector2(x, y));
                            break;

                        case 'C':
                            new Ghost(new Chixel('%', ConsoleColor.Yellow), new Vector2(x, y));
                            break;

                        case '#': { //Wall
                                Chixel ch = new Chixel(lines[y][x], ConsoleColor.Blue, ConsoleColor.Blue);
                                Tile tile = new Tile(ch, new Vector2(x, y), TileType.Wall);
                                FrameBuffer.Instance.SetChixel(tile.Position, tile.Chixel, FrameBuffer.BufferLayers.Obstacles);
                                Game.Instance.Tiles.Add(tile);
                                break;
                            }

                        case 'T': { //Teleport
                                Chixel ch = new Chixel(lines[y][x], ConsoleColor.Green);
                                Teleport tile = new Teleport(ch, new Vector2(x, y), TileType.Teleport);
                                FrameBuffer.Instance.SetChixel(tile.Position, tile.Chixel, FrameBuffer.BufferLayers.Obstacles);
                                Game.Instance.Tiles.Add(tile);
                                _teleports.Add(tile);
                                break;
                            }

                        case '@': //Player
                            Game.Instance.Player = new Pacman(new Chixel('@', ConsoleColor.Yellow), new Vector2(x, y));
                            break;

                        default:
                            break;
                    }
                }
            }
        }
    }
}
