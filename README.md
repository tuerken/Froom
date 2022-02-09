# Froom


Map a json with default mapper 

````c#

        var list = froom.Load<Results>().File(@"responses.json").Map().ToList();

        foreach (var result in list){
        
            Console.WriteLine(result.Id);
        
        }
        
````


Map a csv file using a custom mapper
````c#

        var list = froom.Load<Results>().File(@"responses.csv").Map((results, csv) =>
        {
            {
                var lines = csv.Split("\r\n");
                foreach (var line in lines)
                {
                    var data = line.Split(',');

                    //(0)id,(1)resultId,(2)status
                    if (!data.LineHasAllColumns(3)) continue;
                    results.Add(new Result
                    {
                        Id = data.Column(0),
                        ResultId = data.Column(1),
                        Status = data.Column(2)
                    });
                }
            }
        }).ToList();

        foreach (var result in list){
        
            Console.WriteLine(result.Id);
        
        }
        
````
