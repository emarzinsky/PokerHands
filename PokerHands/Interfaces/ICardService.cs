using PokerHands.Enums;
using PokerHands.Models;
using System.Collections.Generic;

namespace PokerHands.Interfaces
{
      public interface ICardService
      {
            List<Card> BuildDeck();
            List<Player> DealCards(List<Player> players);
            PokerHand DetermineHandValues(Player player);
            List<Player> DetermineWinningHand(List<Player> players);
      }
}
