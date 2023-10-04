﻿using CleanArch.Domain.Intefaces;
using CleanArch.Domain.Models;
using CleanArch.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CleanArch.Infra.Data.Repository
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : Entity, new()
    {
        protected readonly MeuDbContext _dbContextSqlServer;

        protected RepositoryBase(MeuDbContext db)
        {
            _dbContextSqlServer = db;
        }

        public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContextSqlServer.Set<TEntity>().AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> ObterPorId(Guid id)
        {
            return await _dbContextSqlServer.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> ObterTodos()
        {
            return await _dbContextSqlServer.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public virtual async Task Adicionar(TEntity entity)
        {
            _dbContextSqlServer.Set<TEntity>().Add(entity);
        }

        public virtual async Task Atualizar(TEntity entity)
        {
            _dbContextSqlServer.Set<TEntity>().Update(entity);
        }

        public virtual async Task Remover(Guid id)
        {
            _dbContextSqlServer.Set<TEntity>().Remove(new TEntity { Id = id });
        }

        public void Dispose()
        {
            _dbContextSqlServer?.Dispose();
        }

    }
}