using PokerHands.Enums;

namespace PokerHands.Response
{
      public class GenericResponse<T>
      {
            public int Status { get; set; }

            public bool Success { get; set; }

            public string Message { get; set; }

            public T Data { get; set; }

            private GenericResponse(ErrorCode errorCode)
            {
                  switch (errorCode)
                  {
                        case ErrorCode.None:
                              Status = 200;
                              Success = true;
                              Message = "Poker hand dealt successfully";
                              break;
                        case ErrorCode.PlayerNameEmpty:
                              Status = 400;
                              Success = false;
                              Message = "Player name received was empty. Please enter a player name to add";
                              break;
                        case ErrorCode.Exception:
                              Status = 500;
                              Success = false;
                              Message = "An error occurred while processing your request";
                              break;
                  }
            }

            public GenericResponse(T data, ErrorCode errorCode)
                : this(errorCode)
            {
                  Data = data;
            }
      }
}
