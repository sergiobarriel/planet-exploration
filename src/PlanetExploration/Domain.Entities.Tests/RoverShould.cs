using Domain.Entities.Enums;
using Xunit;

namespace Domain.Entities.Tests
{
    public class RoverShould
    {
        [Fact]
        public void rover_advance()
        {
            var rover = Rover.Create()
                .SetDirection(Direction.North)
                .SetEnergy(100)
                .Build();

            var surface = Surface.Create()
                .WithoutObstacles()
                .SetDimension(10, 10)
                .SetRover(rover)
                .Build();

            var x = rover.GetPosition().GetX();
            var y = rover.GetPosition().GetY();

            rover.ExecuteCommands("A");

            Assert.True(rover.GetPosition().GetY() == y + 1);
            Assert.True(rover.GetPosition().GetX() == x);
        }

        [Fact]
        public void rover_back()
        {
            var rover = Rover.Create()
                .SetDirection(Direction.North)
                .SetEnergy(100)
                .Build();

            var surface = Surface.Create()
                .WithoutObstacles()
                .SetDimension(10, 10)
                .SetRover(rover)
                .Build();

            var x = rover.GetPosition().GetX();
            var y = rover.GetPosition().GetY();

            rover.ExecuteCommands("B");

            Assert.True(rover.GetPosition().GetY() == y - 1);
            Assert.True(rover.GetPosition().GetX() == x);
        }

        [Fact]
        public void rover_turn_left()
        {
            var rover = Rover.Create()
                .SetDirection(Direction.North)
                .SetEnergy(100)
                .Build();

            var surface = Surface.Create()
                .WithoutObstacles()
                .SetDimension(10, 10)
                .SetRover(rover)
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
                .SetDirection(Direction.North)
                .SetEnergy(100)
                .Build();

            var surface = Surface.Create()
                .WithoutObstacles()
                .SetDimension(10, 10)
                .SetRover(rover)
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
                .SetDirection(Direction.North)
                .SetEnergy(100)
                .Build();

            var surface = Surface.Create()
                .WithoutObstacles()
                .SetDimension(10, 10)
                .SetRover(rover)
                .Build();

            var x = rover.GetPosition().GetX();
            var y = rover.GetPosition().GetY();

            rover.ExecuteCommands("ARARARA");
            Assert.True(rover.GetPosition().GetY() == y);
            Assert.True(rover.GetPosition().GetX() == x);
            Assert.True(rover.GetDirection() == Direction.West);
        }

        [Fact]
        public void rover_wants_go_out_on_x_axis_but_keeps_in_limit()
        {
            var rover = Rover.Create()
                .SetDirection(Direction.East)
                .SetEnergy(100)
                .Build();

            var surface = Surface.Create()
                .WithoutObstacles()
                .SetDimension(10, 10)
                .SetRover(rover)
                .Build();

            rover.ExecuteCommands("AAAAAAAAAA");
            Assert.True(rover.GetPosition().GetX() == surface.GetWidth());
            Assert.True(rover.GetDirection() == Direction.East);

            rover.ExecuteCommands("LLAAAAAAAAAA");
            Assert.True(rover.GetPosition().GetX() == 1);
            Assert.True(rover.GetDirection() == Direction.West);
        }

        [Fact]
        public void rover_wants_go_out_on_y_axis_but_keeps_in_limit()
        {
            var rover = Rover.Create()
                .SetDirection(Direction.North)
                .SetEnergy(100)
                .Build();

            var surface = Surface.Create()
                .WithoutObstacles()
                .SetDimension(10, 10)
                .SetRover(rover)
                .Build();

            rover.ExecuteCommands("AAAAAAAAAA");
            Assert.True(rover.GetPosition().GetY() == surface.GetHeight());
            Assert.True(rover.GetDirection() == Direction.North);

            rover.ExecuteCommands("LLAAAAAAAAAA");
            Assert.True(rover.GetPosition().GetY() == 1);
            Assert.True(rover.GetDirection() == Direction.South);
        }

        [Fact]
        public void rover_without_energy_doesnt_advance()
        {
            var rover = Rover.Create()
                .SetDirection(Direction.North)
                .SetEnergy(0)
                .Build();

            var surface = Surface.Create()
                .WithoutObstacles()
                .SetDimension(10, 10)
                .SetRover(rover)
                .Build();

            var x = rover.GetPosition().GetX();
            var y = rover.GetPosition().GetY();

            rover.ExecuteCommands("AAAAA");

            Assert.True(rover.GetPosition().GetY() == y);
            Assert.True(rover.GetPosition().GetX() == x);
        }

        [Fact]
        public void rover_waste_full_energy_when_execute_commands_and_stop()
        {
            var rover = Rover.Create()
                .SetDirection(Direction.North)
                .SetEnergy(5)
                .Build();

            var surface = Surface.Create()
                .WithoutObstacles()
                .SetDimension(10, 10)
                .SetRover(rover)
                .Build();

            var x = rover.GetPosition().GetX();
            var y = rover.GetPosition().GetY();

            var commands = "RLRLRLRLRLRLRLRLRLA";

            rover.ExecuteCommands(commands);

            Assert.True(rover.GetPosition().GetY() == y);
            Assert.True(rover.GetPosition().GetX() == x);
        }


        [Fact]
        public void rover_cannot_advance_over_a_rock()
        {
            var rover = Rover.Create()
                .SetDirection(Direction.North)
                .SetEnergy(5)
                .Build();

            var surface = Surface.Create()
                .WithObstacles(weight: 100)
                .SetDimension(10, 10)
                .SetRover(rover)
                .Build();

            var x = rover.GetPosition().GetX();
            var y = rover.GetPosition().GetY();

            var commands = "ALALALA";

            rover.ExecuteCommands(commands);

            Assert.True(rover.GetPosition().GetY() == y);
            Assert.True(rover.GetPosition().GetX() == x);
        }

        [Fact]
        public void rover_cannot_back_over_a_rock()
        {
            var rover = Rover.Create()
                .SetDirection(Direction.North)
                .SetEnergy(5)
                .Build();

            var surface = Surface.Create()
                .WithObstacles(weight: 100)
                .SetDimension(10, 10)
                .SetRover(rover)
                .Build();

            var x = rover.GetPosition().GetX();
            var y = rover.GetPosition().GetY();

            var commands = "BLBLBLB";

            rover.ExecuteCommands(commands);

            Assert.True(rover.GetPosition().GetY() == y);
            Assert.True(rover.GetPosition().GetX() == x);
        }
    }
}
