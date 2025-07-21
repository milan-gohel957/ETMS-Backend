using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETMS.Domain.Entities;

public class Attachment : BaseEntity
{

    [Required]
    [MaxLength(255)]
    public string FileName { get; set; } = string.Empty;

    [Required]
    [MaxLength(500)]
    public string StoredFileName { get; set; } = string.Empty; // Unique name on disk/cloud

    [Required]
    [MaxLength(100)]
    public string ContentType { get; set; } = string.Empty; // MIME type

    public long FileSizeInBytes { get; set; }

    [MaxLength(10)]
    public string FileExtension { get; set; } = string.Empty;

    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

    // Who uploaded this file
    public int UploadedByUserId { get; set; }
    public User UploadedBy { get; set; } = null!;

    // Security
    public bool IsVirusScanCompleted { get; set; } = false;
    public bool IsVirusFree { get; set; } = false;
    public DateTime? VirusScanDate { get; set; }

    // Polymorphic relationships (similar to comments)
    public int? TaskId { get; set; }
    public ProjectTask? Task { get; set; }

    public int? ProjectId { get; set; }
    public Project? Project { get; set; }

    // Optional metadata
    [MaxLength(1000)]
    public string? Description { get; set; }

    [MaxLength(32)]
    public string? MD5Hash { get; set; } // For duplicate detection

}