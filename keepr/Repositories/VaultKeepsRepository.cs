using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
    public class VaultKeepsRepository
    {
        private readonly IDbConnection _db;

        public VaultKeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal VaultKeep GetById(int id)
        {
            string sql = @"
            SELECT
                *
            FROM vaultkeeps
            WHERE id = @id;
            ";
            return _db.Query<VaultKeep>(sql, new { id }).FirstOrDefault();
        }

        internal List<KeepVaultKeepVM> GetByVaultId(int id)
        {
            string sql = @"
            SELECT
                k.*,
                COUNT(k.id = vk.keepId) AS kept,
                vk.id AS vaultKeepId,
                a.*
            FROM vaultkeeps vk
            JOIN keeps k on vk.keepId = k.id
            JOIN accounts a ON k.creatorId = a.id
            WHERE vk.vaultId = @id
            GROUP BY vk.id;
            ";
            return _db.Query<KeepVaultKeepVM, Profile, KeepVaultKeepVM>(sql, (vm, account) => {
                vm.Creator = account;
                return vm;
            },new { id }).ToList();
        }

        internal List<VaultKeep> GetByKeepAndUserIds(int keepId, string userId)
        {
            string sql = @"
            SELECT
                *
            FROM vaultkeeps
            WHERE id = @keepId AND creatorId = @userId;
            ";
            return _db.Query<VaultKeep>(sql, new { keepId, userId }).ToList();
        }

        internal VaultKeep Create(VaultKeep data)
        {
            string sql = @"
            INSERT
            INTO vaultkeeps
            (creatorId, vaultId, keepId)
            VALUES
            (@CreatorId, @VaultId, @KeepId);
            SELECT LAST_INSERT_ID();
            ";
            data.Id = _db.ExecuteScalar<int>(sql, data);
            data.CreatedAt = DateTime.UtcNow;
            return data;
        }

        internal void Remove(int id)
        {
            string sql = @"
            DELETE
            FROM vaultkeeps
            WHERE id = @id
            LIMIT 1;
            ";
            _db.Execute(sql, new { id });
        }
    }
}