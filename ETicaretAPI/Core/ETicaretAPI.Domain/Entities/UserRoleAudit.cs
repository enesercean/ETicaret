using ETicaretAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Domain.Entities
{
    public class UserRoleAudit : BaseEntity
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }

        // RoleName özelliği, RoleId'ye bağlı kalmadan rol adını da saklar
        public string RoleName { get; set; }

        // İşlem türü (Ekleme, Silme)
        public AuditActionType ActionType { get; set; }

        // İşlemi yapan kullanıcı bilgisi (admin vb.)
        public string? ActionByUserId { get; set; }

        // Role atama/çıkarma nedeni
        public string? Reason { get; set; }

        // Süre sınırlı roller için
        public DateTime? ExpiryDate { get; set; }

        // Rol aktif/deaktif durumu
        public bool IsActive { get; set; } = true;
    }

    public enum AuditActionType
    {
        Added = 1,
        Removed = 2,
        Modified = 3
    }
}
