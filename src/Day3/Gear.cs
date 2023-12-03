namespace Day3
{
    public record Gear(PartNumber PartNumberA, PartNumber PartNumberB)
    {
        public const char GearSymbol = '*';

        public int GetGearValue => PartNumberA.Value * PartNumberB.Value;
    }
}