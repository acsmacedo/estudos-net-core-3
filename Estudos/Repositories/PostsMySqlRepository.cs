using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estudos.DTO;
using Estudos.Entities;
using Estudos.Interfaces.DAL;
using Estudos.Interfaces.Repositories;
using MySql.Data.MySqlClient;

namespace Estudos.Repositories
{
    public class PostsMySqlRepository : IPostsRepository
    {
        private readonly IMySqlDatabaseClient _dbClient;

        public PostsMySqlRepository(IMySqlDatabaseClient dbClient)
        {
            _dbClient = dbClient;
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await GetInternalAsync(null);
        }

        public async Task<Post> GetByIdAsync(int id)
        {
            var entities = await GetInternalAsync(id);

            return entities.FirstOrDefault();
        }

        private async Task<IEnumerable<Post>> GetInternalAsync(int? postId)
        {
            using var conn = _dbClient.CreateDatabaseConnection();

            var sql = $"SELECT id, user_id, title, body FROM posts";

            if (postId.HasValue) 
                sql += $" WHERE id = {postId}";
    
            using var cmd = new MySqlCommand(sql, conn);
            using var reader = await cmd.ExecuteReaderAsync();

            var entities = new List<Post>();
            while (await reader.ReadAsync())
            {
                var id = int.Parse(reader["id"].ToString());
                var userId = int.Parse(reader["user_id"].ToString());
                var title = reader["title"].ToString();
                var body = reader["body"].ToString();

                entities.Add(new Post(id, userId, title, body));
            }

            return entities;
        }

        public async Task<int> AddAsync(InboundAddPost data)
        {
            using var conn = _dbClient.CreateDatabaseConnection();

            var sql = $"INSERT INTO posts (user_id, title, body) VALUES (@user_id, @title, @body)";

            using var cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@user_id", data.UserId.ToString());
            cmd.Parameters.AddWithValue("@title", data.Title);
            cmd.Parameters.AddWithValue("@body", data.Body);

            await cmd.ExecuteNonQueryAsync();

            return await GetLastId(conn);
        }

        private async Task<int> GetLastId(MySqlConnection conn)
        {
            var sql = $"SELECT LAST_INSERT_ID() as id";

            using var cmd = new MySqlCommand(sql, conn);
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                return int.Parse(reader["id"].ToString());
            }
            
            return 0;
        }

        public async Task DeleteByIdAsync(int id)
        {
            using var conn = _dbClient.CreateDatabaseConnection();

            var sql = $"DELETE FROM posts WHERE id = {id}";
    
            using var cmd = new MySqlCommand(sql, conn);
            
            await cmd.ExecuteReaderAsync();
        }

        public async Task UpdateAsync(int id, InboundUpdatePost data)
        {
            using var conn = _dbClient.CreateDatabaseConnection();

            var sql = $"UPDATE posts SET title = @title, body = @body";

            using var cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@title", data.Title);
            cmd.Parameters.AddWithValue("@body", data.Body);

            await cmd.ExecuteNonQueryAsync();
        }
    }
}
