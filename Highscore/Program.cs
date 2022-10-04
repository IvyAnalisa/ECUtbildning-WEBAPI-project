using Highscore.Models;
using Highscore.Models.DTO;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using static System.Console;

namespace Highscore; // Note: actual namespace depends on the project name.

class Program

{
    static readonly HttpClient httpClient = new()
    {
        BaseAddress = new Uri("https://localhost:5000/api/")
    };


    static void Main(string[] args)
    {
        bool appicationRunning = false;
        var token = Login();


        if (token != null)
        {






            // Sätter headern "Authorization" till "Bearer <token>"
            httpClient.DefaultRequestHeaders.Authorization
               = new AuthenticationHeaderValue("Bearer", token);

            //Clear();

            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            var firstName = jwtSecurityToken.Claims.First(claim => claim.Type == "given_name").Value;
            var lastName = jwtSecurityToken.Claims.First(claim => claim.Type == "family_name").Value;
            var administrator = jwtSecurityToken.Claims.First(claim => claim.Type == "role")?.Value;

            if (administrator != null)
            {
                WriteLine("You are administrator");

            }
            else
            {
                WriteLine("You are not administrator");
            }
            Clear();
            WriteLine($"Welcome {firstName} {lastName}");



            WriteLine("*******************************************************");
            WriteLine("*********    Inlämningsuppgift-WEB API      *********");
            WriteLine("***** Namn: IVY ANALISA LA - Webutvecklare.Net 2021****");
            WriteLine("*******************************************************");
            do
            {
                WriteLine("********************************");
                WriteLine("***  1. Register HighScore       ***");
                WriteLine("***  2. Display HighScore    ***");
                WriteLine("***  3. Add New Game ***");
                WriteLine("***  4. Delete Game   ***");
                WriteLine("***  5. Search Game    ***");
                WriteLine("***  6. Display Game List    ***");
                WriteLine("***  7. Exit                   ***");
                WriteLine("********************************");
                ConsoleKeyInfo input = ReadKey(true);
                switch (input.Key)
                {
                    case ConsoleKey.D1:
                        RegisterHighScore();
                        break;

                    case ConsoleKey.D2:

                        DisplayHighScore();
                        break;

                    case ConsoleKey.D3:
                        AddNewGame();
                        break;

                    case ConsoleKey.D4:
                        DeleteGame();
                        break;

                    case ConsoleKey.D5:
                        SearchGame();
                        break;
                    case ConsoleKey.D6:
                        DisplayGame();
                        break;

                    case ConsoleKey.D7:
                        {
                            appicationRunning = true;
                        }
                        return;
                    default:
                        WriteLine("Wrong input");
                        break;
                }
            } while (!appicationRunning);


        }
        else
        {
            WriteLine("login failed ... Try again");


        }



    }

    private static string Login()
    {

        WriteLine("Username:");
        string username = ReadLine();
        WriteLine("Password");
        string password = ReadLine();
        /* if (username != "john@doe.com" && password == "Abcd1234@")
         {
             WriteLine("fail login. Try again");
         }*/


        var credentialsDto = new CredentialsDto
        {
            Username = username,
            Password = password
        };

        /*if (credentialsDto.Username != username || credentialsDto.Password != password)
        {
            WriteLine("Login failed.Try again....");
        }*/
        var serializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        var serializedCredentialsDto = JsonSerializer.Serialize(credentialsDto, serializeOptions);

        var body = new StringContent(
            serializedCredentialsDto,
            Encoding.UTF8,
            "application/json");

        // POST https://localhost:5000/api/auth
        var response = httpClient.PostAsync("auth", body).Result;

        var json = response.Content.ReadAsStringAsync().Result;

        var tokenDto = JsonSerializer.Deserialize<TokenDto>(json, serializeOptions);

        return tokenDto.Token;



    }

    private static void DisplayHighScore()
    {
        FetchHighscore();


    }
    private static void RegisterHighScore()
    {



        WriteLine("Score:");
        int score = Convert.ToInt32(ReadLine());
        WriteLine("Player:");
        string player = ReadLine();
        WriteLine("Date:");
        DateTime date = DateTime.Parse(ReadLine());
        DisplayGame();
        WriteLine("seclect Game Id of Game :");
        int gameId = Convert.ToInt32(ReadLine());
        // WriteLine("GameName:");
        // string gameName = ReadLine();

        // skicka till HighScore med POST
        var createHighScoreDto = new CreateHighScoreDto
        {
            Score = score,
            Player = player,
            Date = date,
            GameId = gameId
            // GameName = gameName


        };

        var serializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        var serializedCreateHighScoreDto = JsonSerializer.Serialize(createHighScoreDto, serializeOptions);
        var body = new StringContent(serializedCreateHighScoreDto,
            Encoding.UTF8,
            "application/json");
        // POST https://localhost:5000/api/highscores
        var response = httpClient.PostAsync("highscores", body).Result;
        //   httpClient.PostAsJsonAsync()
        Clear();
        //httpClient.PostAsJsonAsync()

        Clear();

        if (response.IsSuccessStatusCode)
        {
            WriteLine(" New  Score is added");
        }
        else
        {
            WriteLine("Imposible to add new Score");
        }


        Thread.Sleep(2000);
    }

