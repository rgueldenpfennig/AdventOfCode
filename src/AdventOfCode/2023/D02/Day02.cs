namespace AdventOfCode.Y2023.D02;

internal class Day02 : Problem<int>
{
    internal record Game(int Id, IList<Draw> Draws);

    internal record Draw(int Blue = 0, int Green = 0, int Red = 0);

    public override async ValueTask<int> SolveAsync()
    {
        var games = await GetGamesFromInputAsync();
        return SumPowerOfMinimumCubes(games);
    }

    public async ValueTask<int> SolveFirstPartAsync()
    {
        var games = await GetGamesFromInputAsync();

        var red = 12;
        var green = 13;
        var blue = 14;

        return SumPossibleGames(games, red, green, blue);
    }

    private async ValueTask<List<Game>> GetGamesFromInputAsync()
    {
        var gameInputs = await File.ReadAllLinesAsync(Path.Combine(Environment.CurrentDirectory, "2023", "D02", "input.txt"));
        var games = new List<Game>(capacity: gameInputs.Length);

        foreach (var gameInput in gameInputs)
        {
            games.Add(ParseGame(gameInput));
        }

        return games;
    }

    internal Game ParseGame(string input)
    {
        // Game 1: 20 green, 3 red, 2 blue; 9 red, 16 blue, 18 green; 6 blue, 19 red, 10 green; 12 red, 19 green, 11 blue
        var span = input.AsSpan();
        var gameIndexLeft = input.IndexOf(' ') + 1;
        var gameIndexRight = input.IndexOf(':');

        var gameId = Convert.ToInt32(span[gameIndexLeft..gameIndexRight].ToString());
        gameIndexRight++;

        var draws = new List<Draw>();
        var drawsRaw = span[gameIndexRight..].ToString();
        foreach (var drawRaw in drawsRaw.Split(';'))
        {
            var cubes = drawRaw.Split(',', StringSplitOptions.TrimEntries);

            int blue = 0;
            int green = 0;
            int red = 0;

            foreach (var cube in cubes)
            {
                var cubeSpan = cube.AsSpan();
                var cubeIndex = cubeSpan.IndexOf(' ');
                var count = Convert.ToInt32(cubeSpan[..cubeIndex].ToString());

                cubeIndex++;
                var color = cubeSpan[cubeIndex..].ToString();

                switch (color)
                {
                    case "green":
                        green = count;
                        break;
                    case "blue":
                        blue = count;
                        break;
                    case "red":
                        red = count;
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            }

            draws.Add(new Draw(blue, green, red));
        }

        return new Game(gameId, draws);
    }

    internal int SolveSample()
    {
        // 3 blue, 4 red;
        // 1 red, 2 green, 6 blue;
        // 2 green
        var game1 = new Game(1,
            [
                new Draw { Blue = 3, Red = 4 },
                new Draw { Blue = 6, Green = 2, Red = 1 },
                new Draw { Green = 2 }
            ]);

        // 1 blue, 2 green;
        // 3 green, 4 blue, 1 red;
        // 1 green, 1 blue
        var game2 = new Game(2,
            [
                new Draw { Blue = 1, Green = 2, Red = 0 },
                new Draw { Blue = 4, Green = 3, Red = 1 },
                new Draw { Blue = 1, Green = 1, Red = 0 }
            ]);

        // 8 green, 6 blue, 20 red;
        // 5 blue, 4 red, 13 green;
        // 5 green, 1 red
        var game3 = new Game(3,
            [
                new Draw { Blue = 6, Green = 8, Red = 20 },
                new Draw { Blue = 5, Green = 13, Red = 4 },
                new Draw { Blue = 0, Green = 5, Red = 1 }
            ]);

        // 1 green, 3 red, 6 blue;
        // 3 green, 6 red;
        // 3 green, 15 blue, 14 red
        var game4 = new Game(4,
            [
                new Draw { Blue = 6, Green = 1, Red = 3 },
                new Draw { Blue = 0, Green = 3, Red = 6 },
                new Draw { Blue = 15, Green = 3, Red = 14 }
            ]);

        // 6 red, 1 blue, 3 green;
        // 2 blue, 1 red, 2 green
        var game5 = new Game(5,
            [
                new Draw { Blue = 1, Green = 3, Red = 6 },
                new Draw { Blue = 2, Green = 2, Red = 1 }
            ]);

        var games = new[] { game1, game2, game3, game4, game5 };

        // the Elf has 12 reds, 13 greens and 14 blues
        var red = 12;
        var green = 13;
        var blue = 14;

        return SumPossibleGames(games, red, green, blue);
    }

    internal static int SumPossibleGames(IList<Game> games, int reds, int greens, int blues)
    {
        var sumIds = 0;
        foreach (var game in games)
        {
            var gamePossible = true;
            foreach (var draw in game.Draws)
            {
                if (draw.Red > reds || draw.Green > greens || draw.Blue > blues)
                {
                    gamePossible = false;
                    break;
                }
            }

            if (gamePossible) sumIds += game.Id;
        }

        return sumIds;
    }

    internal static int SumPowerOfMinimumCubes(IList<Game> games)
    {
        var powerResults = new List<int>(capacity: games.Count);
        foreach (var game in games)
        {
            var minRed = 0;
            var minGreen = 0;
            var minBlue = 0;

            foreach (var draw in game.Draws)
            {
                minRed = Math.Max(minRed, draw.Red);
                minGreen = Math.Max(minGreen, draw.Green);
                minBlue = Math.Max(minBlue, draw.Blue);
            }

            powerResults.Add(minRed * minGreen * minBlue);
        }

        return powerResults.Sum();
    }
}
