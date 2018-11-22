//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RegisterActivityWebApi.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Person
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Person()
        {
            this.TimeSpents = new HashSet<TimeSpent>();
        }
    
        public int ID { get; set; }
        public string PersonName { get; set; }
        public string Email { get; set; }
        public string SecurityCode { get; set; }
        public int OrganizationID { get; set; }
    
        public virtual Organization Organization { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TimeSpent> TimeSpents { get; set; }
    }
}