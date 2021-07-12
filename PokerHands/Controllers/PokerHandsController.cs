using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PokerHands.Enums;
using PokerHands.Interfaces;
using PokerHands.Models;
using PokerHands.Response;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerHands.Controllers
{
      [ApiController]
      [Route("api/")]
      [Produces("application/json")]
      public class PokerHandsController : ControllerBase
      {
            private readonly IPlayerService _playerService;
            private readonly ILogger<PokerHandsController> _logger;

            public PokerHandsController(ILogger<PokerHandsController> logger, IPlayerService playerService)
            {
                  _playerService = playerService;
                  _logger = logger;
            }

            [HttpGet("players/{playerNames}")]
            public GenericResponse<List<Player>> SetPlayers(string playerNames)
            {
                  if (string.IsNullOrEmpty(playerNames))
                  {
                        return new GenericResponse<List<Player>>(null, ErrorCode.PlayerNameEmpty);
                  }
                  try
                  {
                        var response = _playerService.SetPlayers(playerNames);

                        if(response == null)
                        {
                              return new GenericResponse<List<Player>>(null, ErrorCode.Exception);
                        }
                        else
                        {
                              return new GenericResponse<List<Player>>(response, ErrorCode.None);
                        }
                        
                  }
                  catch (Exception ex)
                  {
                        _logger.LogError($"PlayerController::AddPlayer: service threw exception adding players: {playerNames}. ex: {ex.Message}");

                        return new GenericResponse<List<Player>>(null, ErrorCode.Exception);
                  }
            }
      }
}
