using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Syntax
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var result = "{\"张三\":[1, 2]}";
            Dictionary<string, IList<int>> dic2 = JsonConvert.DeserializeObject<Dictionary<string, IList<int>>>(result);


            var result2 = "{ \"zack\": 1 ,\"张三\":[1, 2]}";
            Dictionary<Dictionary<string, int>, Dictionary<string, IList<int>>> dic2 = JsonConvert.DeserializeObject<Dictionary<string, int>, Dictionary<string, IList<int>>>(result2);
            Console.WriteLine(result);

            string s = "[{\"ConfiguredChipSets\": [{\"Key\": 1, \"Value\": []},{ \"Key\": 2,\"Value\": [\"HKD-NN-5\"]}]}]";
            string s2 = "{\"ConfiguredChipSets\": [{1,  []},{  2, [\"HKD-NN-5\"]}]}";

            IDictionary<string, IList<string>> ConfiguredChipSets;

            ConfiguredChipSets = JsonConvert.DeserializeObject<IDictionary<string, IList<string>>>(s);

            Console.WriteLine(ConfiguredChipSets);



        }

            
    }
}
