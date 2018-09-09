using Hangman.Domain;
using System;
using System.Linq;

namespace Hangman.Service
{
    public interface IGameDataService
    {
        IQueryable<GameData> GetAllGameData();
        IQueryable<GameData> FindInGameData(GameData gameData);
        int SaveGameData(GameData gameData);
        int UpdateGameData(GameData gameData);
        GameData GetGameDataById(Guid gameDataId);
        int DeleteGameData(GameData gameData);
    }
}
