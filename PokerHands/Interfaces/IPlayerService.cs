using PokerHands.Models;
using System.Collections.Generic;

namespace PokerHands.Interfaces
{
      public interface IPlayerService
      {
            List<Player> SetPlayers(string playerNames);
      }
}
