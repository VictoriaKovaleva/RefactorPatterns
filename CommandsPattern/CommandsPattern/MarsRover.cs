using System;
using System.Collections;

namespace CommandsPattern
{
    public class MarsRover
    {
        public int _x;
        public int _y;
        private char _direction;
        private readonly string _availableDirections = "NESW";
        public readonly string[] _obstacles;
        public bool _obstacleFound;
        private readonly MoveToSouth _moveToSouth;
        private readonly MoveToWest _moveToWest;
        private readonly MoveToNorth _moveToNorth;
        private readonly MoveToEast _moveToEast;

        public MarsRover(int x, int y, char direction, string[] obstacles)
        {
            _x = x;
            _y = y;
            _direction = direction;
            _obstacles = obstacles;
            _moveToSouth = new MoveToSouth(this);
            _moveToWest = new MoveToWest(this);
            _moveToNorth = new MoveToNorth(this);
            _moveToEast = new MoveToEast(this);
        }
        
        public string GetState()
        {
            return !_obstacleFound ? $"{_x}:{_y}:{_direction}" : $"O:{_x}:{_y}:{_direction}";
        }

        public void Execute(string commands)
        {
            foreach(char command in commands)
            {
                if (command == 'M')
                {
                    switch (_direction)
                    {
                        case 'E':
                            _moveToEast.MoveEast();
                            break;
                        case 'S':
                            _moveToSouth.MoveSouth();
                            break;
                        case 'W':
                            _moveToWest.MoveWest();
                            break;
                        case 'N':
                            _moveToNorth.MoveNorth();
                            break;
                    }
                }
                else if(command == 'L')
                {
                    // get new direction
                    var currentDirectionPosition = _availableDirections.IndexOf(_direction);
                    if (currentDirectionPosition != 0)
                    {
                        _direction = _availableDirections[currentDirectionPosition-1];
                    }
                    else
                    {
                        _direction = _availableDirections[3];
                    }
                } else if (command == 'R')
                {
                    // get new direction
                    var currentDirectionPosition = _availableDirections.IndexOf(_direction);
                    if (currentDirectionPosition != 3)
                    {
                        _direction = _availableDirections[currentDirectionPosition+1];
                    }
                    else
                    {
                        _direction = _availableDirections[0];
                    }
                }
            }
        }
    }
}