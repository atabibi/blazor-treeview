using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace T00.Shared.ErthTreeCodes
{
    public class SaveLoadJsonFile
    {
        public static string Serialize(Person movareth)
        {
            return JsonSerializer.Serialize<Person>(movareth, new JsonSerializerOptions
                                                        {
                                                            WriteIndented = true
                                                        }
                                                    );
        }

        public static async Task Download(IJSRuntime jsRuntime ,Person movareth)
        {            
            var jsonStr = Serialize(movareth);
            

            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(jsonStr);
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                await stream.WriteAsync(buffer, 0, buffer.Length);
                await jsRuntime.InvokeAsync<object>("saveAsFile", "moreth.json" ,stream.ToArray()).AsTask();
            }  
        }

        // public static async Task<Person> Upload(IJSRuntime jsRuntime)
        // {

        // }
    }
}