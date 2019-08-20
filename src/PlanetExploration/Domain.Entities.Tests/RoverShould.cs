﻿using Domain.Entities.Enums;
using Xunit;

namespace Domain.Entities.Tests
{
    public class RoverShould
    {
        [Fact]
        public void rover_advance_from_zero_zero_to_zero_five()
        {
            var rover = Rover.Create()
                .SetLimits(10, 10)
                .SetPosition(0, 0)
                .SetDirection(Direction.North)
                .SetEnergy(100)
                .Build();


            rover.ExecuteCommands("AAAAA");

            Assert.True(rover.GetPosition().GetY() == 5);
            Assert.True(rover.GetPosition().GetX() == 0);
        }

        [Fact]
        public void rover_back_from_zero_five_to_zero_zero()
        {
            var rover = Rover.Create()
                .SetLimits(10, 10)
                .SetPosition(0, 5)
                .SetDirection(Direction.North)
                .SetEnergy(100)
                .Build();

            rover.ExecuteCommands("BBBBB");

            Assert.True(rover.GetPosition().GetY() == 0);
            Assert.True(rover.GetPosition().GetX() == 0);
        }

        [Fact]
        public void rover_turn_left()
        {
            var rover = Rover.Create()
                .SetLimits(10, 10)
                .SetPosition(0, 0)
                .SetDirection(Direction.North)
                .SetEnergy(100)
                .Build();

            rover.ExecuteCommands("L");
            Assert.True(rover.GetDirection() == Direction.West);

            rover.ExecuteCommands("L");
            Assert.True(rover.GetDirection() == Direction.South);

            rover.ExecuteCommands("L");
            Assert.True(rover.GetDirection() == Direction.East);

            rover.ExecuteCommands("L");
            Assert.True(rover.GetDirection() == Direction.North);
        }

        [Fact]
        public void rover_turn_right()
        {
            var rover = Rover.Create()
                .SetLimits(10, 10)
                .SetPosition(0, 0)
                .SetDirection(Direction.North)
                .SetEnergy(100)
                .Build();

            rover.ExecuteCommands("R");
            Assert.True(rover.GetDirection() == Direction.East);

            rover.ExecuteCommands("R");
            Assert.True(rover.GetDirection() == Direction.South);

            rover.ExecuteCommands("R");
            Assert.True(rover.GetDirection() == Direction.West);

            rover.ExecuteCommands("R");
            Assert.True(rover.GetDirection() == Direction.North);
        }

        [Fact]
        public void rover_performs_a_loop()
        {
            var rover = Rover.Create()
                .SetLimits(10, 10)
                .SetPosition(0, 0)
                .SetDirection(Direction.North)
                .SetEnergy(100)
                .Build();

            rover.ExecuteCommands("ARAARALAA");
            Assert.True(rover.GetPosition().GetY() == 0);
            Assert.True(rover.GetPosition().GetX() == 4);
            Assert.True(rover.GetDirection() == Direction.East);

            rover.ExecuteCommands("LALAALARAA");
            Assert.True(rover.GetPosition().GetY() == 0);
            Assert.True(rover.GetPosition().GetX() == 0);
            Assert.True(rover.GetDirection() == Direction.West);
        }

        [Fact]
        public void rover_wants_go_out_on_x_axis_but_keep_in_limit()
        {
            var rover = Rover.Create()
                .SetLimits(10, 10)
                .SetPosition(0, 0)
                .SetDirection(Direction.West)
                .SetEnergy(100)
                .Build();

            rover.ExecuteCommands("AAA");
            Assert.True(rover.GetPosition().GetY() == 0);
            Assert.True(rover.GetPosition().GetX() == 0);
            Assert.True(rover.GetDirection() == Direction.West);
        }

        [Fact]
        public void rover_wants_go_out_on_y_axis_but_keep_in_limit()
        {
            var rover = Rover.Create()
                .SetLimits(10, 10)
                .SetPosition(0, 0)
                .SetDirection(Direction.North)
                .SetEnergy(100)
                .Build();

            rover.ExecuteCommands("AAAAAAAAAAAAAAA");
            Assert.True(rover.GetPosition().GetY() == 10);
            Assert.True(rover.GetPosition().GetX() == 0);
            Assert.True(rover.GetDirection() == Direction.North);
        }

        [Fact]
        public void rover_without_energy_doesnt_advance()
        {
            var rover = Rover.Create()
                .SetLimits(10, 10)
                .SetPosition(0, 0)
                .SetDirection(Direction.North)
                .SetEnergy(0)
                .Build();

            rover.ExecuteCommands("AAAAA");

            Assert.True(rover.GetPosition().GetY() == 0);
            Assert.True(rover.GetPosition().GetX() == 0);
        }

        [Fact]
        public void rover_waste_full_energy_when_execute_commands_and_stop()
        {
            var rover = Rover.Create()
                .SetLimits(10, 10)
                .SetPosition(0, 0)
                .SetDirection(Direction.North)
                .SetEnergy(5)
                .Build();

            var commands = "AAAAAAAAAA";

            rover.ExecuteCommands(commands);

            Assert.True(rover.GetPosition().GetY() < commands.Length);
            Assert.True(rover.GetPosition().GetX() == 0);
        }
    }
}