// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using Day9;

var inputLines = File.ReadLines("input.txt");
var sequences = inputLines.Select(item => new Sequence(item.Split(" ").Select(int.Parse).ToArray())).ToList();

var stopWatch = new Stopwatch();
stopWatch.Start();
var result = sequences.Sum(sequence => sequence.GetNextItem());

stopWatch.Stop();
Console.WriteLine($"first - {result} - {stopWatch.Elapsed.Seconds}:{stopWatch.Elapsed.Milliseconds}");

stopWatch.Reset();
stopWatch.Start();
result = sequences.Sum(sequence => sequence.GetPreviousItem());
Console.WriteLine($"second - {result} - {stopWatch.Elapsed.Seconds}:{stopWatch.Elapsed.Milliseconds}");
stopWatch.Stop();

