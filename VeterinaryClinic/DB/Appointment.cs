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
    
    public partial class Appointment
    {
        public int ID_Appointment { get; set; }
        public System.DateTime DateAppointment { get; set; }
        public int ID_Client { get; set; }
        public int ID_Pet { get; set; }
        public Nullable<int> ID_Service { get; set; }
        public string Reason { get; set; }
        public int ID_AppointmentStatus { get; set; }
    
        public virtual AppointmentStatus AppointmentStatus { get; set; }
        public virtual Clients Clients { get; set; }
        public virtual Pets Pets { get; set; }
        public virtual Service Service { get; set; }
    }
}
