using System.Text.Json;
using System.Threading.Channels;
using TaskForApi;

public class Program
{
    public static void Main()
    {

        Show();
        Loop();

    }  
    
    public async static void Show()
    {
        ApiService apiService = new ApiService();
        await apiService.UserList();
        await apiService.UserPostList();

    }
    public static void Loop()
    {
        int x = 0;
        while(x<100)
        {
            Console.WriteLine(++x);
            Thread.Sleep(100);
        }
    }
}

public class ApiService
{
    HttpClient client = new HttpClient();
    public async  Task UserList()
    {
        Thread.Sleep(2000);
        string url = "https://jsonplaceholder.typicode.com/users";
        var response = await client.GetAsync(url);
        string responseBody = await response.Content.ReadAsStringAsync();
        List<Root1> users = JsonSerializer.Deserialize<List<Root1>>(responseBody);
        foreach (var user in users)
        {
           Console.WriteLine($"\nId:{user.id}  \nName:{user.name} \nUserName:{user.username} \nEmail:{user.email}");
        }
    }
    public async Task UserPostList()
    {
            Thread.Sleep(2000);
            Console.Write("\nenter Id:"); int Id = int.Parse(Console.ReadLine());
            Console.WriteLine("Loading....");
            string url = "https://jsonplaceholder.typicode.com/posts/";
            var response = await client.GetAsync(url);
            string responseBody = await response.Content.ReadAsStringAsync();
            List<Root2> posts = JsonSerializer.Deserialize<List<Root2>>(responseBody);
            posts.Where(post => post.userId == Id)
                  .ToList()
                  .ForEach(post =>
                  {
                      Console.WriteLine($"\nUserId:{post.userId} \nId:{post.id} \nTitle:{post.title} \nBody:{post.body}");
                  });
    }
}


