using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Todo_App.Domain.Entities;

namespace Todo_App.Infrastructure.Persistence.Configurations
{
    internal class TodoItemTagsConfiguration : IEntityTypeConfiguration<TodoItemTags>
    {
        public void Configure(EntityTypeBuilder<TodoItemTags> builder)
        {
            builder.HasKey(tt => tt.Id);

            builder.Property(tt => tt.TodoItemId)
                .IsRequired();

            builder.Property(tt => tt.Tag)
                .HasMaxLength(200)
                .IsRequired();

            builder.Ignore(tt => tt.Tag);
        }
    }
}
