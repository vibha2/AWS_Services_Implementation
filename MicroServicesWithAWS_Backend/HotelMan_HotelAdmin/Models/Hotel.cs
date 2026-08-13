using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMan_HotelAdmin.Models
{
    [DynamoDBTable("Hotels")] //Table name- Hotels
    public class Hotel
    {
        //partition key
        [DynamoDBHashKey("userId")]
        public string UserId { get; set; }

        //sort key
        [DynamoDBRangeKey("Id")]
        public string Id { get; set; }

        public string Name {  get; set; }
        public string Price { get; set; }
        public string Rating { get; set; }
        public string CityName { get; set; }
        public string FileName { get; set; }

    }
}
