//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VeterinaryClinic.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class ConsentToPerformService
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ConsentToPerformService()
        {
            this.Agreement = new HashSet<Agreement>();
        }
    
        public int ID_CTPS { get; set; }
        public string Name_CTPS { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Agreement> Agreement { get; set; }
    }
}