    private static void SearchGame()
    {
        WriteLine("Search Game:");

        string searchTerm = ReadLine();

        Clear();
        var games = Search(searchTerm);
        if (games.Any())
        {
            WriteLine(" Game Id               Game Name");
            WriteLine("--------------------------------");
            foreach (var game in games)
            {
                WriteLine($"{game.GameId}        {game.GameName} ");
            }
        }
        else
        {
            WriteLine("Game is not found");
        }
        Thread.Sleep(2000);
    }
    public static IEnumerable<Game> Search(string searchTerm)
    {
        //skicka ett Http GET https://localhost:5000/api/games?name={searchTerm}
        var response = httpClient.GetAsync($"games?name={searchTerm}").Result;
        IEnumerable<Game> games;
        if (response.IsSuccessStatusCode)
        {
            var json = response.Content.ReadAsStringAsync().Result;



            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            games = JsonSerializer.Deserialize<IEnumerable<Game>>(json, serializeOptions);
        }
        else
        {
            throw new Exception("HTTP request failed");
        }

        return games;
    }

    private static void AddNewGame()
    {
        WriteLine("Name of Game:");
        string gameName = ReadLine();
        WriteLine("Description:");
        string description = ReadLine();
        WriteLine("Year:");
        string year = ReadLine();
        WriteLine("Genre:");
        string genre = ReadLine();
        WriteLine("ImageUrl:");
        Uri imageUrl = new Uri(ReadLine());
        // skicka till IvyGame med POST
        var createGameDto = new CreateGameDto
        {
            GameName = gameName,
            Description = description,
            Year = year,
            Genre = genre,
            ImageUrl = imageUrl
        };

        var serializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        var serializedCreateGameDto = JsonSerializer.Serialize(createGameDto, serializeOptions);
        var body = new StringContent(serializedCreateGameDto,
            Encoding.UTF8,
            "application/json");
        // POST https://localhost:5000/api/games
        var response = httpClient.PostAsync("games", body).Result;
        //   httpClient.PostAsJsonAsync()
        Clear();
        //httpClient.PostAsJsonAsync()

        Clear();

        if (response.IsSuccessStatusCode)
        {
            WriteLine(" New game is added");
        }
        else
        {
            WriteLine("Imposible to add new game");
        }


        Thread.Sleep(2000);

    }


    private static void DeleteGame()
    {

        var games = FetchGames();

        foreach (var game in games)
        {
            WriteLine($"{game.GameId}     {game.GameName}");
        }

        WriteLine();

        Write(" Game ID: ");

        var gameId = int.Parse(ReadLine());

        Clear();


        // DELETE http://localhost:5000/api/games/1
        var response = httpClient.DeleteAsync($"games/{gameId}")
            .Result;

        if (response.IsSuccessStatusCode)
        {
            WriteLine("Game is deleted");
        }
        else
        {
            WriteLine("Imposible to delete game");
        }

        Thread.Sleep(2000);
    }

    private static void DisplayGame()
    {
        var games = FetchGames();
        WriteLine(" GameID               Game Name        ");
        WriteLine("---------------------------------------");
        foreach (var game in games)
        {

            WriteLine($"{game.GameId}         {game.GameName}         ");
        }
    }
    private static IEnumerable<Game> FetchGames()
    {
        // Skicka ett HTTP GET https://localhost:5000/api/games
        var response = httpClient.GetAsync($"games")
            .Result;


        var json = response.Content.ReadAsStringAsync().Result;

        var serializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        IEnumerable<Game> games = JsonSerializer.Deserialize<IEnumerable<Game>>(json, serializeOptions);


        return games;

    }

    private static IEnumerable<HighScore> FetchHighscore()
    {
        // Skicka ett HTTP GET https://localhost:5000/api/highscores
        var response = httpClient.GetAsync($"highscores")
            .Result;
        //IEnumerable<HighScore> highscores;

        var json = response.Content.ReadAsStringAsync().Result;

        var serializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        IEnumerable<HighScore> highscores = JsonSerializer.Deserialize<IEnumerable<HighScore>>(json, serializeOptions);
        WriteLine(" Player       Score        Game Name             Date");
        WriteLine("-----------------------------------------------------");
        foreach (var highscore in highscores)
        {
            WriteLine($"{highscore.Player}        {highscore.Score}    {highscore.Game.GameName}     {highscore.Date} ");
        }

        return highscores;

    }
    /*
    public static IEnumerable<HighScore> SearchGame(string gameName)
    {
        //skicka ett Http GET https://localhost:5000/api/games?name={searchTerm}
        var response = httpClient.GetAsync($"highscores").Result;
        IEnumerable<HighScore> highscores;
        if (response.IsSuccessStatusCode)
        {
            var json = response.Content.ReadAsStringAsync().Result;



            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            highscores = JsonSerializer.Deserialize<IEnumerable<HighScore>>(json, serializeOptions);
        }
        else
        {
            throw new Exception("HTTP request failed");
        }

        return highscores;
    }*/

}

