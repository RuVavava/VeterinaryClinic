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
    
    public partial class FinancialTransactions
    {
        public int ID_FT { get; set; }
        public Nullable<int> ID_Service { get; set; }
        public Nullable<int> ID_Product { get; set; }
        public int Quantity { get; set; }
        public double TotalCost { get; set; }
        public System.DateTime Date { get; set; }
        public int ID_Employee { get; set; }
        public int ID_PayType { get; set; }
    
        public virtual Employees Employees { get; set; }
        public virtual PaymentType PaymentType { get; set; }
        public virtual Product Product { get; set; }
        public virtual Service Service { get; set; }
    }
}