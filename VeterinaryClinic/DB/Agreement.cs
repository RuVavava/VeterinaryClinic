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
    
    public partial class Agreement
    {
        public int ID_Agreement { get; set; }
        public int ID_Client { get; set; }
        public int ID_Pet { get; set; }
        public int ID_Service { get; set; }
        public int Quantity { get; set; }
        public double TotalCost { get; set; }
        public int ID_CTPS { get; set; }
        public System.DateTime Date { get; set; }
        public int ID_Employee { get; set; }
    
        public virtual Clients Clients { get; set; }
        public virtual ConsentToPerformService ConsentToPerformService { get; set; }
        public virtual Employees Employees { get; set; }
        public virtual Pets Pets { get; set; }
    }
}
