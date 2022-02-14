using System;
using System.Collections;
using System.Collections.Generic;

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
        private Dictionary<char, IMove> moveDirection = new Dictionary<char, IMove>();

        public MarsRover(int x, int y, char direction, string[] obstacles)
        {
            _x = x;
            _y = y;
            _direction = direction;
            _obstacles = obstacles;
            moveDirection.Add('E', new MoveToEast(this));
            moveDirection.Add('N', new MoveToNorth(this));
            moveDirection.Add('S', new MoveToSouth(this));
            moveDirection.Add('W', new MoveToWest(this));
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
                    moveDirection[_direction].Move();
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