using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Froom.Package.Helpers;
using Froom.Tests.Models;
using Xunit;
using Xunit.Abstractions;
using froom = Froom.Package.Froom;

#pragma warning disable CS8604
#pragma warning disable CS8602
namespace Froom.Tests;

public class FroomIs
{
    private readonly ITestOutputHelper _testOutputHelper;

    public FroomIs(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Theory]
    [InlineData("[{\"id\":1,\"name\":\"default name\"}]")]
    public void MappingFromInlineJsonDefaultMapper(string data)
    {
        var list = froom.Load<List<Example>>().Text(data).Map().ToList();
        foreach (var example in list) _testOutputHelper.WriteLine(example.Name);

        Assert.NotEmpty(list);
    }

    [Theory]
    [InlineData("[{\"id\":1,\"name\":\"adnan\"}]")]
    public void MappingFromInlineJson(string data)
    {


        var list = froom.Load<ExampleList>().Text(data).Map(JsonExampleMapper).ToList();

        foreach (var example in list) _testOutputHelper.WriteLine(example.Name);

        Assert.NotEmpty(list);
    }
    

    [Theory]
    [InlineData("1,Adnan\r\n2,Manuel")]
    [InlineData("1,Adnan\r\n2,Manuel\r\n3,Turken")]
    public void MappingFromInlineCsv(string data)
    {
        var list = froom.Load<ExampleList>().Text(data).Map(CsvExampleMapper).ToList();

        foreach (var example in list) _testOutputHelper.WriteLine(example.Name);

        Assert.NotEmpty(list);
    }

    [Fact]
    public void MappingFromFile()
    {
        var list = froom.Load<Results>().File(@"c:\zepto\files\responses.csv").Map(ResultParser).ToList();

        foreach (var result in list) _testOutputHelper.WriteLine(result.ResultId);

        Assert.NotEmpty(list);
    }


    private static void ResultParser(Results? l, string? s)
    {
        var lines = s.Split("\r\n");
        foreach (var line in lines)
        {
            var data = line.Split(',');

            //(0)id,(1)resultId,(2)status
            if (!data.LineHasAllColumns(3)) continue;
            l.Add(new Result
            {
                Id = data.Column(0),
                ResultId = data.Column(1),
                Status = data.Column(2)
            });
        }
    }

    private static void CsvExampleMapper(ExampleList? m, string? s)
    {
        var lines = s.Split("\r\n");
        foreach (var line in lines) m.Add(new Example(line.Split(',')[1]));
    }

    private static void JsonExampleMapper(ExampleList? m, string? s)
    {
        var j = JsonSerializer.Deserialize<List<ResponseDto>>(s);

        m.AddRange(j.Select(n => new Example(n.name)));
    }
}
#pragma warning restore CS8604
#pragma warning restore CS8602