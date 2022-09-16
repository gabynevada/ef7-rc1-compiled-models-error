using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ef7Rc1Error.Models;

public class User : IdentityUser<Guid>
{
    public Guid? PatientId { get; set; }

    public Patient? Patient { get; set; }
}

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        entity.HasOne(d => d.Patient)
            .WithMany(p => p.User)
            .HasForeignKey(d => d.PatientId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_UserPatient");
    }
}