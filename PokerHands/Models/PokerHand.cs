using PokerHands.Enums;
using System.Collections.Generic;

namespace PokerHands.Models
{
      public class PokerHand
      {
            public List<Card> Cards { get; set; }
            public HandValue HandValue { get; set; }
            public List<CardValue> HighCards { get; set; }
            public bool IsWinner { get; set; }
      }
}
