using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using System;
using BlazorInputFile;

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

        public static async Task Download(IJSRuntime jsRuntime, Person movareth)
        {
            var jsonStr = Serialize(movareth);


            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(jsonStr);
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                await stream.WriteAsync(buffer, 0, buffer.Length);
                await jsRuntime.InvokeAsync<object>("saveAsFile", "moreth.json", stream.ToArray()).AsTask();
            }
        }

        public static async Task<Person> Upload(IFileListEntry file)
        {
            // Console.WriteLine($"Content type (not always supplied by the browser): {file.Type}");
            try
            {
                string allJson = "";
                using (var reader = new System.IO.StreamReader(file.Data))
                {
                    allJson = await reader.ReadToEndAsync();
                }

                // Console.WriteLine(allJson);
                return await JsonSerializer.DeserializeAsync<Person>(GenerateStreamFromString(allJson));
            }
            catch (System.Exception)
            {
                return new Person {FullName="خطا! فایل آبلود شده نامعتبر است", Gender = Gender.Male};
            }



        }

        private static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}