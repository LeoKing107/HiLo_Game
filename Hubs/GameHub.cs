using Microsoft.AspNetCore.SignalR;
using System;

namespace HiLoGame.Hubs
{
    public class GameHub : Hub
    {
        private static int mysteryNumber = new Random().Next(1, 101);
        private static bool gameStarted = false;
        public async Task MakeGuess(int guess, string playerName)
        {
            if (!gameStarted)
            {
                gameStarted = true; 
                mysteryNumber = new Random().Next(1, 101); 
            }

            string result;

            if (guess < mysteryNumber)
            {
                result = "LO"; 
            }
            else if (guess > mysteryNumber)
            {
                result = "HI"; 
            }
            else
            {
                result = "Correct!";
                gameStarted = false; 
            }

            await Clients.All.SendAsync("ReceiveMessage", playerName, result, mysteryNumber);
        }


        public override Task OnDisconnectedAsync(Exception? exception)
        {
            gameStarted = false; 
            return base.OnDisconnectedAsync(exception);
        }
    }
}
