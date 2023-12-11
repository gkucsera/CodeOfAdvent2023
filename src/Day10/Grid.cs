namespace Day10;

public class Grid(Pipe[,] tiles, Pipe start)
{
    private PipeType _startType = PipeType.None;

    public int GetFarthestPosition()
    {
        var length = -1;
        foreach (var possibleStart in PossibleStarts)
        {
            var currentLength = GetLoopLength(possibleStart);
            if (currentLength > length)
            {
                length = currentLength;
                _startType = possibleStart;
            }
        }

        return length / 2;
    }

    private int GetLoopLength(PipeType startType, bool markTile = false)
    {
        var newStart = new Pipe(startType, start.Position, 0);


        var currentPipe = newStart;
        var firstMove = newStart.GetFirstStep();
        var nextPosition = newStart.GetNewPosition(firstMove);
        if (IsOutOfGrid(nextPosition))
        {
            return -1;
        }

        var nextPipe = tiles[nextPosition.PosY, nextPosition.PosX];

        if (!nextPipe.IsValidMove(firstMove))
        {
            return -1;
        }

        var length = 1;
        if (markTile)
        {
            start.IsBorder = true;
            nextPipe.IsBorder = true;
        }

        while (nextPipe.Type != PipeType.Start)
        {
            var nextMove = nextPipe.GetNextStep(currentPipe.Position);
            var next = nextPipe.GetNewPosition(nextMove);

            if (IsOutOfGrid(next))
            {
                return -1;
            }

            currentPipe = nextPipe;
            nextPipe = tiles[next.PosY, next.PosX];
            if (!nextPipe.IsAvailable || !nextPipe.IsValidMove(nextMove))
            {
                return -1;
            }

            nextPipe.IsAvailable = false;
            length++;
            if (markTile)
            {
                start.IsBorder = true;
                nextPipe.IsBorder = true;
            }
        }

        return length;
    }

    public int GetEnclosedCount()
    {
        GetLoopLength(_startType, true);
        tiles[start.Position.PosY, start.Position.PosX] = new Pipe(_startType, start.Position, start.Id);
        var emptyTiles = new List<Pipe>();
        for (var i = 0; i < tiles.GetLength(0); i++)
        {
            for (var j = 0; j < tiles.GetLength(1); j++)
            {
                if (tiles[i, j].Type == PipeType.None)
                {
                    emptyTiles.Add(tiles[i, j]);
                }
            }
        }

        var result = 0;
        foreach (var emptyTile in emptyTiles)
        {
            if (
                IsOutsideOfLoop(emptyTile.Position, MoveType.Down, new HashSet<Position>()) ||
                IsOutsideOfLoop(emptyTile.Position, MoveType.Up, new HashSet<Position>()) ||
                IsOutsideOfLoop(emptyTile.Position, MoveType.Right, new HashSet<Position>()) ||
                IsOutsideOfLoop(emptyTile.Position, MoveType.Left, new HashSet<Position>())
            )
            {
                result++;
            }
        }

        return result;
    }

    private bool IsOutsideOfLoop(Position position, MoveType moveType, HashSet<Position> checkedPosition)
    {
        if (IsOutOfGrid(position))
        {
            return true;
        }

        if (checkedPosition.Contains(position))
        {
            return false;
        }

        var currentTile = tiles[position.PosY, position.PosX];
        if (currentTile.Type == PipeType.None)
        {
            checkedPosition.Add(position);
            if (IsOutsideOfLoop(currentTile.GetNewPosition(MoveType.Up), MoveType.Up, checkedPosition))
            {
                return true;
            }

            if (IsOutsideOfLoop(currentTile.GetNewPosition(MoveType.Left), MoveType.Left, checkedPosition))
            {
                return true;
            }

            if (IsOutsideOfLoop(currentTile.GetNewPosition(MoveType.Down), MoveType.Down, checkedPosition))
            {
                return true;
            }

            if (IsOutsideOfLoop(currentTile.GetNewPosition(MoveType.Right), MoveType.Right, checkedPosition))
            {
                return true;
            }

            return false;
        }

        var moveNext = MoveBetweenPipes(position, moveType);
        if (moveNext.HasValue)
        {
            return IsOutsideOfLoop(currentTile.GetNewPosition(moveNext.Value), moveNext.Value, checkedPosition);
        }

        return false;
    }

    private MoveType? MoveBetweenPipes(Position position, MoveType from)
    {
        if (position.PosX == 1 || position.PosX == tiles.GetLength(0) - 1 || position.PosY == 0 || position.PosY == tiles.GetLength(1) - 1)
        {
            return from;
        }

        if (from is MoveType.Down or MoveType.Up)
        {
            if (
                (tiles[position.PosY, position.PosX].Type is PipeType.Vertical or PipeType.BottomLeft or PipeType.TopLeft
                 &&
                 tiles[position.PosY, position.PosX + 1].Type is PipeType.Vertical or PipeType.BottomRight or PipeType.TopRight)
                ||
                (tiles[position.PosY, position.PosX].Type is PipeType.Vertical or PipeType.BottomRight or PipeType.TopRight
                 &&
                 tiles[position.PosY, position.PosX - 1].Type is PipeType.Vertical or PipeType.BottomLeft or PipeType.TopLeft)
            )
            {
                return from;
            }
        }

        if (
            (tiles[position.PosY, position.PosX].Type is PipeType.Horizontal or PipeType.TopLeft or PipeType.TopRight
             &&
             tiles[position.PosY + 1, position.PosX].Type is PipeType.Horizontal or PipeType.BottomLeft or PipeType.BottomRight)
            ||
            (tiles[position.PosY, position.PosX].Type is PipeType.Horizontal or PipeType.BottomLeft or PipeType.BottomRight
             &&
             tiles[position.PosY - 1, position.PosX].Type is PipeType.Horizontal or PipeType.TopRight or PipeType.TopLeft))
        {
            return from;
        }

        return null;
    }

    private bool IsOutOfGrid(Position position)
    {
        return position.PosX < 0 || position.PosX >= tiles.GetLength(0) || position.PosY < 0 || position.PosY > tiles.GetLength(1);
    }

    private static readonly IEnumerable<PipeType> PossibleStarts = new[]
    {
        PipeType.TopRight,
        PipeType.TopLeft,
        PipeType.BottomRight,
        PipeType.BottomLeft,
        PipeType.Vertical,
        PipeType.Horizontal,
    };
}