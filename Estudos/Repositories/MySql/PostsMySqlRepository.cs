using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Estudos.DTO;
using Estudos.Entities;
using Estudos.Interfaces.DAL;
using Estudos.Interfaces.Repositories;

namespace Estudos.Repositories.MySql
{
    public class PostsMySqlRepository : IPostsRepository
    {
        private readonly IMySqlDatabaseClient _dbClient;
        private readonly PostsMySqlRepositoryQuery _query;

        public PostsMySqlRepository(IMySqlDatabaseClient dbClient)
        {
            _dbClient = dbClient;
            _query = new PostsMySqlRepositoryQuery();
        }

        public async Task<IEnumerable<Post>> GetAsync(IEnumerable<int> ids)
        {
            using var conn = _dbClient.CreateDatabaseConnection();

            return await _query.GetQueryAsync(conn, ids);
        }

        public async Task DeleteAsync(IEnumerable<int> ids)
        {
            using var conn = _dbClient.CreateDatabaseConnection();

            await _query.DeleteQueryAsync(conn, ids);
        }

        public async Task UpdateAsync(IEnumerable<InboundUpdatePost> data)
        {
            using var conn = _dbClient.CreateDatabaseConnection();
            using var tran = conn.BeginTransaction();

            try 
            {
                foreach (var item in data)
                {
                    await _query.UpdateQueryAsync(conn, item);
                }

                await tran.CommitAsync();
            }
            catch (Exception ex)
            {
                await tran.RollbackAsync();
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<IEnumerable<int>> AddAsync(IEnumerable<InboundAddPost> data)
        {
            using var conn = _dbClient.CreateDatabaseConnection();
            using var tran = conn.BeginTransaction();
            
            var ids = new List<int>();
            
            try 
            {
                foreach (var item in data)
                {
                    ids.Add(await _query.AddQueryAsync(conn, item));
                }

                await tran.CommitAsync();
                return ids;
            }
            catch (Exception ex)
            {
                await tran.RollbackAsync();
                throw new Exception(ex.Message, ex);
            } 
        }
    }
}
