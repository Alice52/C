using System;

namespace Golden
{
    public class Things
    {
        public int Get(int number) => Newtonsoft.Json.JsonConvert.DeserializeObject<int>($"{number}");
    }

}
