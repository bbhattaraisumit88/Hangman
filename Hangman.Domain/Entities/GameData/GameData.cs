using System;

namespace Hangman.Domain
{
    public class GameData
    {
        public Guid Id { get; set; }
        public string Hint { get; set; }
        public string Answer { get; set; }
    }
}
