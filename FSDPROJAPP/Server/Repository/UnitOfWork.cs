using FSDPROJAPP.Client.Pages;
using FSDPROJAPP.Server.Data;
using FSDPROJAPP.Server.IRepository;
using FSDPROJAPP.Server.Models;
using FSDPROJAPP.Server.Repository;
using FSDPROJAPP.Shared.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Profile = FSDPROJAPP.Shared.Domain.Profile;
using Subscription = FSDPROJAPP.Shared.Domain.Subscription;

namespace FSDPROJAPP.Server.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IGenericRepository<Hobby> _hobbys;
        private IGenericRepository<Detail> _details;
        private IGenericRepository<Dislike> _dislikes;
        private IGenericRepository<Like> _likes;
        private IGenericRepository<Subscription> _subscriptions;
        private IGenericRepository<Profile> _profiles;

        private UserManager<ApplicationUser> _userManager;

        public UnitOfWork(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IGenericRepository<Hobby> Hobbys
            => _hobbys ??= new GenericRepository<Hobby>(_context);
        public IGenericRepository<Detail> Details
            => _details ??= new GenericRepository<Detail>(_context);
        public IGenericRepository<Dislike> Dislikes
            => _dislikes ??= new GenericRepository<Dislike>(_context);
        public IGenericRepository<Like> Likes
            => _likes ??= new GenericRepository<Like>(_context);
        public IGenericRepository<Subscription> Subscriptions
            => _subscriptions ??= new GenericRepository<Subscription>(_context);
        public IGenericRepository<Profile> Profiles
            => _profiles ??= new GenericRepository<Profile>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save(HttpContext httpContext)
        {
            //To be implemented
            string user = "System";

            var entries = _context.ChangeTracker.Entries()
                .Where(q => q.State == EntityState.Modified ||
                    q.State == EntityState.Added);

            foreach (var entry in entries)
            {
                ((BaseDomainModel)entry.Entity).DateUpdated = DateTime.Now;
                ((BaseDomainModel)entry.Entity).UpdatedBy = user;
                if (entry.State == EntityState.Added)
                {
                    ((BaseDomainModel)entry.Entity).DateCreated = DateTime.Now;
                    ((BaseDomainModel)entry.Entity).CreatedBy = user;
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}