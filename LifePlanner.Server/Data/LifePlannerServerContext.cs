using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LifePlanner.Server.Models;

namespace LifePlanner.Server.Data
{
    public class LifePlannerServerContext : DbContext
    {
        public LifePlannerServerContext(DbContextOptions<LifePlannerServerContext> options)
            : base(options)
        {
        }

        // DbSet for each entity
        public DbSet<User> Users { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Subtask> Subtasks { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<MonthlyGoal> MonthlyGoals { get; set; }
        public DbSet<DailyAgenda> DailyAgendas { get; set; }
        public DbSet<DailyTask> DailyTasks { get; set; }


        // Configure relationships and additional settings
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the relationships between entities
            modelBuilder.Entity<Goal>()
                .HasOne(g => g.User)
                .WithMany(u => u.Goals)
                .HasForeignKey(g => g.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Goal>()
                .HasOne(g => g.Category)
                .WithMany(c => c.Goals)
                .HasForeignKey(g => g.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Subtask>()
                .HasOne(s => s.Goal)
                .WithMany(g => g.Subtasks)
                .HasForeignKey(s => s.GoalId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MonthlyGoal>()
                .HasOne(m => m.User)
                .WithMany(u => u.MonthlyGoals)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DailyAgenda>()
                .HasOne(d => d.User)
                .WithMany(u => u.DailyAgendas)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DailyTask>()
                .HasOne(d => d.DailyAgenda)
                .WithMany(da => da.DailyTasks)
                .HasForeignKey(d => d.DailyAgendaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DailyTask>()
                .HasOne(d => d.User)
                .WithMany(u => u.DailyTasks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
