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

        public MarsRover(int x, int y, char direction, string[] obstacles)
        {
            _x = x;
            _y = y;
            _direction = direction;
            _obstacles = obstacles;
            _moveToSouth = new MoveToSouth(this);
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
                            MoveEast();
                            break;
                        case 'S':
                            MoveSouth();
                            break;
                        case 'W':
                            MoveWest();
                            break;
                        case 'N':
                            _moveToSouth.MoveNorth();
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

        private void MoveWest()
        {
            _obstacleFound = ((IList) _obstacles).Contains($"{_x - 1}:{_y}");
            // check if rover reached plateau limit or found an obstacle
            _x = _x > 0 && !_obstacleFound ? _x -= 1 : _x;
        }

        private void MoveSouth()
        {
            _obstacleFound = ((IList) _obstacles).Contains($"{_x}:{_y + 1}");
            // check if rover reached plateau limit or found an obstacle
            _y = _y < 9 && !_obstacleFound ? _y += 1 : _y;
        }

        private void MoveEast()
        {
            _obstacleFound = ((IList) _obstacles).Contains($"{_x + 1}:{_y}");
            // check if rover reached plateau limit or found an obstacle
            _x = _x < 9 && !_obstacleFound ? _x += 1 : _x;
        }
    }
}