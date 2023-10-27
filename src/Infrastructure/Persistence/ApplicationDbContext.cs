using System.Reflection;
using System.Reflection.Emit;
using Duende.IdentityServer.EntityFramework.Options;
using MediatR;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Todo_App.Application.Common.Interfaces;
using Todo_App.Domain.Entities;
using Todo_App.Infrastructure.Identity;
using Todo_App.Infrastructure.Persistence.Configurations;
using Todo_App.Infrastructure.Persistence.Interceptors;

namespace Todo_App.Infrastructure.Persistence;

public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IApplicationDbContext
{
    private readonly IMediator _mediator;
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IOptions<OperationalStoreOptions> operationalStoreOptions,
        IMediator mediator,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
        : base(options, operationalStoreOptions)
    {
        _mediator = mediator;
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }

    public DbSet<TodoList> TodoLists => Set<TodoList>();

    public DbSet<TodoItem> TodoItems => Set<TodoItem>();
    public DbSet<TodoItemTags> TodoItemTag { get; set; }

    public DbSet<Tag> Tags { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new TodoItemConfiguration());
        builder.ApplyConfiguration(new TagConfiguration());
        builder.ApplyConfiguration(new TodoItemTagsConfiguration());

        builder.Entity<TodoItem>()
               .HasMany(ti => ti.Tags)
               .WithMany(t => t.TodoItems)
               .UsingEntity<Dictionary<string, object>>(
                   "TodoItemTag",
                   join => join
                       .HasOne<Tag>()
                       .WithMany()
                       .HasForeignKey("TagId")
                       .HasConstraintName("FK_TodoItemTag_TagId")
                       .OnDelete(DeleteBehavior.Cascade),
                   join => join
                       .HasOne<TodoItem>()
                       .WithMany()
                       .HasForeignKey("TodoItemId")
                       .HasConstraintName("FK_TodoItemTag_TodoItemId")
                       .OnDelete(DeleteBehavior.Cascade),
                   join =>
                   {
                       join.HasKey("TodoItemId", "TagId");
                       join.ToTable("TodoItemTags");
                   });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(this);

        return await base.SaveChangesAsync(cancellationToken);
    }
}
