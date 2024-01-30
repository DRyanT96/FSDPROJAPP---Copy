using FSDPROJAPP.Shared.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FSDPROJAPP.Server.IRepository;
using System.Drawing;

namespace FSDPROJAPP.Server.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        Task Save(HttpContext httpContext);
        IGenericRepository<Hobby> Hobbys { get; }
        IGenericRepository<Detail> Details { get; }
        IGenericRepository<Dislike> Dislikes { get; }
        IGenericRepository<Like> Likes { get; }
        IGenericRepository<Subscription> Subscriptions { get; }
        IGenericRepository<Profile> Profiles { get; }
    }
}