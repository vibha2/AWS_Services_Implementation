using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using HttpMultipartParser;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

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

            var bodyContent = request.IsBase64Encoded
                ? Convert.FromBase64String(request.Body)
                : Encoding.UTF8.GetBytes(request.Body);

            using var memStream = new MemoryStream(bodyContent);
            var formData = MultipartFormDataParser.Parse(memStream);

            //now fetch all the values from payload/frontend
            var hotelName = formData.GetParameterValue(name: "hotelName");
            var hotelRating = formData.GetParameterValue(name: "hotelRating");
            var hotelCity = formData.GetParameterValue(name: "hotelCity");
            var hotelPrice = formData.GetParameterValue(name: "hotelPrice");

            var file = formData.Files.FirstOrDefault();
            var fileName = file.FileName;
            //file.Data - use this when you want to store somewhere

            var userId = formData.GetParameterValue(name: "userId");
            var idToken = formData.GetParameterValue(name: "idToken");

            var token = new JwtSecurityToken(jwtEncodedString: idToken);
            var group = token.Claims.FirstOrDefault(x => x.Type == "cognito:groups");

            if(group == null || group.Value != "Admin")
            {
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
                response.Body = JsonSerializer.Serialize(new { Error = "Unauthorized. Must be a member of Admin group" });

            }

            //request.Headers["Authorization"]
            Console.WriteLine("OK=> ", response);
            return response;
        }
    }
}
