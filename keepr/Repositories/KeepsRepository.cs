using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
    public class KeepsRepository
    {
        private readonly IDbConnection _db;

        public KeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal List<Keep> GetAll()
        {
            string sql = @"
            SELECT
                k.*,
                COUNT(k.id = v.keepId) AS kept,
                a.*
            FROM keeps k
            LEFT JOIN vaultkeeps v ON k.id = v.keepId
            JOIN accounts a ON k.creatorId = a.id
            GROUP BY k.id;
            ";
            return _db.Query<Keep, Profile, Keep>(sql, (keep, account) => {
                keep.Creator = account;
                return keep;
            }).ToList();
        }

        internal Keep GetById(int id)
        {
            string sql = @"
            SELECT
                k.*,
                COUNT(k.id = v.keepId) AS kept,
                a.*
            FROM keeps k
            LEFT JOIN vaultkeeps v ON k.id = v.keepId
            JOIN accounts a ON k.creatorId = a.id
            WHERE k.id = @id
            GROUP BY k.id;
            ";
            return _db.Query<Keep, Profile, Keep>(sql, (keep, account) => {
                keep.Creator = account;
                return keep;
            }, new { id }).FirstOrDefault();
        }

        internal List<Keep> GetByUserId(string id)
        {
            string sql = @"
            SELECT
                k.*,
                COUNT(k.id = v.keepId) AS kept,
                a.*
            FROM keeps k
            LEFT JOIN vaultkeeps v ON k.id = v.keepId
            JOIN accounts a ON k.creatorId = a.id
            WHERE a.id = @id
            GROUP BY k.id;
            ";
            return _db.Query<Keep, Profile, Keep>(sql, (keep, account) => {
                keep.Creator = account;
                return keep;
            }, new { id }).ToList();
        }

        internal Keep IncreaseViewsByOne(Keep viewed)
        {
            string sql = @"
            UPDATE keeps
            SET
                views = @views
            WHERE id = @id
            LIMIT 1;
            ";
            _db.Execute(sql, new { id = viewed.Id, views = ++viewed.Views });
            return viewed;
        }

        internal Keep Create(Keep data)
        {
            string sql = @"
            INSERT
            INTO keeps
            (creatorId, name, description, img)
            VALUES
            (@CreatorId, @Name, @Description, @Img);
            SELECT LAST_INSERT_ID();
            ";
            data.Id = _db.ExecuteScalar<int>(sql, data);
            data.CreatedAt = DateTime.UtcNow;
            data.UpdatedAt = DateTime.UtcNow;
            data.Kept = 0;
            return data;
        }

        internal Keep Edit(Keep update)
        {
            string sql = @"
            UPDATE keeps
            SET
                name = @Name,
                description = @Description,
                img = @Img,
                updatedAt = CURRENT_TIMESTAMP
            WHERE id = @Id
            LIMIT 1;
            SELECT CURRENT_TIMESTAMP;
            ";
            update.UpdatedAt = _db.ExecuteScalar<DateTime>(sql, update);
            return update;
        }

        internal void Remove(int id)
        {
            string sql = @"
            DELETE
            FROM keeps
            WHERE id = @id
            LIMIT 1;
            ";
            _db.Execute(sql, new { id });
        }
    }
}