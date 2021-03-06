using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estudos.DTO;
using Estudos.Entities;
using Estudos.Helpers.Extensions;
using MySql.Data.MySqlClient;

namespace Estudos.Repositories.MySql
{
    public class PostsMySqlRepositoryQuery
    {
        public async Task<IEnumerable<Post>> GetQueryAsync(
            MySqlConnection conn,
            InboundSearchPosts search)
        {
            var sql = $"SELECT id, user_id, title, body FROM posts";

            var filters = new List<string>();

            var ids = search.Ids.SplitToInt();
            var userIds = search.UserIds.SplitToInt();

            if (ids.Any())
                filters.Add($"id IN ({string.Join(",", ids)})");

            if (userIds.Any())
               filters.Add($"user_id IN ({string.Join(",", userIds)})");

            if (!string.IsNullOrEmpty(search.Title))
               filters.Add($"title LIKE @title");

            if (!string.IsNullOrEmpty(search.Body))
               filters.Add($"body LIKE @body");

            if (filters.Any())
                sql += $" WHERE {string.Join(" AND ", filters)}";
                
            using var cmd = new MySqlCommand(sql, conn);

            if (!string.IsNullOrEmpty(search.Title))
                cmd.Parameters.AddWithValue("@title", "%" + search.Title + "%");

            if (!string.IsNullOrEmpty(search.Body))
               cmd.Parameters.AddWithValue("@body", "%" + search.Body + "%");

            using var reader = await cmd.ExecuteReaderAsync();

            var entities = new List<Post>();
            while (await reader.ReadAsync())
            {
                var id = int.Parse(reader["id"].ToString());
                var userId = int.Parse(reader["user_id"].ToString());
                var title = reader["title"].ToString();
                var body = reader["body"].ToString();

                var post = new Post(
                    id: id, 
                    userId: userId, 
                    title: title, 
                    body: body);
                
                entities.Add(post);
            }

            return entities;
        }

        public async Task DeleteQueryAsync(
            MySqlConnection conn, 
            IEnumerable<int> ids)
        {
            var sql = $"DELETE FROM posts";

            if (ids != null && ids.Any())
            {
                sql += $" WHERE id IN ({string.Join(",", ids)})";
            }
    
            using var cmd = new MySqlCommand(sql, conn);
            
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task UpdateQueryAsync(
            MySqlConnection conn, 
            InboundUpdatePost data)
        {
            var sql = $"UPDATE posts SET title = @title, body = @body WHERE id = @id";

            using var cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@id", data.Id);
            cmd.Parameters.AddWithValue("@title", data.NewTitle);
            cmd.Parameters.AddWithValue("@body", data.NewBody);

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<int> AddQueryAsync(
            MySqlConnection conn, 
            InboundAddPost data)
        {
            var sql = $"INSERT INTO posts (user_id, title, body) VALUES (@user_id, @title, @body)";

            using var cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@user_id", data.UserId);
            cmd.Parameters.AddWithValue("@title", data.Title);
            cmd.Parameters.AddWithValue("@body", data.Body);

            await cmd.ExecuteNonQueryAsync();

            return await GetLastIdQuery(conn);
        }

        private async Task<int> GetLastIdQuery(MySqlConnection conn)
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
    }
}
