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
    
    public partial class MedicalRecord
    {
        public int ID_MedicalRecord { get; set; }
        public System.DateTime DateMR { get; set; }
        public int ID_Pet { get; set; }
        public string Reason { get; set; }
        public string ResultMR { get; set; }
        public string Treatment { get; set; }
        public Nullable<int> ID_Employee { get; set; }
    
        public virtual Employees Employees { get; set; }
        public virtual Pets Pets { get; set; }
    }
}
