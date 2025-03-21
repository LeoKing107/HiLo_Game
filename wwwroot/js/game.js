"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/gameHub").build();

connection.on("ReceiveMessage", function (playerName, result, mysteryNumber) {
    let gameResult = document.getElementById("gameResult");
    gameResult.innerHTML = `${playerName}: ${result} (Mystery Number: ${mysteryNumber})`;
});

document.getElementById("guessBtn").addEventListener("click", function () {
    var guess = parseInt(document.getElementById("guess").value);
    var playerName = document.getElementById("playerName").value || "Player";

    connection.invoke("MakeGuess", guess, playerName).catch(function (err) {
        return console.error(err.toString());
    });
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});
