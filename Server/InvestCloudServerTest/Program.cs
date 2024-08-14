using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

class Program
{
    private static readonly HttpClient client = new HttpClient();

    static async Task Main(string[] args)
    {
        try
        {
            // URLs for the services
            string urlA = "https://recruitment-test.investcloud.com/api/numbers/init/1000";
            string urlB = "https://recruitment-test.investcloud.com/api/numbers/init/1000";
            string submitUrl = "https://recruitment-test.investcloud.com/api/numbers/validate";

            // Fetch datasets A and B
            var matrixA = await FetchMatrixAsync(urlA);
            var matrixB = await FetchMatrixAsync(urlB);

            // Multiply matrices
            var resultMatrix = MultiplyMatrices(matrixA, matrixB);

            // Compute MD5 hash
            string md5Hash = ComputeMd5Hash(resultMatrix);

            // Submit result
            var result = await SubmitResultAsync(submitUrl, md5Hash);

            Console.WriteLine("Submission result: " + result);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }

    private static async Task<int[,]> FetchMatrixAsync(string url)
    {
        var response = await client.GetStringAsync(url);
        var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(response);

        if (!apiResponse.Success)
        {
            throw new Exception($"Error fetching data: {apiResponse.Cause}");
        }

        int size = 1000;
        var matrix = new int[size, size];

        // Mocking matrix population as an example; replace this with actual data parsing
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                matrix[i, j] = apiResponse.Value; // Replace with actual values
            }
        }

        return matrix;
    }

    private static int[,] MultiplyMatrices(int[,] a, int[,] b)
    {
        int size = a.GetLength(0);
        var result = new int[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                for (int k = 0; k < size; k++)
                {
                    result[i, j] += a[i, k] * b[k, j];
                }
            }
        }
        return result;
    }

    private static string ComputeMd5Hash(int[,] matrix)
    {
        var sb = new StringBuilder();
        int size = matrix.GetLength(0);

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                sb.Append(matrix[i, j]);
            }
        }

        using (var md5 = MD5.Create())
        {
            byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString()));
            var hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
            return hashString;
        }
    }

    private static async Task<string> SubmitResultAsync(string url, string md5Hash)
    {
        var content = new StringContent(md5Hash, Encoding.UTF8, "application/json");
        var response = await client.PostAsync(url, content);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    class ApiResponse
    {
        public int Value { get; set; }
        public string Cause { get; set; }
        public bool Success { get; set; }
    }
}
