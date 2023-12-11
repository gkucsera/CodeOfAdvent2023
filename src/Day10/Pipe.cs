namespace Day10;

public class Pipe(PipeType type, Position position, int id)
{
    public Position Position => position;
    public PipeType Type => type;
    public int Id => id;

    public bool IsBorder { get; set; }
    public bool IsAvailable { get; set; } = true;

    public bool IsValidMove(MoveType moveType) => type switch
    {
        PipeType.Horizontal when moveType is MoveType.Left or MoveType.Right => true,
        PipeType.Vertical when moveType is MoveType.Up or MoveType.Down => true,
        PipeType.BottomLeft when moveType is MoveType.Up or MoveType.Right => true,
        PipeType.BottomRight when moveType is MoveType.Up or MoveType.Left => true,
        PipeType.TopLeft when moveType is MoveType.Right or MoveType.Down => true,
        PipeType.TopRight when moveType is MoveType.Left or MoveType.Down => true,
        PipeType.Start => true,
        _ => false
    };

    public MoveType GetFirstStep() => type switch
    {
        PipeType.Horizontal => MoveType.Right,
        PipeType.Vertical => MoveType.Down,
        PipeType.BottomLeft => MoveType.Left,
        PipeType.BottomRight => MoveType.Right,
        PipeType.TopLeft => MoveType.Left,
        PipeType.TopRight => MoveType.Right,
        _ => throw new Exception()
    };

    public MoveType GetNextStep(Position previousPosition) => type switch
    {
        PipeType.Horizontal when previousPosition.PosX < position.PosX => MoveType.Right,
        PipeType.Horizontal => MoveType.Left,
        PipeType.Vertical when previousPosition.PosY < position.PosY => MoveType.Down,
        PipeType.Vertical => MoveType.Up,
        PipeType.BottomLeft when previousPosition.PosX == position.PosX => MoveType.Left,
        PipeType.BottomLeft => MoveType.Down,
        PipeType.BottomRight when previousPosition.PosX == position.PosX => MoveType.Right,
        PipeType.BottomRight => MoveType.Down,
        PipeType.TopLeft when previousPosition.PosX == position.PosX => MoveType.Left,
        PipeType.TopLeft => MoveType.Up,
        PipeType.TopRight when previousPosition.PosX == position.PosX => MoveType.Right,
        PipeType.TopRight => MoveType.Up,
        _ => throw new Exception($"type: {type}")
    };

    public Position GetNewPosition(MoveType moveType) => moveType switch
    {
        MoveType.Up => new Position(position.PosX, position.PosY - 1),
        MoveType.Down => new Position(position.PosX, position.PosY + 1),
        MoveType.Left => new Position(position.PosX - 1, position.PosY),
        MoveType.Right => new Position(position.PosX + 1, position.PosY),
        _ => throw new Exception()
    };

    public override int GetHashCode() => id;
}