using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ProjectN.Models;
using ProjectN.Parameter;

namespace ProjectN.Repository
{
    /// <summary>
    /// 卡片資料操作
    /// </summary>
    public class CardRepository
    {
        /// <summary>
        /// 連線字串
        /// </summary>
        private readonly string _connectString = @"Server=(LocalDB)\MSSQLLocalDB;Database=Newbie;Trusted_Connection=True;";

        /// <summary>
        /// 查詢卡片列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Card> GetList()
        {
            var sql = "SELECT * FROM Card";

            using var conn = new SqlConnection(_connectString);
            var result = conn.Query<Card>(sql);
            return result;
        }

        /// <summary>
        /// 查詢卡片
        /// </summary>
        /// <returns></returns>
        public Card Get(int id)
        {
            var sql =
            @"
                SELECT * 
                FROM Card 
                Where Id = @id
            ";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, System.Data.DbType.Int32);

            using (var conn = new SqlConnection(_connectString))
            {
                var result = conn.QueryFirstOrDefault<Card>(sql, parameters);
                return result;
            }
        }

        /// <summary>
        /// 新增卡片
        /// </summary>
        /// <param name="parameter">參數</param>
        /// <returns></returns>
        public int Create(CardParameter parameter)
        {
            var sql =
            @"
                INSERT INTO Card 
                (
                   [Name]
                  ,[Description]
                  ,[Attack]
                  ,[Health]
                  ,[Cost]
                ) 
                VALUES 
                (
                    @Name
                   ,@Description
                   ,@Attack
                   ,@Health
                   ,@Cost
                );
                
                SELECT @@IDENTITY;
            ";

            using (var conn = new SqlConnection(_connectString))
            {
                var result = conn.QueryFirstOrDefault<int>(sql, parameter);
                return result;
            }
        }

        /// <summary>
        /// 修改卡片
        /// </summary>
        /// <param name="id">卡片編號</param>
        /// <param name="parameter">參數</param>
        /// <returns></returns>
        public bool Update(int id, CardParameter parameter)
        {
            var sql =
            @"
                UPDATE Card
                SET 
                    [Name] = @Name
                   ,[Description] = @Description
                   ,[Attack] = @Attack
                   ,[Health] = @Health
                   ,[Cost] = @Cost
                WHERE
                    Id = @id
            ";

            var parameters = new DynamicParameters(parameter);
            parameters.Add("Id", id, System.Data.DbType.Int32);

            using (var conn = new SqlConnection(_connectString))
            {
                var result = conn.Execute(sql, parameters);
                return result > 0;
            }
        }

        /// <summary>
        /// 刪除卡片
        /// </summary>
        /// <param name="id">卡片編號</param>
        /// <returns></returns>
        public void Delete(int id)
        {
            var sql =
            @"
                DELETE FROM Card
                WHERE Id = @Id
            ";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, System.Data.DbType.Int32);

            using (var conn = new SqlConnection(_connectString))
            {
                var result = conn.Execute(sql, parameters);
            }
        }
    }
}
