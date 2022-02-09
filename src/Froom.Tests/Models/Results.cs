using System.Collections.Generic;

namespace Froom.Tests.Models;

public class Result
{
    public string? Id { get; set; }
    public string? ResultId { get; set; }
    public string? Status { get; set; }
}

public class Results : List<Result>
{
}