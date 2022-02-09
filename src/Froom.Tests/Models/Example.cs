using System.Collections.Generic;

namespace Froom.Tests.Models;

public class Example
{
    public Example(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
}

public class ExampleList : List<Example>
{
}