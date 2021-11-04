//using Microsoft.AspNetCore.Http;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using Ocelot.Configuration;
//using Ocelot.Middleware;
//using Ocelot.Multiplexer;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.IO.Compression;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Text;
//using System.Threading.Tasks;

//namespace OcelotGateway
//{
//    internal class FakeDefinedAggregator : IDefinedAggregator
//    {
//        //public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
//        //{
//        //    var contentBuilder = new StringBuilder();
//        //    contentBuilder.Append(responses);

//        //    var stringContent = new StringContent(contentBuilder.ToString())
//        //    {
//        //        Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
//        //    };

//        //    return new DownstreamResponse(stringContent, HttpStatusCode.OK, new List<KeyValuePair<string, IEnumerable<string>>>(), "OK");

//        //}
//        //public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
//        //{
//        //    JObject postsByUserName = new JObject();

//        //    var xResponseContent = await responses.FirstOrDefault(r => r.DownstreamReRoute.Key.Equals("users")).DownstreamResponse.Content.ReadAsByteArrayAsync();
//        //    var yResponseContent = await responses.FirstOrDefault(r => r.DownstreamReRoute.Key.Equals("posts")).DownstreamResponse.Content.ReadAsByteArrayAsync();

//        //    List<User> users = JsonConvert.DeserializeObject<List<User>>(DeCompressBrotli(xResponseContent));
//        //    List<Post> posts = JsonConvert.DeserializeObject<List<Post>>(DeCompressBrotli(yResponseContent));

//        //    var postsByUserID = posts.GroupBy(n => n.UserID);

//        //    foreach (var post in postsByUserID)
//        //    {
//        //        string userName = users.Find(n => n.ID == post.Key).Name;
//        //        var selectPost = JsonConvert.SerializeObject(post.Select(n => new { n.ID, n.Title, n.Body }));
//        //        var selectPostString = JsonConvert.DeserializeObject<JArray>(selectPost);
//        //        postsByUserName.Add(new JProperty(userName, selectPostString));
//        //    }

//        //    var postsByUsernameString = JsonConvert.SerializeObject(postsByUserName);

//        //    var stringContent = new StringContent(postsByUsernameString)
//        //    {
//        //        Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
//        //    };
//        //    return new DownstreamResponse(stringContent, HttpStatusCode.OK, new List<KeyValuePair<string, IEnumerable<string>>>(), "OK");
//        //}

//        /**************************************************/
//        public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
//        {
//            var one = await responses.FirstOrDefault(r => r.DownstreamRoute.Key.Equals("Departement")).DownstreamResponse().Content.ReadAsStringAsync();
//            var two = await responses.FirstOrDefault(r => r.DownstreamReRoute.Key.Equals("Company")).DownstreamResponse().Content.ReadAsStringAsync();

//            var merge = $"{one}, {two}";
//            merge = merge.Replace("Hello", "Bye").Replace("{", "").Replace("}", "");
//            var headers = responses.SelectMany(x => x.Items.DownstreamResponse().Headers).ToList();
//            return new DownstreamResponse(new StringContent(merge), HttpStatusCode.OK, headers, "some reason");
//        }
//    }
//}