using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PokerHands.Enums;
using PokerHands.Models;
using PokerHands.Services;
using System.Collections.Generic;

namespace PokerHandsUnitTests
{
      [TestClass]
      public class CardServiceTests
      {
            private readonly Mock<ILogger<CardService>> _mockLogger;
            private readonly CardService _cardService;

            public CardServiceTests()
            {
                  _mockLogger = new Mock<ILogger<CardService>>();
                  _cardService = new CardService(_mockLogger.Object);
            }

            [TestMethod]
            public void GetStraightFlushCardsReturnsStraightFlush()
            {
                  var cards = new List<Card>();
                  cards.Add(new Card
                  {
                        Suit = Suit.Clubs,
                        Value = CardValue.Ace
                  });
                  cards.Add(new Card
                  {
                        Suit = Suit.Clubs,
                        Value = CardValue.King
                  });
                  cards.Add(new Card
                  {
                        Suit = Suit.Clubs,
                        Value = CardValue.Queen
                  });
                  cards.Add(new Card
                  {
                        Suit = Suit.Clubs,
                        Value = CardValue.Jack
                  });
                  cards.Add(new Card
                  {
                        Suit = Suit.Clubs,
                        Value = CardValue.Ten
                  });

                  var player = new Player
                  {
                        Hand = new PokerHand
                        {
                              Cards = cards,
                              HandValue = HandValue.none,
                              HighCards = new List<CardValue>(),
                              IsWinner = false
                        },
                        Name = "player1"
                  };

                  Assert.AreEqual(HandValue.StraightFlush, _cardService.DetermineHandValues(player).HandValue);
            }

            [TestMethod]
            public void GetFourOfAKindCardsReturnsFourOfAKind()
            {
                  var cards = new List<Card>();
                  cards.Add(new Card
                  {
                        Suit = Suit.Clubs,
                        Value = CardValue.Ace
                  });
                  cards.Add(new Card
                  {
                        Suit = Suit.Diamonds,
                        Value = CardValue.Ace
                  });
                  cards.Add(new Card
                  {
                        Suit = Suit.Hearts,
                        Value = CardValue.Ace
                  });
                  cards.Add(new Card
                  {
                        Suit = Suit.Spades,
                        Value = CardValue.Ace
                  });
                  cards.Add(new Card
                  {
                        Suit = Suit.Clubs,
                        Value = CardValue.Ten
                  });

                  var player = new Player
                  {
                        Hand = new PokerHand
                        {
                              Cards = cards,
                              HandValue = HandValue.none,
                              HighCards = new List<CardValue>(),
                              IsWinner = false
                        },
                        Name = "player1"
                  };

                  Assert.AreEqual(HandValue.FourOfAKind, _cardService.DetermineHandValues(player).HandValue);
            }

            [TestMethod]
            public void GetFullHouseCardsReturnsFullHouse()
            {
                  var cards = new List<Card>();
                  cards.Add(new Card
                  {
                        Suit = Suit.Clubs,
                        Value = CardValue.Ace
                  });
                  cards.Add(new Card
                  {
                        Suit = Suit.Diamonds,
                        Value = CardValue.Ace
                  });
                  cards.Add(new Card
                  {
                        Suit = Suit.Hearts,
                        Value = CardValue.Ace
                  });
                  cards.Add(new Card
                  {
                        Suit = Suit.Spades,
                        Value = CardValue.Jack
                  });
                  cards.Add(new Card
                  {
                        Suit = Suit.Clubs,
                        Value = CardValue.Jack
                  });

                  var player = new Player
                  {
                        Hand = new PokerHand
                        {
                              Cards = cards,
                              HandValue = HandValue.none,
                              HighCards = new List<CardValue>(),
                              IsWinner = false
                        },
                        Name = "player1"
                  };

                  Assert.AreEqual(HandValue.FullHouse, _cardService.DetermineHandValues(player).HandValue);
            }

            [TestMethod]
            public void GetFlushCardsReturnsFlush()
            {
                  var cards = new List<Card>();
                  cards.Add(new Card
                  {
                        Suit = Suit.Clubs,
                        Value = CardValue.Ace
                  });
                  cards.Add(new Card
                  {
                        Suit = Suit.Clubs,
                        Value = CardValue.Jack
                  });
                  cards.Add(new Card
                  {
                        Suit = Suit.Clubs,
                        Value = CardValue.Four
                  });
                  cards.Add(new Card
                  {
                        Suit = Suit.Clubs,
                        Value = CardValue.King
                  });
                  cards.Add(new Card
                  {
                        Suit = Suit.Clubs,
                        Value = CardValue.Queen
                  });

                  var player = new Player
                  {
                        Hand = new PokerHand
                        {
                              Cards = cards,
                              HandValue = HandValue.none,
                              HighCards = new List<CardValue>(),
                              IsWinner = false
                        },
                        Name = "player1"
                  };

                  Assert.AreEqual(HandValue.Flush, _cardService.DetermineHandValues(player).HandValue);
            }

