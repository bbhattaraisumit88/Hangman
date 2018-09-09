using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hangman.Domain;
using Hangman.Repo;

namespace Hangman.Service
{
    public class GameDataService : IGameDataService
    {
        private readonly IUnitOfWork _uow;
        public GameDataService(IUnitOfWork _uow)
        {
            this._uow = _uow;
        }

        public int DeleteGameData(GameData gameData)
        {
            try
            {
                _uow.GameDataRepository.Delete(gameData);
                return _uow.Save();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IQueryable<GameData> GetAllGameData()
        {
            try
            {
                return _uow.GameDataRepository.GetAllRecords();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IQueryable<GameData> FindInGameData(GameData gameData)
        {
            try
            {
                return _uow.GameDataRepository.FindInRecords(x => x.Hint == gameData.Hint && x.Answer == gameData.Answer);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public GameData GetGameDataById(Guid gameDataId)
        {
            try
            {
                return _uow.GameDataRepository.GetById(gameDataId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int SaveGameData(GameData gameData)
        {
            try
            {
                _uow.GameDataRepository.AddEntity(gameData);
                return _uow.Save();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int UpdateGameData(GameData gameData)
        {
            try
            {
                _uow.GameDataRepository.Update(gameData);
                return _uow.Save();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
