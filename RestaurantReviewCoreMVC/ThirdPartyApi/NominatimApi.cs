using RestaurantReviewCoreMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace RestaurantReviewCoreMVC.ThirdPartyApi
{
    public class NominatimApi
    {
        // reference for search: https://nominatim.org/release-docs/latest/api/Search/
        // reference for accesss/policy: https://operations.osmfoundation.org/policies/nominatim/

        public Coordinate GetCoordinate(string address)
        {
            string formatAddress = Uri.EscapeDataString(address);
            string url = "https://nominatim.openstreetmap.org/search?q=" + formatAddress + "&format=json";

            WebRequest request = WebRequest.Create(url);
            request.Method = "GET";
            request.Headers.Add("User-Agent", "RestaurantReview/1.0 (tui96569@temple.edu)"); // must give a "User-Agent" - program name with version and email ?
            
            WebResponse response = request.GetResponse();
            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);
            string data = reader.ReadToEnd();
            reader.Close();
            response.Close();

            List<Coordinate> coordinateList = JsonSerializer.Deserialize<List<Coordinate>>(data); // always returns a json list of coordinates
                                                                                                  // could query for more than one place
            Coordinate coordinate = new Coordinate();
            if (coordinateList != null && coordinateList.Count > 0) // ensure a coordinate is returned
            {
                coordinate.lat = coordinateList[0].lat;
                coordinate.lon = coordinateList[0].lon;
            }

            return coordinate;
        }
    }
}
