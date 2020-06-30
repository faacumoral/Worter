using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Worter.DAO;
using Worter.DAO.Models;

namespace Worter.Services.Shared
{
    public class Service<T> where T : class, IEntity
    {
        private readonly WorterContext _ctx;
        private readonly int IdUsuario;

        public Service(WorterContext ctx, int idUsuario)
        {
            _ctx = ctx ?? throw new ArgumentNullException(nameof(ctx));
            IdUsuario = idUsuario;
        }

        public virtual IQueryable<T> GetForRead()
        {
            return _ctx.Set<T>().Where(e => e.Activo).AsNoTracking();
        }

        public virtual IQueryable<T> GetForUpdate()
        {
            return _ctx.Set<T>().Where(e => e.Activo);
        }

        public virtual void Update(T entidad, bool saveChanges = true)
        {
            entidad.FechaModificacion = DateTime.Now;
            entidad.IdUsuarioModificacion = IdUsuario;
            _ctx.Entry(entidad).State = EntityState.Modified;
            if (saveChanges) _ctx.SaveChanges();
        }

        public virtual void Insert(T entidad, bool saveChanges = true)
        {
            entidad.FechaCreacion = DateTime.Now;
            entidad.FechaModificacion = DateTime.Now;
            entidad.IdUsuarioCreador = IdUsuario;
            entidad.IdUsuarioModificacion = IdUsuario;
            entidad.Activo = true;
            _ctx.Entry(entidad).State = EntityState.Added;
            if (saveChanges) _ctx.SaveChanges();
        }

        public virtual void Delete(T entidad, bool saveChanges = true)
        {
            entidad.Activo = false;
            this.Update(entidad, saveChanges);
        }
    }
}