            [TestMethod]
            public void GetStraightCardsReturnsStraight()
            {
                  var cards = new List<Card>();
                  cards.Add(new Card
                  {
                        Suit = Suit.Clubs,
                        Value = CardValue.Five
                  });
                  cards.Add(new Card
                  {
                        Suit = Suit.Diamonds,
                        Value = CardValue.Six
                  });
                  cards.Add(new Card
                  {
                        Suit = Suit.Hearts,
                        Value = CardValue.Seven
                  });
                  cards.Add(new Card
                  {
                        Suit = Suit.Spades,
                        Value = CardValue.Eight
                  });
                  cards.Add(new Card
                  {
                        Suit = Suit.Clubs,
                        Value = CardValue.Nine
                  });

                  var player = new Player
                  {
                        Hand = new PokerHand
                        {
                              Cards = cards,
                              HandValue = HandValue.none,
                              HighCards = new List<CardValue>(),
                              IsWinner = false
                        },
                        Name = "player1"
                  };

                  Assert.AreEqual(HandValue.Straight, _cardService.DetermineHandValues(player).HandValue);
            }

            [TestMethod]
            public void GetThreeOfAKindCardsReturnsThreeOfAKind()
            {
                  var cards = new List<Card>();
                  cards.Add(new Card
                  {
                        Suit = Suit.Clubs,
                        Value = CardValue.Ace
                  });
                  cards.Add(new Card
                  {
                        Suit = Suit.Diamonds,
                        Value = CardValue.Ace
                  });
                  cards.Add(new Card
                  {
                        Suit = Suit.Hearts,
                        Value = CardValue.Ace
                  });
                  cards.Add(new Card
                  {
                        Suit = Suit.Spades,
                        Value = CardValue.Jack
                  });
                  cards.Add(new Card
                  {
                        Suit = Suit.Clubs,
                        Value = CardValue.Ten
                  });

                  var player = new Player
                  {
                        Hand = new PokerHand
                        {
                              Cards = cards,
                              HandValue = HandValue.none,
                              HighCards = new List<CardValue>(),
                              IsWinner = false
                        },
                        Name = "player1"
                  };

                  Assert.AreEqual(HandValue.ThreeOfAKind, _cardService.DetermineHandValues(player).HandValue);
            }

            [TestMethod]
            public void GetTwoPairCardsReturnsTwoPair()
            {
                  var cards = new List<Card>();
                  cards.Add(new Card
                  {
                        Suit = Suit.Clubs,
                        Value = CardValue.Ace
                  });
                  cards.Add(new Card
                  {
                        Suit = Suit.Diamonds,
                        Value = CardValue.Ace
                  });
                  cards.Add(new Card
                  {
                        Suit = Suit.Hearts,
                        Value = CardValue.Jack
                  });
                  cards.Add(new Card
                  {
                        Suit = Suit.Spades,
                        Value = CardValue.Jack
                  });
                  cards.Add(new Card
                  {
                        Suit = Suit.Clubs,
                        Value = CardValue.Ten
                  });

                  var player = new Player
                  {
                        Hand = new PokerHand
                        {
                              Cards = cards,
                              HandValue = HandValue.none,
                              HighCards = new List<CardValue>(),
                              IsWinner = false
                        },
                        Name = "player1"
                  };

                  Assert.AreEqual(HandValue.TwoPair, _cardService.DetermineHandValues(player).HandValue);
            }

            [TestMethod]
            public void GetPairCardsReturnsPair()
            {
                  var cards = new List<Card>();
                  cards.Add(new Card
                  {
                        Suit = Suit.Clubs,
                        Value = CardValue.Ace
                  });
                  cards.Add(new Card
                  {
                        Suit = Suit.Diamonds,
                        Value = CardValue.Ace
                  });
                  cards.Add(new Card
                  {
                        Suit = Suit.Hearts,
                        Value = CardValue.Jack
                  });
                  cards.Add(new Card
                  {
                        Suit = Suit.Spades,
                        Value = CardValue.Eight
                  });
                  cards.Add(new Card
                  {
                        Suit = Suit.Clubs,
                        Value = CardValue.Ten
                  });

                  var player = new Player
                  {
                        Hand = new PokerHand
                        {
                              Cards = cards,
                              HandValue = HandValue.none,
                              HighCards = new List<CardValue>(),
                              IsWinner = false
                        },
                        Name = "player1"
                  };

                  Assert.AreEqual(HandValue.Pair, _cardService.DetermineHandValues(player).HandValue);
            }

            [TestMethod]
            public void GetNoMatchReturnsNone()
            {
                  var cards = new List<Card>();
                  cards.Add(new Card
                  {
                        Suit = Suit.Clubs,
                        Value = CardValue.Ace
                  });
                  cards.Add(new Card
                  {
                        Suit = Suit.Diamonds,
                        Value = CardValue.Two
                  });
                  cards.Add(new Card
                  {
                        Suit = Suit.Hearts,
                        Value = CardValue.Jack
                  });
                  cards.Add(new Card
                  {
                        Suit = Suit.Spades,
                        Value = CardValue.Eight
                  });
                  cards.Add(new Card
                  {
                        Suit = Suit.Clubs,
                        Value = CardValue.Ten
                  });

                  var player = new Player
                  {
                        Hand = new PokerHand
                        {
                              Cards = cards,
                              HandValue = HandValue.none,
                              HighCards = new List<CardValue>(),
                              IsWinner = false
                        },
                        Name = "player1"
                  };

                  Assert.AreEqual(HandValue.none, _cardService.DetermineHandValues(player).HandValue);
            }
      }
}
