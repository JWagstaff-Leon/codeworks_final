using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
    public class VaultsRepository
    {
        private readonly IDbConnection _db;

        public VaultsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal List<Vault> GetAllPublic()
        {
            string sql = @"
            SELECT
                v.*,
                a.*
            FROM vaults v
            JOIN accounts a ON v.creatorId = a.id
            WHERE v.isPriavte = false;
            ";
            return _db.Query<Vault, Profile, Vault>(sql, (vault, account) => {
                vault.Creator = account;
                return vault;
            }).ToList();
        }

        internal List<Vault> GetAllAuthorized(string userId)
        {
            string sql = @"
            SELECT
                v.*,
                a.*
            FROM vaults v
            JOIN accounts a ON v.creatorId = a.id
            WHERE v.isPrivate = false OR v.creatorId = @userId;
            ";
            return _db.Query<Vault, Profile, Vault>(sql, (vault, account) => {
                vault.Creator = account;
                return vault;
            }, new { userId }).ToList();
        }

        internal List<Vault> GetByUserId(string userId, string authId)
        {
            string sql = @"
            SELECT
                v.*,
                a.*
            FROM vaults v
            JOIN accounts a ON v.creatorId = a.id
            WHERE v.creatorId = @userId AND v.creatorId = @authId OR v.creatorId = @userId AND NOT v.isPrivate;
            ";//get rows where the person you're looking up is you OR where it's the person you're looking for and they're public
            return _db.Query<Vault, Profile, Vault>(sql, (vault, account) => {
                vault.Creator = account;
                return vault;
            }, new { userId, authId }).ToList();
        }

        internal Vault GetById(int id)
        {
            string sql = @"
            SELECT
                v.*,
                a.*
            FROM vaults v
            JOIN accounts a ON v.creatorId = a.id
            WHERE v.id = @id;
            ";
            return _db.Query<Vault, Profile, Vault>(sql, (vault, account) => {
                vault.Creator = account;
                return vault;
            }, new { id }).FirstOrDefault();
        }

        internal Vault Create(Vault data)
        {
            string sql = @"
            INSERT
            INTO vaults
            (creatorId, name, img, description, isPrivate)
            VALUES
            (@CreatorId, @Name, @Img, @Description, @IsPrivate);
            SELECT LAST_INSERT_ID();
            ";
            data.Id = _db.ExecuteScalar<int>(sql, data);
            data.CreatedAt = DateTime.UtcNow;
            data.UpdatedAt = DateTime.UtcNow;
            return data;
        }

        internal Vault Edit(Vault update)
        {
            string sql = @"
            UPDATE vaults
            SET
                name = @Name,
                description = @Description,
                isPrivate = @IsPrivate
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
            FROM vaults
            WHERE id = @id
            LIMIT 1;
            ";
            _db.Execute(sql, new { id });
        }
    }
}