using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Estudos.DTO;
using Estudos.Entities;
using Estudos.Interfaces.Repositories;
using MySql.Data.MySqlClient;

namespace Estudos.Repositories
{
    public class PostsMySqlRepository : IPostsRepository
    {
        private readonly string _connStr = "server=172.17.0.2;user=root;database=estudos;port=3306;password=admin";
        
        public Task<Post> AddAsync(InboundAddPost data)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            var entities = new List<Post>();

            MySqlConnection conn = new MySqlConnection(_connStr);
            try
            {
                conn.Open();

                string sql = "SELECT id, user_id, title, body FROM posts";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                var rdr = await cmd.ExecuteReaderAsync();

                while (rdr.Read())
                {
                    var id = int.Parse(rdr["id"].ToString());
                    var userId = int.Parse(rdr["user_id"].ToString());
                    var title = rdr["title"].ToString();
                    var body = rdr["body"].ToString();

                    var entity = new Post(id, userId, title, body);

                    entities.Add(entity);
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
            Console.WriteLine("Done.");
            return entities;
        }

        public Task<Post> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Post> UpdateAsync(int id, InboundUpdatePost data)
        {
            throw new System.NotImplementedException();
        }
    }
}
