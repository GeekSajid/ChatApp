using ChatApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Data
{
    public class ChatAppContext : DbContext
    {
        public ChatAppContext()
        {

        }
        public ChatAppContext(DbContextOptions<ChatAppContext> options) : base(options)
        { }
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
