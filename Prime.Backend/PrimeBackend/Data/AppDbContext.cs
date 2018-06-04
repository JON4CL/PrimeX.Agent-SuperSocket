using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Prime.Backend.Data
{
    public class MessageRecordContext : DbContext
    {
        public MessageRecordContext(DbContextOptions<MessageRecordContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MessageRecord>()
                .HasKey(c => c.Id);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<MessageRecord> MessageRecords { get; set; }
    }
}