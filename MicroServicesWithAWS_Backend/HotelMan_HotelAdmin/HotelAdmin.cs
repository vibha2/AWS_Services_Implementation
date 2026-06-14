using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;

namespace HotelMan_HotelAdmin
{
    public class HotelAdmin
    {
        public APIGatewayProxyResponse AddHotel(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var response = new APIGatewayProxyResponse()
            {
                Headers = new Dictionary<string, string>(),
                StatusCode = 200
            };

            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.Headers.Add("Access-Control-Allow-Headers", "*");
            response.Headers.Add("Access-Control-Allow-Methods", "OPTIONS, POST");

            Console.WriteLine("OK=> ", response);
            return response;
        }
    }
}
