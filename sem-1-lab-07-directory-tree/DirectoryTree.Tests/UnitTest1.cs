using System;
using System.IO;
using NUnit.Framework;

public class TreeBuilderTests
{
    [Test]
    public void  Test1()
    {
        string[] lines =
        {
            "A",
            "BEGIN B",
            "BEGIN C",
            "END",
            "END"
        };

        var original = Console.Out;
        using var output = new StringWriter();

        try
        {
            Console.SetOut(output);

            Program.PrintFromLines(lines);

            string expected =
                "A" + Environment.NewLine +
                "└─B" + Environment.NewLine +
                "  └─C" + Environment.NewLine;

            Assert.That(output.ToString(), Is.EqualTo(expected));
        }
        finally
        {
            Console.SetOut(original);
        }
    }
    
    [Test]
    public void  Test2()
    {
        string[] lines =
        {
            "A",
            "BEGIN B",
            "END",
            "BEGIN C",
            "END",
            "BEGIN D",
            "END",
        };

        var original = Console.Out;
        using var output = new StringWriter();

        try
        {
            Console.SetOut(output);

            Program.PrintFromLines(lines);

            string expected =
                "A" + Environment.NewLine +
                "├─B" + Environment.NewLine +
                "├─C" + Environment.NewLine +
                "└─D" + Environment.NewLine;

            Assert.That(output.ToString(), Is.EqualTo(expected));
        }
        finally
        {
            Console.SetOut(original);
        }
    }
    
    [Test]
    public void  Test3()
    {
        string[] lines =
        {
            "A",
            "BEGIN B",
            "BEGIN C",
            "END",
            "END",
            "BEGIN D",
            "BEGIN E",
            "END",
            "END"
        };

        var original = Console.Out;
        using var output = new StringWriter();

        try
        {
            Console.SetOut(output);

            Program.PrintFromLines(lines);

            string expected =
                "A" + Environment.NewLine +
                "├─B" + Environment.NewLine +
                "│ └─C" + Environment.NewLine +
                "└─D" + Environment.NewLine +
                "  └─E" + Environment.NewLine;

            Assert.That(output.ToString(), Is.EqualTo(expected));
        }
        finally
        {
            Console.SetOut(original);
        }
    }
    
    [Test]
    public void  Test4()
    {
        string[] lines =
        {
            "A",
            "",
            "BEGIN B",
            "",
            "END",
            "",
            "BEGIN C",
            "END",
        };

        var original = Console.Out;
        using var output = new StringWriter();

        try
        {
            Console.SetOut(output);

            Program.PrintFromLines(lines);

            string expected =
                "A" + Environment.NewLine +
                "├─B" + Environment.NewLine +
                "└─C" + Environment.NewLine;

            Assert.That(output.ToString(), Is.EqualTo(expected));
        }
        finally
        {
            Console.SetOut(original);
        }
    }
}