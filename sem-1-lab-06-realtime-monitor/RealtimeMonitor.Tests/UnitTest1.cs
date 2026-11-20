using System;
using System.IO;
using NUnit.Framework;

public class MonitorTests
{
    [Test]
    public void DrawGraph_ShowsTitleAndCurrentValue()
    {
        var originalOutput = Console.Out;
        using var output = new StringWriter();

        try
        {
            Console.SetOut(output);

            RealtimeMonitor.Program.DrawGraph(
                new double[] { 0, 50, 100 }, "CPU");

            string result = output.ToString();

            Assert.That(result, Does.Contain("CPU: 100"));
        }
        finally
        {
            Console.SetOut(originalOutput);
        }
    }
}