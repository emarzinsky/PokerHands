using Microsoft.Extensions.Logging;
using PokerHands.Interfaces;
using PokerHands.Models;
using System;
using System.Collections.Generic;

namespace PokerHands.Services
{
      public class PlayerService : IPlayerService
      {
            private readonly ILogger _logger;
            private readonly ICardService _cardService;

            public PlayerService(ILogger<PlayerService> logger, ICardService cardService)
            {
                  _logger = logger;
                  _cardService = cardService;
            }

            public List<Player> SetPlayers(string playerNames)
            {
                  try
                  {
                        var players = playerNames.Split(',');
                        var playerList = new List<Player>();
                        foreach(var player in players)
                        {
                              if (!string.IsNullOrEmpty(player))
                              {
                                    playerList.Add(new Player
                                    {
                                          Hand = null,
                                          Name = player
                                    });
                              }
                        }

                        return _cardService.DealCards(playerList);

                  }
                  catch (Exception ex)
                  {
                        _logger.LogError($"An error occurred while setting up players: {playerNames}. error: {ex.Message}");
                        return null;
                  }

                  
            }

      }
}
