using Microsoft.Extensions.Logging;
using PokerHands.Enums;
using PokerHands.Interfaces;
using PokerHands.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerHands.Services
{
      public class CardService: ICardService
      {
            private readonly ILogger _logger;

            public CardService(ILogger<CardService> logger)
            {
                  _logger = logger;
            }

            //build and shuffle a virtual deck of cards
            public List<Card> BuildDeck()
            {
                  var deck = new List<Card>();

                  //build deck
                  foreach(Suit suit in Enum.GetValues(typeof(Suit)))
                  {
                        foreach(CardValue value in Enum.GetValues(typeof(CardValue)))
                        {
                              deck.Add(new Card
                              {
                                    Suit = suit,
                                    Value = value
                              });
                        }
                  }

                  //shuffle deck
                  var rand = new Random();
                  var randomizedDeck = deck.OrderBy(x => rand.Next()).ToList();
                  return randomizedDeck;
            }

            public List<Player> DealCards(List<Player> players)
            {
                  try
                  {
                        var deck = BuildDeck();
                        var playerCount = 0;

                        //deal and determine hand value and high card
                        foreach (var player in players)
                        {
                              player.Hand = new PokerHand
                              {
                                    Cards = deck.Skip(playerCount * 5).Take(5).ToList()
                              };
                              player.Hand = DetermineHandValues(player);
                              playerCount++;
                        }

                        //determine winner
                        var stuff = DetermineWinningHand(players);
                        return stuff;
                  }
                  catch(Exception ex)
                  {
                        _logger.LogError($"An error occurred while dealing cards. ex: {ex.Message}");
                  }

                  return players;
            }

            #region Hand Values
            public PokerHand DetermineHandValues(Player player)
            {
                  try
                  {
                        if (player.Hand == null || player.Hand.Cards == null || player.Hand.Cards.Count == 0)
                        {
                              _logger.LogError($"DetermineHandValues received empty set of cards for player: {player.Name}");
                              return new PokerHand();
                        }

                        var cards = player.Hand.Cards;

                        //straight flush(all same suit)
                        var straightFlushCards = GetStraightFlushCards(cards);
                        if(straightFlushCards != null && straightFlushCards.Count() > 0)
                        {
                              player.Hand.HandValue = HandValue.StraightFlush;
                              player.Hand.HighCards = straightFlushCards;
                              return player.Hand;
                        }

                        //four of a kind(4 of same suit)
                        var fourOfAKindCards = GetFourOfAKindCards(cards);
                        if(fourOfAKindCards != null && fourOfAKindCards.Count() > 0)
                        {
                              player.Hand.HandValue = HandValue.FourOfAKind;
                              player.Hand.HighCards = fourOfAKindCards;
                              return player.Hand;
                        }
                        //full house(3 of same value, 2 pair)
                        var fullHouseCards = GetFullHouseCards(cards);
                        if(fullHouseCards != null && fullHouseCards.Count() > 0)
                        {
                              player.Hand.HandValue = HandValue.FullHouse;
                              player.Hand.HighCards = fullHouseCards;
                              return player.Hand;
                        }

                        //flush(all same suit)
                        var flushCards = GetFlushCards(cards);
                        if(flushCards.Count() > 0)
                        {
                              player.Hand.HandValue = HandValue.Flush;
                              player.Hand.HighCards = flushCards;
                              return player.Hand;
                        }

                        //straight(5 consecutive)
                        else if(IsStraight(cards))
                        {
                              player.Hand.HandValue = HandValue.Straight;
                              player.Hand.HighCards = cards.OrderByDescending(c => c.Value)
                                    .Select(x => x.Value)
                                    .ToList();
                              return player.Hand;
                        }
                        //three of a kind(3 of same value)
                        var threeOfAKindCards = GetThreeOfAKindcards(cards);
                        if(threeOfAKindCards != null && threeOfAKindCards.Count() > 0)
                        {
                              player.Hand.HandValue = HandValue.ThreeOfAKind;
                              player.Hand.HighCards = threeOfAKindCards;
                              return player.Hand;
                        }
                        //two pair(two pairs of same value)
                        var twoPairCards = GetTwoPairCards(cards);
                        if(twoPairCards != null && twoPairCards.Count() == 4)
                        {
                              player.Hand.HandValue = HandValue.TwoPair;
                              player.Hand.HighCards = twoPairCards;
                              return player.Hand;
                        }

                        //one pair
                        var pairCards = GetPairCards(cards);
                        if(pairCards != null && pairCards.Count() == 2)
                        {
                              player.Hand.HandValue = HandValue.Pair;
                              player.Hand.HighCards = pairCards;
                              return player.Hand;
                        }

                        //no value, just get ordered list of high cards
                        player.Hand.HandValue = HandValue.none;
                        player.Hand.HighCards = GetHighCards(cards);
                        return player.Hand;

                  }
                  catch(Exception ex)
                  {
                        _logger.LogError($"An error occurred while determining the hand values. ex: {ex.Message}");
                  }

                  return player.Hand;
            }

            private List<CardValue> GetHighCards(List<Card> cards)
            {
                  return cards.OrderByDescending(c => c.Value)
                        .Select(x => x.Value).ToList();
            }

            private List<CardValue> GetFlushCards(List<Card> cards)
            {
                  return cards.GroupBy(c => c.Suit)
                        .Where(c => c.Count() == 5)
                        .SelectMany(x => x.ToList()
                        .Select(y => y.Value)).ToList();
            }

            private List<CardValue> GetFullHouseCards(List<Card> cards)
            {
                  var pairCards = GetPairCards(cards);
                  var threeOfAKindCards = GetThreeOfAKindcards(cards);

                  if(pairCards != null && pairCards.Count> 0
                        && threeOfAKindCards != null && threeOfAKindCards.Count() > 0)
                  {
                        pairCards.AddRange(threeOfAKindCards);
                        return pairCards;
                  }
                  else
                  {
                        return null;
                  }
            }

            private List<CardValue> GetFourOfAKindCards(List<Card> cards)
            {
                  return cards.GroupBy(c => c.Value)
                        .Where(c => c.Count() == 4)
                        .SelectMany(x => x.ToList()
                        .Select(y => y.Value)).ToList();
            }

            private List<CardValue> GetThreeOfAKindcards(List<Card> cards)
            {
                  return cards.GroupBy(c => c.Value)
                        .Where(c => c.Count() == 3)
                        .SelectMany(x => x.ToList()
                        .Select(y => y.Value)).ToList();
            }

            private List<CardValue> GetPairCards(List<Card> cards)
            {
                  return cards.GroupBy(c => c.Value)
                        .Where(c => c.Count() == 2)
                        .SelectMany(x => x.ToList()
                        .Select(y => y.Value)).ToList();
            }

            private List<CardValue> GetTwoPairCards(List<Card> cards)
            {
                  return cards.GroupBy(c => c.Value)
                        .Where(c => c.Count() == 2)
                        .SelectMany(x => x.ToList()
                        .Select(y => y.Value)).ToList();
            }

            private List<CardValue> GetStraightFlushCards(List<Card> cards)
            {
                  if(IsStraight(cards))
                  {
                        return GetFlushCards(cards);
                  }
                  else
                  {
                        return null;
                  }
            }

            private bool IsStraight(List<Card> cards)
            {
                  cards = cards.OrderBy(c => (int)c.Value).ToList();

                  for (int i = 0; i < cards.Count()-1; i++)
                  {
                        if (cards[i].Value != cards[i + 1].Value - 1)
                        {
                              return false;
                        }
                  }
                  return true;
            }
            #endregion

            #region Determine Winner
            public List<Player> DetermineWinningHand(List<Player> players)
            {
                  //order by hand value, then check for matching hand values
                  players = players.OrderByDescending(p => (int)p.Hand.HandValue).ToList();

                  var matchingValue = players.GroupBy(p => p.Hand.HandValue)
                              .Where(p => p.Count() > 1 && p.Key == players[0].Hand.HandValue).FirstOrDefault();

                  //hands have same value, check high cards
                  if (matchingValue != null)
                  {
                        var matchingPlayerHands = players.Where(p => p.Hand.HandValue == matchingValue.Key);

                        //players have matching high cards, check other cards
                        if(matchingPlayerHands.GroupBy(p => p.Hand.HighCards)
                                              .Where(p => p.Count() > 1).Any())
                        {
                              foreach(var player in matchingPlayerHands)
                              {
                                    var playerCards = player.Hand.Cards.OrderByDescending(c => c.Value);

                                    foreach(var card in playerCards)
                                    {
                                          if(matchingPlayerHands.Any(x => x.Hand.Cards.Where(c => c.Value > card.Value).Count() == 0))
                                          {
                                                player.Hand.IsWinner = true;
                                          }
                                    }
                              }

                              
                        }
                        //no matching high cards, set winner
                        else
                        {
                              players.Where(p => p.Hand.HandValue == matchingValue.Key)
                                    .OrderByDescending(p => p.Hand.HighCards).First().Hand.IsWinner = true;
                        }
                  }
                  //no ties, set winner
                  else
                  {
                        players.OrderByDescending(p => p.Hand.HandValue).First().Hand.IsWinner = true;
                  }

                  return players;
            }
            #endregion
      }
}
